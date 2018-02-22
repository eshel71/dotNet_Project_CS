using System;
using System.Collections.Generic;
using System.Linq;
using GoogleMapsApi;
using GoogleMapsApi.Entities.Directions.Request;
using GoogleMapsApi.Entities.Directions.Response;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using BE;
using System.IO;

namespace BL
{
    /// <summary>
    /// contains the implementions of all the functions (and more) in "IBL".
    /// </summary>
    public class Bl_imp : IBL
    {
        DAL.Idal myIdal = DAL.FactoryDal.GetDal(); // get the dal type

        #region Nanny function

        /// <summary>
        /// add nanny if she inder 18 years old
        /// </summary>
        /// <param name="myNanny"></param>
        public void AddNanny(Nanny myNanny)
        {
            DateTime date = myNanny.DateOfBirth;
            DateTime ourTime = DateTime.Now.AddYears(-18);
            int comp = date.CompareTo(ourTime);
            if (comp <= 0) // nanny more then 18 years
            {
                myIdal.AddNanny(myNanny);
                return;
            }
            throw new Exception("Nanny under 18 years !!");
        }

        /// <summary>
        /// delete the nanny
        /// </summary>
        /// <param name="nannyId"></param>
        public void DeleteNanny(int nannyId)
        {
            myIdal.DeleteNanny(nannyId);
        }

        /// <summary>
        /// update the nanny details by the variable myNanny
        /// </summary>
        /// <param name="myNanny"></param>
        public void UpdateNanny(Nanny myNanny)
        {
            myIdal.UpdateNanny(myNanny);
        }

        #endregion

        #region Mother function

        /// <summary>
        /// adding new mother to the database
        /// </summary>
        /// <param name="myMother"></param>
        public void AddMother(Mother myMother)
        {
            myIdal.AddMother(myMother);
        }

        /// <summary>
        /// delete the mother by her id
        /// </summary>
        /// <param name="motherId"></param>
        public void DeleteMother(int motherId)
        {
            myIdal.DeleteMother(motherId);
        }

        /// <summary>
        /// update the mother by the variable myMother
        /// </summary>
        /// <param name="myMother"></param>
        public void UpdateMother(Mother myMother)
        {
            myIdal.UpdateMother(myMother);
        }

        #endregion

        #region Child function

        /// <summary>
        /// add a child to the database
        /// </summary>
        /// <param name="myChild"></param>
        public void AddChild(Child myChild)
        {
            myIdal.AddChild(myChild);
        }

        /// <summary>
        /// delete a child from the database by id number
        /// </summary>
        /// <param name="childId"></param>
        public void DeleteChild(int childId)
        {
            myIdal.DeleteChild(childId);
        }

        /// <summary>
        /// update the child with new details from the variable myChild
        /// </summary>
        /// <param name="myChild"></param>
        public void UpdateChild(Child myChild)
        {
            myIdal.UpdateChild(myChild);
        }

        #endregion

        #region Contract functions

        /// <summary>
        /// add new contract. 
        /// conditions: 
        /// 1: the child is above 3 month old
        /// 2: the nanny not reached her max amount of children
        /// </summary>
        /// <param name="myContract"></param>
        public void AddContract(Contract myContract)
        {
            Child ch = myIdal.FindChild(myContract.ChildId);
            Nanny nan = myIdal.FindNanny(myContract.NannyId);
            Mother mom = myIdal.FindMother(ch.MotherId);

            DateTime ourTime = DateTime.Now.AddMonths(-3);
            if (ch.DateOfBirth > ourTime)
                throw new Exception("Child under 3 month");

            if (nan.MaxChildNumber == nan.NumOfContract)
                throw new Exception("nanny already reach the max amount of children ");

            int newSalary = CalculateSalary(ch, nan, mom, myContract.ConType); // gets the new salary

            if (myContract.ConType == ContractType.hourly) // if the contract required hourly wage
                myContract.HourlyWage = newSalary;
            else
                myContract.MonthlyWage = newSalary;

            myIdal.AddContract(myContract);

            nan.NumOfContract += 1; ; // updating the number of contracts
            myIdal.UpdateNanny(nan);
        }

