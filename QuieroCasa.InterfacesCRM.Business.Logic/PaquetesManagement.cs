// ------------------------------------------------------------------------------------------------
// <copyright file="PaquetesManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

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
    public class PaquetesManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public PaqueteDTO SearchByName(IOrganizationService organizationServiceProxy, string qc_name)
        {
            try
            {
                PaqueteDTO paquete = null;

                QueryExpression sdkPaqueteQuery = new QueryExpression
                {
                    EntityName = "qc_paquetes",
                    ColumnSet = new ColumnSet("qc_paquetesid"),
                    Criteria = new FilterExpression()
                    {
                        FilterOperator = LogicalOperator.Or,
                        Conditions =
                            {
                                new ConditionExpression("qc_name", ConditionOperator.Equal, qc_name)
                            }
                    }
                };

                EntityCollection paqueteCollections = organizationServiceProxy.RetrieveMultiple(sdkPaqueteQuery);

                if (paqueteCollections.Entities.Count > 0)
                {
                    paquete = new PaqueteDTO()
                    {
                        qc_name = qc_name,
                        qc_paquetesid = paqueteCollections[0].Attributes.Contains("qc_paquetesid") ? paqueteCollections[0].Attributes["qc_paquetesid"].ToString() : string.Empty
                    };
                }

                return paquete;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda de paquete con el qc_name {0} con el siguiente mensaje: {1}", qc_name, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}