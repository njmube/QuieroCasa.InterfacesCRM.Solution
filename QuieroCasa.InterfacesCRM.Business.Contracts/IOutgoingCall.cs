// ------------------------------------------------------------------------------------------------
// <copyright file="IOutgoingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IOutgoingCall
    {
        int RequestOutgoingCall(string contactId, string callerId, string address, string guidCall);
    }
}