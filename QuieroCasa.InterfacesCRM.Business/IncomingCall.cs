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

        public ResponseIncomingCall RegisterIncomingCall(string callerId, int typeCall, DateTime dateTimeStart, string username, string callId, string urlNimbus)
        {
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

                response.caseId = incidents.Add(organization, "Caso Nimbus del Agente " + username, (int)PriorityCode.Alta, (int)CaseOriginCode.Telefono, (int)CaseTypeCode.Pregunta, "Caso generado para: " + urlNimbus, contactId, (int)Department.Ventas, dateTimeStart, callerId, dateTimeStart.ToString("HH:mm:ss"), (int) CaseCreatedBy.Llamada, response.ListContacts.Count, callId, identifiedPersonId);
                response.urlCase = string.Format(StringHelper.GetURLConnectionString(ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString) + "/main.aspx?etn=incident&pagetype=entityrecord&id=%7b{0}%7d", response.caseId);
                response.codError = 0;
                response.error = string.Empty;
                response.success = true;
                response.message = "Caso creado exitosamente";
                log.Info(string.Format("Caso creado exitosamente: caseID {0} para el contacto {1} con la solicitud callerId {2} fecha y hora de inicio {3} con un total de {4} coincidencias encontradas. URL generada: {5}", response.caseId, contactId, callerId, dateTimeStart, response.ListContacts.Count, response.urlCase));
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
        public ResponseIncomingCall UpdateIncomingCall(string caseId, DateTime dateTimeClosing, string urlNimbus, string urlRecording)
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