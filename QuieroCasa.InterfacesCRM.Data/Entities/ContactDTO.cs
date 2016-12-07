using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ContactDTO
    {
        public EntityReference accountid { get; set; }
        public string contactid { get; set; }
        public string emailaddress1 { get; set; }
        public string fullname { get; set; }
        public EntityReference parentcustomerid { get; set; }
        public EntityReference parentcontactid { get; set; }
        

    }
}
