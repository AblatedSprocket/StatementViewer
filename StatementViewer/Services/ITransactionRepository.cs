using StatementViewer.Costs;
using StatementViewer.Transactions;
using StatementViewer.Vendors;
using System;
using System.Collections.Generic;

namespace StatementViewer.Services
{
    interface ITransactionRepository
    {
        int AddBulkTransactions(IEnumerable<Transaction> transactions);
        bool AddTransaction(Transaction transaction);
        bool DeleteTransaction(int transactionId);
        Transaction GetTransaction(int transactionId);
        IEnumerable<CostBreakdown> GetMonthCostBreakdowns();
        IEnumerable<CostBreakdown> GetYearCostBreakdowns();
        CostBreakdown GetLifetimeCostBreakdown();
        IEnumerable<Transaction> GetTransactions();
        IEnumerable<Transaction> GetTransactionsByMonth(DateTime month);
        IEnumerable<Transaction> GetTransactionsByYear(int year);
        bool SetDatabaseConnectionString(string path);
        void UpdateTransaction(Transaction transaction);
        void UpdateTransactionVendors(Vendor vendor);
        void UpdateTransactionVendors(IEnumerable<Vendor> vendors);
    }
}
