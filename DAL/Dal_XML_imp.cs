using BE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DAL
{
    /// <summary>
    /// All the implementions of the xml functions
    /// </summary>
    public class Dal_XML_imp
    {
        XElement childsRoot;
        XElement nanniesRoot;
        XElement mothersRoot;
        XElement contractsRoot;
        XElement configRoot;

        public static string childsPath = @"C:\Users\Ariel Eshel\Desktop\dotNet5778_Project01_6372_0410\XML_collections\childs_XML.xml";
        public static string nanniesPath = @"C:\Users\Ariel Eshel\Desktop\dotNet5778_Project01_6372_0410\XML_collections\nannies_XML.xml";
        public static string mothersPath = @"C:\Users\Ariel Eshel\Desktop\dotNet5778_Project01_6372_0410\XML_collections\mothers_XML.xml";
        public static string contractsPath = @"C:\Users\Ariel Eshel\Desktop\dotNet5778_Project01_6372_0410\XML_collections\contracts_XML.xml";
        public static string configPath = @"C:\Users\Ariel Eshel\Desktop\dotNet5778_Project01_6372_0410\XML_collections\config.xml";

        public static bool DataExists; // this will help us in the first input data. when we insert the data in the first time it will be changed to "True"
        public Dal_XML_imp()
        {
            try
            {
                if (!File.Exists(configPath))
                {
                    configRoot = new XElement("config");

                    XElement var = new XElement("transactionNumber", 10000000);  // adding this for the first time 
                    configRoot.Add(var);

                    var = new XElement("isDataExist", false);  // thus we can know if we already put the data from the "bl" insert first data function
                    configRoot.Add(var);

                    configRoot.Save(configPath);
                }
                else
                    configRoot = XElement.Load(configPath);

                if (!File.Exists(childsPath))
                {
                    childsRoot = new XElement("childs");
                    childsRoot.Save(childsPath);
                }
                else
                    childsRoot = XElement.Load(childsPath);

                if (!File.Exists(nanniesPath))
                {
                    nanniesRoot = new XElement("nannies");
                    nanniesRoot.Save(nanniesPath);
                }
                else
                    nanniesRoot = XElement.Load(nanniesPath);

                if (!File.Exists(mothersPath))
                {
                    mothersRoot = new XElement("mothers");
                    mothersRoot.Save(mothersPath);
                }
                else
                    mothersRoot = XElement.Load(mothersPath);

                if (!File.Exists(contractsPath))
                {
                    contractsRoot = new XElement("contracts");
                    contractsRoot.Save(contractsPath);
                }
                else
                    contractsRoot = XElement.Load(contractsPath);

                LoadData();

            }
            catch (Exception)
            {
                throw new Exception("File upload problem");
            }

        }

        /// <summary>
        /// loading the data from the XML files into the roots elements 
        /// </summary>
        private void LoadData()
        {
            try
            {
                childsRoot = XElement.Load(childsPath);
                nanniesRoot = XElement.Load(nanniesPath);
                mothersRoot = XElement.Load(mothersPath);
                contractsRoot = XElement.Load(contractsPath);
                configRoot = XElement.Load(configPath);

                DataExists = bool.Parse((from vari in configRoot.Elements()
                                         where vari.Name == "isDataExist"
                                         select vari).FirstOrDefault().Value); ; // help for next data insertions
            }
            catch
            {
                throw new Exception("File upload problem");
            }
        }

        /// <summary>
        /// this funtion will update the transaction number in our XML to the last one that we used in our program
        /// </summary>
        /// <param name="num"></param>
        public void UpdateTransactionNumber(int num)
        {
            XElement var1 = (from vari in configRoot.Elements()
                             where vari.Name == "transactionNumber"
                             select vari).FirstOrDefault();

            var1.Value = num.ToString();
            configRoot.Save(configPath);
        }

        /// <summary>
        /// take the last transaction number from the XML file
        /// </summary>
        /// <returns></returns>
        public int GetTransactionNumber()
        {
            LoadData();
            return int.Parse((from vari in configRoot.Elements()
                              where vari.Name == "transactionNumber"
                              select vari).FirstOrDefault().Value);

        }

        /// <summary>
        /// we activate this function when we add the initial data to our program. we call this function from BL 
        /// this function will change the status to "true" - data inserted
        /// </summary>
        /// <returns></returns>
        public void ChangeDataStatus()
        {
            XElement var1 = (from vari in configRoot.Elements()
                             where vari.Name == "isDataExist"
                             select vari).FirstOrDefault();

            var1.Value = true.ToString();
            configRoot.Save(configPath);
            DataExists = true;
        }

        #region child functions

        /// <summary>
        /// add child to XML dataBase
        /// </summary>
        /// <param name="myChild"></param>
        public void AddChild(Child myChild)
        {
            XElement id = new XElement("id", myChild.Id);
            XElement motherId = new XElement("motherId", myChild.MotherId);
            XElement firstName = new XElement("firstName", myChild.FirstName);
            XElement lastName = new XElement("lastName", myChild.LastName);
            XElement dateOfBirth = new XElement("dateOfBirth", myChild.DateOfBirth);
            XElement isSpecialNeeds = new XElement("isSpecialNeeds", myChild.IsSpecialNeeds);
            XElement specialNeeds = new XElement("specialNeeds", myChild.SpecialNeeds);


            childsRoot.Add(new XElement("child", id, motherId, firstName, lastName, dateOfBirth, isSpecialNeeds, specialNeeds));
            childsRoot.Save(childsPath);
        }

        /// <summary>
        /// remove child from XML dataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveChild(int id)
        {
            XElement childElement;
            try
            {
                childElement = (from myChild in childsRoot.Elements()
                                where int.Parse(myChild.Element("id").Value) == id
                                select myChild).FirstOrDefault();
                childElement.Remove();
                childsRoot.Save(childsPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// update child in XML dataBase
        /// </summary>
        /// <param name="myChild"></param>
        public void UpdateChild(Child myChild)
        {
            XElement childElement = (from chi in childsRoot.Elements()
                                     where int.Parse(chi.Element("id").Value) == myChild.Id
                                     select chi).FirstOrDefault();

            childElement.Element("isSpecialNeeds").Value = myChild.IsSpecialNeeds.ToString();
            childElement.Element("specialNeeds").Value = myChild.SpecialNeeds;

            childsRoot.Save(childsPath);
        }

        /// <summary>
        /// returns all the childs in the XML dataBase
        /// </summary>
        /// <returns></returns>
        public List<Child> GetchildList()
        {
            LoadData();
            List<Child> childs;
            try
            {
                childs = (from myChild in childsRoot.Elements()
                          select new Child()
                          {
                              Id = int.Parse(myChild.Element("id").Value),
                              MotherId = int.Parse(myChild.Element("motherId").Value),
                              FirstName = myChild.Element("firstName").Value,
                              LastName = myChild.Element("lastName").Value,
                              DateOfBirth = DateTime.Parse(myChild.Element("dateOfBirth").Value),
                              IsSpecialNeeds = bool.Parse(myChild.Element("isSpecialNeeds").Value),
                              SpecialNeeds = myChild.Element("specialNeeds").Value
                          }).ToList();
            }
            catch
            {
                childs = null;
            }
            return childs;
        }

        #endregion

        #region nanny functions

        /// <summary>
        /// add nanny to XML dataBase
        /// </summary>
        /// <param name="myNanny"></param>
        public void Addnanny(Nanny myNanny)
        {
            XElement id = new XElement("id", myNanny.Id);
            XElement firstName = new XElement("firstName", myNanny.FirstName);
            XElement lastName = new XElement("lastName", myNanny.LastName);

            XElement dateOfBirth = new XElement("dateOfBirth", myNanny.DateOfBirth);
            XElement cellphone = new XElement("cellphone", myNanny.Cellphone);
            XElement address = new XElement("address", myNanny.Address);
            XElement elvatorExist = new XElement("elvatorExist", myNanny.ElevatorExist);
            XElement floor = new XElement("floor", myNanny.Floor);
            XElement experience = new XElement("experience", myNanny.Experience);

            XElement maxChildNumber = new XElement("maxChildNumber", myNanny.MaxChildNumber);
            XElement minChildAge = new XElement("minChildAge", myNanny.MinChildAge);
            XElement maxChildAge = new XElement("maxChildAge", myNanny.MaxChildAge);
            XElement perHourSalary = new XElement("perHourSalary", myNanny.PerHourSalary);

            XElement hourlyWage = new XElement("hourlyWage", myNanny.HourlyWage);
            XElement monthlyWage = new XElement("monthlyWage", myNanny.MonthlyWage);
            XElement workDays = new XElement("workDays", myNanny.TempArray);
            XElement schedule = new XElement("schedule", myNanny.TempMatrix);
            XElement HolidaysByTMT = new XElement("HolidaysByTMT", myNanny.HolidaysByTMT);

            XElement Recommendation = new XElement("Recommendation", myNanny.Recommendation);
            XElement NumOfContract = new XElement("NumOfContract", myNanny.NumOfContract);


            nanniesRoot.Add(new XElement("nanny", id, firstName, lastName, dateOfBirth, cellphone, address, elvatorExist, floor, experience, maxChildNumber, maxChildAge, minChildAge, perHourSalary, hourlyWage, monthlyWage, workDays, schedule, HolidaysByTMT, Recommendation, NumOfContract));
            nanniesRoot.Save(nanniesPath);
        }

        /// <summary>
        ///  remove nanny from XML dataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveNanny(int id)
        {
            XElement nannyElement;
            try
            {
                nannyElement = (from myNanny in nanniesRoot.Elements()
                                where int.Parse(myNanny.Element("id").Value) == id
                                select myNanny).FirstOrDefault();
                nannyElement.Remove();
                nanniesRoot.Save(nanniesPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///  update nanny in XML dataBase
        /// </summary>
        /// <param name="myNanny"></param>
        public void UpdateNanny(Nanny myNanny)
        {
            XElement nannyElement = (from nan in nanniesRoot.Elements()
                                     where int.Parse(nan.Element("id").Value) == myNanny.Id
                                     select nan).FirstOrDefault();

            nannyElement.Element("cellphone").Value = myNanny.Cellphone;
            nannyElement.Element("address").Value = myNanny.Address;
            nannyElement.Element("elvatorExist").Value = myNanny.ElevatorExist.ToString();

            nannyElement.Element("floor").Value = myNanny.Floor.ToString();
            nannyElement.Element("experience").Value = myNanny.Experience.ToString();
            nannyElement.Element("maxChildNumber").Value = myNanny.MaxChildNumber.ToString();
            nannyElement.Element("minChildAge").Value = myNanny.MinChildAge.ToString();
            nannyElement.Element("maxChildAge").Value = myNanny.MaxChildAge.ToString();
            nannyElement.Element("perHourSalary").Value = myNanny.PerHourSalary.ToString();

            nannyElement.Element("hourlyWage").Value = myNanny.HourlyWage.ToString();
            nannyElement.Element("monthlyWage").Value = myNanny.MonthlyWage.ToString();
            nannyElement.Element("workDays").Value = myNanny.TempArray;
            nannyElement.Element("schedule").Value = myNanny.TempMatrix;
            nannyElement.Element("HolidaysByTMT").Value = myNanny.HolidaysByTMT.ToString();
            nannyElement.Element("Recommendation").Value = myNanny.Recommendation;
            nannyElement.Element("NumOfContract").Value = myNanny.NumOfContract.ToString();

            nanniesRoot.Save(nanniesPath);

        }

        /// <summary>
        /// returns all the nannies in the XML dataBase
        /// </summary>
        /// <returns></returns>
        public List<Nanny> GetnannyList()
        {
            LoadData();
            List<Nanny> nannies;
            try
            {
                nannies = (from myNanny in nanniesRoot.Elements()
                           select new Nanny()
                           {
                               Id = int.Parse(myNanny.Element("id").Value),
                               FirstName = myNanny.Element("firstName").Value,
                               LastName = myNanny.Element("lastName").Value,
                               DateOfBirth = DateTime.Parse(myNanny.Element("dateOfBirth").Value),
                               Cellphone = myNanny.Element("cellphone").Value,
                               Address = myNanny.Element("address").Value,
                               ElevatorExist = bool.Parse(myNanny.Element("elvatorExist").Value),
                               Floor = int.Parse(myNanny.Element("floor").Value),
                               Experience = int.Parse(myNanny.Element("experience").Value),
                               MaxChildNumber = int.Parse(myNanny.Element("maxChildNumber").Value),
                               MinChildAge = int.Parse(myNanny.Element("minChildAge").Value),
                               MaxChildAge = int.Parse(myNanny.Element("maxChildAge").Value),
                               PerHourSalary = bool.Parse(myNanny.Element("perHourSalary").Value),
                               HourlyWage = int.Parse(myNanny.Element("hourlyWage").Value),
                               MonthlyWage = int.Parse(myNanny.Element("monthlyWage").Value),
                               TempArray = myNanny.Element("workDays").Value.ToString(),
                               TempMatrix = myNanny.Element("schedule").Value,
                               HolidaysByTMT = bool.Parse(myNanny.Element("HolidaysByTMT").Value),
                               Recommendation = myNanny.Element("Recommendation").Value,
                               NumOfContract = int.Parse(myNanny.Element("NumOfContract").Value)

                           }).ToList();
            }
            catch
            {
                nannies = null;
            }
            return nannies;
        }

        #endregion

        #region mother functions

        /// <summary>
        ///add mother to XML dataBase
        /// </summary>
        /// <param name="myMother"></param>
        public void AddMother(Mother myMother)
        {

            XElement id = new XElement("id", myMother.Id);
            XElement firstName = new XElement("firstName", myMother.FirstName);
            XElement lastName = new XElement("lastName", myMother.LastName);
            XElement cellphone = new XElement("cellphone", myMother.Cellphone);
            XElement address = new XElement("address", myMother.Address);
            XElement areaNanny = new XElement("areaNanny", myMother.AreaNanny);
            XElement nannyDays = new XElement("nannyDays", myMother.TempArray);
            XElement schedule = new XElement("schedule", myMother.TempMatrix);
            XElement Notes = new XElement("notes", myMother.Notes);

            mothersRoot.Add(new XElement("Mother", id, firstName, lastName, cellphone, address, areaNanny, nannyDays, schedule, Notes));
            mothersRoot.Save(mothersPath);
        }

        /// <summary>
        /// remove mother from XML dataBase
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveMother(int id)
        {
            XElement motherElement;
            try
            {
                motherElement = (from myMother in mothersRoot.Elements()
                                 where int.Parse(myMother.Element("id").Value) == id
                                 select myMother).FirstOrDefault();
                motherElement.Remove();
                mothersRoot.Save(mothersPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// update mother in XML dataBase
        /// </summary>
        /// <param name="myMother"></param>
        public void UpdateMother(Mother myMother)
        {
            XElement MotherElement = (from mother in mothersRoot.Elements()
                                      where int.Parse(mother.Element("id").Value) == myMother.Id
                                      select mother).FirstOrDefault();

            MotherElement.Element("cellphone").Value = myMother.Cellphone;
            MotherElement.Element("address").Value = myMother.Address;
            MotherElement.Element("areaNanny").Value = myMother.AreaNanny;
            MotherElement.Element("nannyDays").Value = myMother.TempArray;
            MotherElement.Element("schedule").Value = myMother.TempMatrix;
            MotherElement.Element("notes").Value = myMother.Notes;

            mothersRoot.Save(mothersPath);
        }

        /// <summary>
        /// returns all the mothers in the XML dataBase
        /// </summary>
        /// <returns></returns>
        public List<Mother> GetMotherList()
        {
            LoadData();
            List<Mother> Mothers;
            try
            {
                Mothers = (from myMother in mothersRoot.Elements()
                           select new Mother()
                           {
                               Id = int.Parse(myMother.Element("id").Value),
                               FirstName = myMother.Element("firstName").Value,
                               LastName = myMother.Element("lastName").Value,
                               Cellphone = myMother.Element("cellphone").Value,
                               Address = myMother.Element("address").Value,
                               AreaNanny = myMother.Element("areaNanny").Value,
                               TempMatrix = myMother.Element("schedule").Value,
                               TempArray = myMother.Element("nannyDays").Value,
                               Notes = myMother.Element("notes").Value

                           }).ToList();
            }
            catch
            {
                Mothers = null;
            }
            return Mothers;
        }

        #endregion

        #region contract functions

        /// <summary>
        /// add contract to XML dataBase
        /// </summary>
        /// <param name="myContract"></param>
        public void AddContract(Contract myContract)
        {
            XElement transactionNumber = new XElement("transactionNumber", myContract.TransactionNumber);
            XElement nannyId = new XElement("nannyId", myContract.NannyId);
            XElement childId = new XElement("childId", myContract.ChildId);
            XElement met = new XElement("met", myContract.Met);
            XElement contractSigned = new XElement("contractSigned", myContract.ContractSigned);
            XElement hourlyWage = new XElement("hourlyWage", myContract.HourlyWage);
            XElement monthlyWage = new XElement("monthlyWage", myContract.MonthlyWage);
            XElement conType = new XElement("conType", myContract.ConType);
            XElement contractBegin = new XElement("contractBegin", myContract.ContractBegin);
            XElement contractEnd = new XElement("contractEnd", myContract.ContractEnd);

            contractsRoot.Add(new XElement("Contract", transactionNumber, nannyId, childId, met, contractSigned, hourlyWage, monthlyWage, conType, contractBegin, contractEnd));
            contractsRoot.Save(contractsPath);
        }

        /// <summary>
        /// remove contract from XML dataBase
        /// </summary>
        /// <param name="transactionNumber"></param>
        /// <returns></returns>
        public bool RemoveContract(int transactionNumber)
        {
            XElement ContractElement;
            try
            {
                ContractElement = (from myContract in contractsRoot.Elements()
                                   where int.Parse(myContract.Element("transactionNumber").Value) == transactionNumber
                                   select myContract).FirstOrDefault();
                ContractElement.Remove();
                contractsRoot.Save(contractsPath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// update contract in XML dataBase
        /// </summary>
        /// <param name="myContract"></param>
        public void UpdateContract(Contract myContract)
        {
            XElement ContractElement = (from Contract in contractsRoot.Elements()
                                        where int.Parse(Contract.Element("transactionNumber").Value) == myContract.TransactionNumber
                                        select Contract).FirstOrDefault();

            ContractElement.Element("conType").Value = myContract.ConType.ToString();
            ContractElement.Element("met").Value = myContract.Met.ToString();
            ContractElement.Element("hourlyWage").Value = myContract.HourlyWage.ToString();
            ContractElement.Element("monthlyWage").Value = myContract.MonthlyWage.ToString();
            ContractElement.Element("contractBegin").Value = myContract.ContractBegin.ToString();
            ContractElement.Element("contractEnd").Value = myContract.ContractEnd.ToString();


            contractsRoot.Save(contractsPath);
        }

        /// <summary>
        /// returns all the contracts in the XML dataBase
        /// </summary>
        /// <returns></returns>
        public List<Contract> GetContractList()
        {
            LoadData();
            List<Contract> Contracts;
            try
            {
                Contracts = (from myContract in contractsRoot.Elements()
                             select new Contract()
                             {
                                 TransactionNumber = int.Parse(myContract.Element("transactionNumber").Value),
                                 NannyId = int.Parse(myContract.Element("nannyId").Value),
                                 ChildId = int.Parse(myContract.Element("childId").Value),
                                 Met = bool.Parse(myContract.Element("met").Value),
                                 ContractSigned = bool.Parse(myContract.Element("contractSigned").Value),
                                 HourlyWage = int.Parse(myContract.Element("hourlyWage").Value),
                                 MonthlyWage = int.Parse(myContract.Element("monthlyWage").Value),
                                 ConType = (ContractType)Enum.Parse(typeof(ContractType), myContract.Element("conType").Value),
                                 ContractBegin = DateTime.Parse(myContract.Element("contractBegin").Value),
                                 ContractEnd = DateTime.Parse(myContract.Element("contractEnd").Value)
                             }).ToList();
            }
            catch
            {
                Contracts = null;
            }
            return Contracts;

        }

        #endregion

    }
}


