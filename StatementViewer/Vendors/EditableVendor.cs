using CustomPresentationControls.Utilities;
using System;
using System.ComponentModel.DataAnnotations;

namespace StatementViewer.Vendors
{
    public class EditableVendor : ValidatableObservableObject
    {
        public int Id { get; }
        private string _name;
        [Required]
        public string Name
        {
            get { return _name; }
            set { OnPropertyChanged(ref _name, value); }
        }
        private string _category;
        [Required]
        public string Category
        {
            get { return _category; }
            set { OnPropertyChanged(ref _category, value); }
        }
        private string _transactionKey;
        public string TransactionKey
        {
            get { return _transactionKey; }
            set { OnPropertyChanged(ref _transactionKey, value); }
        }
        private int _transactionCount;
        public int TransactionCount
        {
            get { return _transactionCount; }
            set { OnPropertyChanged(ref _transactionCount, value); }
        }
        public EditableVendor(int id)
        {
            Id = id;
        }
    }
}
