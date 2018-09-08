using CustomPresentationControls.Utilities;
using StatementViewer.Transactions;

namespace StatementViewer.Vendors
{
    public class Vendor : ObservableObject
    {
        private string _name;
        private TransactionCategory _category;
        private string _associatedText;
        private int _transactionCount;
        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set { OnPropertyChanged(ref _name, value); }
        }
        public TransactionCategory Category
        {
            get { return _category; }
            set { OnPropertyChanged(ref _category, value); }
        }
        public string TransactionKey
        {
            get { return _associatedText; }
            set { OnPropertyChanged(ref _associatedText, value); }
        }
        public int TransactionCount
        {
            get { return _transactionCount; }
            set { OnPropertyChanged(ref _transactionCount, value); }
        }
        public Vendor() { }
        public Vendor(int id)
        {
            Id = id;
        }
    }
}
