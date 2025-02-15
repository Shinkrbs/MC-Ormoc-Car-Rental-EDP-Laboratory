using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Payment
    {
        public int TransactionID { get; }
        public Rent Transaction { get; set; }
        private double Amount { get; }
        private DateTime Date { get; }
        private string Method {  get; }
        private Payment_Status status { get; set; }

        public Payment(double amount, Rent transaction, string method)
        {
            this.TransactionID = transaction.TransactionID;
            this.Transaction = transaction;
            this.Amount = amount;
            this.Method = method;
            this.Date = DateTime.Now;
            this.status = Payment_Status.Not_Completed;
        }

        public bool ProcessPayment(Car car, int days)
        {
            
            if (car.status == Car_Status.Available)       
            {
                this.status = Payment_Status.Completed;
                return true;
            }
            else return false;
        }

        public Payment_Status GetStatus(Car car, int days)
        {
            return status;
        }

        public void Receipt(Car car, int days)
        {
            this.ProcessPayment(car, days); // Process the Payment 
            if (this.status == Payment_Status.Completed)
            {
                Console.WriteLine("\n======= RECEIPT =======");
                Console.WriteLine($"Transaction ID   : {TransactionID}");
                Console.WriteLine($"Amount Paid      : {Amount}"); 
                Console.WriteLine($"Payment Date     : {Date}");
                Console.WriteLine($"Method of Payment: {Method}");
                Console.WriteLine($"Car Rented       : {car.Brand} {car.Model}");
                Console.WriteLine($"Rental Duration  : {days} days");
                Console.WriteLine($"Total Cost       : {this.Transaction.CalculateTotalCost(car, days)}");
                Console.WriteLine($"Payment Status   : {GetStatus(car, days)}");
                Console.WriteLine("=========================\n");
            }
            else Console.WriteLine("Payment Failed! Please check your amount or car availability.");
        }
    }
}
