using CustomPresentationControls.Utilities;
using StatementViewer.Transactions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StatementViewer.Costs
{
    public class CostsViewModel : ViewModel
    {
        #region Fields
        private bool _monthlyFlag = false;
        private bool _yearlyFlag = false;
        private bool _allFlag = false;
        private DateTime _startDate;
        private Dictionary<string, decimal> _costBreakdown;
        private ObservableCollection<Transaction> _transactions;
        #endregion
        #region Properties
        public bool MonthlyFlag
        {
            get { return _monthlyFlag; }
            set { OnPropertyChanged(ref _monthlyFlag, value); }
        }
        public bool YearlyFlag
        {
            get { return _yearlyFlag; }
            set { OnPropertyChanged(ref _yearlyFlag, value); }
        }
        public bool AllFlag
        {
            get { return _allFlag; }
            set { OnPropertyChanged(ref _allFlag, value); }
        }
        public DateTime StartDate
        {
            get { return _startDate; }
            set { OnPropertyChanged(ref _startDate, value); }
        }
        public Dictionary<string, decimal> CostBreakdown
        {
            get { return _costBreakdown; }
            set { OnPropertyChanged(ref _costBreakdown, value); }
        }
        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { OnPropertyChanged(ref _transactions, value); }
        }
        #endregion
        #region Commands
        public RelayCommand AddVendorCommand { get; }
        #endregion
        #region Events
        public event Action AddVendor = delegate { };
        #endregion
        public CostsViewModel(string timeSpan)
        {
            switch (timeSpan)
            {
                case "Monthly":
                    MonthlyFlag = true;
                    break;
                case "Yearly":
                    YearlyFlag = true;
                    break;
                default:
                    AllFlag = true;
                    break;

            }
            AddVendorCommand = new RelayCommand(OnAddVendor);
        }
        #region Public Methods
        public void SetTransactions(IEnumerable<Transaction> transactions)
        {
            Transactions = new ObservableCollection<Transaction>(transactions);
            Transaction transaction = Transactions.FirstOrDefault();
            if (transaction != null)
            {
                StartDate = Transactions.First().PostDate;
            }
            else
            {
                StartDate = DateTime.Today;
            }
            BuildCostBreakdown();
        }
        #endregion
        #region Command Methods
        private void OnAddVendor()
        {
            AddVendor();
        }
        #endregion
        #region Private Methods
        private void BuildCostBreakdown()
        {
            Dictionary<string, decimal> costs = new Dictionary<string, decimal>();
            foreach (Transaction transaction in Transactions)
            {
                string category = transaction.Category.ToString();
                if (costs.TryGetValue(category, out decimal val))
                {
                    costs[category] += transaction.Amount;
                }
                else
                {
                    costs.Add(category, transaction.Amount);
                }
            }
            CostBreakdown = costs;
        }
        #endregion
    }
}
