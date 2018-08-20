using CustomPresentationControls.Utilities;
using System;

namespace StatementViewer.Transactions
{
    public enum TransactionCategory
    {
        Misc,
        Auto,
        Dining,
        Grocery,
        Home,
        Interest,
        Loans,
        Luxury,
        Shopping,
        Mortgage,
        Paycheck,
        Payment,
        Travel,
        Utilities,
        Work
    }
    public class Transaction : ObservableObject
    {
        public int Id { get; private set; }
        private string _vendor;
        private decimal _amount;
        private string _type;
        private TransactionCategory _category;
        private DateTime _transactionDate;
        private DateTime _postDate;
        private string _description;
        private string _account;
        private string _serialNumber;
        public string Vendor
        {
            get { return _vendor; }
            set { OnPropertyChanged(ref _vendor, value); }
        }
        public decimal Amount
        {
            get { return _amount; }
            set { OnPropertyChanged(ref _amount, value); }
        }
        public string Type
        {
            get { return _type; }
            set { OnPropertyChanged(ref _type, value); }
        }
        public TransactionCategory Category
        {
            get { return _category; }
            set { OnPropertyChanged(ref _category, value); }
        }
        public DateTime TransactionDate
        {
            get { return _transactionDate; }
            set { OnPropertyChanged(ref _transactionDate, value); }
        }
        public DateTime PostDate
        {
            get { return _postDate; }
            set { OnPropertyChanged(ref _postDate, value); }
        }
        public string Description
        {
            get { return _description; }
            set { OnPropertyChanged(ref _description, value); }
        }
        public string Account
        {
            get { return _account; }
            set { OnPropertyChanged(ref _account, value); }
        }
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { OnPropertyChanged(ref _serialNumber, value); }
        }
        public Transaction() { }
        public Transaction(int id)
        {
            Id = id;
        }
        public void SetId(int id)
        {
            Id = id;
        }
    }
}
