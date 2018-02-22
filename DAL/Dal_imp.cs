using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;
using System.IO;

namespace DAL
{
    /// <summary>
    /// contains the implementions of all the functions (and more) in "Idal".
    /// </summary>
    public class Dal_imp : Idal
    {
        public static int NextTransactionNumber; // field for the contract number

        public Dal_XML_imp myXML = new Dal_XML_imp();

        /// <summary>
        /// dal c-tor
        /// gets the next transaction number from the XML dataBase
        /// </summary>
        public Dal_imp()
        {
            NextTransactionNumber = myXML.GetTransactionNumber(); // get the initial number
        }

        #region Implementations of Nanny functions

        /// <summary>
        /// add Nanny to the Data Source
        /// </summary>
        /// <param name="myNanny"></param>
        public void AddNanny(Nanny myNanny)
        {
            if (FindNanny(myNanny.Id) != null)
                throw new Exception("Nanny already exist");

            myXML.Addnanny(myNanny);
        }

        /// <summary>
        /// delete Nanny from the Data Source if she exist
        /// delete also all the contracts of that nanny
        /// </summary>
        /// <param name="nannyId"></param>
        public void DeleteNanny(int nannyId)
        {
            if (FindNanny(nannyId) == null)
                throw new Exception("Nanny not exist");

            IEnumerable<Contract> nannyContracts = GetAllContracts(x => x.NannyId == nannyId); // the contracts of that nanny
            List<int> contNumList = new List<int>(); //  transaction numbers to remove

            foreach (Contract item in nannyContracts) // for all her contracts
                contNumList.Add(item.TransactionNumber); // adding the contract number to the list

            while (contNumList.Count() != 0)  // remove his contracts
            {
                DeleteContract(contNumList.First());
                contNumList.RemoveAt(0);
            }

            myXML.RemoveNanny(nannyId); // remove the nanny
        }

        /// <summary>
        /// update nanny by id number
        /// </summary>
        /// <param name="myNanny"></param>
        public void UpdateNanny(Nanny myNanny)
        {
            int index = myXML.GetnannyList().FindIndex(x => x.Id == myNanny.Id);
            if (index == -1)
                throw new Exception("Nanny not exist");

            myXML.UpdateNanny(myNanny);
        }

        /// <summary>
        /// return the nanny if she exist
        /// </summary>
        /// <param name="nannyId"></param>
        /// <returns></returns>
        public Nanny FindNanny(int nannyId)
        {
            return myXML.GetnannyList().Find(x => x.Id == nannyId);
        }

        #endregion

        #region Implementations of Mother functions

        /// <summary>
        /// add Mother to the Data Source
        /// </summary>
        /// <param name="myMother"></param>
        public void AddMother(Mother myMother)
        {
            if (FindMother(myMother.Id) != null)
                throw new Exception("Mother already exist");

            myXML.AddMother(myMother);
        }

        /// <summary>
        /// delete Mother from Data Source
        /// also delete her children and their contracts
        /// </summary>
        /// <param name="motherId"></param>
        public void DeleteMother(int motherId)
        {
            Mother myMother = FindMother(motherId);
            if (myMother == null)
                throw new Exception("Mother not exist");

            IEnumerable<Child> sons = GetAllChildsByMother(myMother); // the sons of that mother
            List<int> sonIdList = new List<int>(); //  sons id  to remove

            foreach (Child item in sons) // for all her childs
                sonIdList.Add(item.Id); // adding the child Id number to the list

            while (sonIdList.Count() != 0)  //remove all the children (and their contracts)
            {
                DeleteChild(sonIdList.First());
                sonIdList.RemoveAt(0);
            }

            myXML.RemoveMother(myMother.Id); // remove the mother
        }

        /// <summary>
        /// update mother by id number
        /// </summary>
        /// <param name="myMother"></param>
        public void UpdateMother(Mother myMother)
        {
            int index = myXML.GetMotherList().FindIndex(x => x.Id == myMother.Id);
            if (index == -1)
                throw new Exception("Mother not exist");

            myXML.UpdateMother(myMother);
        }

        /// <summary>
        /// return the mother if she exist
        /// </summary>
        /// <param name="motherId"></param>
        /// <returns></returns>
        public Mother FindMother(int motherId)
        {
            return myXML.GetMotherList().Find(x => x.Id == motherId);
        }

        #endregion

        #region Implementations of Child functions

        /// <summary>
        /// add Child to the Data Source 
        /// </summary>
        /// <param name="myChild"></param>
        public void AddChild(Child myChild)
        {
            if (FindChild(myChild.Id) != null)
                throw new Exception("Child already exist");

            Mother myMother = FindMother(myChild.MotherId);
            myChild.LastName = myMother.LastName;
            myXML.AddChild(myChild);

        }

