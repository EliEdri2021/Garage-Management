using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Ex03.ConsoleUI
{
    using Ex03.GarageLogic;

    // $G$ RUL-005 (-20) Wrong zip folder structure, the zip file should contain a single folder.
    // $G$ DSN-999 (-5) The program class should only contain the Main method, any other method which comunicate
    // with the user, suppose to be under different class - for example: UserInterface.
    public class Program
    {
        public static void Main()
        {
            Garage currentGarage = new Garage();
            while(true)
            {
                getAndLaunchUserSelction(ref currentGarage);
            }
        }

        private static void getAndLaunchUserSelction(ref Garage i_Garage)
        {
            Console.WriteLine(
                @"Garage Menu
--== Please Select An Option:
    1) Add New Car To Garage
    2) See All Veichle Registations Numbers (can be filtered)
    3) Change Veichle Status
    4) Fill Veichle Tires To Max
    5) Full Up Car
    6) Charge Up Car
    7) Get Full Car Info
Enter only the option number!");


            string userInput = Console.ReadLine();

            switch(userInput)
            {
                case "1":
                    createVeichle(ref i_Garage);
                    break;
                case "2":
                    showAllRegistationNumbers(ref i_Garage);
                    break;
                case "3":
                    break;
                case "4":
                    fillACarTiresToMax(ref i_Garage);
                    break;
                case "5":
                    fillCarTank(ref i_Garage);
                    break;
                case "6":
                    fillCarBattery(ref i_Garage);
                    break;
                case "7":
                    printCarFullInfo(ref i_Garage);
                    break;
                default:
                    Console.WriteLine("INPUT NOT CORRECT!");
                    break;
            }
        }

        private static void printCarFullInfo(ref Garage i_Garage)
        {
            string registation = getCarRegistatioNumber(ref i_Garage);
            try
            {
                Vehicle toPrint = i_Garage.GetVehicle(registation);
                Console.WriteLine(toPrint.ToString());
            }
            catch(ArgumentException)
            {
                Console.WriteLine("car not found!");
            }
        }

        // $G$ DSN-999 (-2) This method is not in use.
        private static void vehicleStatusUpdate(ref Garage i_Garage)
        {
            string carRegistation = getCarRegistatioNumber(ref i_Garage);
            RepairStatusOptions newStatus = getRepairStauts(null);
            i_Garage.VehicleStatusUpdate(carRegistation, newStatus);
        }

        private static void fillCarTank(ref Garage i_Garage)
        {
            Console.WriteLine("What Car To Fill");
            string carRegistation = getCarRegistatioNumber(ref i_Garage);

            Console.WriteLine("What Fuel Type To Use: ");
            FuelTypeOptions? fuelTypeToUse = getFuelType();

            Console.WriteLine("How Much fuel to put in:");
            float FuelAmount = getFloatInput();

            try
            {
                bool result = i_Garage.fillTank(carRegistation, FuelAmount, fuelTypeToUse.GetValueOrDefault());
                if(result)
                {
                    Console.WriteLine("Sucsessfully Filed Up Car");
                }
                else
                {
                    Console.WriteLine("Car Is Not An EV!");
                }
            }
            catch(ArgumentException)
            {
                Console.WriteLine("FAILED! FUEL TYPE NOT CORRECt");
            }
            catch(ValueOutOfRangeException curretnException)
            {
                Console.WriteLine("FAILED! Fuel Amount Not Correct possible rnage {0} - {1}", curretnException.getMin(), curretnException.getMax());
            }
        }

        private static void fillCarBattery(ref Garage i_Garage)
        {
            string carRegistation = getCarRegistatioNumber(ref i_Garage);
            bool sucsses = false;

            float howMuchToCharge = getFloatInput();

            try
            {
                sucsses = i_Garage.ChargeEV(carRegistation, howMuchToCharge);
            }
            catch (ValueOutOfRangeException exception)
            {
                Console.WriteLine("The Value You Entered Is Out Of Range! Possible Range {0} - {1}. Try Again:", exception.getMax(), exception.getMin());
                howMuchToCharge = getFloatInput();
            }

            Console.WriteLine(sucsses ? "Charged Sucssesfully" : "This is not an EV!");
        }

        private static void fillACarTiresToMax(ref Garage i_Garage)
        {
            string carRegistation = getCarRegistatioNumber(ref i_Garage);
            i_Garage.FillCarTires(carRegistation);
        }

        private static void showAllRegistationNumbers(ref Garage i_Garage)
        {
            RepairStatusOptions? repairStatus = null;

            bool succsesfulyParsed = false;

            while(!succsesfulyParsed)
            {
                Console.WriteLine("Please Enter The Repair Status You're Intrested In, Or Enter 'NO'.");
                
                string userInput = Console.ReadLine();

                if(userInput == "NO")
                {
                    succsesfulyParsed = true;
                }
                else
                {
                    repairStatus = getRepairStauts(userInput);
                }
            }

            List<Vehicle> relevantVehicles = i_Garage.GetAllVeichles(repairStatus);

            Console.WriteLine("The Relevant Vehicles To Your Qurey Are:");

            foreach(Vehicle currentVehicle in relevantVehicles)
            {
                Console.WriteLine(currentVehicle.getVeichleRegistation());
            }

            Console.WriteLine("- End Of List");
        }

        // $G$ DSN-001 (-20) The UI must not know specific types and their properties concretely!
        // It means that when adding a new type you'll have to change the code here too!
        // $G$ DSN-007 (-3) This method is too long, should be divided into several methods.
        private static void createVeichle(ref Garage i_Garage)
        {
            Console.Write("Please input the car model name:");
            string carName = Console.ReadLine();

            Console.Write("Please input the car's registation:");
            string carRegistation = Console.ReadLine();

            if(i_Garage.IsCarExist(carRegistation))
            {
                Console.WriteLine("CAR WITH THAT REGISTATION NUMBER ALREADY EXISTS.");
                throw new ArgumentException("Not Possible To Create two Cars With The Same Registration Number.");
            }

            Vehicle result = ObjectCreator.CreateVehicle(carName, carRegistation);

            string chosenVeihcleTypeName = getVeihcleTypeName();
            Type chosenVeihcleType = ObjectCreator.m_SpecificsNameToType[chosenVeihcleTypeName];

            addTiresToVehicle(chosenVeihcleType, ref result);

            string proplsionType = getVehicleProplsionType();

            if(proplsionType.Equals("EV"))
            {
                Console.WriteLine("Please Enter The Current Battery Hours");
                float currentBatteryHours = getFloatInput();

                ObjectCreator.AddEvAsEnergySolution(currentBatteryHours, chosenVeihcleType, ref result);
            }
            else
            {
                Console.WriteLine("Please Enter The Current Litters in tank");
                float currentTankLitters = getFloatInput();

                ObjectCreator.AddChemicalEnergySolution(currentTankLitters, chosenVeihcleType, ref result);
            }

            Console.Write("Please Enter Owner's Name:");
            string ownersName = Console.ReadLine();

            Console.Write("Please Enter Owner's Phone Number:");
            string ownersPhone = Console.ReadLine();

            ObjectCreator.AddOwnerToVehicle(ownersName, ownersPhone, ref result);

            Console.Write(@"Please enter veichle specifics 
(For Truck: enter is the cargo cooled (true/false) [press return] enter presicion numer with dot resembeling it's max usable weight
For Car: enter it's body door configuration (Two/Three/Four/Five) [press return] enter the car color (White/Grey/Black/Blue)
For Bike: enter the engine displacment in cc [press reutrn] renter the required lisence type (A / AA / B / BB))");
            List<string> vehicleParameters = getStringListInputByParameterNames(2);
            ObjectCreator.AddSpecificsInformation(vehicleParameters, chosenVeihcleTypeName, ref result);

            i_Garage.AdmitVehicle(result);

            Console.WriteLine(@"
Sucssesfully Added New Vehicle!");

        }


        private static RepairStatusOptions getRepairStauts (string i_possibleInput)
        {
            if(i_possibleInput == null)
            {
                i_possibleInput = Console.ReadLine();
            }

            RepairStatusOptions? repairStatus = null;
            bool succsesfulyParsed = false;

            while (!succsesfulyParsed)
            {
                try
                {
                    repairStatus = (RepairStatusOptions)Enum.Parse(typeof(RepairStatusOptions), i_possibleInput);
                    succsesfulyParsed = true;
                }
                catch(ArgumentException argumentException)
                {
                    Console.WriteLine("What a bummer! I didn't get that. :_( let's try again.");
                    i_possibleInput = Console.ReadLine();
                }
            }

            return repairStatus.GetValueOrDefault();
        }

        private static FuelTypeOptions? getFuelType()
        {

            FuelTypeOptions? result = null;
            bool succsesfulyParsed = false;

            string userInput = Console.ReadLine();
            
            while (!succsesfulyParsed)
            {
                try
                {
                    result = (FuelTypeOptions)Enum.Parse(typeof(FuelTypeOptions), userInput);
                    succsesfulyParsed = true;
                }
                catch (ArgumentException argumentException)
                {
                    Console.WriteLine("Ummmmmmm... yeah... I'm not familiar with that kind of fuel. Let's try again:");
                    userInput = Console.ReadLine();
                }
            }

            return result;
        }

        private static string getCarRegistatioNumber(ref Garage i_Garage)
        {
            bool succses = false;
            string userInput = Console.ReadLine();

            while(!succses)
            {
                if(i_Garage.IsCarExist(userInput))
                {
                    succses = true;
                }
                else
                {
                    userInput = Console.ReadLine();
                }
            }

            return userInput;
        }


        private static void addTiresToVehicle(Type i_ChosenVeihcleType, ref Vehicle result)
        {
            bool sucsses = false;

            int tireCount = ObjectCreator.m_TyreAmountByType[i_ChosenVeihcleType];

            while(!sucsses)
            {
                Console.WriteLine("Please Enter Tire Brands:");
                List<string> tireBrands = getStringListInputByParameterNames(tireCount);

                Console.WriteLine("Please Enter Tire Pressures:");
                List<float> tirePressures = getFloatListInputByParameterNames(tireCount);

                try
                {
                    ObjectCreator.AddTiresToVehicle(tireBrands, tirePressures, i_ChosenVeihcleType, ref result);
                    sucsses = true;
                }
                catch(ArgumentException caughtException)
                {
                    Console.WriteLine("Oh No! I didn't get that! Let's try that again.");
                }
            }
        }


        private static List<string> getStringListInputByParameterNames(int i_WantedLength)
        {
            List<string> result = new List<string>();

            for(int index = 0; index < i_WantedLength; index++)
            {
                Console.Write("Please enter value number {0}:", index);
                result.Add(Console.ReadLine());
            }

            return result;
        }

        private static List<float> getFloatListInputByParameterNames(int i_WantedLength)
        {
            List<float> result = new List<float>();

            for (int index = 0; index < i_WantedLength; index++)
            {
                Console.Write("Please enter value for parameter {0}:", index);
                result.Add(getFloatInput());
            }

            return result;
        }

        private static float getFloatInput()
        {
            float result;
            string userInput = Console.ReadLine();

            while(!float.TryParse(userInput, out result))
            {
                Console.Write("Woah! That wasn't a X.Y precision number - please try again:");
                userInput = Console.ReadLine();
            }

            return result;
        }

        private static string getVeihcleTypeName()
        {
            Console.Write("Please Input Veihcle Type Name:");
            string userInput = Console.ReadLine();

            while(!ObjectCreator.m_PossibleTypes.ContainsKey(userInput))
            {
                Console.Write("Woah! I'm not familiar with that type! let's try again:");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private static string getVehicleProplsionType()
        {
            Console.Write("Please Input Veihcle Propolsion Type:");
            string userInput = Console.ReadLine();

            while(!ObjectCreator.m_VeichleEnergy.ContainsKey(userInput))
            {
                Console.Write("Woah! We don't know that tech, possible options are EV / Regular");
                userInput = Console.ReadLine();
            }

            return userInput;
        }
    }
}
