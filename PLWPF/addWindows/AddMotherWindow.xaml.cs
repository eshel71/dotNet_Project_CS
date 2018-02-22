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
    /// Interaction logic for AddMotherWindow.xaml
    /// </summary>
    public partial class AddMotherWindow : Window
    {
        BL.IBL myBl = BL.FactoryBL.GetBL();
        Mother myMother;

        public AddMotherWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            myMother = new Mother();
            this.DataContext = myMother;
        }

        /// <summary>
        /// checks exceptions and adding the mother
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            #region check exceptions

            bool isChecked = false;
            try
            {
                if (string.IsNullOrEmpty(idTextBox.Text))
                    throw new Exception("fill the id!");
                if (!int.TryParse(idTextBox.Text, out int i))
                    throw new Exception("enter id in numbers!");

                bool alreadyExist = myBl.MothersByPredicate().Any(x => x.Id == int.Parse(idTextBox.Text));
                if (alreadyExist)
                    throw new Exception("The mother already exist!");

                if (string.IsNullOrEmpty(firstNameTextBox.Text))
                    throw new Exception("fill the first name!");
                if (int.TryParse(firstNameTextBox.Text, out i))
                    throw new Exception("invalid first name!");

                if (string.IsNullOrEmpty(lastNameTextBox.Text))
                    throw new Exception("fill the last name!");
                if (int.TryParse(lastNameTextBox.Text, out i))
                    throw new Exception("invalid last name!");

                if (string.IsNullOrEmpty(addressTextBox.Text))
                    throw new Exception("fill the address!");
                if (int.TryParse(addressTextBox.Text, out i))
                    throw new Exception("invalid address!");

                if (string.IsNullOrEmpty(cellphoneTextBox.Text))
                    throw new Exception("fill the cellphone!");
                if (int.TryParse(cellphoneTextBox.Text, out i))
                    throw new Exception("invalid cellphone!");
                if (cellphoneTextBox.Text == "like: 052-1234578")
                    throw new Exception("fill real cellphone!");

                if (int.TryParse(areaNannyTextBox.Text, out i))
                    throw new Exception("invalid area nanny!");

                if (string.IsNullOrEmpty(notesTextBox.Text))
                    throw new Exception("fill the  notes!");
                if (int.TryParse(notesTextBox.Text, out i))
                    throw new Exception("invalid notes!");

                int enter, exit; // help variables
                if ((bool)checkSun.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterSun.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterSun.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitSun.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitSun.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterSun.Text);
                    exit = int.Parse(exitSun.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");

                }
                if ((bool)checkMon.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterMon.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterMon.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitMon.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitMon.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterMon.Text);
                    exit = int.Parse(exitMon.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");

                }

                if ((bool)checkTue.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterTue.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterTue.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitTue.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitTue.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterTue.Text);
                    exit = int.Parse(exitTue.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkWed.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterWed.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterWed.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitWed.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitWed.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterWed.Text);
                    exit = int.Parse(exitWed.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkThu.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterThu.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterThu.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitThu.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitThu.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterThu.Text);
                    exit = int.Parse(exitThu.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                if ((bool)checkFri.IsChecked)
                {
                    if (string.IsNullOrEmpty(enterFri.Text))
                        throw new Exception("fill the enter box!");
                    if (!int.TryParse(enterFri.Text, out i))
                        throw new Exception("invalid enter box!");

                    if (string.IsNullOrEmpty(exitFri.Text))
                        throw new Exception("fill the exit box!");
                    if (!int.TryParse(exitFri.Text, out i))
                        throw new Exception("invalid exit box!");

                    enter = int.Parse(enterFri.Text);
                    exit = int.Parse(exitFri.Text);
                    if (exit < enter)
                        throw new Exception("exit time is earlier than the enter time!");
                }

                isChecked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "DATA ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (!isChecked)
                return;
            #endregion

            #region add the mother




            BE.Mother myMother = new BE.Mother()
            {
                Address = addressTextBox.Text,
                Cellphone = cellphoneTextBox.Text,
                FirstName = firstNameTextBox.Text,
                Id = int.Parse(idTextBox.Text),
                LastName = lastNameTextBox.Text,
                AreaNanny = areaNannyTextBox.Text,
                Notes = notesTextBox.Text
            };

            myMother.NannyDays[0] = (bool)checkSun.IsChecked;
            myMother.NannyDays[1] = (bool)checkMon.IsChecked;
            myMother.NannyDays[2] = (bool)checkTue.IsChecked;
            myMother.NannyDays[3] = (bool)checkWed.IsChecked;
            myMother.NannyDays[4] = (bool)checkThu.IsChecked;
            myMother.NannyDays[5] = (bool)checkFri.IsChecked;
            myMother.NannyDays[6] = (bool)checkSat.IsChecked;

            if ((bool)checkSun.IsChecked)
            {
                myMother.Schedule[0, 0] = int.Parse(enterSun.Text);
                myMother.Schedule[1, 0] = int.Parse(exitSun.Text);
            }
            if ((bool)checkMon.IsChecked)
            {
                myMother.Schedule[0, 1] = int.Parse(enterMon.Text);
                myMother.Schedule[1, 1] = int.Parse(exitMon.Text);
            }
            if ((bool)checkTue.IsChecked)
            {
                myMother.Schedule[0, 2] = int.Parse(enterTue.Text);
                myMother.Schedule[1, 2] = int.Parse(exitTue.Text);
            }
            if ((bool)checkWed.IsChecked)
            {
                myMother.Schedule[0, 3] = int.Parse(enterWed.Text);
                myMother.Schedule[1, 3] = int.Parse(exitWed.Text);
            }
            if ((bool)checkThu.IsChecked)
            {
                myMother.Schedule[0, 4] = int.Parse(enterThu.Text);
                myMother.Schedule[1, 4] = int.Parse(exitThu.Text);
            }
            if ((bool)checkFri.IsChecked)
            {
                myMother.Schedule[0, 5] = int.Parse(enterFri.Text);
                myMother.Schedule[1, 5] = int.Parse(exitFri.Text);
            }
            try
            {
                myBl.AddMother(myMother);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ADD MOTHER ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            #endregion

            MessageBox.Show("The mother added successfully :)", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

        private void Click_cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
