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
        public string contactid { get; set; }
        public string emailaddress1 { get; set; }
        public string fullname { get; set; }
        public string qc_telefonodecontacto { get; set; }
        public string mobilephone { get; set; }
        public string qc_otrotelefono { get; set; }
        public string qc_telefonodeoficina { get; set; }
    }
}