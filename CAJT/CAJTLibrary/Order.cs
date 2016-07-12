using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAJTLibrary
{
    public class Order
    {
        public int OrderNumber
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<Product> Products
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public DateTime Date
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public CustomerInfo CustomerInfo
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public Status Status
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public bool Paid
        {
            get
            {
                throw new System.NotImplementedException();
            }

            private set
            {
            }
        }

        public double TotalAmount()
        {
            throw new System.NotImplementedException();
        }
    }
    public enum Status
    {
        Pending,
        Processing,
        Deliverd
    }
}