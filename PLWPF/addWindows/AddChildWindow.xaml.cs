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
    /// Interaction logic for AddChildWindow.xaml
    /// </summary>
    public partial class AddChildWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();

        /// <summary>
        /// c-tor
        /// </summary>
        public AddChildWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            //mothers comboBox definations
            IEnumerable<Mother> mothers = myBl.MothersByPredicate();
            ComboBoxItem newItem;
            foreach (Mother myMother in mothers)
            {
                newItem = new ComboBoxItem();
                newItem.Content = myMother.FirstName + " " + myMother.LastName;
                motherCB.Items.Add(newItem);
            }

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

        /// <summary>
        /// checks exceptions and adding the child
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_ok(object sender, RoutedEventArgs e)
        {

            #region check exceptions

            try
            {
                if (string.IsNullOrEmpty(firstNameTextBox.Text))
                    throw new Exception("fill the first name!");
                if (int.TryParse(firstNameTextBox.Text, out int i))
                    throw new Exception("invalid first name!");

                if (string.IsNullOrEmpty(idTextBox.Text))
                    throw new Exception("fill the id!");
                if (!int.TryParse(idTextBox.Text, out i))
                    throw new Exception("invalid id!");

                bool alreadyExist = myBl.ChildsByPredicate().Any(x => x.Id == int.Parse(idTextBox.Text));
                if (alreadyExist)
                    throw new Exception("The child already exist!");

                if (motherCB.SelectedItem == null)
                    throw new Exception("no mother selected!");

                if (dateOfBirthDatePicker.SelectedDate > DateTime.Today)
                    throw new Exception("invalid date ");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DATA ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            #region add the child

            string motherName = ((ListBoxItem)motherCB.SelectedItem).Content.ToString().Split(':').First();
            Mother myMother = (myBl.MothersByPredicate(x => (x.FirstName + " " + x.LastName) == motherName)).First();

            BE.Child myChild = new BE.Child() // create the new child for ading
            {
                DateOfBirth = (DateTime)dateOfBirthDatePicker.SelectedDate,
                FirstName = firstNameTextBox.Text,
                Id = int.Parse(idTextBox.Text),
                IsSpecialNeeds = (bool)isSpecialNeedsCheckBox.IsChecked,
                MotherId = myMother.Id,
                SpecialNeeds = specialNeedsTextBox.Text
            };
            try
            {
                myBl.AddChild(myChild);
            }
            catch (Exception mes)
            {
                MessageBox.Show(mes.Message, "ADD CHILD ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            MessageBox.Show("The child added successfully :)", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

    }
}
