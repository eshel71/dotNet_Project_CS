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
    /// Interaction logic for DetailsDataWindow.xaml
    /// </summary>
    public partial class DetailsDataWindow : Window
    {

        /// <summary>
        /// c-tor that initiating the title by the type
        /// </summary>
        /// <param name="titleSent"></param>
        public DetailsDataWindow(string titleSent)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            title.Text = titleSent;

            switch (titleSent) // choosing the data for the dataGrid by the title
            {
                case "Nannies:":
                    myDataGrid.ItemsSource = BL.FactoryBL.GetBL().NanniesByPredicate();
                    break;

                case "Mothers:":
                    myDataGrid.ItemsSource = BL.FactoryBL.GetBL().MothersByPredicate();
                    break;

                case "Childs:":
                    myDataGrid.ItemsSource = BL.FactoryBL.GetBL().ChildsByPredicate();
                    break;

                case "Contracts:":
                    myDataGrid.ItemsSource = BL.FactoryBL.GetBL().MatchedContracts();
                    break;

            }

        }

        /// <summary>
        /// closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