        /// <summary>
        /// delete the contract from the database
        /// </summary>
        /// <param name="contractNumber"></param>
        public void DeleteContract(int contractNumber)
        {
            myIdal.DeleteContract(contractNumber);
        }

        /// <summary>
        /// update the contract info by the variable myContract
        /// </summary>
        /// <param name="myContract"></param>
        public void UpdateContract(Contract myContract)
        {
            myIdal.UpdateContract(myContract);
        }

        /// <summary>
        /// returns the salary for the nanny considering the amount of the child's brothers that the nanny already treat.
        /// </summary>
        /// <param name="myChild"></param>
        /// <param name="myNanny"></param>
        /// <param name="myMother"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public int CalculateSalary(Child myChild, Nanny myNanny, Mother myMother, ContractType type)
        {
            IEnumerable<Child> brothers = myIdal.GetAllChildsByMother(myMother); //all the sons of this mom
            brothers = brothers.Where(x => x.Id != myChild.Id); // the brothers without this child
            IEnumerable<Contract> conts = myIdal.GetAllContracts(x => x.NannyId == myNanny.Id); //all the contract of this nanny
            int amount = conts.Count(x => brothers.Any(y => y.Id == x.ChildId)); // counting the brothers
            int salary;

            if (type == ContractType.hourly) // the nanny get hourly wage
            {
                salary = NannyWorkHours(myNanny) * 4 * myNanny.HourlyWage;
                while (amount > 0)
                {
                    salary = (int)(salary * 0.98); // 2% per brother
                    amount--;
                }

                return salary;
            }

            // not per hour salary
            salary = myNanny.MonthlyWage;
            while (amount > 0)
            {
                salary = (int)(salary * 0.98); // 2% per brother
                amount--;
            }
            return salary;
        }

        /// <summary>
        /// returns the amount of hours that the nanny works in
        /// </summary>
        /// <param name="myNanny"></param>
        /// <returns></returns>
        public int NannyWorkHours(Nanny myNanny)
        {
            int amount = 0;

            for (int day = 0; day < 6; day++)
                if (myNanny.WorkDays[day]) // if the nanny works in this day
                    amount += myNanny.Schedule[1, day] - myNanny.Schedule[0, day];

            return amount;
        }

        #endregion

