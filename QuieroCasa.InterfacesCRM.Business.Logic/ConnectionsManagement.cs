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
    public class ConnectionsManagement
    {
        public OrganizationServiceProxy GetOrganizationServiceProxy()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString;
                CrmServiceClient serverConfig = new CrmServiceClient(connectionString);

                if (!serverConfig.IsReady)
                {
                    throw new Exception("Request Connection Error: " + serverConfig.LastCrmError, serverConfig.LastCrmException);
                }

                return serverConfig.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}