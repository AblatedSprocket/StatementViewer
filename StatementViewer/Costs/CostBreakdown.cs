using CustomPresentationControls.Utilities;
using System;
using System.Collections.ObjectModel;

namespace StatementViewer.Costs
{

    public sealed class VendorCategories : ObservableCollection<string>
    {
        public VendorCategories()
        {
            Add("Paycheck");
            Add("Mortgage");
            Add("Loans");
            Add("Payment");
            Add("Interest");
            Add("Utilities");
            Add("Grocery");
            Add("Home");
            Add("Auto");
            Add("Work");
            Add("Dining");
            Add("Luxury");
            Add("Travel");
        }
    }
    public class CostBreakdown : ObservableObject
    {
        private DateTime _timePeriod;
        private decimal _paycheck;
        private decimal _mortgage;
        private decimal _loans;
        private decimal _payments;
        private decimal _interest;
        private decimal _utilities;
        private decimal _grocery;
        private decimal _home;
        private decimal _auto;
        private decimal _work;
        private decimal _dining;
        private decimal _luxury;
        private decimal _travel;
        private decimal _misc;
        public DateTime TimePeriod
        {
            get { return _timePeriod; }
            set { OnPropertyChanged(ref _timePeriod, value); }
        }
        public decimal Paycheck
        {
            get { return _paycheck; }
            set { OnPropertyChanged(ref _paycheck, value); }
        }
        public decimal Mortgage
        {
            get { return _mortgage; }
            set { OnPropertyChanged(ref _mortgage, value); }
        }
        public decimal Loans
        {
            get { return _loans; }
            set { OnPropertyChanged(ref _loans, value); }
        }
        public decimal Payments
        {
            get { return _payments; }
            set { OnPropertyChanged(ref _payments, value); }
        }
        public decimal Interest
        {
            get { return _interest; }
            set { OnPropertyChanged(ref _interest, value); }
        }
        public decimal Utilities
        {
            get { return _utilities; }
            set { OnPropertyChanged(ref _utilities, value); }
        }
        public decimal Grocery
        {
            get { return _grocery; }
            set { OnPropertyChanged(ref _grocery, value); }
        }
        public decimal Home
        {
            get { return _home; }
            set { OnPropertyChanged(ref _home, value); }
        }
        public decimal Auto
        {
            get { return _auto; }
            set { OnPropertyChanged(ref _auto, value); }
        }
        public decimal Work
        {
            get { return _work; }
            set { OnPropertyChanged(ref _work, value); }
        }
        public decimal Dining
        {
            get { return _dining; }
            set { OnPropertyChanged(ref _dining, value); }
        }
        public decimal Luxury
        {
            get { return _luxury; }
            set { OnPropertyChanged(ref _luxury, value); }
        }
        public decimal Travel
        {
            get { return _travel; }
            set { OnPropertyChanged(ref _travel, value); }
        }
        public decimal Misc
        {
            get { return _misc; }
            set { OnPropertyChanged(ref _misc, value); }
        }
    }
}
