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
    /// Interaction logic for UpdateDataWindow.xaml
    /// </summary>
    public partial class UpdateDataWindow : Window
    {
        string dataType;
        BL.IBL myBl = BL.FactoryBL.GetBL();
        Child myChild; Nanny myNanny; Mother myMother; Contract myContract; // we will need to use one of them for the updating

        /// <summary>
        /// c-tor that define the window for specific type that it get in the type
        /// </summary>
        /// <param name="type"></param>
        public UpdateDataWindow(string type)
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            dataType = type;
            title.Text = "Update a " + type; // set the title
            textBlock.Text = "Choose " + type + " to update: ";

            #region comboBox defination

            if (dataType == "nanny")
            {
                ChoodsingCB.ItemsSource = myBl.NanniesByPredicate();
                ChoodsingCB.DisplayMemberPath = "FirstName";
                ChoodsingCB.SelectedValuePath = "Id";

                nannyGrid1.Visibility = System.Windows.Visibility.Visible;
                nannyGrid2.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            if (dataType == "contract")
            {
                ChoodsingCB.ItemsSource = myBl.MatchedContracts();
                ChoodsingCB.DisplayMemberPath = "TransactionNumber";
                ChoodsingCB.SelectedValuePath = "TransactionNumber";
                contractGrid1.Visibility = System.Windows.Visibility.Visible;

                ComboBoxItem newItem;
                newItem = new ComboBoxItem();
                newItem.Content = "Monthly";
                salaryTypeCB.Items.Add(newItem);

                newItem = new ComboBoxItem();
                newItem.Content = "Hourly";
                salaryTypeCB.Items.Add(newItem);
                return;
            }

            if (dataType == "child")
            {
                ChoodsingCB.ItemsSource = myBl.ChildsByPredicate();
                ChoodsingCB.DisplayMemberPath = "FirstName";
                ChoodsingCB.SelectedValuePath = "Id";

                childDataGrid.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            if (dataType == "mother")
            {
                ChoodsingCB.ItemsSource = myBl.MothersByPredicate();
                ChoodsingCB.DisplayMemberPath = "FirstName";
                ChoodsingCB.SelectedValuePath = "Id";

                motherGrid1.Visibility = System.Windows.Visibility.Visible;
                motherGrid2.Visibility = System.Windows.Visibility.Visible;
                return;
            }

            #endregion

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
        /// this event will update the grids , and will link the specific object to the dataContext
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChoodsingCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (dataType == "contract")
            {
                myContract = myBl.MatchedContracts(x => x.TransactionNumber == (int)ChoodsingCB.SelectedValue).First();
                contractGrid1.DataContext = myContract;
            }

            else if (dataType == "nanny")
            {
                myNanny = myBl.NanniesByPredicate(x => x.Id == (int)ChoodsingCB.SelectedValue).First();
                nannyGrid1.DataContext = myNanny;
                nannyGrid2.DataContext = myNanny;

                #region loading grid2

                NcheckSun.IsChecked = myNanny.WorkDays[0];
                NcheckMon.IsChecked = myNanny.WorkDays[1];
                NcheckTue.IsChecked = myNanny.WorkDays[2];
                NcheckWed.IsChecked = myNanny.WorkDays[3];
                NcheckThu.IsChecked = myNanny.WorkDays[4];
                NcheckFri.IsChecked = myNanny.WorkDays[5];
                NcheckSat.IsChecked = myNanny.WorkDays[6];

                if (myNanny.WorkDays[0])
                {
                    NenterSun.Text = myNanny.Schedule[0, 0].ToString();
                    NexitSun.Text = myNanny.Schedule[1, 0].ToString();
                }

                if (myNanny.WorkDays[1])
                {
                    NenterMon.Text = myNanny.Schedule[0, 1].ToString();
                    NexitMon.Text = myNanny.Schedule[1, 1].ToString();
                }

                if (myNanny.WorkDays[2])
                {
                    NenterTue.Text = myNanny.Schedule[0, 2].ToString();
                    NexitTue.Text = myNanny.Schedule[1, 2].ToString();
                }

                if (myNanny.WorkDays[3])
                {
                    NenterWed.Text = myNanny.Schedule[0, 3].ToString();
                    NexitWed.Text = myNanny.Schedule[1, 3].ToString();
                }

                if (myNanny.WorkDays[4])
                {
                    NenterThu.Text = myNanny.Schedule[0, 4].ToString();
                    NexitThu.Text = myNanny.Schedule[1, 4].ToString();
                }

                if (myNanny.WorkDays[5])
                {
                    NenterFri.Text = myNanny.Schedule[0, 5].ToString();
                    NexitFri.Text = myNanny.Schedule[1, 5].ToString();
                }

                if (myNanny.WorkDays[6])
                {
                    NenterSat.Text = myNanny.Schedule[0, 6].ToString();
                    NexitSat.Text = myNanny.Schedule[1, 6].ToString();
                }

                #endregion

            }

            else if (dataType == "child")
            {
                myChild = myBl.ChildsByPredicate(x => x.Id == (int)ChoodsingCB.SelectedValue).First();
                childDataGrid.DataContext = myChild;
            }

            else if (dataType == "mother")
            {
                myMother = myBl.MothersByPredicate(x => x.Id == (int)ChoodsingCB.SelectedValue).First();
                motherGrid1.DataContext = myMother;
                motherGrid2.DataContext = myMother;

                #region loading grid 2 

                checkSun.IsChecked = myMother.NannyDays[0];
                checkMon.IsChecked = myMother.NannyDays[1];
                checkTue.IsChecked = myMother.NannyDays[2];
                checkWed.IsChecked = myMother.NannyDays[3];
                checkThu.IsChecked = myMother.NannyDays[4];
                checkFri.IsChecked = myMother.NannyDays[5];
                checkSat.IsChecked = myMother.NannyDays[6];

                if (myMother.NannyDays[0])
                {
                    enterSun.Text = myMother.Schedule[0, 0].ToString();
                    exitSun.Text = myMother.Schedule[1, 0].ToString();
                }

                if (myMother.NannyDays[1])
                {
                    enterMon.Text = myMother.Schedule[0, 1].ToString();
                    exitMon.Text = myMother.Schedule[1, 1].ToString();
                }

                if (myMother.NannyDays[2])
                {
                    enterTue.Text = myMother.Schedule[0, 2].ToString();
                    exitTue.Text = myMother.Schedule[1, 2].ToString();
                }

                if (myMother.NannyDays[3])
                {
                    enterWed.Text = myMother.Schedule[0, 3].ToString();
                    exitWed.Text = myMother.Schedule[1, 3].ToString();
                }

                if (myMother.NannyDays[4])
                {
                    enterThu.Text = myMother.Schedule[0, 4].ToString();
                    exitThu.Text = myMother.Schedule[1, 4].ToString();
                }

                if (myMother.NannyDays[5])
                {
                    enterFri.Text = myMother.Schedule[0, 5].ToString();
                    exitFri.Text = myMother.Schedule[1, 5].ToString();
                }

                if (myMother.NannyDays[6])
                {
                    enterSat.Text = myMother.Schedule[0, 6].ToString();
                    exitSat.Text = myMother.Schedule[1, 6].ToString();
                }

                #endregion

            }

        }

        /// <summary>
        /// checks exceptions and adding the specific object to the dataBase
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_update(object sender, RoutedEventArgs e)
        {

            try
            {

                #region  adding the specific object and exceptions search

                if (dataType == "mother")
                {
                    #region checks mother exceptions

                    try
                    {

                        if (string.IsNullOrEmpty(addressTextBox.Text))
                            throw new Exception("fill the address!");
                        if (int.TryParse(addressTextBox.Text, out int i))
                            throw new Exception("invalid address!");

                        if (string.IsNullOrEmpty(cellphoneTextBox.Text))
                            throw new Exception("fill the cellphone!");
                        if (int.TryParse(cellphoneTextBox.Text, out i))
                            throw new Exception("invalid cellphone!");

                        if (string.IsNullOrEmpty(areaNannyTextBox.Text))
                            throw new Exception("fill the area nanny!");
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

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "DATA ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    #endregion

                    #region update the mother

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

                    myMother.Address = addressTextBox.Text;
                    myMother.AreaNanny = areaNannyTextBox.Text;
                    myMother.Notes = notesTextBox.Text;
                    myMother.Cellphone = cellphoneTextBox.Text;

                    myBl.UpdateMother(myMother);

                    #endregion

                }

                else if (dataType == "child")
                {
                    myChild.IsSpecialNeeds = (bool)isSpecialNeedsCheckBox.IsChecked;
                    myChild.SpecialNeeds = specialNeedsTextBox.Text;
                    myBl.UpdateChild(myChild);
                }

                else if (dataType == "nanny")
                {
                    #region checks nanny exceptions

                    if (string.IsNullOrEmpty(experienceTextBox.Text))
                        throw new Exception("fill the experience!");
                    if (!int.TryParse(experienceTextBox.Text, out int i))
                        throw new Exception("enter experience in numbers!");

                    if (string.IsNullOrEmpty(cellphoneTextBox1.Text))
                        throw new Exception("fill the cellphone!");
                    if (int.TryParse(cellphoneTextBox1.Text, out i))
                        throw new Exception("invalid cellphone!");

                    if (string.IsNullOrEmpty(floorTextBox.Text))
                        throw new Exception("fill the floor!");
                    if (!int.TryParse(floorTextBox.Text, out i))
                        throw new Exception("enter floor in numbers!");

                    if (string.IsNullOrEmpty(hourlyWageTextBox.Text))
                        throw new Exception("fill the hourly wage!");
                    if (!int.TryParse(hourlyWageTextBox.Text, out i))
                        throw new Exception("enter hourly wage in numbers!");

                    if (string.IsNullOrEmpty(addressTextBox1.Text))
                        throw new Exception("fill the address!");
                    if (int.TryParse(addressTextBox1.Text, out i))
                        throw new Exception("invalid address!");

                    if (string.IsNullOrEmpty(maxChildAgeTextBox.Text))
                        throw new Exception("fill the max child age!");
                    if (!int.TryParse(maxChildAgeTextBox.Text, out i))
                        throw new Exception("enter max child age in numbers!");

                    if (string.IsNullOrEmpty(maxChildNumberTextBox.Text))
                        throw new Exception("fill the max child number!");
                    if (!int.TryParse(maxChildNumberTextBox.Text, out i))
                        throw new Exception("enter max child number in numbers!");

                    if (string.IsNullOrEmpty(minChildAgeTextBox.Text))
                        throw new Exception("fill the min child age!");
                    if (!int.TryParse(minChildAgeTextBox.Text, out i))
                        throw new Exception("enter min child age in numbers!");

                    int min = int.Parse(minChildAgeTextBox.Text);
                    int max = int.Parse(maxChildAgeTextBox.Text);
                    if (max < min)
                        throw new Exception("max child age can't be less than min child age");

                    if (string.IsNullOrEmpty(monthlyWageTextBox.Text))
                        throw new Exception("fill the monthly wage!");
                    if (!int.TryParse(monthlyWageTextBox.Text, out i))
                        throw new Exception("enter monthly wage in numbers!");

                    if (string.IsNullOrEmpty(recommendationTextBox.Text))
                        throw new Exception("fill the  recommendation!");
                    if (int.TryParse(recommendationTextBox.Text, out i))
                        throw new Exception("invalid recommendation !");

                    int enter, exit; // help variables
                    if ((bool)NcheckSun.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterSun.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterSun.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitSun.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitSun.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterSun.Text);
                        exit = int.Parse(NexitSun.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");
                    }

                    if ((bool)NcheckMon.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterMon.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterMon.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitMon.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitMon.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterMon.Text);
                        exit = int.Parse(NexitMon.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");

                    }

                    if ((bool)NcheckTue.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterTue.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterTue.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitTue.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitTue.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterTue.Text);
                        exit = int.Parse(NexitTue.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");
                    }

                    if ((bool)NcheckWed.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterWed.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterWed.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitWed.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitWed.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterWed.Text);
                        exit = int.Parse(NexitWed.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");
                    }

                    if ((bool)NcheckThu.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterThu.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterThu.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitThu.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitThu.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterThu.Text);
                        exit = int.Parse(NexitThu.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");
                    }

                    if ((bool)NcheckFri.IsChecked)
                    {
                        if (string.IsNullOrEmpty(NenterFri.Text))
                            throw new Exception("fill the enter box!");
                        if (!int.TryParse(NenterFri.Text, out i))
                            throw new Exception("invalid enter box!");

                        if (string.IsNullOrEmpty(NexitFri.Text))
                            throw new Exception("fill the exit box!");
                        if (!int.TryParse(NexitFri.Text, out i))
                            throw new Exception("invalid exit box!");

                        enter = int.Parse(NenterFri.Text);
                        exit = int.Parse(NexitFri.Text);
                        if (exit < enter)
                            throw new Exception("exit time is earlier than the enter time!");
                    }

                    #endregion

                    #region update the nanny

                    myNanny.WorkDays[0] = (bool)NcheckSun.IsChecked;
                    myNanny.WorkDays[1] = (bool)NcheckMon.IsChecked;
                    myNanny.WorkDays[2] = (bool)NcheckTue.IsChecked;
                    myNanny.WorkDays[3] = (bool)NcheckWed.IsChecked;
                    myNanny.WorkDays[4] = (bool)NcheckThu.IsChecked;
                    myNanny.WorkDays[5] = (bool)NcheckFri.IsChecked;
                    myNanny.WorkDays[6] = (bool)NcheckSat.IsChecked;

                    if ((bool)NcheckSun.IsChecked)
                    {
                        myNanny.Schedule[0, 0] = int.Parse(NenterSun.Text);
                        myNanny.Schedule[1, 0] = int.Parse(NexitSun.Text);
                    }
                    if ((bool)NcheckMon.IsChecked)
                    {
                        myNanny.Schedule[0, 1] = int.Parse(NenterMon.Text);
                        myNanny.Schedule[1, 1] = int.Parse(NexitMon.Text);
                    }
                    if ((bool)NcheckTue.IsChecked)
                    {
                        myNanny.Schedule[0, 2] = int.Parse(NenterTue.Text);
                        myNanny.Schedule[1, 2] = int.Parse(NexitTue.Text);
                    }
                    if ((bool)NcheckWed.IsChecked)
                    {
                        myNanny.Schedule[0, 3] = int.Parse(NenterWed.Text);
                        myNanny.Schedule[1, 3] = int.Parse(NexitWed.Text);
                    }
                    if ((bool)NcheckThu.IsChecked)
                    {
                        myNanny.Schedule[0, 4] = int.Parse(NenterThu.Text);
                        myNanny.Schedule[1, 4] = int.Parse(NexitThu.Text);
                    }
                    if ((bool)NcheckFri.IsChecked)
                    {
                        myNanny.Schedule[0, 5] = int.Parse(NenterFri.Text);
                        myNanny.Schedule[1, 5] = int.Parse(NexitFri.Text);
                    }

                    myNanny.ElevatorExist = (bool)elevatorExistCheckBox.IsChecked;
                    myNanny.Experience = int.Parse(experienceTextBox.Text);
                    myNanny.Cellphone = cellphoneTextBox1.Text;
                    myNanny.Floor = int.Parse(floorTextBox.Text);
                    myNanny.HolidaysByTMT = (bool)holidaysByTMTCheckBox.IsChecked;
                    myNanny.HourlyWage = int.Parse(hourlyWageTextBox.Text);
                    myNanny.Address = addressTextBox1.Text;
                    myNanny.MaxChildAge = int.Parse(maxChildAgeTextBox.Text);
                    myNanny.MinChildAge = int.Parse(minChildAgeTextBox.Text);
                    myNanny.MaxChildNumber = int.Parse(maxChildNumberTextBox.Text);
                    myNanny.MonthlyWage = int.Parse(monthlyWageTextBox.Text);
                    myNanny.PerHourSalary = (bool)perHourSalaryCheckBox.IsChecked;
                    myNanny.Recommendation = recommendationTextBox.Text;

                    myBl.UpdateNanny(myNanny);

                    #endregion

                }

                else if (dataType == "contract")
                {
                    #region check contract exceptions


                    if (contractEndDatePicker.SelectedDate < contractBeginDatePicker.SelectedDate)
                        throw new Exception("invalid end date ");

                    if (salaryTypeCB.SelectedItem == null)
                        throw new Exception("no salary type selected!");

                    #endregion

                    // adding the contract
                    if (((ComboBoxItem)salaryTypeCB.SelectedItem).Content.ToString() == "Hourly")
                        myContract.ConType = ContractType.hourly;
                    else myContract.ConType = ContractType.monthly;

                    myContract.HourlyWage = int.Parse(hourlyWageTextBox1.Text);
                    myContract.Met = (bool)metCheckBox.IsChecked;
                    myContract.MonthlyWage = int.Parse(monthlyWageTextBox1.Text);
                    myContract.ContractBegin = (DateTime)contractBeginDatePicker.SelectedDate;
                    myContract.ContractEnd = (DateTime)contractEndDatePicker.SelectedDate;

                    myBl.UpdateContract(myContract);
                }

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "UPDATE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("The " + dataType + " updated successfully", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }

    }
}
