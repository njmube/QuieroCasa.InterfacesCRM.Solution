using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IOutgoingCall
    {
        int RequestOutgoingCall(string contactId, string callerId, string address, string guidCall);
    }
}