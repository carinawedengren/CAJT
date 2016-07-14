using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAJTLibrary
{
    public class CustomerInfo
    {
        #region Constructors
        public CustomerInfo(int customerId, string name, Address shippingAddress, Address billingAddress, Phone phone)
        {
            CustomerId = customerId;
            Name = name;
            ShippingAddress = shippingAddress;
            BillingAddress = billingAddress;
            Phone = phone;
        }
        #endregion

        #region Properties
        public int CustomerId { get; set; }

        public string Name { get; set; }

        public Address ShippingAddress { get; set; }

        public Address BillingAddress { get; set; }

        public Phone Phone { get; set; }
        #endregion
    }
}