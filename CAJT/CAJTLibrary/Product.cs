using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAJTLibrary
{
    public class Product
    {
        #region Constructors
        public Product() { }

        public Product(int productId, int productNumber, string productName, double price, string description)
        {
            ProductId = productId;
            ProductNumber = productNumber;
            ProductName = productName;
            Price = price;
            Description = description;
        }
        #endregion

        #region Properties
        private int ProductId { get; set; }

        public int ProductNumber { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        #endregion

        #region Methods
        public int NumberInStock()
        {
            return SQLUtils.GetNumberInStock(ProductId);
        }
        #endregion
    }
}