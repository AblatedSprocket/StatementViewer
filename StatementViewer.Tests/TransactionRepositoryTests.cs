using Microsoft.VisualStudio.TestTools.UnitTesting;
using StatementViewer.Services;
using StatementViewer.Transactions;
using StatementViewer.Utilities;
using System;
using System.IO;
using System.Linq;

namespace StatementViewer.Tests
{
    [TestClass]
    public class TransactionRepositoryTests
    {
        TransactionRepository repo = new TransactionRepository(@"D:\Dev\Database\home.db");
        [TestMethod]
        public void AddTransaction_Errors()
        {
            try
            {
                Transaction transaction = repo.GetTransactions().FirstOrDefault();
                repo.AddTransaction(transaction);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                Assert.IsTrue(ex.Message == "Catch me!");
            }
        }
        [TestMethod]
        public void AddBulkTransactions_WhenTransactionExists_DoesNotAddTransaction()
        {
            Transaction transaction = repo.GetTransaction(50);
            int count = repo.AddBulkTransactions(new Transaction[] { transaction });
            Assert.IsTrue(count == 0);
        }
        [TestMethod]
        public void AddBulkTransactions_WhenTransactionNotExists_AddsTransaction()
        {
            Transaction transaction = repo.GetTransactions().FirstOrDefault();
            Assert.IsNotNull(transaction);
            try
            {
                bool check = repo.DeleteTransaction(transaction.Id);
            }
            catch
            {
                Assert.Fail();
            }
            int count = repo.AddBulkTransactions(new Transaction[] { transaction });
            Assert.AreEqual(1, count);
        }
        [TestMethod]
        public void AddTransaction_WhenTransactionExists_DoesNotAddTransaction()
        {
            Transaction transaction = repo.GetTransactions().FirstOrDefault();
            Assert.IsNotNull(transaction);
            bool inserted = repo.AddTransaction(transaction);
        }
        [TestMethod]
        public void GetTransaction_WhenTransactionNotExistsButWithinIdCount_ReturnsNull()
        {
            Transaction transaction = repo.GetTransaction(1);
            Assert.IsNull(transaction);
        }
        [TestMethod]
        public void TransactionExists_WhenTransactionNotExistsButWithinIdCount_ReturnsFalse()
        {
            bool check = repo.TransactionExists(1);
            Assert.IsFalse(check);
        }
    }
}
