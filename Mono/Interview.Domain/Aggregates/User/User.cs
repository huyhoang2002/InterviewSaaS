using Interview.Domain.Seedworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interview.Domain.Aggregates.User
{
    public class User : EntityBase<Guid>
    {
        public User(string firstName, string lastName, int age, bool gender, string phoneNumber, string address, string city, string province, string citizenId)
        {
            FirstName = firstName;
            LastName = lastName;
            Age = age;
            Gender = gender;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Province = province;
            CitizenId = citizenId;
            IsDeleted = false;
            CreatedAt = DateTime.Now;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public bool Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string? Province { get; set; }
        public string CitizenId { get; set; }
    }
}
