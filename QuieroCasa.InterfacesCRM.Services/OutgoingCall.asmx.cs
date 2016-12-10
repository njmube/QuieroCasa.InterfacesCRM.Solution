// ------------------------------------------------------------------------------------------------
// <copyright file="OutgoingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    /// 
    /// </summary>
    [WebService(Namespace = "http://quierocasa.com.mx/InterfacesCRM/", Description = "Web Services CRM Quiero Casa  para Llamadas de Salida")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class OutgoingCall : System.Web.Services.WebService
    {
        QuieroCasa.InterfacesCRM.Business.OutgoingCall callOut = new QuieroCasa.InterfacesCRM.Business.OutgoingCall();

        [WebMethod]
        public int RequestOutgoingCall(string contactId, string callerId, string address, string guidCall)
        {
            return callOut.RequestOutgoingCall(contactId, callerId, address, guidCall);
        }
    }
}