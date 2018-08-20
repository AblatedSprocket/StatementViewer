using StatementViewer.Services;

namespace StatementViewer.Utilities
{
    static class Container
    {
        public static ITransactionRepository TransactionRepository { get; } = new TransactionRepository();
        public static IVendorRepository VendorRepository { get; } = new VendorRepository();
    }
}
