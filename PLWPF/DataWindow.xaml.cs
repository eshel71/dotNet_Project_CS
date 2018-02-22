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
using BE;
using BL;
namespace PLWPF
{
    /// <summary>
    /// Interaction logic for DataWindow.xaml
    /// </summary>
    public partial class DataWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();
        IEnumerable<Child> childs;
        IEnumerable<Mother> mothers;
        IEnumerable<Nanny> nannies;
        IEnumerable<Contract> contracts;

        /// <summary>
        /// c-tor 
        /// </summary>
        public DataWindow()
        {
            InitializeComponent();

            // gets all the data for using this window
            childs = myBl.ChildsByPredicate();
            mothers = myBl.MothersByPredicate();
            nannies = myBl.NanniesByPredicate();
            contracts = myBl.MatchedContracts();
        }

        /// <summary>
        /// closing this window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClickExit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// show the nannies data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_nannies(object sender, RoutedEventArgs e)
        {
            title.Text = "Nannies:";
            dataTextBlock.Text = ""; // reset the window

            foreach (Nanny myNanny in nannies)
            {
                dataTextBlock.Text += myNanny.FirstName + " " + myNanny.LastName + "\n";
            }
        }

        /// <summary>
        /// show the mothers data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_mothers(object sender, RoutedEventArgs e)
        {
            title.Text = "Mothers:";
            dataTextBlock.Text = ""; // reset the window

            foreach (Mother myMother in mothers)
            {
                dataTextBlock.Text += myMother.FirstName + " " + myMother.LastName + "\n";
            }
        }

        /// <summary>
        /// show the childs data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_child(object sender, RoutedEventArgs e)
        {
            title.Text = "Childs:";
            dataTextBlock.Text = ""; // reset the window

            foreach (Child myChild in childs)
            {
                dataTextBlock.Text += myChild.FirstName + " " + myChild.LastName + "\n";
            }

        }

        /// <summary>
        /// show the contracts data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Contracts(object sender, RoutedEventArgs e)
        {
            title.Text = "Contracts:";
            dataTextBlock.Text = ""; // reset the window

            foreach (Contract myContract in contracts)
            {
                dataTextBlock.Text += myContract.TransactionNumber + "\n";
            }

        }

        /// <summary>
        /// show the details of specific type of object
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_details(object sender, RoutedEventArgs e)
        {
            if (title.Text == "whats your selection?")
            {
                MessageBox.Show("choose data type", "SELECTION ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new DetailsDataWindow(title.Text);
            nextWindow.ShowDialog();
        }

        #region Mouse Move & Leave

        private void nanniesButton_MouseLeave(object sender, MouseEventArgs e)
        {
            nanniesButton.FontSize = 20;
        }

        private void nanniesButton_MouseMove(object sender, MouseEventArgs e)
        {
            nanniesButton.FontSize = 30;
        }

        private void mothersButton_MouseMove(object sender, MouseEventArgs e)
        {
            mothersButton.FontSize = 30;
        }

        private void mothersButton_MouseLeave(object sender, MouseEventArgs e)
        {
            mothersButton.FontSize = 20;
        }

        private void childsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            childsButton.FontSize = 20;
        }

        private void childsButton_MouseMove(object sender, MouseEventArgs e)
        {
            childsButton.FontSize = 30;
        }

        private void contractsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            contractsButton.FontSize = 20;
        }

        private void contractsButton_MouseMove(object sender, MouseEventArgs e)
        {
            contractsButton.FontSize = 30;
        }

        private void detail_MouseLeave(object sender, MouseEventArgs e)
        {
            DetailButton.FontSize = 20;
        }

        private void detail_MouseMove(object sender, MouseEventArgs e)
        {
            DetailButton.FontSize = 25;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            ExitButton.FontSize = 20;
        }

        private void exit_MouseMove(object sender, MouseEventArgs e)
        {
            ExitButton.FontSize = 25;
        }

        #endregion

    }
}
