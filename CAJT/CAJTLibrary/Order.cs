using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAJTLibrary
{
    public class Order
    {
        #region Constructors
        public Order() { }

        public Order(int orderNumber, List<Product> products, DateTime date, CustomerInfo customerInfo, Status status, bool paid, PaymentMethod paymentMethod)
        {
            OrderNumber = orderNumber;
            Products = products;
            Date = date;
            CustomerInfo = customerInfo;
            Status = status;
            Paid = paid;
            PaymentMethod = paymentMethod;
        }
        #endregion

        #region Properties
        public int OrderNumber { get; set; }

        public List<Product> Products { get; set; }

        public DateTime Date { get; set; }

        public CustomerInfo CustomerInfo { get; set; }

        public Status Status { get; set; }

        public bool Paid { get; private set; }

        public PaymentMethod PaymentMethod { get; set; }
        #endregion

        #region Methods
        public double TotalAmount()
        {
            return Products.Select(p => p.Price).Sum();
        }

        public void Pay() { }
        #endregion
    }


    public enum PaymentMethod
    {
        CreditCard
        
    }

    public enum Status
    {
        Pending,
        Processing,
        Deliverd
    }
}