// ------------------------------------------------------------------------------------------------
// <copyright file="PaqueteDTO.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
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
    public class PaqueteDTO
    {
        public string qc_paquetesid { get; set; }
        public string qc_name { get; set; }
    }
}