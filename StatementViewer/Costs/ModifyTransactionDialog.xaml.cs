using StatementViewer.Transactions;
using System.Windows;

namespace StatementViewer.Costs
{
    /// <summary>
    /// Interaction logic for ModifyTransactionDialog.xaml
    /// </summary>
    public partial class ModifyTransactionDialog : Window
    {
        public Transaction Transaction { get; }
        public ModifyTransactionDialog(Transaction transaction)
        {
            InitializeComponent();
            Transaction = transaction;
            DataContext = Transaction;
        }
        private void OnOkClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
