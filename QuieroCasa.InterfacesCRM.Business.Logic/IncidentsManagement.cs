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
        OrganizationServiceProxy _serviceProxy;
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public string Add(OrganizationServiceProxy organizationServiceProxy, string title, int priorityCode, int caseOriginCode, int caseTypeCode, string description, string contactId, int departamentoCode, DateTime dateTimeStart, string callerId)
        {
            try
            {
                List<ContactDTO> listContacts = new List<ContactDTO>();

                using (_serviceProxy = organizationServiceProxy)
                {
                    _serviceProxy.EnableProxyTypes();

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
                        its_telefono = callerId
                    };

                    Guid incidentId = _serviceProxy.Create(newCase);
                    return incidentId.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del caso del contacto {0} con el siguiente mensaje: {1}", contactId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}