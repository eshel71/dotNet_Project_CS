using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for deletePopupWindow.xaml
    /// </summary>
    public partial class deletePopupWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();
        string myType;

        /// <summary>
        /// c-tor that initiating the data by the element type 
        /// </summary>
        /// <param name="dataType"></param>
        public deletePopupWindow(string dataType)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            myType = dataType; // data for the next event (click ok)

            titleTB.Text = "Choose " + dataType + " to delete:"; // set the instructions
            title.Text += " " + dataType;

            #region comboBox defination

            if (dataType == "Nanny")
            {
                deleteCB.ItemsSource = myBl.NanniesByPredicate();
                deleteCB.DisplayMemberPath = "FirstName";
                deleteCB.SelectedValuePath = "Id";
                return;
            }

            if (dataType == "Contract")
            {
                deleteCB.ItemsSource = myBl.MatchedContracts();
                deleteCB.DisplayMemberPath = "TransactionNumber";
                deleteCB.SelectedValuePath = "TransactionNumber";
                return;
            }

            if (dataType == "Child")
            {
                deleteCB.ItemsSource = myBl.ChildsByPredicate();
                deleteCB.DisplayMemberPath = "FirstName";
                deleteCB.SelectedValuePath = "Id";
                return;
            }

            if (dataType == "Mother")
            {
                deleteCB.ItemsSource = myBl.MothersByPredicate();
                deleteCB.DisplayMemberPath = "FirstName";
                deleteCB.SelectedValuePath = "Id";
                MessageBox.Show("Deleting the mother causes her children to be deleted", "WARNING", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            #endregion

        }

        /// <summary>
        /// deleting the specific object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ok(object sender, RoutedEventArgs e)
        {
            try
            {
                if (deleteCB.SelectedItem == null)
                    throw new Exception("no " + myType.ToLower() + " selected!");

                #region delete the specific type of object

                if (myType == "Nanny")
                    myBl.DeleteNanny((int)deleteCB.SelectedValue);

                else if (myType == "Mother")
                {
                    myBl.DeleteMother((int)deleteCB.SelectedValue);
                }
                else if (myType == "Child")
                    myBl.DeleteChild((int)deleteCB.SelectedValue);

                else if (myType == "Contract")
                    myBl.DeleteContract((int)deleteCB.SelectedValue);

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DELETE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // the delete can't be done
            }

            MessageBox.Show("the " + myType.ToLower() + " deleted successfully!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        /// <summary>
        /// closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
