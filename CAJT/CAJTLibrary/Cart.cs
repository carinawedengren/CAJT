﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAJTLibrary
{
    public class Cart
    {
        #region Constructors
        public Cart() { }

        public Cart(int cartId, string cartName, CustomerInfo customerInfo, List<Product> products)
        {
            CartId = cartId;
            CartName = cartName;
            CustomerInfo = customerInfo;
            Products = products;
        }
        #endregion

        #region Properties
        public int CartId { get; set; }

        public string CartName { get; set; }

        public CustomerInfo CustomerInfo { get; set; }

        public List<Product> Products { get; set; }
        #endregion

        #region Methods
        public double TotalAmunt()
        {
            return Products.Select(p => p.Price).Sum();
        }
        #endregion
    }
}