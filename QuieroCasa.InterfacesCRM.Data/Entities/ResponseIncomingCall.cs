using System.Collections.Generic;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ResponseIncomingCall
    {
        public int codError { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string caseId { get; set; }
        public string urlCase { get; set; }
        public List<ContactDTO> ListContacts { get; set; }
    }
}