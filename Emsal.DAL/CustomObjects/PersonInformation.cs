using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Emsal.DAL.CustomObjects
{
 public  class PersonInformation
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string PinNumber { get; set; }
        public Nullable<long> UserId { get; set; }
        public string gender { get; set; }
        public Nullable<long> birtday { get; set; }
        public Nullable<long> educationLevel_eV_Id { get; set; }
        public Nullable<long> job_eV_Id { get; set; }
        public Nullable<long> address_Id { get; set; }
        public Nullable<long> Status { get; set; }
        public Nullable<long> LastUpdatedStatus { get; set; }
        public string createdUser { get; set; }
        public Nullable<long> createdDate { get; set; }
        public string updatedUser { get; set; }
        public Nullable<long> updatedDate { get; set; }
        public string profilePicture { get; set; }
        public Int64 roleId { get; set; }
        public string roleName { get; set; }
        public string roleDescription { get; set; }
      
    }
}
