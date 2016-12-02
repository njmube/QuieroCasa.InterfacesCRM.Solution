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

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class ContactsManagement
    {
        OrganizationServiceProxy _serviceProxy;

        public ResponseIncomingCall SearchByCallerId(string callerId)
        {
            try
            {
                ResponseIncomingCall response = new ResponseIncomingCall();
                string connectionString = ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString;
                CrmServiceClient serverConfig = new CrmServiceClient(connectionString);

                if (!serverConfig.IsReady)
                {
                    throw new Exception("Request Connection Error: " + serverConfig.LastCrmError, serverConfig.LastCrmException);
                }

                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = serverConfig.OrganizationServiceProxy)
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    Guid userid = ((WhoAmIResponse)_serviceProxy.Execute(new WhoAmIRequest())).UserId;
                    SystemUser systemUser = (SystemUser)_serviceProxy.Retrieve("systemuser", userid, new ColumnSet(new string[] { "firstname", "lastname" }));
                    response.firstname = systemUser.FirstName;
                    response.lastname = systemUser.LastName;
                    List<Contact> listContacts = new List<Contact>();

                    QueryExpression sdkContactsQuery = new QueryExpression
                    {
                        EntityName = Contact.EntityLogicalName,
                        ColumnSet = new ColumnSet("accountid", "contactid", "emailaddress1", "fullname"),
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
                        listContacts.Add((Contact)entity);
                    }
                }

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}