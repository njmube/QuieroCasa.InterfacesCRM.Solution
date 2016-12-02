using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.DynamicsCRM.Framework;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ResponseIncomingCall
    {
        public int codError { get; set; }
        public string error { get; set; }
        public Guid caseId { get; set; }
        public Guid contactId { get; set; }
        public Guid accountId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public List<Contact> ListContact { get; set; }
    }
}
