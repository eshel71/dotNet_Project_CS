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
    /// Interaction logic for UpdateDataWindow.xaml
    /// </summary>
    public partial class UpdateMenuWindow : Window
    {

        public UpdateMenuWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        /// <summary>
        /// opening the next window ( update nanny )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_nanny(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new UpdateDataWindow("nanny");
            nextWindow.Height = 900;
            nextWindow.Width=900;
            this.Close();
            nextWindow.Show();
        }

        /// <summary>
        /// opening the next window ( update child )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_child(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new UpdateDataWindow("child");
            this.Close();
            nextWindow.Show();
        }

        /// <summary>
        /// opening the next window ( update contract )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_contract(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new UpdateDataWindow("contract");
            this.Close();
            nextWindow.Show();
        }

        /// <summary>
        /// opening the next window ( update mother )
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_mother(object sender, RoutedEventArgs e)
        {
            Window nextWindow = new UpdateDataWindow("mother");
            this.Close();
            nextWindow.Show();
        }

    }
}
