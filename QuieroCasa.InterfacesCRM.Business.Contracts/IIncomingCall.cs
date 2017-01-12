// ------------------------------------------------------------------------------------------------
// <copyright file="IIncomingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using QuieroCasa.InterfacesCRM.Data.Entities;
using System;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IIncomingCall
    {
        ResponseIncomingCall RegisterIncomingCall(string callerId, int typeCall, DateTime dateTimeStart, string username, string callId, string urlNimbus, string dialedNumber);
        ResponseIncomingCall UpdateIncomingCall(string caseId, DateTime dateTimeClosing, string urlRecording);
    }
}