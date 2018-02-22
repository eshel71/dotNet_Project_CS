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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;

namespace PLWPF
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BL.IBL bl; //the declaration is here because we need to use this bl in the following functions

        /// <summary>
        /// c-tor.
        /// initiating the bl variable
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            bl = BL.FactoryBL.GetBL(); // we will use this bl in all the program
            ((BL.Bl_imp)bl).InsertData(); // activate the son's function to add the first data
            this.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// opening the next window ( queries window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Queries(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new QueriesWindow();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( addData window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_addData(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new AddDataWindow();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( deleteData window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_deleteData(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new DeleteDataWindow();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( updateData window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_updateData(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new UpdateMenuWindow();
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// opening the next window ( data window )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_data(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new DataWindow();
            nextWindow.WindowState = WindowState.Maximized;
            nextWindow.ShowDialog();
        }

        /// <summary>
        /// closing the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
