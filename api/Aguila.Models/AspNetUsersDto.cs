using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Aguila.Models
{
    public class AspNetUsersDto   :IdentityUser//edited
    {
        //public override string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public Int32 HandicapId { get; set; }
        public string ClubType { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Age { get; set; }
        public string SevenIronDistance { get; set; }
        public string DriverDistance { get; set; }
        public bool AccountActive { get; set; }
        public DateTime? DatePaid { get; set; }
        public string Twitter { get; set; }
        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
               "api/contacts/{0}", this.Id);
            }
            set { }
        }

        public string ApplicationRoleId { get; set; }
    }


    //public class AppRole : IdentityRole
    //{
    //    public AppRole() : base() { }
    //    public AppRole(string name) : base(name) { }
    //    // extra properties here 
    //}
}
