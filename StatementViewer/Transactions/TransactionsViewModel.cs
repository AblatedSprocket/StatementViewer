using CustomPresentationControls.Utilities;
using StatementViewer.Services;
using StatementViewer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StatementViewer.Transactions
{
    public class TransactionsViewModel : ViewModel
    {
        #region Fields
        private ITransactionRepository _transactionRepository = Container.TransactionRepository;
        private IVendorRepository _vendorRepository = Container.VendorRepository;
        private bool _busyFlag;
        private ObservableCollection<Transaction> _transactions;
        #endregion
        #region Properties
        public bool BusyFlag
        {
            get { return _busyFlag; }
            set { OnPropertyChanged(ref _busyFlag, value); }
        }
        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { OnPropertyChanged(ref _transactions, value); }
        }
        #endregion
        #region Commands
        public RelayCommand LoadTransactionsCommand { get; }
        public RelayCommand AddVendorCommand { get; }
        public RelayCommand<Transaction> RemoveTransactionCommand { get; }
        #endregion
        #region Events
        public event Action AddVendor = delegate { };
        public event Action ProcessTransactions = delegate { };
        public event Action<Transaction> RemoveTransaction = delegate { };
        #endregion
        public TransactionsViewModel()
        {
            LoadTransactionsCommand = new RelayCommand(OnLoadTransactionsAsync);
            AddVendorCommand = new RelayCommand(OnAddVendor);
            RemoveTransactionCommand = new RelayCommand<Transaction>(OnRemoveTransaction);
        }
        #region Public Methods
        public void SetTransactions(IEnumerable<Transaction> transactions)
        {
            Transactions = new ObservableCollection<Transaction>(transactions);
        }
        #endregion
        #region Command Methods
        private async void OnLoadTransactionsAsync()
        {
            BusyFlag = true;
            await Task.Run(() =>
            {
                ProcessTransactions();
            });
            BusyFlag = false;
        }
        private void OnAddVendor()
        {
            AddVendor();
        }
        private void OnRemoveTransaction(Transaction transaction)
        {
            RemoveTransaction(transaction);
        }
        #endregion
    }
}
