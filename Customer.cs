using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Customer : Person
    {
        private static  Random CustomerIdCounter  = new Random();
        public int CustomerID { get; }
        private string DriversLicense {  get; }

        public Customer(string name, string phoneNumber, string email, string address, string driversLicense)
            : base(name, phoneNumber, email, address)
        {
            this.CustomerID = CustomerIdCounter.Next(100, 999);
            this.DriversLicense = driversLicense;
        }

        public override void DisplayInformation()
        {
            base.DisplayInformation();
            Console.WriteLine($"Customer ID: {CustomerID}");
            Console.WriteLine($"Driver's License: {DriversLicense}");
        }
    }
}