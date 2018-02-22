using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// contains the information of a child.
    /// child is linked directly to his "Mother" and his "Contract".
    /// he also linked to his "Nanny"  but only through a "Contract"
    /// </summary>
    public class Child
    {
        public int Id { get; set; }
        public int MotherId { get; set; }
        public string FirstName { get; set; }
        public string LastName;
        public DateTime DateOfBirth { get; set; }
        public bool IsSpecialNeeds { get; set; }
        public string SpecialNeeds { get; set; }


        public override string ToString()
        {
            return base.ToString();
        }
    }

}
