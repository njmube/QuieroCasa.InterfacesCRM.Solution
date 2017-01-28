// ------------------------------------------------------------------------------------------------
// <copyright file="DesarrollosManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    public class DesarrollosManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public DesarrolloDTO SearchByName(IOrganizationService organizationServiceProxy, string qc_name)
        {
            try
            {
                DesarrolloDTO desarrollo = null;

                QueryExpression sdkDesarrolloQuery = new QueryExpression
                {
                    EntityName = "qc_desarrollos",
                    ColumnSet = new ColumnSet("qc_desarrollosid", "qc_nombrecomercial"),
                    Criteria = new FilterExpression()
                    {
                        FilterOperator = LogicalOperator.Or,
                        Conditions =
                            {
                                new ConditionExpression("qc_name", ConditionOperator.Equal, qc_name)
                            }
                    }
                };

                EntityCollection desarrolloCollections = organizationServiceProxy.RetrieveMultiple(sdkDesarrolloQuery);

                if (desarrolloCollections.Entities.Count > 0)
                {
                    desarrollo = new DesarrolloDTO()
                    {
                        qc_name = qc_name,
                        qc_desarrollosid = desarrolloCollections[0].Attributes.Contains("qc_desarrollosid") ? desarrolloCollections[0].Attributes["qc_desarrollosid"].ToString() : string.Empty,
                        qc_nombrecomercial = desarrolloCollections[0].Attributes.Contains("qc_nombrecomercial") ? desarrolloCollections[0].Attributes["qc_nombrecomercial"].ToString() : string.Empty
                    };
                }

                return desarrollo;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda del desarrollo con el qc_name {0} con el siguiente mensaje: {1}", qc_name, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}