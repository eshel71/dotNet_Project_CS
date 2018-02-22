using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// contains the information of a nanny.
    /// nanny linked directly to her "Contract".
    /// she also linked to her "Child"  but only through a "Contract" 
    /// </summary>
    public class Nanny
    {

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public bool ElevatorExist { get; set; }
        public int Floor { get; set; }
        public int Experience { get; set; }
        public int MaxChildNumber { get; set; }
        public int MinChildAge { get; set; }
        public int MaxChildAge { get; set; }
        public bool PerHourSalary { get; set; }
        public int HourlyWage { get; set; }
        public int MonthlyWage { get; set; }
        public bool[] WorkDays;
        public int[,] Schedule;
        public bool HolidaysByTMT { get; set; }
        public string Recommendation { get; set; }
        public int NumOfContract { get; set; }

        /// <summary>
        /// useful for the XML
        /// </summary>
        public string TempArray
        {
            get
            {
                if (WorkDays == null)
                    return null;

                string result = "";
                int size = WorkDays.Length;
                result += "" + size;
                for (int i = 0; i < size; i++)
                    result += "," + WorkDays[i];

                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int size = int.Parse(values[0]);
                    WorkDays = new bool[size];
                    int index = 1;
                    for (int i = 0; i < size; i++)
                        WorkDays[i] = bool.Parse(values[index++]);
                }
            }
        }

        /// <summary>
        /// useful for the XML
        /// </summary>
        public string TempMatrix
        {
            get
            {
                if (Schedule == null)
                    return null;
                string result = "";
                int sizeA = Schedule.GetLength(0);
                int sizeB = Schedule.GetLength(1);
                result += "" + sizeA + "," + sizeB;
                for (int i = 0; i < sizeA; i++)
                    for (int j = 0; j < sizeB; j++)
                        result += "," + Schedule[i, j];

                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int sizeA = int.Parse(values[0]);
                    int sizeB = int.Parse(values[1]);
                    Schedule = new int[sizeA, sizeB];
                    int index = 2;
                    for (int i = 0; i < sizeA; i++)
                        for (int j = 0; j < sizeB; j++)
                            Schedule[i, j] = int.Parse(values[index++]);
                }
            }
        }

        /// <summary>
        ///Nanny c-tor
        /// </summary>
        public Nanny()
        {
            WorkDays = new bool[7];
            Schedule = new int[2, 6];
        }

        /// <summary>
        /// to string 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }


    }

}
