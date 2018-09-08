using StatementViewer.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StatementViewer.Costs
{
    public class ModifyTransactionEventArgs
    {
        public Transaction Transaction { get; set; }
        public ModifyTransactionEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }
}
