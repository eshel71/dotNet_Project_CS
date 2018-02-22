using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    /// <summary>
    /// contains tha information of a contract.
    /// the contract linking between "Nanny" and her "Child".
    /// </summary>
    public class Contract
    {
        public int TransactionNumber { get; set; }
        public int NannyId { get; set; }
        public int ChildId { get; set; }
        public bool Met { get; set; }
        public bool ContractSigned { get; set; }
        public int HourlyWage { get; set; }
        public int MonthlyWage { get; set; }
        public ContractType ConType { get; set; }
        public DateTime ContractBegin { get; set; }
        public DateTime ContractEnd { get; set; }

        /// <summary>
        /// c-tor of contract
        /// its marks the Contract as not Signed
        /// </summary>
        public Contract()
        {
            ContractSigned = false;
        }

        public override string ToString()
        {
            return base.ToString();
        }

    }

}
