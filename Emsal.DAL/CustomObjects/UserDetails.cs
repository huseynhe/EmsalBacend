using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
   public class UserDetails
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastLoginIP { get; set; }
        public Nullable<long> LastLoginDate { get; set; }
        public string ProfileImageUrl { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public Nullable<long> userType_eV_ID { get; set; }
        public Nullable<long> ASC_ID { get; set; }
        public Nullable<long> KTN_ID { get; set; }
        public Nullable<short> TaxexType { get; set; }
        public string regionName { get; set; }
        public string organizationName { get; set; }
        public string name { get; set; }
        public string surName { get; set; }
        public string fatherName { get; set; }
        public string fullAddress { get; set; }
    }
}
