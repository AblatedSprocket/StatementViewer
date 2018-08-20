using System.Collections.Generic;

namespace StatementViewer.Transactions
{
    class TransactionComparer : IEqualityComparer<Transaction>
    {
        public bool Equals(Transaction x, Transaction y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }
            if (x is null || y is null)
            {
                return false;
            }
            return x.Account == y.Account && x.Amount == y.Amount && x.Description == y.Description && x.TransactionDate == y.TransactionDate;
        }

        public int GetHashCode(Transaction transaction)
        {
            if (transaction is null)
            {
                return 0;
            }
            int accountHash = transaction.Account == null ? 0 : transaction.Account.GetHashCode();
            int amountHash = transaction.Amount.GetHashCode();
            int descriptionHash = transaction.Description == null ? 0 : transaction.Description.GetHashCode();
            int transactionDateHash = transaction.TransactionDate.GetHashCode();
            return accountHash ^ amountHash ^ descriptionHash ^ transactionDateHash;
        }
    }
}
