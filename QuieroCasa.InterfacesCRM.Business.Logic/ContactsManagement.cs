using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Data.Entities;
using System;
using System.Collections.Generic;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class ContactsManagement
    {
        OrganizationServiceProxy _serviceProxy;
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public List<ContactDTO> SearchByCallerId(OrganizationServiceProxy organizationServiceProxy, string callerId)
        {
            try
            {
                List<ContactDTO> listContacts = new List<ContactDTO>();

                using (_serviceProxy = organizationServiceProxy)
                {
                    _serviceProxy.EnableProxyTypes();
                    
                    QueryExpression sdkContactsQuery = new QueryExpression
                    {
                        EntityName = Contact.EntityLogicalName,
                        ColumnSet = new ColumnSet("contactid", "emailaddress1", "fullname", "qc_telefonodecontacto", "mobilephone", "qc_otrotelefono", "qc_telefonodeoficina"),
                        Criteria = new FilterExpression()
                        {
                            FilterOperator = LogicalOperator.Or,
                            Conditions =
                            {
                                new ConditionExpression("qc_telefonodecontacto", ConditionOperator.Equal, callerId),
                                new ConditionExpression("mobilephone", ConditionOperator.Equal, callerId),
                                new ConditionExpression("qc_otrotelefono", ConditionOperator.Equal, callerId),
                                new ConditionExpression("qc_telefonodeoficina", ConditionOperator.Equal, callerId)
                            }
                        }
                    };

                    EntityCollection contacts = _serviceProxy.RetrieveMultiple(sdkContactsQuery);

                    foreach (Entity entity in contacts.Entities)
                    {
                        Contact contact = entity.ToEntity<Contact>();

                        listContacts.Add(new ContactDTO()
                        {
                            contactid = contact.Attributes.Contains("contactid") ? contact.Attributes["contactid"].ToString() : string.Empty,
                            emailaddress1 = contact.Attributes.Contains("emailaddress1") ? contact.Attributes["emailaddress1"].ToString() : string.Empty,
                            fullname = contact.Attributes.Contains("fullname") ? contact.Attributes["fullname"].ToString() : string.Empty,
                            qc_telefonodecontacto = contact.Attributes.Contains("qc_telefonodecontacto") ? contact.Attributes["qc_telefonodecontacto"].ToString() : string.Empty,
                            mobilephone = contact.Attributes.Contains("mobilephone") ? contact.Attributes["mobilephone"].ToString() : string.Empty,
                            qc_otrotelefono = contact.Attributes.Contains("qc_otrotelefono") ? contact.Attributes["qc_otrotelefono"].ToString() : string.Empty,
                            qc_telefonodeoficina = contact.Attributes.Contains("qc_telefonodeoficina") ? contact.Attributes["qc_telefonodeoficina"].ToString() : string.Empty
                        });

                    }
                }

                return listContacts;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda de contactos con el caller id {0} con el siguiente mensaje: {1}", callerId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        public string Add(OrganizationServiceProxy organizationServiceProxy, string callerId, string emailAddress)
        {
            try
            {
                using (_serviceProxy = organizationServiceProxy)
                {
                    _serviceProxy.EnableProxyTypes();

                    Contact newContact = new Contact()
                    {
                        qc_telefonodecontacto = callerId,
                        EMailAddress1 = emailAddress
                    };

                    Guid incidentId = _serviceProxy.Create(newContact);
                    return incidentId.ToString();
                }
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del contacto con el caller id {0} con el siguiente mensaje: {1}", callerId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}