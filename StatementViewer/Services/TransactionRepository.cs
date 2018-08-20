using StatementViewer.Costs;
using StatementViewer.Transactions;
using StatementViewer.Utilities;
using StatementViewer.Vendors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace StatementViewer.Services
{
    public class TransactionRepository : ITransactionRepository
    {
        private string _databaseConn;
        public TransactionRepository() : this(Config.DatabasePath) { }
        public TransactionRepository(string path)
        {
            SetDatabaseConnectionString(path);
        }
        public int AddBulkTransactions(IEnumerable<Transaction> transactions)
        {
            int transCount = 0;
            using (SQLiteConnection readConn = new SQLiteConnection(_databaseConn))
            using (SQLiteConnection writeConn = new SQLiteConnection(_databaseConn))
            {
                foreach (Transaction transaction in transactions)
                {
                    if (AddTransaction(transaction))
                    {
                        transCount++;
                    }
                }
            }
            return transCount;
        }
        public bool AddTransaction(Transaction transaction)
        {
            if (!TransactionExists(transaction))
            {
                Logger.Log("Adding transaction");
                using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
                {
                    conn.Open();
                    string sql = @"
                        INSERT INTO Transactions
                        (Vendor, Amount, Type, Category, TransactionDate, PostDate, Description, Account, SerialNumber)
                        VALUES (@VENDOR, @AMOUNT, @TYPE, @CATEGORY, @TRANSACTIONDATE, @POSTDATE, @DESCRIPTION, @ACCOUNT, @SERIALNUMBER)";
                    SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                    cmd.Parameters.Add("@VENDOR", DbType.String).Value = transaction.Vendor;
                    cmd.Parameters.Add("@AMOUNT", DbType.String).Value = transaction.Amount;
                    cmd.Parameters.Add("@TYPE", DbType.String).Value = transaction.Type;
                    cmd.Parameters.Add("@CATEGORY", DbType.String).Value = transaction.Category;
                    cmd.Parameters.Add("@TRANSACTIONDATE", DbType.String).Value = transaction.TransactionDate.ToSQLiteDateString();
                    cmd.Parameters.Add("@POSTDATE", DbType.String).Value = transaction.PostDate.ToSQLiteDateString();
                    cmd.Parameters.Add("@DESCRIPTION", DbType.String).Value = transaction.Description;
                    cmd.Parameters.Add("@ACCOUNT", DbType.String).Value = transaction.Account;
                    cmd.Parameters.Add("@SERIALNUMBER", DbType.String).Value = transaction.SerialNumber;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteTransaction(int transactionId)
        {
            Logger.Log($"Deleting transaction ({transactionId})");
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        DELETE FROM Transactions 
                        WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@ID", DbType.Int32).Value = transactionId;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public IEnumerable<CostBreakdown> GetCostBreakdowns()
        {
            Logger.Log("Retrieving breakdowns of costs by month");
            List<CostBreakdown> costBreakdowns = new List<CostBreakdown>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn);
                StringBuilder sql = new StringBuilder("SELECT ");
                int index = 0;
                foreach (TransactionCategory category in Enum.GetValues(typeof(TransactionCategory)))
                {
                    sql.Append($"SUM(CASE WHEN Category = @category{index} AND Type = 'Credit' THEN -Amount WHEN Category = @category{index} AND Type = 'Debit' THEN Amount ELSE 0 END) AS {category.ToString()}, ");
                    cmd.Parameters.Add($"category{index}", DbType.String).Value = category.ToString();
                    index++;
                }
                sql.Append("strftime('%m-%Y', PostDate) as monthyear FROM Transactions GROUP BY monthyear");
                cmd.CommandText = sql.ToString();
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CostBreakdown costBreakdown = new CostBreakdown();
                    costBreakdown.Auto = Convert.ToDecimal(reader["Auto"]);
                    costBreakdown.Dining = Convert.ToDecimal(reader["Dining"]);
                    costBreakdown.Grocery = Convert.ToDecimal(reader["Grocery"]);
                    costBreakdown.Home = Convert.ToDecimal(reader["Home"]);
                    costBreakdown.Interest = Convert.ToDecimal(reader["Interest"]);
                    costBreakdown.Loans = Convert.ToDecimal(reader["Loans"]);
                    costBreakdown.Luxury = Convert.ToDecimal(reader["Luxury"]);
                    costBreakdown.Misc = Convert.ToDecimal(reader["Misc"]);
                    costBreakdown.Mortgage = Convert.ToDecimal(reader["Mortgage"]);
                    costBreakdown.Paycheck = Convert.ToDecimal(reader["Paycheck"]);
                    costBreakdown.Payments = Convert.ToDecimal(reader["Payment"]);
                    var value = reader["monthyear"].ToString();
                    costBreakdown.TimePeriod = Convert.ToDateTime(reader["monthyear"]);
                    costBreakdown.Travel = Convert.ToDecimal(reader["Travel"]);
                    costBreakdown.Utilities = Convert.ToDecimal(reader["Utilities"]);
                    costBreakdown.Work = Convert.ToDecimal(reader["Work"]);
                    costBreakdowns.Add(costBreakdown);
                }
                return costBreakdowns;
            }
        }

        public Transaction GetTransaction(int transactionId)
        {
            if (TransactionExists(transactionId))
            {
                Transaction transaction = new Transaction();
                using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
                {
                    conn.Open();
                    string sql = @"
                        SELECT * FROM Transactions 
                        WHERE Id = @ID";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@ID", DbType.Int32).Value = transactionId;
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                transaction = new Transaction(Convert.ToInt32(reader["Id"].ToString()))
                                {
                                    Account = reader["Account"].ToString(),
                                    Amount = Convert.ToDecimal(reader["Amount"]),
                                    Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                                    Vendor = reader["Vendor"].ToString(),
                                    Description = reader["Description"].ToString(),
                                    PostDate = Convert.ToDateTime(reader["PostDate"]),
                                    TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                                    Type = reader["Type"].ToString(),
                                    SerialNumber = reader["SerialNumber"].ToString()
                                };
                            }
                        }
                    }
                }
                return transaction;
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<Transaction> GetTransactions()
        {
            List<Transaction> transactions = new List<Transaction>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT * FROM Transactions";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction(Convert.ToInt32(reader["Id"]))
                        {
                            Account = reader["Account"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                            Vendor = reader["Vendor"].ToString(),
                            Description = reader["Description"].ToString(),
                            PostDate = Convert.ToDateTime(reader["PostDate"]),
                            TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                            Type = reader["Type"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString()
                        });
                    }
                }
            }
            return transactions;
        }

        public IEnumerable<Transaction> GetTransactionsByMonth(DateTime month)
        {
            List<Transaction> transactions = new List<Transaction>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT * FROM Transactions
                        WHERE PostDate > @STARTDATE
                        AND PostDate < @ENDDATE";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@STARTDATE", DbType.String).Value = month.ToSQLiteDateString();
                cmd.Parameters.Add("@ENDDATE", DbType.String).Value = GetEndDate(month).ToSQLiteDateString();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction(Convert.ToInt32(reader["Id"].ToString()))
                        {
                            Account = reader["Account"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                            Vendor = reader["Vendor"].ToString(),
                            Description = reader["Description"].ToString(),
                            PostDate = Convert.ToDateTime(reader["PostDate"]),
                            TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                            Type = reader["Type"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString()
                        });
                    }
                }
            }
            return transactions;
        }
        public IEnumerable<Transaction> GetTransactionsByYear(int year)
        {
            List<Transaction> transactions = new List<Transaction>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT * FROM Transactions
                        WHERE PostDate > @STARTDATE
                        AND PostDate < @ENDDATE";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@STARTDATE", DbType.DateTime).Value = new DateTime(year, 1, 1);
                cmd.Parameters.Add("@ENDDATE", DbType.DateTime).Value = new DateTime(year + 1, 1, 1);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        transactions.Add(new Transaction(Convert.ToInt32(reader["Id"].ToString()))
                        {
                            Account = reader["Account"].ToString(),
                            Amount = Convert.ToDecimal(reader["Amount"]),
                            Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                            Vendor = reader["Vendor"].ToString(),
                            Description = reader["Description"].ToString(),
                            PostDate = Convert.ToDateTime(reader["PostDate"]),
                            TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
                            Type = reader["Type"].ToString(),
                            SerialNumber = reader["SerialNumber"].ToString()
                        });
                    }
                }
            }
            return transactions;
        }

        public bool SetDatabaseConnectionString(string path)
        {
            if (!string.IsNullOrEmpty(path) && File.Exists(path) && Path.GetExtension(path) == ".db")
            {
                SQLiteConnectionStringBuilder builder = new SQLiteConnectionStringBuilder
                {
                    DataSource = path,
                    DefaultTimeout = 5000,
                    SyncMode = SynchronizationModes.Off,
                    JournalMode = SQLiteJournalModeEnum.Memory,
                    PageSize = 65536,
                    CacheSize = 16777216,
                    FailIfMissing = false,
                    ReadOnly = false
                };
                _databaseConn = builder.ConnectionString;
                return true;
            }
            return false;
        }

        public bool TransactionExists(int transactionId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                    SELECT COUNT(*)
                    FROM Transactions
                    WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@ID", DbType.Int32).Value = transactionId;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count != 0;
            }
        }

        public bool TransactionExists(Transaction transaction)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT Id
                        FROM Transactions
                        WHERE Amount = @AMOUNT
                        AND Type = @TYPE
                        AND PostDate = @POSTDATE
                        AND Description = @DESCRIPTION";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@AMOUNT", DbType.String).Value = transaction.Amount;
                cmd.Parameters.Add("@TYPE", DbType.String).Value = transaction.Type;
                cmd.Parameters.Add("@POSTDATE", DbType.String).Value = transaction.PostDate.ToSQLiteDateString();
                cmd.Parameters.Add("@DESCRIPTION", DbType.String).Value = transaction.Description;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count != 0;
            }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        UPDATE Transactions
                        SET
                            Vendor = @VENDOR,
                            Amount = @AMOUNT,
                            Type = @TYPE,
                            Category = @CATEGORY,
                            TransactionDate = @TRANSACTIONDATE,
                            PostDate = @POSTDATE,
                            Description = @DESCRIPTION,
                            Account = @ACCOUNT,
                            SerialNumber = @SERIALNUMBER
                        WHERE Id = @ID";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@VENDOR", DbType.String).Value = transaction.Vendor;
                    cmd.Parameters.Add("@AMOUNT", DbType.String).Value = transaction.Amount;
                    cmd.Parameters.Add("@TYPE", DbType.String).Value = transaction.Type;
                    cmd.Parameters.Add("@CATEGORY", DbType.String).Value = transaction.Category;
                    cmd.Parameters.Add("@TRANSACTIONDATE", DbType.String).Value = transaction.TransactionDate.ToSQLiteDateString();
                    cmd.Parameters.Add("@POSTDATE", DbType.String).Value = transaction.PostDate.ToSQLiteDateString();
                    cmd.Parameters.Add("@DESCRIPTION", DbType.String).Value = transaction.Description;
                    cmd.Parameters.Add("@ACCOUNT", DbType.String).Value = transaction.Account;
                    cmd.Parameters.Add("@SERIALNUMBER", DbType.String).Value = transaction.SerialNumber;
                    cmd.Parameters.Add("@ID", DbType.Int32).Value = transaction.Id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateTransactionVendors(Vendor vendor)
        {
            List<int> transactionIds = new List<int>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT Id FROM Transactions
                        WHERE Description LIKE @DESCRIPTION";
                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    cmd.Parameters.Add("@DESCRIPTION", DbType.String).Value = $"%{vendor.TransactionKey}%";
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transactionIds.Add(int.Parse(reader["Id"].ToString()));
                        }
                    }
                }
                foreach (int id in transactionIds)
                {
                    sql = @"
                        UPDATE Transactions
                        SET Vendor = @VENDOR,
                            Category = @CATEGORY
                        WHERE Id = @ID";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.Parameters.Add("@VENDOR", DbType.String).Value = vendor.Name;
                        cmd.Parameters.Add("@CATEGORY", DbType.String).Value = vendor.Category;
                        cmd.Parameters.Add("@ID", DbType.String).Value = id;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        private FinanceManagement.Transaction ConvertModelToData(Transaction transaction)
        {
            return new FinanceManagement.Transaction(transaction.Id)
            {
                Account = transaction.Account,
                Amount = transaction.Amount,
                Category = transaction.Category.ToString(),
                Description = transaction.Description,
                PostDate = transaction.PostDate,
                SerialNumber = transaction.SerialNumber,
                TransactionDate = transaction.TransactionDate,
                Type = transaction.Type,
                Vendor = transaction.Vendor
            };
        }
        private Transaction ConvertDataToModel(FinanceManagement.Transaction transaction)
        {
            return new Transaction(transaction.Id)
            {
                Account = transaction.Account,
                Amount = transaction.Amount,
                Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), transaction.Category),
                Description = transaction.Description,
                PostDate = transaction.PostDate,
                SerialNumber = transaction.SerialNumber,
                TransactionDate = transaction.TransactionDate,
                Type = transaction.Type,
                Vendor = transaction.Vendor
            };
        }
        private DateTime GetEndDate(DateTime date)
        {
            if (date.Month == 12)
            {
                return new DateTime(date.Year + 1, 1, 1);
            }
            else
            {
                return new DateTime(date.Year, date.Month + 1, 1);
            }
        }
    }
}
