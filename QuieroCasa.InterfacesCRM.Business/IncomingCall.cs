using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;
using QuieroCasa.InterfacesCRM.Business.Logic;
using QuieroCasa.InterfacesCRM.Data.Entities;
using Microsoft.Xrm.Sdk.Client;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class IncomingCall : IIncomingCall
    {
        public ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart, string url, string guidCase, string urlCase)
        {
            try
            {
                ConnectionsManagement conn = new ConnectionsManagement();
                ContactsManagement contacts = new ContactsManagement();
                ResponseIncomingCall response = new ResponseIncomingCall();
                IncidentsManagement incidents = new IncidentsManagement();

                OrganizationServiceProxy organization = conn.GetOrganizationServiceProxy();
                response.ListContacts = contacts.SearchByCallerId(organization, callerId);

                if (response.ListContacts.Count == 1)
                {
                    ContactDTO contact = response.ListContacts.First();
                    response.caseId = incidents.Add(organization, "Caso Nimbus" + " " + DateTime.Now.ToString(), (int)PriorityCode.Alta, (int)CaseOriginCode.Telefono, (int)CaseTypeCode.Pregunta, "Caso de prueba", contact.customerId, (int) Department.Ventas, dateTimeStart);
                    response.contactId = contact.contactid;
                }

                return response;
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
