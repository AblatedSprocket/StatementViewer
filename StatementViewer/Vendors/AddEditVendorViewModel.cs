using CustomPresentationControls;
using CustomPresentationControls.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;

namespace StatementViewer.Vendors
{
    public class AddEditVendorViewModel : ViewModel
    {
        #region Fields
        private IEnumerable<string> _categories;
        private bool _editMode;
        private EditableVendor _vendor;
        private Vendor _editingVendor = null;
        #endregion

        #region Properties
        public IEnumerable<string> Categories
        {
            get { return _categories; }
            set { OnPropertyChanged(ref _categories, value); }
        }
        public bool EditMode
        {
            get { return _editMode; }
            set { OnPropertyChanged(ref _editMode, value); }
        }
        public EditableVendor Vendor
        {
            get { return _vendor; }
            set { OnPropertyChanged(ref _vendor, value); }
        }
        public void SetVendor(Vendor vendor)
        {
            _editingVendor = vendor;
            if (Vendor != null)
            {
                Vendor.ErrorsChanged -= RaiseCanExecuteChanged;
            }
            Vendor = new EditableVendor(_editingVendor.Id);
            Vendor.ErrorsChanged += RaiseCanExecuteChanged;
            CopyVendor(vendor, Vendor);
        }
        #endregion

        #region Commands
        public RelayCommand SaveCommand { get; }
        public RelayCommand CancelCommand { get; }
        #endregion

        #region Events
        public event Action<Vendor> AddVendor = delegate { };
        public event Action<Vendor> ModifyVendor = delegate { };
        public event Action Done = delegate { };
        #endregion

        public AddEditVendorViewModel()
        {
            //Categories = Config.VendorCategories;
            SaveCommand = new RelayCommand(OnSave, CanSave);
            CancelCommand = new RelayCommand(OnCancel);
        }
        

        #region Private Methods
        private void CopyVendor(Vendor source, EditableVendor target)
        {
            target.Name = source.Name;
            target.Category = source.Category;
            target.TransactionKey = source.TransactionKey;
        }
        private void UpdateVendor(EditableVendor source, Vendor target)
        {
            target.Name = source.Name;
            target.Category = source.Category;
            target.TransactionKey = source.TransactionKey;
            target.TransactionCount = source.TransactionCount;
        }
        #endregion
        #region Command Methods
        private void OnCancel()
        {
            try
            {
                Done();
            }
            catch (Exception ex)
            {
                WpfMessageBox.ShowDialog("Data Error", ex.Message, MessageIcon.Error, MessageBoxButton.OK);
            }
        }
        private void OnSave()
        {
            try
            {
                UpdateVendor(Vendor, _editingVendor);
                if (EditMode)
                {
                    ModifyVendor(_editingVendor);
                }
                else
                {
                    AddVendor(_editingVendor);
                }
                Done();
            }
            catch (Exception ex)
            {
                WpfMessageBox.ShowDialog("Data Error", ex.Message, MessageIcon.Error, MessageBoxButton.OK);
            }
        }
        private bool CanSave()
        {
            return !Vendor.HasErrors;
        }
        private void RaiseCanExecuteChanged(object sender, EventArgs e)
        {
            SaveCommand.RaiseCanExecuteChanged();
        }
        #endregion
    }
}
