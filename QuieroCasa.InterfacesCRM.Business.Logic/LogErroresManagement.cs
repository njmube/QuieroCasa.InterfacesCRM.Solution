// ------------------------------------------------------------------------------------------------
// <copyright file="LogErroresManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    public class LogErroresManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public static void Add(IOrganizationService organizationServiceProxy, string mensajeError, string persona, string its_correoelectronico, string telefono, string its_area, string its_paquete, string its_desarrollo)
        {
            try
            {
                Entity logErrores = new Entity("its_logerrorespw");
                logErrores["its_fechayhoradeerror"] = DateTime.Now;
                logErrores["its_name"] = mensajeError;
                logErrores["its_nombrepersona"] = persona;
                logErrores["its_correoelectronico"] = its_correoelectronico;
                logErrores["its_telefono"] = telefono;

                EntityReference area = new EntityReference("qc_area", new Guid(its_area));
                logErrores["its_area"] = area;

                EntityReference paquete = new EntityReference("qc_paquetes", new Guid(its_paquete));
                logErrores["its_paquete"] = paquete;

                EntityReference desarrollo = new EntityReference("qc_desarrollos", new Guid(its_desarrollo));
                logErrores["its_desarrollo"] = desarrollo;

                organizationServiceProxy.Create(logErrores);
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del log de errores con el siguiente mensaje: {0}", ExceptionHelper.GetErrorMessage(ex, false)), ex);
            }
        }
    }
}