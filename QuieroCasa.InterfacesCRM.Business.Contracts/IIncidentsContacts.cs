// ------------------------------------------------------------------------------------------------
// <copyright file="IIncidentsContacts.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using QuieroCasa.InterfacesCRM.Data.Entities;
using System;

namespace QuieroCasa.InterfacesCRM.Business.Contracts
{
    public interface IIncidentsContacts
    {
        ResponseIncidentsContacts RegisterIncidentContact(string nombres, string apPaterno, string apMaterno, DateTime? fechaNacimiento, string correo, string telefono, int tipoSolicitud, string desarrollo, string comentarios
            , bool? sala, bool? comedor, bool? cocina, bool? patioServicio, bool? bano, bool? recamara, bool? cajonEstacionamiento, bool? bodega, bool? areasComunes, bool? otro, string especificar, bool? esCliente);
    }
}