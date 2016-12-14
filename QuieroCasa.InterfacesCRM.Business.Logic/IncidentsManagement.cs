// ------------------------------------------------------------------------------------------------
// <copyright file="IncidentsManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using QuieroCasa.InterfacesCRM.Data.Entities;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class IncidentsManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public string Add(IOrganizationService organizationServiceProxy, string title, int priorityCode, int caseOriginCode, int caseTypeCode, string description, string contactId, int departamentoCode, DateTime dateTimeStart, string callerId, string timeStart)
        {
            try
            {
                List<ContactDTO> listContacts = new List<ContactDTO>();

                Incident newCase = new Incident()
                {
                    Title = title,
                    PriorityCode = new OptionSetValue(priorityCode),
                    CaseOriginCode = new OptionSetValue(caseOriginCode),
                    CaseTypeCode = new OptionSetValue(caseTypeCode),
                    Description = description,
                    CustomerId = new EntityReference(Contact.EntityLogicalName, new Guid(contactId)),
                    its_departamento = new OptionSetValue(departamentoCode),
                    its_fechayhoradeinicio = dateTimeStart,
                    its_telefono = callerId,
                    its_horadeinicio = timeStart
                };

                Guid incidentId = organizationServiceProxy.Create(newCase);
                return incidentId.ToString();
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del caso del contacto {0} con el siguiente mensaje: {1}", contactId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        public bool Update(IOrganizationService organizationServiceProxy, string caseId, DateTime dateTimeClosing, string urlRecording, string timeEnd)
        {
            try
            {
                Incident updateCase = new Incident()
                {
                    IncidentId = new Guid(caseId),
                    its_urldegrabacion = urlRecording,
                    its_fechayhoradecierre = dateTimeClosing,
                    its_horadecierre = timeEnd
                };

                organizationServiceProxy.Update(updateCase);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la actualización del caso {0} fecha y hora de cierre {1} url de grabación {2} con el siguiente mensaje: {3}", caseId, dateTimeClosing, urlRecording, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}