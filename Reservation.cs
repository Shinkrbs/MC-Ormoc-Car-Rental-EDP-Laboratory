using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public class Reservation
    {
        private static Random ReservationIdCounter = new Random();
        public int ReservationId { get; }
        public Customer customer { get; }
        public Car car { get; }
        public DateTime PickUpDate { get; }
        public DateTime DropoffDate { get; }
        public Reservation_Status status { get; set; }
        public Payment DownPaymentTransaction { get; private set; } // Store down payment
        public Rent RentTransaction { get; private set; } // Store rent transaction


        public Reservation(Customer customer, Car car, DateTime pickup, int days) 
        {
            this.ReservationId = ReservationIdCounter.Next(1000, 9999);
            this.customer = customer;
            this.car = car;
            this.PickUpDate = pickup;
            this.DropoffDate = pickup.AddDays(days);
            this.status = Reservation_Status.Pending;
        }

        public bool MakeDownPayment(double amount)
        {
            int rentalDays = (DropoffDate - PickUpDate).Days;
            double totalCost = car.RentalPrice * rentalDays; // Calculate total rental cost
            double requiredDownPayment = totalCost * 0.2; // 20% down payment required

            if (amount >= requiredDownPayment)
            {
                this.RentTransaction = new Rent(customer, car, PickUpDate, rentalDays); // Create rent transaction
                this.DownPaymentTransaction = new Payment(amount, RentTransaction, "Down Payment");
                this.status = Reservation_Status.Confirmed;
                this.car.UpdateStatus(Car_Status.Rented);

                Console.WriteLine("Down payment successful! Reservation confirmed.");
                return true;
            }
            else
            {
                Console.WriteLine($"Down payment failed! Minimum required: {requiredDownPayment}");
                return false;
            }
        }
        public void DisplayReservation()
        {
            car.DisplayInfo();
            Console.WriteLine("\n======= RESERVATION DETAILS =======");
            Console.WriteLine($"Reservation ID: {ReservationId}");
            Console.WriteLine($"Status: {status}");
            Console.WriteLine($"Pick-Up Date: {PickUpDate}");
            Console.WriteLine($"Drop-Off Date: {DropoffDate}");
            Console.WriteLine($"Rental Duration: {(DropoffDate - PickUpDate).Days} days");

            if (DownPaymentTransaction != null)
            {
                Console.WriteLine("\n======= DOWN PAYMENT DETAILS =======");
                DownPaymentTransaction.Receipt(car, (DropoffDate - PickUpDate).Days);
            }

            Console.WriteLine();
        }
    }
}
