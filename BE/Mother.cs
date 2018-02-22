using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// contains the information of a mother.
    /// mother linked directly to her "Child".
    /// </summary>
    public class Mother
    {

        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Cellphone { get; set; }
        public string Address { get; set; }
        public string AreaNanny { get; set; }
        public bool[] NannyDays;
        public int[,] Schedule;
        public string Notes { get; set; }

        /// <summary>
        /// useful for the XML
        /// </summary>
        public string TempArray
        {
            get
            {
                if (NannyDays == null)
                    return null;

                string result = "";
                int size = NannyDays.Length;
                result += "" + size;
                for (int i = 0; i < size; i++)
                    result += "," + NannyDays[i];

                return result;
            }
            set
            {
                if (value != null && value.Length > 0)
                {
                    string[] values = value.Split(',');
                    int size = int.Parse(values[0]);
                    NannyDays = new bool[size];
                    int index = 1;
                    for (int i = 0; i < size; i++)
                        NannyDays[i] = bool.Parse(values[index++]);
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
        /// Mother c-tor
        /// </summary>
        public Mother()
        {
            NannyDays = new bool[7];
            Schedule = new int[2, 6];
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

}
