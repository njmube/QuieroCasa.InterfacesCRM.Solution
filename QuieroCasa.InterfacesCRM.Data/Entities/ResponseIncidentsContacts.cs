// ------------------------------------------------------------------------------------------------
// <copyright file="ResponseIncidentsContacts.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public class ResponseIncidentsContacts
    {
        public int codError { get; set; }
        public string error { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public string caseId { get; set; }
        public string ticketnumber { get; set; }
        public string urlCase { get; set; }
        public List<ContactDTO> ListContacts { get; set; }
        public List<ContactDTO> ListIdentifiedPersons { get; set; }
    }
}