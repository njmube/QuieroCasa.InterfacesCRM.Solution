// ------------------------------------------------------------------------------------------------
// <copyright file="IncomingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

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
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class IncomingCall : IIncomingCall
    {
        static readonly NLogWriter log = HostLogger.Get<IncomingCall>();

        public ResponseIncomingCall RegisterIncomingCall(string callerId, int typeCall, DateTime dateTimeStart, string username, string callId, string urlNimbus, string dialedNumber)
        {
            StringBuilder sbLog = new StringBuilder();
            ResponseIncomingCall response = new ResponseIncomingCall();
            IOrganizationService organization = null;
            ConnectionsManagement conn = new ConnectionsManagement();
            ContactsManagement contacts = new ContactsManagement();
            IncidentsManagement incidents = new IncidentsManagement();

            try
            {
                organization = conn.GetOrganizationServiceProxy();
                response.ListContacts = contacts.SearchByCallerId(organization, callerId);
                string identifiedPersonId = string.Empty;

                if (response.ListContacts.Count == 1)
                {
                    identifiedPersonId = response.ListContacts.First<ContactDTO>().contactid;
                }

                string contactId = ConfigurationManager.AppSettings["defaultContactId"].ToString();
                string area = string.Empty;
                string phoneCallCenter = ConfigurationManager.AppSettings["phoneCallCenter"].ToString();
                string areaIdCallCenter = ConfigurationManager.AppSettings["areaIdCallCenter"].ToString();
                string phoneCustomerService = ConfigurationManager.AppSettings["phoneCustomerService"].ToString();
                string areaIdCustomerService = ConfigurationManager.AppSettings["areaIdCustomerService"].ToString();

                if (dialedNumber.Equals(phoneCallCenter))
                {
                    area = areaIdCallCenter;
                }
                else
                if (dialedNumber.Equals(phoneCustomerService))
                {
                    area = areaIdCustomerService;
                }

                int typeInputCall = 0;

                if (typeCall == 1)
                {
                    typeInputCall = (int)TypeCall.Local;
                }
                else
                if (typeCall == 2)
                {
                    typeInputCall = (int)TypeCall.Celular;
                }

                response.caseId = incidents.Add(organization, "Caso Nimbus del Agente " + username, (int)PriorityCode.Alta, (int)CaseOriginCode.Telefono, (int)CaseTypeCode.Pregunta, urlNimbus, contactId, area, dateTimeStart, callerId, dateTimeStart.ToString("HH:mm:ss"), (int) CaseCreatedBy.LlamadaAtendida, response.ListContacts.Count, callId, identifiedPersonId, typeInputCall);
                response.urlCase = string.Format(StringHelper.GetURLConnectionString(ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString) + "/main.aspx?etn=incident&pagetype=entityrecord&id=%7b{0}%7d", response.caseId);
                response.codError = 0;
                response.error = string.Empty;
                response.success = true;
                response.message = "Caso creado exitosamente";

                sbLog.AppendFormat("Caso creado exitosamente con el CaseId {0} para el Contacto {1} con un total de {2} coincidencias encontradas. URL generada: {3} con los parametros de entrada callerId: {4}, dateTimeStart: {5}, username: {6}, callId: {7}, urlNimbus: {8}, dialedNumber: {9} \n", response.caseId, contactId, response.ListContacts.Count, response.urlCase, callerId, dateTimeStart, username, callId, urlNimbus, dialedNumber);

                log.Info(sbLog.ToString());
            }
            catch (Exception ex)
            {
                sbLog = new StringBuilder();
                sbLog.AppendFormat("Error en el registro de la llamada de Nimbus con el siguiente mensaje: {0} con los parametros de entrada callerId: {1}, dateTimeStart: {2}, username: {3}, callId: {4}, urlNimbus: {5}, dialedNumber: {6} \n", ExceptionHelper.GetErrorMessage(ex, false), callerId, dateTimeStart, username, callId, urlNimbus, dialedNumber);

                log.Error(sbLog.ToString(), ex);

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
        public ResponseIncomingCall UpdateIncomingCall(string caseId, DateTime dateTimeClosing, string urlRecording)
        {
            ResponseIncomingCall response = new ResponseIncomingCall();
            IOrganizationService organization = null;
            ConnectionsManagement conn = new ConnectionsManagement();
            IncidentsManagement incidents = new IncidentsManagement();

            try
            {
                organization = conn.GetOrganizationServiceProxy();

                response.caseId = string.Empty;
                response.success = incidents.Update(organization, caseId, dateTimeClosing, urlRecording, dateTimeClosing.ToString("HH:mm:ss"));
                response.urlCase = string.Empty;
                response.codError = 0;
                response.error = string.Empty;
                response.message = "Caso actualizado exitosamente";
                log.Info(string.Format("Caso actualizado exitosamente: caseID {0} fecha y hora de cierre {1} URL de grabación {2}", caseId, dateTimeClosing, urlRecording));
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la actualización de la llamada de Nimbus para el caso id {0} fecha y hora de cierre {1} con el siguiente mensaje: {2}", caseId, dateTimeClosing, ExceptionHelper.GetErrorMessage(ex, false)), ex);

                response.codError = 5002;
                response.error = ExceptionHelper.GetErrorMessage(ex, false);
                response.success = false;
                response.message = "El caso no pudo actualizarse. Intente más tarde";
                response.caseId = string.Empty;
                response.urlCase = string.Empty;
                response.ListContacts = null;
            }
            finally
            {
                incidents = null;
                organization = null;
                conn = null;
            }

            return response;
        }
    }
}