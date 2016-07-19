using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAJTLibrary
{
    public class Phone
    {
        #region Constructors
        public Phone(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        #endregion

        #region Properties
        public string PhoneNumber { get; set; }
        #endregion
    }
}
