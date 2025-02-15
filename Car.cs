using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Car : Vehicle
    {
        private string CarType {  get; } // Example: Sedan, SUV, Van, etc.
        private string FuelType {  get; }
        public double RentalPrice { get; private set; }
        public Car_Status status { get;  set; } // Available, Rented, Maintenance
        private int Capacity { get; }

        public Car(string cartype, string fueltype, string Brand, string Model, int Year, Transmission transmission, string Plate, double Price, int capacity) 
            : base(Brand, Model, Plate, Year, transmission)
        {
            this.CarType = cartype;
            this.FuelType = fueltype;  
            this.RentalPrice = Price;
            this.status = Car_Status.Available;
            this.Capacity = capacity;
        }

        public void UpdateStatus(Car_Status NewStatus)
        {
            this.status = NewStatus;
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Car Type: {CarType}");
            Console.WriteLine($"Fuel Type: {FuelType}");
            Console.WriteLine($"Rental Price: {RentalPrice}");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"Capacity: {Capacity}\n");
        }
    }
}
