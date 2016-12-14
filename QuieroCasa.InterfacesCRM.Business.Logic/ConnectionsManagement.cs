// ------------------------------------------------------------------------------------------------
// <copyright file="ConnectionsManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Tooling.Connector;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using System;
using System.Configuration;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class ConnectionsManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ConnectionsManagement>();

        public IOrganizationService GetOrganizationServiceProxy()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString;
                CrmServiceClient serverConfig = new CrmServiceClient(connectionString);

                if (!serverConfig.IsReady)
                {
                    throw new Exception(string.Format("Error en la conexión: LastCrmError {0} - LastCrmException {1}", serverConfig.LastCrmError, serverConfig.LastCrmException));
                }

                return (IOrganizationService)serverConfig.OrganizationServiceProxy;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("No se pudo conectar al servidor CRM. Mensaje recibido {0}", ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}