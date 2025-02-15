using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // For Debugging Purposes
            //// Testing Car Entity
            //Car c = new Car("Sedan", "Diesel", "BMW", "23QWR", 2002, "Manual", "123TR", 1500.50, 5);
            ////c.DisplayInfo();
            ////c.UpdateStatus("Maintenance");
            ////c.DisplayInfo();

            //// Testing Customer Entity
            //Customer p1 = new Customer("Bob", "09981806721", "bob123@gmail.com", "Tacloban", "123 - 456 - 789");
            //Customer p2 = new Customer("Sarah", "09102629704", "sarah1456@gmail.com", "Dagami", "345-678-123");
            //p1.DisplayInformation();
            ////p2.DisplayCustomerInfo();

            //Rent rent = new Rent(p1, c, DateTime.Today, 2);
            //Payment p = new Payment(1500.50 * 2, rent, "Cash");
            ////p.Receipt(c, 2);

            //Reservation res = new Reservation(p1, c, DateTime.Now, 2);
            //res.ConfirmedReservation();
            //res.DisplayReservation();

            //Employee emp = new Employee("Bob", "09981806721", "bob123@gmail.com", "Tacloban", "Admin", DateTime.Today);
            //emp.DisplayInformation();

            // UI
            Admin admin = new Admin("Admin Name", "Administrator", "1234567890", "admin@email.com", "Admin Address", DateTime.Now, "admin");
            admin.createCars();
            admin.createCustomer();

            int selection;
            do
            {
                Console.Clear();
                Console.Write("======= MC ORMOC CAR RENTAL =======\n" +
                "1. Customer\n" +
                "2. Admin\n" +
                "3. Exit\n" +
                "Selection: ");

                if (int.TryParse(Console.ReadLine(), out selection))
                {
                    switch (selection)
                    {
                        case 1:
                            CustomerPrompt(admin);
                            break;
                        case 2:
                            AdminPrompt(admin);
                            break;
                        case 3:
                            Console.WriteLine("Exiting... Thank you for using MC Ormoc Car Rental!");
                            break;
                        default:
                            Console.WriteLine("Invalid selection! Please choose between 1 and 3.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
            while (selection != 3);
            
            
        }
        static void CustomerPrompt(Admin admin)
        {
            Console.Clear();
            Console.WriteLine("======== CUSTOMER LOGIN ========");

            // Prompt the user for Customer ID to log in
            admin.ViewCustomers();
            Console.WriteLine("Are You Here?\n");
            Console.Write("Enter Customer ID: ");
            int customerIdInput;
            bool isValidId = int.TryParse(Console.ReadLine(), out customerIdInput);

            // Use isExistingCustomer method to check if the customer exists
            if (isValidId && admin.isExistingCustomer(customerIdInput))
            {
                // If the customer exists, show the customer menu
                Customer existingCustomer = admin.GetCustomerList().FirstOrDefault(c => c.CustomerID == customerIdInput);
                Console.WriteLine($"\nWelcome back, {existingCustomer.Name}!\n");

                Console.Clear();
                Console.WriteLine("======== CUSTOMER MENU ========\n");
                Console.WriteLine("1. View Available Cars");
                Console.WriteLine("2. Rent a Car");
                Console.WriteLine("3. Reserve a Car");
                Console.WriteLine("4. Return to Main Menu");
                Console.Write("Selection: ");

                int customerChoice;
                if (int.TryParse(Console.ReadLine(), out customerChoice))
                {
                    switch (customerChoice)
                    {
                        case 1:
                            admin.ViewAvailableCars();
                            break;
                        case 2:
                            admin.RentCar();
                            break;
                        case 3:
                            admin.ReserveCar();
                            break;
                        case 4:
                            return;
                        default:
                            Console.WriteLine("Invalid selection! Please choose between 1 and 4.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                }
            }
            else
            {
                // If the customer does not exist, prompt them to add a new customer
                Console.WriteLine("\nCustomer ID not found. Would you like to register as a new customer? (y/n): ");
                string response = Console.ReadLine().ToLower();

                if (response == "y")
                {
                    admin.AddCustomer();  
                    Console.WriteLine("\nNew customer added successfully!");
                }
                else
                {
                    Console.WriteLine("\nReturning to main menu...");
                    return;
                }
            }
        }


        static void AdminPrompt(Admin admin)
        {
            Console.Clear();
            Console.Write("Input Admin Code: ");
            string pass = Console.ReadLine();

            if (pass != admin.AdminCode)
            {
                Console.WriteLine("Incorrect Admin Code! Access denied.");
                return; 
            }

            int adminChoice;
            do
            {
                Console.Clear();
                Console.WriteLine("======== ADMIN MENU ========");
                Console.WriteLine("1. Add a New Car");
                Console.WriteLine("2. View All Cars");
                Console.WriteLine("3. Change Car Status");
                Console.WriteLine("4. View Customers");  
                Console.WriteLine("5. Return to Main Menu");
                Console.Write("Selection: ");

                if (!int.TryParse(Console.ReadLine(), out adminChoice))
                {
                    Console.WriteLine("Invalid input! Please enter a number.");
                    continue;
                }

                switch (adminChoice)
                {
                    case 1:
                        admin.AddNewCar();
                        break;
                    case 2:
                        admin.ViewAllCars();
                        break;
                    case 3:
                        admin.ChangeCarStatus();
                        break;
                    case 4:
                        admin.ViewCustomers(); 
                        break;
                    case 5:
                        Console.WriteLine("Returning to Main Menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid selection! Please choose between 1- 5.");
                        break;
                }

                if (adminChoice != 5)
                {
                    Console.WriteLine("\nPress any key to return to the Admin Menu...");
                    Console.ReadKey();
                }
            } while (adminChoice != 5);
        }
    }
}
