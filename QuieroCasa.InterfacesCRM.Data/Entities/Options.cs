// ------------------------------------------------------------------------------------------------
// <copyright file="Options.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Data.Entities
{
    public enum PriorityCode
    {
        Critica = 0,
        Alta = 1,
        Normal = 2,
        Baja = 3
    }
    public enum CaseOriginCode
    {
        Telefono = 1,
        CorreoElectronico = 2,
        Web = 3,
        Facebook = 2483,
        Twitter = 3986
    }
    public enum CaseTypeCode
    {
        Pregunta = 1,
        Problema = 2,
        Solicitud = 3
    }
    public enum Department
    {
        Ventas = 960760000,
        PostVenta = 960760001,
        AtencionaClientes = 960760002,
        Otro = 960760003
    }
    public enum CaseCreatedBy
    {
        Llamada = 960760000,
        Correo = 960760001
    }
    public enum CalledStatus
    {
        SinAtender = 960760000,
        EnCurso = 960760001,
        Resuelto = 960760002
    }
}