        /// <summary>
        /// delete Child from Data Source
        /// delete also the contracts of that child
        /// </summary>
        /// <param name="childId"></param>
        public void DeleteChild(int childId)
        {
            if (FindChild(childId) == null)
                throw new Exception("Child not exist");

            IEnumerable<Contract> childContracts = GetAllContracts(x => x.ChildId == childId); // the contracts of that child
            List<int> contNumList = new List<int>(); //  transaction numbers to remove

            foreach (Contract item in childContracts)
                contNumList.Add(item.TransactionNumber);

            while (contNumList.Count() != 0)  // remove his contracts
            {
                DeleteContract(contNumList.First());
                contNumList.RemoveAt(0);
            }

            myXML.RemoveChild(childId); // remove the child
        }

        /// <summary>
        /// update Child by id number
        /// </summary>
        /// <param name="myChild"></param>
        public void UpdateChild(Child myChild)
        {
            int index = myXML.GetchildList().FindIndex(x => x.Id == myChild.Id);
            if (index == -1)
                throw new Exception("Child not exist");

            myXML.AddChild(myChild);
        }

        /// <summary>
        /// return the Child if he exist
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public Child FindChild(int childId)
        {
            return myXML.GetchildList().Find(x => x.Id == childId);
        }

        #endregion

        #region Implementations of Contract functions

        /// <summary>
        /// add Contract to the Data Source 
        /// </summary>
        /// <param name="myContract"></param>
        public void AddContract(Contract myContract)
        {
            if (FindContract(myContract.TransactionNumber) != null)
                throw new Exception("Contract already exist");

            Nanny nan = FindNanny(myContract.NannyId);
            if (nan == null) // nanny not found
                throw new Exception("nanny of this contract not exist");

            Child chi = FindChild(myContract.ChildId);
            Mother mom = FindMother(chi.MotherId);
            if (mom == null) // mother not found
                throw new Exception("mother of this contract not exist");

            if ((DateTime.Now.Year - chi.DateOfBirth.Year) > nan.MaxChildAge)
                throw new Exception("child age exceeded from the max age!");

            if ((DateTime.Now.Year - chi.DateOfBirth.Year) < nan.MinChildAge)
                throw new Exception("child age exceeded from the min age!");

            myContract.ContractSigned = true;
            nan.NumOfContract += 1;
            myXML.UpdateNanny(nan);
            myContract.TransactionNumber = NextTransactionNumber++;
            myXML.UpdateTransactionNumber(NextTransactionNumber);
            myXML.AddContract(myContract);

        }

        /// <summary>
        /// delete Contract from Data Source
        /// </summary>
        /// <param name="contractNumber"></param>
        public void DeleteContract(int contractNumber)
        {
            Contract con = FindContract(contractNumber);
            if (con == null)
                throw new Exception("Contract not exist");

            Nanny nan = FindNanny(con.NannyId);
            nan.NumOfContract--;

            myXML.RemoveContract(contractNumber);
            myXML.UpdateNanny(nan);
        }

        /// <summary>
        /// update Contract 
        /// </summary>
        /// <param name="myContract"></param>
        public void UpdateContract(Contract myContract)
        {
            int index = myXML.GetContractList().FindIndex(x => x.TransactionNumber == myContract.TransactionNumber);
            if (index == -1)
                throw new Exception("Contract not exist");

            myXML.UpdateContract(myContract);
        }

        /// <summary>
        /// return the Contract if exist
        /// </summary>
        /// <param name="contractNumber"></param>
        /// <returns></returns>
        public Contract FindContract(int contractNumber)
        {
            return myXML.GetContractList().Find(x => x.TransactionNumber == contractNumber);
        }

        #endregion

        #region implementations of enumerable  

        /// <summary>
        /// return list of nannies by predicate if exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> GetAllNannies(Func<Nanny, bool> predicate = null)
        {
            if (predicate == null) // with no condition
                return myXML.GetnannyList().AsEnumerable();

            return myXML.GetnannyList().Where(predicate);

        }

        /// <summary>
        /// return list of mothers by predicate if exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Mother> GetAllMothers(Func<Mother, bool> predicate = null)
        {
            if (predicate == null) // with no condition
                return myXML.GetMotherList().AsEnumerable();

            return myXML.GetMotherList().Where(predicate);

        }

        /// <summary>
        /// return list of childs by predicate if exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Child> GetAllChilds(Func<Child, bool> predicate = null)
        {
            if (predicate == null) // with no condition
                return myXML.GetchildList().AsEnumerable();

            return myXML.GetchildList().Where(predicate);
        }

        /// <summary>
        /// return list of childs by mother id
        /// </summary>
        /// <param name="myMother"></param>
        /// <returns></returns>
        public IEnumerable<Child> GetAllChildsByMother(Mother myMother)
        {
            return myXML.GetchildList().Where(x => x.MotherId == myMother.Id);
        }

        /// <summary>
        /// return list of contracts by predicate if exist
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Contract> GetAllContracts(Func<Contract, bool> predicate = null)
        {
            if (predicate == null) // with no condition
                return myXML.GetContractList().AsEnumerable();

            return myXML.GetContractList().Where(predicate);
        }

        #endregion

    }
}
