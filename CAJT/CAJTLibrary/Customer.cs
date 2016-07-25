using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAJTLibrary
{
    public class Customer
    {
        #region Constructors
        public Customer()
        {
            ActiveCart = new Cart();
        }

        public Customer(CustomerInfo info) : this()
        {
            Info = info;
        }

        public Customer(CustomerInfo info, Cart activeCart) : this(info)
        {
            ActiveCart = activeCart;
        }
        #endregion

        #region Properties
        public Cart ActiveCart { get; set; }

        public List<KeyValuePair<int, string>> SavedCarts { get; set; }

        public CustomerInfo Info { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Fetches the cart named cartName and sets it as ActivCart.
        /// </summary>
        /// <param name="cartName">A string containing the user specified name of the cart.</param>
        public void SetActiveCart(string cartName)
        {
            ActiveCart = SQLUtils.GetActiveCart(SavedCarts.Where(kvp => kvp.Value.CompareTo(cartName) == 0).Select(kvp => kvp.Key).First());
        }

        public string SaveActiveCart()
        {
            if (SavedCarts.Where(kvp => kvp.Value.CompareTo(ActiveCart.CartName) == 0).Count() == 0)
            {
                SavedCarts.Add(new KeyValuePair<int, string>(ActiveCart.CartId, ActiveCart.CartName));
                return ActiveCart.CartName + " was added!";
            }
            else
                return ActiveCart.CartName + " is already saved!";
        }
        #endregion
    }
}
