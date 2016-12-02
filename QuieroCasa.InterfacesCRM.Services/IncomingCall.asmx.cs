﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using QuieroCasa.InterfacesCRM.Business;
using QuieroCasa.InterfacesCRM.Data.Entities;

namespace QuieroCasa.InterfacesCRM.Services
{
    /// <summary>
    /// Summary description for IncomingCall
    /// </summary>
    [WebService(Namespace = "http://quierocasa.com.mx/Interfaces/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IncomingCall : System.Web.Services.WebService
    {
        [WebMethod]
        public ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart, string url, string guidCase, string urlCase)
        {
            QuieroCasa.InterfacesCRM.Business.IncomingCall call = new QuieroCasa.InterfacesCRM.Business.IncomingCall();
            return call.RegisterIncomingCall(callerId, dateTimeStart, url, guidCase, url);
        }
    }
}
