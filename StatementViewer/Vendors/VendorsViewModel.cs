using CustomPresentationControls;
using CustomPresentationControls.Utilities;
using StatementViewer.Services;
using StatementViewer.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace StatementViewer.Vendors
{
    public class VendorsViewModel : ViewModel
    {
        #region Fields
        private ITransactionRepository _transactionRepository = Container.TransactionRepository;
        private IVendorRepository _vendorRepository = Container.VendorRepository;
        private ObservableCollection<Vendor> _vendors;
        #endregion
        #region Properties
        public ObservableCollection<Vendor> Vendors
        {
            get { return _vendors; }
            set { OnPropertyChanged(ref _vendors, value); }
        }
        #endregion
        #region Commands
        public RelayCommand AddVendorCommand { get; }
        public RelayCommand<Vendor> EditVendorCommand { get; }
        public RelayCommand<Vendor> RemoveVendorCommand { get; }
        public RelayCommand UpdateTransactionVendorsCommand { get; }
        #endregion
        #region Events
        public event Action AddVendor = delegate { };
        public event Action<Vendor> EditVendor = delegate { };
        public event Action<Vendor> RemoveVendor = delegate { };
        public event Action UpdateTransactionVendors = delegate { };
        #endregion
        public VendorsViewModel()
        {
            AddVendorCommand = new RelayCommand(OnAddVendor);
            EditVendorCommand = new RelayCommand<Vendor>(OnEditVendor);
            RemoveVendorCommand = new RelayCommand<Vendor>(OnRemoveVendor);
            UpdateTransactionVendorsCommand = new RelayCommand(OnUpdateTransactionVendors);
        }
        #region Command Methods
        public void SetVendors(IEnumerable<Vendor> vendors)
        {
            Vendors = new ObservableCollection<Vendor>(vendors);
        }
        private void OnAddVendor()
        {
            try
            {
                AddVendor();
            }
            catch (Exception ex)
            {
                WpfMessageBox.ShowDialog("Data Error", ex.Message, MessageBoxButton.OK, MessageIcon.Error);
            }
        }
        private void OnEditVendor(Vendor vendor)
        {
            try
            {
                EditVendor(vendor);
            }
            catch (Exception ex)
            {
                WpfMessageBox.ShowDialog("Data Error", ex.Message, MessageBoxButton.OK, MessageIcon.Error);
            }
        }
        private void OnRemoveVendor(Vendor vendor)
        {
            try
            {
            Vendors.Remove(vendor);
            RemoveVendor(vendor);
            }
            catch (Exception ex)
            {
                WpfMessageBox.ShowDialog("Data Error", ex.Message, MessageBoxButton.OK, MessageIcon.Error);
            }
        }
        private void OnUpdateTransactionVendors()
        {
            UpdateTransactionVendors();
        }
        #endregion
    }
}
