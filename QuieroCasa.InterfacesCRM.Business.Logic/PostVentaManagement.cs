// ------------------------------------------------------------------------------------------------
// <copyright file="PostVentaManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    public class PostVentaManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public PostVentaDTO SearchByName(IOrganizationService organizationServiceProxy, string qc_name)
        {
            try
            {
                PostVentaDTO postVenta = null;

                QueryExpression sdkPostVentaQuery = new QueryExpression
                {
                    EntityName = "qc_postventa",
                    ColumnSet = new ColumnSet("qc_postventaid", "qc_contactoid"),
                    Criteria = new FilterExpression()
                    {
                        FilterOperator = LogicalOperator.Or,
                        Conditions =
                            {
                                new ConditionExpression("qc_name", ConditionOperator.Equal, qc_name)
                            }
                    }
                };

                EntityCollection postVentaCollections = organizationServiceProxy.RetrieveMultiple(sdkPostVentaQuery);

                if (postVentaCollections.Entities.Count > 0)
                {
                    postVenta = new PostVentaDTO()
                    {
                        qc_name = qc_name,
                        qc_postventaId = postVentaCollections[0].Attributes.Contains("qc_postventaid") ? postVentaCollections[0].Attributes["qc_postventaid"].ToString() : string.Empty,
                        qc_contactoid = postVentaCollections[0].Attributes.Contains("qc_contactoid") ? ((EntityReference)postVentaCollections[0].Attributes["qc_contactoid"]).Id.ToString() : string.Empty
                    };
                }

                return postVenta;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda de postVenta con el qc_name {0} con el siguiente mensaje: {1}", qc_name, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}