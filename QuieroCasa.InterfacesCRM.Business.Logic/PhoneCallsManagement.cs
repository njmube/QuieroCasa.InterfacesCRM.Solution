// ------------------------------------------------------------------------------------------------
// <copyright file="PhoneCallsManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using QuieroCasa.InterfacesCRM.Data.Entities;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class PhoneCallsManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public bool Update(IOrganizationService organizationServiceProxy, string activityId, int statusCall, DateTime dateTimeStart, string timeStart, DateTime dateTimeClosing, string timeEnd, int callDuration, string callURL, string callId)
        {
            try
            {
                PhoneCall updatePhoneCall = new PhoneCall
                {
                    ActivityId = new Guid(activityId),
                    qc_estatusllamadasnimbus = new OptionSetValue(GetOptionStatusCall(statusCall)),
                    its_fechadeinicio = dateTimeStart,
                    its_horadeinicio = timeStart,
                    its_fechadecierre = dateTimeClosing,
                    its_horadecierre = timeEnd,
                    ActualDurationMinutes = callDuration,
                    qc_urldellamada = callURL,
                    qc_iddellamada = callId
                };

                organizationServiceProxy.Update(updatePhoneCall);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la actualización de la llamada telefonica con activityId {0} con el estatus {1}, fecha y hora de inicio {2}, fecha y hora de cierre con el siguiente mensaje: {3}", activityId, dateTimeStart, dateTimeClosing, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        private int GetOptionStatusCall(int option)
        {
            switch (option)
            {
                case 1:
                    return (int)CalledStatusNimbus.Contestado;
                case 2:
                    return (int)CalledStatusNimbus.Ocupado;
                case 3:
                    return (int)CalledStatusNimbus.Cancelado;
                default:
                    return 0;
            }
        }
    }
}