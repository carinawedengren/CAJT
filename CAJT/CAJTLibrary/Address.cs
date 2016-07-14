using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAJTLibrary
{
    public class Address
    {
        #region Constructors
        public Address(string street, string zipCode, string city, string country)
        {
            Street = street;
            ZipCode = zipCode;
            City = city;
            Country = country;
        }
        public Address(string careOf, string street, string zipCode, string city, string country) : this(street, zipCode, city, country)
        {
            CareOf = careOf;
        }
        #endregion

        #region Properties
        public string CareOf { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string City { get; set; }

        public string Country { get; set; }
        #endregion
    }
}
