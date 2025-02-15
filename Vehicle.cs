using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public abstract class Vehicle
    {
        private static Random IDCounter = new Random();
        public int ID { get; }
        public string Brand { get; }
        public string Model { get; }
        private string PlateNumber {  get; }
        private int YearManufactured {  get; }
        private Transmission transmission { get; set; } // Automatic, Manual

        public Vehicle(string brand, string model, string plateNumber, int yearManufactured, Transmission transmission)
        {
            this.ID = IDCounter.Next(10000, 99999); 
            this.Brand = brand;
            this.Model = model;
            this.PlateNumber = plateNumber;
            this.YearManufactured = yearManufactured;
            this.transmission = transmission;  
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine("\n======= VEHICLE INFORMATION =======");
            Console.WriteLine($"Vehicle ID: {ID}");
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Year Manufactured: {YearManufactured}");
            Console.WriteLine($"Plate Number: {PlateNumber}");
            Console.WriteLine($"Transmission: {transmission}");
        }
    }
}
