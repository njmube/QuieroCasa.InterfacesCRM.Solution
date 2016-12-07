using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using QuieroCasa.DynamicsCRM.Framework;
using Microsoft.Xrm.Tooling.Connector;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;
using QuieroCasa.InterfacesCRM.Data.Entities;
using System.Collections;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class IncidentsManagement
    {
        OrganizationServiceProxy _serviceProxy;

        public string Add(OrganizationServiceProxy organizationServiceProxy, string title, int priorityCode, int caseOriginCode, int caseTypeCode, string description, EntityReference parentcustomerid, string contactId, int departamentoCode, DateTime dateTimeStart)
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
                        its_fechayhoradeinicio = dateTimeStart
                    };

                    Guid incidentId = _serviceProxy.Create(newCase);
                    return incidentId.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}