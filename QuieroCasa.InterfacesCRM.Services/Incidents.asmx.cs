// ------------------------------------------------------------------------------------------------
// <copyright file="Incidents.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

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
    /// Clase que nos permite el tratamiento de Incidentes/Casos del CRM.
    /// </summary>
    [WebService(Namespace = "http://quierocasa.com.mx/InterfacesCRM/", Description = "Web Services CRM Quiero Casa  para Incidentes")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Incidents : System.Web.Services.WebService
    {
        QuieroCasa.InterfacesCRM.Business.IncidentsContacts incidents = new QuieroCasa.InterfacesCRM.Business.IncidentsContacts();

        [WebMethod]
        public ResponseIncidentsContacts RegisterIncidentContact(string nombres, string apPaterno, string apMaterno, DateTime fechaNacimiento, string correo, string telefono, int tipoSolicitud, string desarrollo, string comentarios
            ,bool? sala, bool? comedor, bool? cocina, bool? patioServicio, bool? bano, bool? recamara, bool? cajonEstacionamiento, bool? bodega, bool? areasComunes, bool? otro, string especificar)
        {
            return incidents.RegisterIncidentContact(nombres, apPaterno, apMaterno, fechaNacimiento, correo, telefono, tipoSolicitud, desarrollo, comentarios, sala, comedor, cocina, patioServicio, bano,  recamara,  cajonEstacionamiento,  bodega,  areasComunes,  otro, especificar);
        }
    }
}