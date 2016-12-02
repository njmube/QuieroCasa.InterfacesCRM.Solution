using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;
using QuieroCasa.InterfacesCRM.Business.Logic;
using QuieroCasa.InterfacesCRM.Data.Entities;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class IncomingCall : IIncomingCall
    {
        public ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart, string url, string guidCase, string urlCase)
        {
            try
            {
                ContactsManagement contacts = new ContactsManagement();
                return contacts.SearchByCallerId(callerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public int UpdateIncomingCall(string urlRecording, string dateTimeClosing, string guidCase)
        {
            throw new NotImplementedException();
        }
    }
}
