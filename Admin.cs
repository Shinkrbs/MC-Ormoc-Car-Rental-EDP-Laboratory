using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Admin 
    {
        private static List<Car> CarList = new List<Car>(); // Car List
        private static List<Customer> CustomerList = new List<Customer>(); // Customer List
        public Customer CurrentCustomer { get; set; } //  Current Logged-In User
        public string AdminCode { get; set; }

        public Admin(string name, string position, string phoneNumber, string email, string address, DateTime hireDate, string adminCode)
        {
            this.AdminCode = "admin";
        }

        // Allows admin to add a new car
        public void AddNewCar()
        {
            Console.Clear();
            Console.WriteLine("======== ADD A NEW CAR ========");

            Console.Write("Enter Car Type: ");
            string carType = Console.ReadLine();

            Console.Write("Enter Fuel Type: ");
            string fuelType = Console.ReadLine();

            Console.Write("Enter Brand: ");
            string brand = Console.ReadLine();

            Console.Write("Enter Model: ");
            string model = Console.ReadLine();

            Console.Write("Enter Year: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Enter Transmission (Manual/Automatic): ");
            string transmissionInput = Console.ReadLine();

            Console.Write("Enter Plate Number: ");
            string plate = Console.ReadLine();

            Console.Write("Enter Rental Price: ");
            double price = double.Parse(Console.ReadLine());

            Console.Write("Enter Capacity: ");
            int capacity = int.Parse(Console.ReadLine());

            // Convert input string to Transmission enum
            Transmission transmission;
            while (!Enum.TryParse(transmissionInput, true, out transmission) || !Enum.IsDefined(typeof(Transmission), transmission))
            {
                Console.Write("Invalid input. Enter Transmission (Manual/Automatic): ");
                transmissionInput = Console.ReadLine();
            }

            Car newCar = new Car(carType, fuelType, brand, model, year, transmission, plate, price, capacity);
            CarList.Add(newCar);

            Console.WriteLine("Car added successfully!");
        }

        public void ViewAvailableCars()
        {
            Console.Clear();
            Console.WriteLine("======== AVAILABLE CARS ========");
            foreach (var car in CarList)
            {
                if (car.status == Car_Status.Available)  
                    car.DisplayInfo();
            }
        }

        // Allows a customer to rent a car
        public void RentCar()
        {
            Console.Clear();
            ViewAllCars();
            Console.WriteLine("======== RENT A CAR ========");
            Console.Write("Enter Car ID to Rent: ");
            int carId;
            if (int.TryParse(Console.ReadLine(), out carId))
            {
                Car selectedCar = CarList.Find(c => c.ID == carId);
                if (selectedCar != null && selectedCar.status == Car_Status.Available)
                {
                    Console.Write("Enter Number of Rental Days: ");
                    int days;
                    if (int.TryParse(Console.ReadLine(), out days) && days > 0)
                    {
                        double totalCost = selectedCar.RentalPrice * days;
                        Console.WriteLine($"Total Cost: {totalCost}");

                        // Ask the user for the payment method
                        Console.WriteLine("Choose Payment Method: ");
                        Console.WriteLine("1. Cash");
                        Console.WriteLine("2. Credit Card");
                        Console.Write("Select payment method (1 or 2): ");
                        string paymentMethod = Console.ReadLine() == "1" ? "Cash" : "Credit Card";

                        // Create the Rent object to pass to the Payment class
                        Rent rent = new Rent(CurrentCustomer, selectedCar, DateTime.Now, days);

                        // Create the Payment object
                        Payment payment = new Payment(totalCost, rent, paymentMethod);

                        // Call the Receipt method to process the payment and display the receipt
                        payment.Receipt(selectedCar, days);

                        // After the payment is successful, update the car's status
                        if (payment.GetStatus(selectedCar, days) == Payment_Status.Pending)
                        {
                            selectedCar.UpdateStatus(Car_Status.Rented);
                            Console.WriteLine("Car rented successfully!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid number of days!");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Car ID or Car is not available!");
                }
            }
            else
            {
                Console.WriteLine("Invalid input!");
            }
        }

        // Displays all cars (including rented)
        public void ViewAllCars()
        {
            Console.Clear();
            Console.WriteLine("======== ALL CARS ========");
            foreach (var car in CarList)
            {
                car.DisplayInfo();
            }
        }

        public void ChangeCarStatus()
        {
            Console.Clear();
            ViewAllCars(); 

            Console.WriteLine("Enter Car ID: ");

            int carID;
            if (int.TryParse(Console.ReadLine(), out carID)) 
            {
                // Find the car with the given ID in the CarList
                Car carToUpdate = CarList.FirstOrDefault(car => car.ID == carID);

                if (carToUpdate != null)
                {
                    Console.WriteLine("Enter new status (1 - Available, 2 - Rented, 3 - Maintenance): ");

                    int statusChoice;
                    if (int.TryParse(Console.ReadLine(), out statusChoice))
                    {
                        switch (statusChoice)
                        {
                            case 1:
                                carToUpdate.status = Car_Status.Available;
                                Console.WriteLine("Car status updated to Available.");
                                break;
                            case 2:
                                carToUpdate.status = Car_Status.Rented;
                                Console.WriteLine("Car status updated to Rented.");
                                break;
                            case 3:
                                carToUpdate.status = Car_Status.Maintenance;
                                Console.WriteLine("Car status updated to Maintenance.");
                                break;
                            default:
                                Console.WriteLine("Invalid status choice.");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for status.");
                    }
                }
                else
                {
                    Console.WriteLine("Car not found with the provided ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Car ID.");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }

        public void ReserveCar()
        {
            Console.Clear();
            Console.WriteLine("======== RESERVE A CAR ========");

            ViewAvailableCars();
            Console.Write("Enter the Car ID to reserve: ");
            if (int.TryParse(Console.ReadLine(), out int carID))
            {
                // Find the car by ID
                Car selectedCar = CarList.FirstOrDefault(car => car.ID == carID);

                if (selectedCar != null && selectedCar.status == Car_Status.Available)
                {
                    Console.Write("Enter Pickup Date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime pickupDate))
                    {
                        Console.Write("Enter the number of days for the reservation: ");
                        if (int.TryParse(Console.ReadLine(), out int days) && days > 0)
                        {
                            Reservation newReservation = new Reservation(CurrentCustomer, selectedCar, pickupDate, days);

                            // Show reservation details
                            newReservation.DisplayReservation();

                            // Calculate required down payment (20%)
                            double totalCost = selectedCar.RentalPrice * days;
                            double requiredDownPayment = totalCost * 0.2;

                            Console.WriteLine($"\nThe required down payment is: {requiredDownPayment}");
                            Console.Write("Enter down payment amount: ");
                            if (double.TryParse(Console.ReadLine(), out double downPaymentAmount))
                            {
                                if (newReservation.MakeDownPayment(downPaymentAmount))
                                {
                                    Console.WriteLine("Reservation confirmed with down payment.");
                                }
                                else
                                {
                                    Console.WriteLine("Down payment unsuccessful. Reservation cannot proceed.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid amount entered. Reservation canceled.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid number of days.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid pickup date.");
                    }
                }
                else
                {
                    Console.WriteLine("Car not available or invalid ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Car ID.");
            }

            // Wait for user input before going back to the menu
            Console.WriteLine("\nPress any key to return to the customer menu...");
            Console.ReadKey();
        }


        // Method to add a new customer
        public void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("======== ADD NEW CUSTOMER ========");

            // Prompt the admin for customer details
            Console.Write("Enter Customer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Phone Number: ");
            string phoneNumber = Console.ReadLine();

            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();

            Console.Write("Enter Driver's License: ");
            string driversLicense = Console.ReadLine();

            Customer newCustomer = new Customer(name, phoneNumber, email, address, driversLicense);
            CustomerList.Add(newCustomer);
            Console.WriteLine("\nCustomer added successfully!");

            newCustomer.DisplayInformation();
            Console.WriteLine("\nPress any key to return to the admin menu...");
            Console.ReadKey();
        }

        public bool isExistingCustomer(int ID)
        {
            foreach (var customer in CustomerList)
            {
                if (customer.CustomerID == ID)
                    return true; 
            }
            return false;
        }

        public List<Customer> GetCustomerList()
        {
            return CustomerList;
        }

        // View Customers
        public void ViewCustomers()
        {
            Console.Clear();
            Console.WriteLine("======== VIEW CUSTOMERS ========");

            if (CustomerList.Count == 0)
            {
                Console.WriteLine("No customers found.");
            }
            else
            {
                foreach (var customer in CustomerList)
                {
                    customer.DisplayInformation();  
                    Console.WriteLine(); 
                }
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
        }


        // Pre-Instantiated objects for debugging purposes
        public void createCars()
        {
            CarList.Add(new Car("Sedan", "Gasoline", "Toyota", "Camry", 2022, Transmission.Automatic, "ABC123", 2500, 5));
            CarList.Add(new Car("SUV", "Diesel", "Ford", "Everest", 2021, Transmission.Manual, "XYZ456", 3500, 7));
            CarList.Add(new Car("Hatchback", "Electric", "Tesla", "Model 3", 2023, Transmission.Automatic, "TES789", 5000, 5));
            CarList.Add(new Car("Pickup", "Diesel", "Chevrolet", "Colorado", 2020, Transmission.Manual, "PCK234", 4000, 5));
            CarList.Add(new Car("Coupe", "Gasoline", "BMW", "M4", 2019, Transmission.Automatic, "BMWM4", 6000, 4));
            CarList.Add(new Car("Van", "Diesel", "Toyota", "HiAce", 2021, Transmission.Manual, "VAN567", 4500, 12));
            CarList.Add(new Car("Convertible", "Gasoline", "Mercedes", "C-Class", 2022, Transmission.Automatic, "CONV89", 7000, 2));
            CarList.Add(new Car("Sports Car", "Gasoline", "Ferrari", "488", 2023, Transmission.Automatic, "FER488", 15000, 2));
            CarList.Add(new Car("Luxury SUV", "Hybrid", "Lexus", "RX 500h", 2023, Transmission.Automatic, "LUX987", 8000, 5));
            CarList.Add(new Car("Compact", "Gasoline", "Honda", "Civic", 2021, Transmission.Manual, "CIVIC99", 3000, 5));
        }
        public void createCustomer()
        {
            CustomerList.Add(new Customer("John Doe", "09123456789", "john.doe@email.com", "Ormoc City", "D1234567"));
            CustomerList.Add(new Customer("Jane Smith", "09234567890", "jane.smith@email.com", "Tacloban City", "D7654321"));
        }
    }
}
