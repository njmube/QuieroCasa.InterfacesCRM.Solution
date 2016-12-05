using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ResponseIncomingCall
    {
        public int codError { get; set; }
        public string error { get; set; }
        public string caseId { get; set; }
        public string contactId { get; set; }
        public string accountId { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public List<ContactDTO> ListContacts { get; set; }
    }
}