        /// <summary>
        ///  help function for matchedNanny
        ///  checks if the nanny and mother are matched
        /// </summary>
        /// <param name="myMother"></param>
        /// <param name="myNanny"></param>
        /// <returns></returns>
        public bool CheckSchedule(Mother myMother, Nanny myNanny)
        {
            for (int day = 0; day < 6; day++)
            {
                if (myMother.NannyDays[day])
                {
                    if (!myNanny.WorkDays[day]) // the nanny not work in this day
                        return false;
                    if (myMother.Schedule[0, day] < myNanny.Schedule[0, day]) // check the start time
                        return false;
                    if (myMother.Schedule[1, day] > myNanny.Schedule[1, day]) // check the end time
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// return the children that untreated
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Child> UntreatedChilds(Func<Child, bool> predicate = null)
        {
            IEnumerable<Contract> conts = myIdal.GetAllContracts();
            IEnumerable<Child> childs;
            childs = myIdal.GetAllChilds(predicate).Where(x => !conts.Any(y => y.ChildId == x.Id)); // the childs with no any contract
            return childs;
        }

        /// <summary>
        /// return the nannies that working by TMT
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Nanny> NanniesByTMT()
        {
            return myIdal.GetAllNannies(x => x.HolidaysByTMT == true);
        }

        /// <summary>
        /// return all the contracts that suitable for condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Contract> MatchedContracts(Func<Contract, bool> predicate = null)
        {
            return myIdal.GetAllContracts(predicate);
        }

        /// <summary>
        /// returns the number of the contracts that suitable for condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public int NumberMatchedContract(Func<Contract, bool> predicate = null)
        {
            if (predicate != null) // if there is a condition
                return myIdal.GetAllContracts().Count(predicate);

            return myIdal.GetAllContracts().Count();
        }

        /// <summary>
        /// returns the distance between "source" and "dest" in meters
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dest"></param>
        /// <returns></returns>
        public int CalculateDistance(string source, string dest)
        {
            var drivingDirectionRequest = new DirectionsRequest
            {
                TravelMode = TravelMode.Walking,
                Origin = source,
                Destination = dest,
            };
            DirectionsResponse drivingDirections = GoogleMaps.Directions.Query(drivingDirectionRequest);
            Route route = drivingDirections.Routes.First();
            Leg leg = route.Legs.First();
            return leg.Distance.Value;
        }

        /// <summary>
        /// returns the nannies that matched to the mother requirements
        /// </summary>
        /// <param name="myMother"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> MatchedNanny(Mother myMother)
        {
            IEnumerable<Nanny> allNan = myIdal.GetAllNannies();
            return allNan.Where(x => CheckSchedule(myMother, x)); // return the nannies that matched to the mother requires
        }

        /// <summary>
        /// returns the five best nannies that matched to the mother requirement.
        /// we decided that the priority will be for the nannies with the smallest exceeded from the mother schedule (requirement)
        /// </summary>
        /// <param name="myMother"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> FiveBestMatched(Mother myMother)
        {
            IEnumerable<Nanny> result = myIdal.GetAllNannies();

            result = from item in result
                     orderby CalculateGap(myMother, item)
                     select item;

            return result.Take(5); // returns the five best matched
        }

        /// <summary>
        /// returns the nannies that close enough to the mother address
        /// her address is default if she didn't mentioned nanny address.
        /// we assume that "close enough" means 10 km
        /// </summary>
        /// <param name="myMother"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> NanniesByAddress(Mother myMother)
        {
            string motherAddress = (myMother.AreaNanny != "" ? myMother.AreaNanny : myMother.Address); // default address is the mother address (instead of the nanny area)
            var result = MatchedNanny(myMother);

            const int MAX_DISTANCE = 10 * 1000; // the maximum distance up to 10 km
            List<Nanny> newResult = new List<Nanny>();
            int dis = -1;
            Thread myThread;
            foreach (Nanny myNanny in result)
            {
                myThread = new Thread(() => dis = CalculateDistance(motherAddress, myNanny.Address));
                myThread.Start();

                if (dis < MAX_DISTANCE)
                    newResult.Add(myNanny);
            }

            return newResult;
        }

        /// <summary>
        /// returns all the mothers by a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Mother> MothersByPredicate(Func<Mother, bool> predicate = null)
        {
            return myIdal.GetAllMothers(predicate);
        }

        /// <summary>
        /// returns all the childs by a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Child> ChildsByPredicate(Func<Child, bool> predicate = null)
        {
            return myIdal.GetAllChilds(predicate);
        }

        /// <summary>
        /// returns all the nannies by a predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public IEnumerable<Nanny> NanniesByPredicate(Func<Nanny, bool> predicate = null)
        {
            return myIdal.GetAllNannies(predicate);
        }

        /// <summary>
        /// returns groups of the nannies by key. the key is one year for every group
        /// we got an option of return sorted groups or not 
        /// </summary>
        /// <param name="byorder"></param>
        /// <param name="groupByMinAge"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Nanny>> GroupNannyByAge(bool byorder = false, bool groupByMinAge = false) // default: non ordered groups , grouped by max age
        {
            IEnumerable<IGrouping<int, Nanny>> result;
            if (groupByMinAge) // groups the nannies by min' age
            {
                result = from item in myIdal.GetAllNannies()
                         group item by item.MinChildAge / 12; // 12 months = 1 year
            }
            else // groups by max' age
            {
                result = from item in myIdal.GetAllNannies()
                         group item by item.MaxChildAge / 12;
            }

            if (byorder) //  have to sort
            {
                result = from item in result
                         orderby item.Key
                         select item;
            }
            return result;
        }

        /// <summary>
        /// returns groups of the contracts by key. the key is ten meters for every group
        /// we got an option of return sorted groups by the key or not 
        /// </summary>
        /// <param name="byorder"></param>
        /// <returns></returns>
        public IEnumerable<IGrouping<int, Contract>> GroupContractByDistance(bool byorder = false) // default: non ordered groups 
        {
            IEnumerable<IGrouping<int, Contract>> result;
            result = from item in myIdal.GetAllContracts()
                     group item by GetDistanceOfContract(item) / 10;

            if (byorder) //  have to sort
            {
                result = from item in result
                         orderby item.Key
                         select item;
            }
            return result;
        }

        /// <summary>
        /// help function for "groupContractByDistance".
        /// this function returns the distance in meters between the mother and the nanny
        /// </summary>
        /// <param name="myC"></param>
        /// <returns></returns>
        public int GetDistanceOfContract(Contract myCon)
        {
            Nanny n = myIdal.FindNanny(myCon.NannyId);
            Child c = myIdal.FindChild(myCon.ChildId);
            Mother m = myIdal.FindMother(c.MotherId);

            return CalculateDistance(n.Address, m.Address);
        }

        /// <summary>
        /// help function for FiveBestMatched
        /// returns the amount of hours that the nanny exceeded from the mother requirement
        /// </summary>
        /// <param name="myMother"></param>
        /// <param name="myNanny"></param>
        /// <returns></returns>
        public int CalculateGap(Mother myMother, Nanny myNanny)
        {
            int gap = 0;
            for (int day = 0; day < 6; day++)
            {
                if (myMother.NannyDays[day])
                {
                    if (!myNanny.WorkDays[day]) // whole day gap (the nanny dont work in this day)
                        gap += myMother.Schedule[1, day] - myMother.Schedule[0, day];
                    else
                    {
                        if (myMother.Schedule[0, day] < myNanny.Schedule[0, day]) // check the start time
                            gap += myNanny.Schedule[0, day] - myMother.Schedule[0, day]; // adding the gap: nanny startTime - mother startTime

                        if (myMother.Schedule[1, day] > myNanny.Schedule[1, day]) // check the end time
                            gap += myMother.Schedule[1, day] - myNanny.Schedule[1, day]; // mother endTime - nanny endTime
                    }
                }
            }
            return gap;
        }

        /// <summary>
        /// insert the initial data for using
        /// </summary>
        public void InsertData()
        {
            if (DAL.Dal_XML_imp.DataExists)
                return;
            ((DAL.Dal_imp)myIdal).myXML.ChangeDataStatus(); // change the status in our xml

            Nanny myNanny = new Nanny
            {
                Id = 118190410,
                LastName = "Dola",
                FirstName = "Shifra",
                DateOfBirth = new DateTime(1984, 12, 2),
                Cellphone = "052-6780961",
                Address = "Gedera Shimon 15",
                ElevatorExist = false,
                Floor = 5,
                Experience = 5,
                HolidaysByTMT = false,
                HourlyWage = 40,
                MaxChildAge = 15,
                MaxChildNumber = 10,
                MinChildAge = 5,
                MonthlyWage = 3000,
                PerHourSalary = true,
                Recommendation = "works in the united states",
                Schedule = new int[2, 6] { { 9, 9, 9, 9, 9, 9 }, { 15, 15, 15, 15, 15, 15 } },
                WorkDays = new bool[7] { true, true, true, true, true, true, false }
            }; AddNanny(myNanny);
            Nanny myNanny2 = new Nanny
            {
                Id = 218199410,
                LastName = "Lim",
                FirstName = "Lima",
                DateOfBirth = new DateTime(1995, 12, 2),
                Cellphone = "052-6340961",
                Address = "Gedera Efraim 10",
                ElevatorExist = true,
                Floor = 0,
                Experience = 0,
                HolidaysByTMT = true,
                HourlyWage = 30,
                MaxChildAge = 18,
                MaxChildNumber = 5,
                MinChildAge = 5,
                MonthlyWage = 3000,
                PerHourSalary = true,
                Recommendation = "worked 2 years with special kids",
                Schedule = new int[2, 6] { { 9, 9, 9, 9, 9, 9 }, { 15, 15, 15, 15, 15, 15 } },
                WorkDays = new bool[7] { false, true, true, true, true, true, false }
            }; AddNanny(myNanny2);
            Nanny myNanny3 = new Nanny
            {
                Id = 315199410,
                LastName = "Din",
                FirstName = "Dina",
                DateOfBirth = new DateTime(1995, 8, 2),
                Cellphone = "052-634877",
                Address = "HaUman Street Jerusalem 15",
                ElevatorExist = true,
                Floor = 2,
                Experience = 5,
                HolidaysByTMT = true,
                HourlyWage = 35,
                MaxChildAge = 15,
                MaxChildNumber = 7,
                MinChildAge = 3,
                MonthlyWage = 3050,
                PerHourSalary = true,
                Recommendation = "works in the united kinkdom",
                Schedule = new int[2, 6] { { 9, 9, 9, 9, 9, 9 }, { 15, 15, 19, 15, 15, 15 } },
                WorkDays = new bool[7] { true, false, true, true, true, true, false }
            }; AddNanny(myNanny3);
            Nanny myNanny4 = new Nanny
            {
                Id = 418199450,
                LastName = "Alfasi",
                FirstName = "Zippi",
                DateOfBirth = new DateTime(1995, 12, 2),
                Cellphone = "052-6340961",
                Address = "kiryat shmona",
                ElevatorExist = true,
                Floor = 3,
                Experience = 3,
                HolidaysByTMT = true,
                HourlyWage = 60,
                MaxChildAge = 13,
                MaxChildNumber = 2,
                MinChildAge = 2,
                MonthlyWage = 3000,
                PerHourSalary = false,
                Recommendation = "works in the united states",
                Schedule = new int[2, 6] { { 9, 9, 9, 9, 9, 9 }, { 15, 15, 15, 15, 15, 15 } },
                WorkDays = new bool[7] { true, true, true, true, true, true, false }
            }; AddNanny(myNanny4);
            Nanny myNanny5 = new Nanny
            {
                Id = 518139410,
                LastName = "Sasson",
                FirstName = "Yafa",
                DateOfBirth = new DateTime(1995, 12, 2),
                Cellphone = "052-6340961",
                Address = "Rehovot Yakov 3",
                ElevatorExist = true,
                Floor = 1,
                Experience = 5,
                HolidaysByTMT = false,
                HourlyWage = 50,
                MaxChildAge = 20,
                MaxChildNumber = 5,
                MinChildAge = 2,
                MonthlyWage = 3500,
                PerHourSalary = true,
                Recommendation = "very responsible person",
                Schedule = new int[2, 6] { { 11, 9, 9, 9, 9, 9 }, { 15, 15, 15, 15, 15, 15 } },
                WorkDays = new bool[7] { true, true, true, true, true, true, false }
            }; AddNanny(myNanny5);
            Nanny myNanny6 = new Nanny
            {
                Id = 618185410,
                LastName = "Arela",
                FirstName = "Saud",
                DateOfBirth = new DateTime(1995, 12, 2),
                Cellphone = "052-6340961",
                Address = "Gedera Shapira 15",
                ElevatorExist = true,
                Floor = 0,
                Experience = 10,
                HolidaysByTMT = true,
                HourlyWage = 30,
                MaxChildAge = 9,
                MaxChildNumber = 3,
                MinChildAge = 6,
                MonthlyWage = 4000,
                PerHourSalary = false,
                Recommendation = "",
                Schedule = new int[2, 6] { { 9, 9, 9, 9, 9, 9 }, { 15, 15, 15, 15, 15, 14 } },
                WorkDays = new bool[7] { true, true, true, true, true, true, false }
            }; AddNanny(myNanny6);

            Mother myMother = new Mother
            {
                Id = 318190411,
                LastName = "Shaoli",
                FirstName = "Shula",
                Cellphone = "052-45459854",
                Address = "Six Days 12  Mevasseret Zion",
                AreaNanny = "Jerusalem",
                NannyDays = new bool[7] { true, true, true, true, true, true, false },
                Schedule = new int[2, 6] { { 10, 10, 10, 10, 10, 10 }, { 12, 13, 12, 12, 15, 12 } },
                Notes = "be happy!"
            }; AddMother(myMother);
            Mother myMother2 = new Mother
            {
                Id = 418190411,
                LastName = "Kola",
                FirstName = "Koka",
                Cellphone = "052-45779854",
                Address = "Gedera Levi 20",
                AreaNanny = "Gedera",
                NannyDays = new bool[7] { true, false, true, true, false, true, false },
                Schedule = new int[2, 6] { { 11, 10, 10, 10, 11, 10 }, { 12, 12, 12, 14, 12, 12 } },
                Notes = "have fun!"
            }; AddMother(myMother2);

            Child myChild = new Child()
            {
                Id = 318190410,
                MotherId = 318190411,
                FirstName = "Ori",
                DateOfBirth = new DateTime(2005, 5, 3),
                IsSpecialNeeds = false,
                SpecialNeeds = ""
            };
            AddChild(myChild);
            Child myChild2 = new Child
            {
                Id = 418190410,
                MotherId = 318190411,
                FirstName = "Oriel",
                DateOfBirth = new DateTime(2004, 5, 3),
                IsSpecialNeeds = true,
                SpecialNeeds = "wheels chair"
            };
            AddChild(myChild2);

            Child myChild3 = new Child
            {
                Id = 338190410,
                MotherId = 418190411,
                FirstName = "Alon",
                DateOfBirth = new DateTime(2005, 6, 15),
                IsSpecialNeeds = false,
                SpecialNeeds = ""
            };
            AddChild(myChild3);

            Child myChild4 = new Child
            {
                Id = 222190410,
                MotherId = 418190411,
                FirstName = "Yochai",
                DateOfBirth = new DateTime(2000, 6, 15),
                IsSpecialNeeds = false,
                SpecialNeeds = "hello world"
            };
            AddChild(myChild4);

            Contract myContract = new Contract
            {
                NannyId = 218199410,
                ChildId = 338190410,
                Met = true,
                ContractSigned = false,
                HourlyWage = 0,
                MonthlyWage = 0,
                ConType = ContractType.hourly,
                ContractBegin = new DateTime(2014, 5, 6),
                ContractEnd = new DateTime(2015, 5, 6)
            }; AddContract(myContract);
            Contract myContract2 = new Contract
            {
                NannyId = 118190410,
                ChildId = 318190410,
                Met = false,
                ContractSigned = true,
                HourlyWage = 0,
                MonthlyWage = 0,
                ConType = ContractType.monthly,
                ContractBegin = new DateTime(2015, 1, 6),
                ContractEnd = new DateTime(2015, 5, 6)
            }; AddContract(myContract2);
            Contract myContract3 = new Contract
            {
                NannyId = 118190410,
                ChildId = 418190410,
                Met = false,
                ContractSigned = true,
                HourlyWage = 0,
                MonthlyWage = 0,
                ConType = ContractType.monthly,
                ContractBegin = new DateTime(2015, 1, 6),
                ContractEnd = new DateTime(2015, 5, 6)
            }; AddContract(myContract3);


        }
    }

}
