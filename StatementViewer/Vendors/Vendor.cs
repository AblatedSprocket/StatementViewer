using CustomPresentationControls.Utilities;

namespace StatementViewer.Vendors
{
    public class Vendor : ObservableObject
    {
        private string _name;
        private string _category;
        private string _associatedText;
        private int _transactionCount;
        public int Id { get; set; }
        public string Name
        {
            get { return _name; }
            set { OnPropertyChanged(ref _name, value); }
        }
        public string Category
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
