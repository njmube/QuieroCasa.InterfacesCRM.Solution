// ------------------------------------------------------------------------------------------------
// <copyright file="IncidentDTO.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class IncidentDTO
    {
        public string ticketnumber { get; set; }
        public string incidentid { get; set; }
    }
}