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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for DeleteDataWindow.xaml
    /// </summary>
    public partial class DeleteDataWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();

        /// <summary>
        /// c-tor
        /// </summary>
        public DeleteDataWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// opening the next window ( delete nanny window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_deleteNanny(object sender, RoutedEventArgs e)
        {
            IEnumerable<Nanny> nannies = myBl.NanniesByPredicate();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (nannies.Count() == 0)
            {
                MessageBox.Show("There is no any nanny!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new deletePopupWindow("Nanny");
            this.Close();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( delete mother window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_deleteMother(object sender, RoutedEventArgs e)
        {
            IEnumerable<Mother> mothers = myBl.MothersByPredicate();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (mothers.Count() == 0)
            {
                MessageBox.Show("There is no any mother!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new deletePopupWindow("Mother");
            this.Close();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( delete child window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_deleteChild(object sender, RoutedEventArgs e)
        {
            IEnumerable<Child> untreatedChilds = myBl.UntreatedChilds();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (untreatedChilds.Count() == 0)
            {
                MessageBox.Show("There is no any child to delete!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new deletePopupWindow("Child");
            this.Close();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( delete contract window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_deleteContract(object sender, RoutedEventArgs e)
        {
            IEnumerable<Contract> contracts = myBl.MatchedContracts();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (contracts.Count() == 0)
            {
                MessageBox.Show("There is no any contract!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new deletePopupWindow("Contract");
            this.Close();
            nextWindow.ShowDialog();
        }

    }
}
