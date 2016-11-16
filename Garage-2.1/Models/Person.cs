using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Garage_2._1.Models
{
    public class Person : ICloneable
    {
        [Key]
        public string SSN { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Phonenumber { get; set; }

        public Person()
        {

        }

        public Person(string name, string address, string phonenumber)
        {
            this.Name = name;
            this.Address = address;
            this.Phonenumber = phonenumber;
            //this.SSN = ssn;
        }

        public object Clone()
        {
           // return new Person(this.Name, this.Address, this.Phonenumber, this.SSN);
            throw new NotImplementedException();
        }
    }
}
