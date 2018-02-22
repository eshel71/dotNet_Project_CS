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
    /// Interaction logic for AddDataWindow.xaml
    /// </summary>
    public partial class AddDataWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();
        public AddDataWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// open the next window (adding nanny window)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_AddNanny(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new AddNannyWindow();
            this.Close();
            nextWindow.ShowDialog();

        }

        /// <summary>
        /// open the next window (adding child window)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addChild(object sender, RoutedEventArgs e)
        {
            IEnumerable<Mother> mothers = myBl.MothersByPredicate();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (mothers.Count() == 0)
            {
                MessageBox.Show("There is no any mother!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new AddChildWindow();
            this.Close();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// open the next window (adding mother window)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addMother(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new AddMotherWindow();
            this.Close();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// open the next window (adding contract window)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addContract(object sender, RoutedEventArgs e)
        {

            IEnumerable<Child> untreatedChilds = myBl.UntreatedChilds();  // checks is there is untreated child. Because if there is no untreated child there is no point in opening this window
            if (untreatedChilds.Count() == 0)
            {
                MessageBox.Show("There is no untreated child!", "Error 404", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Window nextWindow = new AddContractWindow(untreatedChilds);
            this.Close();
            nextWindow.ShowDialog();
        }

    }
}
