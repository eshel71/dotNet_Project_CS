using System;
using System.Collections.Generic;
using System.Text;
using BE;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{

    /// <summary>
    /// define the interface of the DAL
    /// </summary>
    public interface Idal
    {
        // functions of nanny
        void AddNanny(Nanny myNanny);
        void DeleteNanny(int nannyId);
        void UpdateNanny(Nanny myNanny);
        Nanny FindNanny(int nannyId);

        // functions of mother
        void AddMother(Mother myMother);
        void DeleteMother(int motherId);
        void UpdateMother(Mother myMother);
        Mother FindMother(int motherId);

        // functions of child
        void AddChild(Child myChild);
        void DeleteChild(int childId);
        void UpdateChild(Child myChild);
        Child FindChild(int childId);

        // functions of contract
        void AddContract(Contract myContract);
        void DeleteContract(int contractNumber);
        void UpdateContract(Contract myContract);
        Contract FindContract(int contractNumber);

        // enumerator functions
        IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null);
        IEnumerable<Mother> GetAllMothers(Func<Mother, bool> predicate = null);
        IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null);
        IEnumerable<Child> GetAllChildsByMother(Mother myMother);
        IEnumerable<Contract> GetAllContracts(Func<Contract, bool> predicate = null);
    }
}
