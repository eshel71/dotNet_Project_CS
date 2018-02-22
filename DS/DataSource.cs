using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;

namespace DS
{
    /// <summary>
    /// note: THIS IS NOT NECESSARY WHEN USING THE XML!!
    /// contain the lists which contain all the data
    /// </summary>
    public class DataSource
    {
        // database lists
        public static List<Nanny> nannyList = new List<Nanny>();
        public static List<Mother> motherList = new List<Mother>();
        public static List<Child> childList = new List<Child>();
        public static List<Contract> contractList = new List<Contract>();

    }
}
