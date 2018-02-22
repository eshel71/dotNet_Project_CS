using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace BL
{
    /// <summary>
    /// define the interface of the BL
    /// </summary>
    public interface IBL
    {

        // functions of nanny
        void AddNanny(Nanny myNanny);
        void DeleteNanny(int nannyId);
        void UpdateNanny(Nanny myNanny);

        // functions of mother
        void AddMother(Mother myMother);
        void DeleteMother(int motherId);
        void UpdateMother(Mother myMother);

        // functions of child
        void AddChild(Child myChild);
        void DeleteChild(int childId);
        void UpdateChild(Child myChild);

        // functions of contract
        void AddContract(Contract myContract);
        void DeleteContract(int contractNumber);
        void UpdateContract(Contract myContract);

        // enumerator functions
        IEnumerable<Nanny> MatchedNanny(Mother myMother);
        IEnumerable<Nanny> FiveBestMatched(Mother myMother);
        IEnumerable<Nanny> NanniesByAddress(Mother myMother);
        IEnumerable<Child> UntreatedChilds(Func<Child, bool> predicate = null);
        IEnumerable<Nanny> NanniesByTMT();
        IEnumerable<Contract> MatchedContracts(Func<Contract, bool> predicate = null);
        IEnumerable<Mother> MothersByPredicate(Func<Mother, bool> predicate = null);
        IEnumerable<Nanny> NanniesByPredicate(Func<Nanny, bool> predicate = null);
        IEnumerable<Child> ChildsByPredicate(Func<Child, bool> predicate = null);

        // other important functions
        int NumberMatchedContract(Func<Contract, bool> predicate = null);
        int CalculateDistance(string source, string dest);
    }
}
