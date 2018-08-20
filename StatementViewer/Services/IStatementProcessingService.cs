using StatementViewer.Transactions;
using System.Collections.Generic;

namespace StatementViewer.Services
{
    internal interface IStatementProcessingService
    {
        IEnumerable<Transaction> ProcessStatements();
        void SetStatementPath(string path);
    }
}
