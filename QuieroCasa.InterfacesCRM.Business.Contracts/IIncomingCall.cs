using QuieroCasa.InterfacesCRM.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IIncomingCall
    {
        ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart);
        int UpdateIncomingCall(string urlRecording, string dateTimeClosing, string guidCase);
    }
}