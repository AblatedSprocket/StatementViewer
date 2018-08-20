using FinanceManagement.StatementProcessing;
using StatementViewer.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatementViewer.Services
{
    public class StatementProcessingService : IStatementProcessingService
    {
        private StatementProcessor _statementProcessor;
        public StatementProcessingService(string path)
        {
            _statementProcessor = new StatementProcessor(path);
        }
        public IEnumerable<Transaction> ProcessStatements()
        {
            return _statementProcessor.ProcessStatements().Select(t => ConvertDataToModel(t));
        }
        public void SetStatementPath(string path)
        {
            _statementProcessor = new StatementProcessor(path);
        }
        private Transaction ConvertDataToModel(FinanceManagement.Transaction transaction)
        {
            return new Transaction
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
        private FinanceManagement.Transaction ConvertModelToData(Transaction transaction)
        {
            return new FinanceManagement.Transaction
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
    }
}
