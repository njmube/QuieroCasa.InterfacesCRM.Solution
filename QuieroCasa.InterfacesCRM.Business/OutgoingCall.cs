using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class OutgoingCall : IOutgoingCall
    {
        public int RequestOutgoingCall(string contactId, string callerId, string address, string guidCall)
        {
            throw new NotImplementedException();
        }
    }
}
