// ------------------------------------------------------------------------------------------------
// <copyright file="IncomingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using QuieroCasa.InterfacesCRM.Business;
using QuieroCasa.InterfacesCRM.Data.Entities;

namespace QuieroCasa.InterfacesCRM.Services
{
    /// <summary>
    /// Clase que nos permite el tratamiento del registro y actualización de Llamadas Entrantes.
    /// </summary>
    [WebService(Namespace = "http://quierocasa.com.mx/InterfacesCRM/", Description = "Web Services CRM Quiero Casa  para Llamadas Entrantes")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class IncomingCall : System.Web.Services.WebService
    {
        QuieroCasa.InterfacesCRM.Business.IncomingCall callIn = new QuieroCasa.InterfacesCRM.Business.IncomingCall();

        [WebMethod]
        public ResponseIncomingCall RegisterIncomingCall(string callerId, int typeCall, DateTime? dateTimeStart, string username, string callId, string urlNimbus, string dialedNumber, string urlRecording)
        {
            return callIn.RegisterIncomingCall(callerId, typeCall, dateTimeStart, username, callId, urlNimbus, dialedNumber, urlRecording);
        }
        [WebMethod]
        public ResponseIncomingCall UpdateIncomingCall(string caseId, DateTime dateTimeClosing, string urlRecording)
        {
            return callIn.UpdateIncomingCall(caseId, dateTimeClosing, urlRecording);
        }
    }
}