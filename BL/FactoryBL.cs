using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    /// <summary>
    /// returns the current bl
    /// </summary>
    public class FactoryBL
    {
        static IBL bl = null;

        /// <summary>
        ///  returns the bl that we using in the main window
        /// </summary>
        /// <returns></returns>
        public static IBL GetBL()
        {
            if (bl == null)  //not initiated yet
                bl = new Bl_imp();

            return bl;
        }

    }

}
