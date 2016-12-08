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
}