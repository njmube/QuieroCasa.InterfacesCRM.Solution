// ------------------------------------------------------------------------------------------------
// <copyright file="IOutgoingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using QuieroCasa.InterfacesCRM.Data.Entities;
using System;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IOutgoingCall
    {
        ResponseOutgoingCall UpdateOutgoingCall(string activityId, int statusCall, DateTime dateTimeStart, DateTime dateTimeClosing, int callDuration, string callURL, string callId);
    }
}