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

namespace QuieroCasa.InterfacesCRM.Business
{
    public class OutgoingCall : IOutgoingCall
    {
        public int RequestOutgoingCall(string contactId, string callerId, string address, string guidCall)
        {
            throw new NotImplementedException();
        }
    }
}
