// ------------------------------------------------------------------------------------------------
// <copyright file="ResponseOutgoingCall.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ResponseOutgoingCall
    {
        public int codError { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string activityId { get; set; }
    }
}