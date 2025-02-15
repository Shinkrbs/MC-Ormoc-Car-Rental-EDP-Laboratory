using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MC_Ormoc_Car_Rental_EDP_LAB_1
{
    public abstract class Person 
    {
        public  string Name { get; set;}
        public string PhoneNumber { get;set;}
        public string Email { get; set;}
        public string Address { get; set; }

        public Person(string name, string phoneNumber, string email, string address) : base()
        {
            this.Name = name;
            this.PhoneNumber = phoneNumber;
            this.Email = email;
            this.Address = address;
        }

        public virtual void DisplayInformation()
        {
            Console.WriteLine("\n======= PERSONAL INFORMATION =======");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Phone Number: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Address: {Address}");
        }
    }
}
