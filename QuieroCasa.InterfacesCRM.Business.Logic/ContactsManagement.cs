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
    public class ContactsManagement
    {
        OrganizationServiceProxy _serviceProxy;

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
                        ColumnSet = new ColumnSet("accountid", "contactid", "emailaddress1", "fullname", "parentcustomerid", "parentcontactid"),
                        Criteria = new FilterExpression()
                        {
                            FilterOperator = LogicalOperator.Or,
                            Conditions =
                            {
                                new ConditionExpression("telephone1", ConditionOperator.Equal, callerId),
                                new ConditionExpression("telephone2", ConditionOperator.Equal, callerId),
                                new ConditionExpression("telephone3", ConditionOperator.Equal, callerId),
                                new ConditionExpression("mobilephone", ConditionOperator.Equal, callerId),
                                new ConditionExpression("address1_telephone1", ConditionOperator.Equal, callerId),
                                new ConditionExpression("address1_telephone2", ConditionOperator.Equal, callerId)
                            }
                        }
                    };

                    EntityCollection contacts = _serviceProxy.RetrieveMultiple(sdkContactsQuery);

                    foreach (Entity entity in contacts.Entities)
                    {
                        Contact contact = entity.ToEntity<Contact>();

                        listContacts.Add(new ContactDTO()
                        {
                            accountid = contact.Attributes.Contains("accountid") ? ((EntityReference)contact.Attributes["accountid"]) : null,
                            contactid = contact.Attributes.Contains("contactid") ? contact.Attributes["contactid"].ToString() : string.Empty,
                            emailaddress1 = contact.Attributes.Contains("emailaddress1") ? contact.Attributes["emailaddress1"].ToString() : string.Empty,
                            fullname = contact.Attributes.Contains("fullname") ? contact.Attributes["fullname"].ToString() : string.Empty,
                            parentcustomerid = contact.Attributes.Contains("parentcustomerid") ? ((EntityReference)contact.Attributes["parentcustomerid"]) : null,
                            parentcontactid = contact.Attributes.Contains("parentcontactid") ? ((EntityReference)contact.Attributes["parentcontactid"]) : null
                        });

                    }
                }

                return listContacts;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Add(OrganizationServiceProxy organizationServiceProxy, string callerId)
        {
            try
            {
                using (_serviceProxy = organizationServiceProxy)
                {
                    _serviceProxy.EnableProxyTypes();

                    Contact newContact = new Contact()
                    {
                        qc_telefonodecontacto = callerId,
                        EMailAddress1 = "miguel.martinez@grw.com.mx"
                    };

                    Guid incidentId = _serviceProxy.Create(newContact);
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