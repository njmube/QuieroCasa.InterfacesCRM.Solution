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
    /// Summary description for IncomingCall
    /// </summary>
    [WebService(Namespace = "http://quierocasa.com.mx/Interfaces/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class IncomingCall : System.Web.Services.WebService
    {
        [WebMethod]
        public ResponseIncomingCall RegisterIncomingCall(string callerId, DateTime dateTimeStart)
        {
            QuieroCasa.InterfacesCRM.Business.IncomingCall call = new QuieroCasa.InterfacesCRM.Business.IncomingCall();
            return call.RegisterIncomingCall(callerId, dateTimeStart);
        }
    }
}