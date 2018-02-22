using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
    /// Interaction logic for QueriesWindow.xaml
    /// </summary>
    public partial class QueriesWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();

        /// <summary>
        /// c-tor that position the window to the center and define the fields of combo box.
        /// </summary>
        public QueriesWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            MotherCB.ItemsSource = myBl.MothersByPredicate();
            MotherCB.DisplayMemberPath = "FirstName";
            MotherCB.SelectedValuePath = "Address";

            NannyCB.ItemsSource = myBl.NanniesByPredicate();
            NannyCB.DisplayMemberPath = "FirstName";
            NannyCB.SelectedValuePath = "Address";

        }
        /// <summary>
        /// event to closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// present on the Data Grid all nannies that work by TMT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NanniesByTMT(object sender, RoutedEventArgs e)
        {
            this.DataGridQueries.ItemsSource = myBl.NanniesByTMT();
            this.DataGridQueries.IsReadOnly = true;
        }
        /// <summary>
        ///  present on the Data Grid all nannies that work per hour
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NanniesWithHourlySalary(object sender, RoutedEventArgs e)
        {
            this.DataGridQueries.ItemsSource = myBl.NanniesByPredicate(x => x.PerHourSalary == true);
            this.DataGridQueries.IsReadOnly = true;
        }
        /// <summary>
        ///  present on the Data Grid all nannies that have elevator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_NanniesWithElevator(object sender, RoutedEventArgs e)
        {
            this.DataGridQueries.ItemsSource = myBl.NanniesByPredicate(x => x.ElevatorExist == true);
            this.DataGridQueries.IsReadOnly = true;
        }
        /// <summary>
        ///  present on the Data Grid all childs that untreated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_UntreatedChilds(object sender, RoutedEventArgs e)
        {
            this.DataGridQueries.ItemsSource = myBl.UntreatedChilds();
            this.DataGridQueries.IsReadOnly = true;
        }
        /// <summary>
        /// present on the Data Grid all childs with special needs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ChildsWithSpecialNeeds(object sender, RoutedEventArgs e)
        {
            this.DataGridQueries.ItemsSource = myBl.ChildsByPredicate(x => x.IsSpecialNeeds = true);
            this.DataGridQueries.IsReadOnly = true;
        }
        /// <summary>
        /// calculate the distance between some mother to nanny
        /// we're use with thread so we're send the details to Distance_Thread to do the calculate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Calculate(object sender, RoutedEventArgs e)
        {
            if (MotherCB.SelectedValue == null || NannyCB.SelectedValue == null)
            {
                MessageBox.Show("choose Mother/Nanny", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            TheAnswer.Text = "";
            Calculate.IsEnabled = false;
            string source = MotherCB.SelectedValue.ToString();
            string dest = NannyCB.SelectedValue.ToString();
            Thread myThread = new Thread(() => Distance_Thread(source, dest));
            myThread.Start();

        }

        /// <summary>
        /// Thread to calculating distance between mother and nanny
        /// </summary>
        /// <param name="sour"></param>
        /// <param name="dest"></param>
        public void Distance_Thread(string sour, string dest)
        {
            int result = myBl.CalculateDistance(sour, dest);
            result = result / 1000; // convert to kilometers
            Action act = (() => { TheAnswer.Text += "  " + result.ToString() + " kilometers"; Calculate.IsEnabled = true; }); // So that we can change the text box in the main thread window
            Dispatcher.BeginInvoke(act);
        }
        /// <summary>
        /// decreases the size of the font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_MouseMove(object sender, MouseEventArgs e)
        {
            BackButton.FontSize = 25;
        }
        /// <summary>
        /// decreases the size of the font
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackButton_MouseLeave(object sender, MouseEventArgs e)
        {
            BackButton.FontSize = 20;
        }


    }
}
