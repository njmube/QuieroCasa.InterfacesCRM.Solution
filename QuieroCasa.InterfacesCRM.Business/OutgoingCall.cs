// ------------------------------------------------------------------------------------------------
// <copyright file="OutgoingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;
using QuieroCasa.InterfacesCRM.Business.Logic;
using QuieroCasa.InterfacesCRM.Data.Entities;
using Microsoft.Xrm.Sdk.Client;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using QuieroCasa.InterfacesCRM.Business.Commons.Utils;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class OutgoingCall : IOutgoingCall
    {
        static readonly NLogWriter log = HostLogger.Get<IncomingCall>();

        public ResponseOutgoingCall UpdateOutgoingCall(string activityId, int statusCall, DateTime dateTimeStart, DateTime dateTimeClosing, int callDuration, string callURL, string callId)
        {
            ResponseOutgoingCall response = new ResponseOutgoingCall();
            IOrganizationService organization = null;
            ConnectionsManagement conn = new ConnectionsManagement();
            PhoneCallsManagement phoneCall = new PhoneCallsManagement();

            try
            {
                organization = conn.GetOrganizationServiceProxy();

                response.success = phoneCall.Update(organization, activityId, statusCall, dateTimeStart, dateTimeStart.ToString("HH:mm:ss"), dateTimeClosing, dateTimeClosing.ToString("HH:mm:ss"), callDuration, callURL, callId);
                response.codError = 0;
                response.error = string.Empty;
                response.message = "Llamada telefónica actualizada exitosamente";
                response.activityId = activityId;
                log.Info(string.Format("Llamada telefónica actualizado exitosamente: activityId {0}, estatus de la llamada {1}, fecha y hora de inicio {2}, fecha y hora de cierre {3}", activityId, statusCall, dateTimeStart, dateTimeClosing));
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la actualización de la Llamada Telefónica con el activityId {0}, estatus de la llamada {1}, fecha y hora de inicio {2}, fecha y hora de cierre {3}", activityId, statusCall, dateTimeStart, dateTimeClosing, ExceptionHelper.GetErrorMessage(ex, false)), ex);

                response.codError = 5010;
                response.error = ExceptionHelper.GetErrorMessage(ex, false);
                response.success = false;
                response.message = "La Llamada Telefónica no pudo actualizarse. Intente más tarde";
                response.activityId = string.Empty;
            }
            finally
            {
                organization = null;
                conn = null;
                phoneCall = null;
            }

            return response;
        }
    }
}