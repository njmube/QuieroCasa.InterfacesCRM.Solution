// ------------------------------------------------------------------------------------------------
// <copyright file="CarritoManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    public class CarritoManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public CarritoDTO SearchByPaqueteId(IOrganizationService organizationServiceProxy, string qc_paqueteid)
        {
            try
            {
                CarritoDTO carrito = null;

                QueryExpression sdkCarritoQuery = new QueryExpression
                {
                    EntityName = "qc_carrito",
                    ColumnSet = new ColumnSet("qc_busquedadecontactocarritos"),
                    Criteria = new FilterExpression()
                    {
                        FilterOperator = LogicalOperator.Or,
                        Conditions =
                            {
                                new ConditionExpression("qc_paqueteid", ConditionOperator.Equal, qc_paqueteid)
                            }
                    }
                };

                EntityCollection carritoCollections = organizationServiceProxy.RetrieveMultiple(sdkCarritoQuery);

                if (carritoCollections.Entities.Count > 0)
                {
                    foreach (Entity entity in carritoCollections.Entities)
                    {
                        string busqueda = entity.Attributes.Contains("qc_busquedadecontactocarritos") ? ((EntityReference)entity.Attributes["qc_busquedadecontactocarritos"]).Id.ToString() : string.Empty;

                        if (!string.IsNullOrEmpty(busqueda))
                        {
                            carrito = new CarritoDTO
                            {
                                qc_paqueteid = qc_paqueteid,
                                qc_busquedadecontactocarritos = busqueda
                            };

                            break;
                        }
                    }
                }

                return carrito;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda de carrito con el qc_paqueteid {0} con el siguiente mensaje: {1}", qc_paqueteid, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}