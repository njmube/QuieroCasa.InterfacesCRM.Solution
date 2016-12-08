using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;
using QuieroCasa.InterfacesCRM.Business.Logic;
using QuieroCasa.InterfacesCRM.Data.Entities;
using Microsoft.Xrm.Sdk.Client;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using QuieroCasa.InterfacesCRM.Business.Commons.Utils;
using System.Configuration;
using System.Data.SqlClient;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class IncomingCall : IIncomingCall
    {
        static readonly NLogWriter log = HostLogger.Get<IncomingCall>();

        public ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart)
        {
            ResponseIncomingCall response = new ResponseIncomingCall();
            OrganizationServiceProxy organization = null;
            ConnectionsManagement conn = new ConnectionsManagement();
            ContactsManagement contacts = new ContactsManagement();
            IncidentsManagement incidents = new IncidentsManagement();

            try
            {
                organization = conn.GetOrganizationServiceProxy();
                response.ListContacts = contacts.SearchByCallerId(organization, callerId);

                string contactId = string.Empty;

                if (response.ListContacts.Count == 1)
                {
                    contactId = response.ListContacts.First().contactid;
                }
                else
                if (response.ListContacts.Count == 0)
                {
                    contactId = contacts.Add(organization, callerId, "miguel.martinez@grw.com.mx");
                }

                response.caseId = incidents.Add(organization, "Caso Nimbus" + " " + DateTime.Now.ToString(), (int)PriorityCode.Alta, (int)CaseOriginCode.Telefono, (int)CaseTypeCode.Pregunta, "Caso de prueba", contactId, (int)Department.Ventas, dateTimeStart, callerId);
                response.urlCase = string.Format(StringHelper.GetURLConnectionString(ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString) + "/main.aspx?etn=incident&pagetype=entityrecord&id=%7b{0}%7d", response.caseId);
                response.codError = 0;
                response.error = string.Empty;
                response.success = true;
                response.message = "Caso creado exitosamente";
                log.Info(string.Format("Caso creado exitosamente: caseID {0} para el contacto {1} con la solicitud callerId {2} fecha inicio {3} con un total de {4} coincidencias encontradas. URL generada: {5}", response.caseId, contactId, callerId, dateTimeStart, response.ListContacts.Count, response.urlCase));
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro de la llamada de Nimbus para el contacto con el caller id {0} fecha y hora de inicio {1} con el siguiente mensaje: {2}", callerId, dateTimeStart, ExceptionHelper.GetErrorMessage(ex, false)), ex);

                response.codError = 5001;
                response.error = ExceptionHelper.GetErrorMessage(ex, false);
                response.success = false;
                response.message = "El caso no pudo crearse. Intente más tarde";
                response.caseId = Guid.Empty.ToString();
                response.urlCase = string.Empty;
                response.ListContacts = null;
            }
            finally
            {
                contacts = null;
                incidents = null;
                organization = null;
                conn = null;
            }

            return response;
        }

        public int UpdateIncomingCall(string urlRecording, string dateTimeClosing, string caseId)
        {
            throw new NotImplementedException();
        }
    }
}