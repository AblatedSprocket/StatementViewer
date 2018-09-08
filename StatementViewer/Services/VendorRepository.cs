using StatementViewer.Transactions;
using StatementViewer.Utilities;
using StatementViewer.Vendors;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace StatementViewer.Services
{
    public class VendorRepository : IVendorRepository
    {
        private string _databaseConn;
        public VendorRepository() : this(Config.DatabasePath) { }
        public VendorRepository(string path)
        {
            SetDatabaseConnectionString(path);
        }
        public bool AddVendor(Vendor vendor)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        INSERT INTO Vendors 
                        (Name, Category, TransactionKey, TransactionCount)
                        VALUES (@NAME, @CATEGORY, @TRANSACTIONKEY, @TRANSACTIONCOUNT)";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@NAME", DbType.String, 30).Value = vendor.Name;
                cmd.Parameters.Add("@CATEGORY", DbType.String, 30).Value = vendor.Category;
                cmd.Parameters.Add("@TRANSACTIONKEY", DbType.String, 50).Value = vendor.TransactionKey;
                cmd.Parameters.Add("@TRANSACTIONCOUNT", DbType.Int32).Value = vendor.TransactionCount;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteVendor(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        DELETE FROM Vendors 
                        WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@ID", DbType.Int32).Value = id;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public Vendor GetVendor(int id)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT * FROM Vendors 
                        WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@ID", DbType.Int32).Value = id;
                SQLiteDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    return new Vendor(Convert.ToInt32(reader["Id"].ToString()))
                    {
                        Name = reader["Name"].ToString(),
                        Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                        TransactionKey = reader["TransactionKey"].ToString(),
                        TransactionCount = Convert.ToInt32(reader["TransactionCount"])
                    };
                }
            }
            return null;
        }

        public IEnumerable<Vendor> GetVendors()
        {
            List<Vendor> vendors = new List<Vendor>();
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        SELECT * FROM Vendors";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        vendors.Add(new Vendor(Convert.ToInt32(reader["Id"].ToString()))
                        {
                            Name = reader["Name"].ToString(),
                            Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), reader["Category"].ToString()),
                            TransactionKey = reader["TransactionKey"].ToString(),
                            TransactionCount = Convert.ToInt32(reader["TransactionCount"])
                        });
                    }
                }
            }
            return vendors;
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

        public void UpdateVendor(Vendor vendor)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                        UPDATE Vendors
                        SET
                            Name = @NAME,
                            Category = @CATEGORY,
                            TransactionKey = @TRANSACTIONKEY,
                            TransactionCount = @TRANSACTIONCOUNT
                        WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@NAME", DbType.String, 30).Value = vendor.Name;
                cmd.Parameters.Add("@CATEGORY", DbType.String, 30).Value = vendor.Category;
                cmd.Parameters.Add("@TRANSACTIONKEY", DbType.String, 50).Value = vendor.TransactionKey;
                cmd.Parameters.Add("@TRANSACTIONCOUNT", DbType.String).Value = vendor.TransactionCount;
                cmd.Parameters.Add("@ID", DbType.Int32).Value = vendor.Id;
                cmd.ExecuteNonQuery();
            }
        }
        private FinanceManagement.Vendor ConvertModelToData(Vendor vendor)
        {
            return new FinanceManagement.Vendor
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Category = vendor.Category.ToString(),
                TransactionKey = vendor.TransactionKey,
                TransactionCount = vendor.TransactionCount
            };
        }
        private Vendor ConvertDataToModel(FinanceManagement.Vendor vendor)
        {
            return new Vendor
            {
                Id = vendor.Id,
                Name = vendor.Name,
                Category = (TransactionCategory)Enum.Parse(typeof(TransactionCategory), vendor.Category),
                TransactionKey = vendor.TransactionKey,
                TransactionCount = vendor.TransactionCount
            };
        }
        public bool VendorExists(int vendorId)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                    SELECT COUNT(*)
                    FROM Vendors
                    WHERE Id = @ID";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@ID", DbType.Int32).Value = vendorId;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count != 0;
            }
        }
        public bool VendorExists(Vendor vendor)
        {
            using (SQLiteConnection conn = new SQLiteConnection(_databaseConn))
            {
                conn.Open();
                string sql = @"
                    SELECT COUNT(*)
                    FROM Vendors
                    WHERE Name = @NAME";
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.Add("@NAME", DbType.String).Value = vendor.Name;
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count != 0;
            }
        }
    }
}
