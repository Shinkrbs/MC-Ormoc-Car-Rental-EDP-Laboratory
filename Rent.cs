using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Rent
    {
        private static Random TransactionIdCounter = new Random();
        public int TransactionID { get; }
        public Customer customer { get; }
        public Car car { get; }
        public DateTime Start { get; }
        public DateTime End { get; } 
        public double TotaCost {  get; }
        public Rent_Status status { get; set; }

        public Rent(Customer customer, Car car, DateTime start, int days)
        {
            this.TransactionID = TransactionIdCounter.Next(100000, 999999); ;
            this.customer = customer;   
            this.car = car;
            this.Start = start;
            this.End = start.AddDays(days);
            this.TotaCost = this.CalculateTotalCost(car, days);
            this.status = Rent_Status.Ongoing;
        }

        public double CalculateTotalCost(Car car, int days)
        {
            return days * car.RentalPrice;
        }

        public void CompleteTransaction()
        {
            this.status = Rent_Status.Completed;
            car.UpdateStatus(Car_Status.Available);
        }
    }
}
