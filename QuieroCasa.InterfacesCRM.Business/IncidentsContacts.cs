// ------------------------------------------------------------------------------------------------
// <copyright file="IncidentsContacts.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuieroCasa.InterfacesCRM.Business.Contracts;
using QuieroCasa.InterfacesCRM.Business.Logic;
using QuieroCasa.InterfacesCRM.Data.Entities;
using Microsoft.Xrm.Sdk.Client;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using QuieroCasa.InterfacesCRM.Business.Commons.Utils;
using System.Configuration;
using System.Data.SqlClient;
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Business
{
    public class IncidentsContacts : IIncidentsContacts
    {
        static readonly NLogWriter log = HostLogger.Get<IncomingCall>();

        public ResponseIncidentsContacts RegisterIncidentContact(string nombres, string apPaterno, string apMaterno, DateTime? fechaNacimiento, string correo, string telefono, int tipoSolicitud, string desarrollo, string comentarios
            , bool? sala, bool? comedor, bool? cocina, bool? patioServicio, bool? bano, bool? recamara, bool? cajonEstacionamiento, bool? bodega, bool? areasComunes, bool? otro, string especificar, bool? esCliente)
        {
            StringBuilder sbLog = new StringBuilder();
            StringBuilder sbTrace = new StringBuilder();
            
            IOrganizationService organization = null;
            ConnectionsManagement conn = new ConnectionsManagement();
            ResponseIncidentsContacts response = new ResponseIncidentsContacts();
            ContactsManagement contacts = new ContactsManagement();
            IncidentsManagement incidents = new IncidentsManagement();
            PaquetesManagement paquetes = new PaquetesManagement();
            PaqueteDTO paquete = new PaqueteDTO();
            CarritoManagement carritos = new CarritoManagement();
            CarritoDTO carrito = new CarritoDTO();
            DesarrolloDTO desa = new DesarrolloDTO();
            DesarrollosManagement desarrollos = new DesarrollosManagement();
            PostVentaDTO postVenta = new PostVentaDTO();
            PostVentaManagement postVentas = new PostVentaManagement();
            string area = string.Empty;
            string paqueteId = string.Empty;
            string postVentaId = string.Empty;
            string parametros = string.Empty;

            try
            {
                sbTrace.AppendLine("0. Iniciando el procesamiento iniciando conexion ...");

                organization = conn.GetOrganizationServiceProxy();

                parametros = string.Format("Parametros recibidos: nombres {0}, apPaterno {1}, apMaterno {2}, fechaNacimiento {3}, correo {4}, telefono {5}, tipoSolicitud {6}, desarrollo {7}, comentarios {8}, sala {9}, comedor {10}, cocina {11}, patioServicio {12},  bano {13}, recamara {14}, cajonEstacionamiento {15}, bodega {16}, areasComunes {17}, otro {18}, especificar {19} \n", nombres, apPaterno, apMaterno, fechaNacimiento, correo, telefono, tipoSolicitud, desarrollo, comentarios, sala, comedor, cocina, patioServicio, bano, recamara, cajonEstacionamiento, bodega, areasComunes, otro, especificar);
                sbLog.AppendLine(parametros);
                log.Info(sbLog.ToString());
                sbLog = new StringBuilder();

                sbTrace.AppendLine(string.Format("1. Buscando contactos por el email {0}", correo));

                string its_personaidentificada = string.Empty;
                response.ListIdentifiedPersons = contacts.SearchByEmail(organization, correo);

                if (response.ListIdentifiedPersons.Count > 0)
                {
                    its_personaidentificada = response.ListIdentifiedPersons[0].contactid;
                }

                sbTrace.AppendLine(string.Format("2. Buscando contactos por el telefono {0}", telefono));

                string qc_personaidentificadaportelefono = string.Empty;
                response.ListContacts = contacts.SearchByCallerId(organization, telefono);

                if (response.ListContacts.Count == 1)
                {
                    qc_personaidentificadaportelefono = response.ListContacts[0].contactid;
                }

                sbTrace.AppendLine("Obteniendo configuraciones desde web.config");

                string contactId = ConfigurationManager.AppSettings["defaultContactId"].ToString();
                string areaIdCallCenter = ConfigurationManager.AppSettings["areaIdCallCenter"].ToString();
                string areaIdCustomerService = ConfigurationManager.AppSettings["areaIdCustomerService"].ToString();


                string areaIdSales = ConfigurationManager.AppSettings["areaIdSales"].ToString();
                string areaIdOthers = ConfigurationManager.AppSettings["areaIdOthers"].ToString();
                string areaIdPostVenta = ConfigurationManager.AppSettings["areaIdPostVenta"].ToString();

                sbTrace.AppendLine(string.Format("3. Obteniendo información del desarrollo {0}", desarrollo));

                string desarrolloKey = string.Empty;

                if (!string.IsNullOrEmpty(desarrollo))
                {
                    desarrolloKey = desarrollo.Substring(0, 3);
                    desa = desarrollos.SearchByName(organization, desarrolloKey);
                }
                
                string qc_personaidentificadaporvivienda = string.Empty;

                sbTrace.AppendLine(string.Format("4. Obteniendo información del tipo de solicitud {0}", tipoSolicitud));

                switch (tipoSolicitud)
                {
                    case 1: // Ventas
                        area = areaIdSales; // "112D12F5-7ACC-E611-8122-0050568A0735";
                        break;
                    case 2: // Post-venta
                        area = areaIdPostVenta; // "5363FDFB-7ACC-E611-8122-0050568A0735";

                        sbTrace.AppendLine(string.Format("4.1 Obteniendo información para postventa del desarrollo {0}", desarrollo));

                        postVenta = postVentas.SearchByName(organization, desarrollo);

                        if (postVenta != null)
                        {
                            postVentaId = postVenta.qc_postventaId;
                            qc_personaidentificadaporvivienda = postVenta.qc_contactoid;
                        }

                        break;
                    case 3: // Atención a Clientes
                        area = areaIdCustomerService;  //"87B04607-7BCC-E611-8122-0050568A0735";

                        sbTrace.AppendLine(string.Format("4.2 Obteniendo información para atencion a clientes del desarrollo {0}", desarrollo));

                        if (esCliente.Value)
                        {
                            paquete = paquetes.SearchByName(organization, desarrollo);

                            if (paquete != null)
                            {
                                paqueteId = paquete.qc_paquetesid;
                                carrito = carritos.SearchByPaqueteId(organization, paquete.qc_paquetesid);

                                if (carrito != null)
                                {
                                    qc_personaidentificadaporvivienda = carrito.qc_busquedadecontactocarritos;
                                }
                            }
                        }
                        else
                        {
                            carrito = new CarritoDTO();
                            desa = new DesarrolloDTO();
                            paqueteId = string.Empty;
                        }

                        break;
                    case 4: // Otro
                        area = areaIdOthers; // "49B7060E-7BCC-E611-8122-0050568A0735";
                        break;
                }

                if (tipoSolicitud != 2)
                {
                    sala = null;
                    comedor = null;
                    cocina = null;
                    patioServicio = null;
                    bano = null;
                    recamara = null;
                    cajonEstacionamiento = null;
                    bodega = null;
                    areasComunes = null;
                    otro = null;
                    especificar = string.Empty;
                }

                sbTrace.AppendLine(string.Format("5. Registrando información del Incidente para el area {0}", area));

                response.caseId = incidents.Add(organization, contactId, nombres + " " + apPaterno + " " + apMaterno, correo, its_personaidentificada, area, telefono, response.ListContacts.Count, paqueteId, desa.qc_desarrollosid, qc_personaidentificadaporvivienda, qc_personaidentificadaportelefono, postVentaId, comentarios
                    , sala, comedor, cocina, patioServicio, bano, recamara, cajonEstacionamiento, bodega, areasComunes, otro, especificar, fechaNacimiento, esCliente);

                response.ticketnumber = incidents.SearchById(organization, response.caseId).ticketnumber;

                response.urlCase = string.Format(StringHelper.GetURLConnectionString(ConfigurationManager.ConnectionStrings["CRMOnline"].ConnectionString) + "/main.aspx?etn=incident&pagetype=entityrecord&id=%7b{0}%7d", response.caseId);
                response.codError = 0;
                response.error = string.Empty;
                response.success = true;
                response.message = "Caso creado exitosamente";
                sbTrace.AppendLine(string.Format("6. Caso creado exitosamente con el numero de ticket {0}", response.ticketnumber));

                sbLog.AppendFormat("Caso creado exitosamente con el CaseId {0} para el Contacto {1} con un total de {2} coincidencias encontradas. URL generada: {3} con los parametros de entrada \n", response.caseId, contactId, response.ListContacts.Count, response.urlCase);

                log.Info(sbLog.ToString());
            }
            catch (Exception ex)
            {
                sbTrace.AppendLine(string.Format("7. Error al crear el Caso: {0}", ex.ToString()));

                LogErroresManagement.Add(organization, "Error al reportar el Incidente para el desarrollo: " + desarrollo + " .Error: " + ExceptionHelper.GetErrorMessage(ex, false), nombres + " " + apPaterno + " " + apMaterno, correo, telefono, area, paqueteId, desa.qc_desarrollosid);

                log.Error(string.Format("Error en el registro de incidente del contacto con el nombre: {0} apellido paterno {1}", nombres, apPaterno, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                response.codError = 6001;
                response.error = ExceptionHelper.GetErrorMessage(ex, false);
                response.success = false;
                response.message = "El caso no pudo crearse. Intente más tarde";
                response.caseId = Guid.Empty.ToString();
                response.urlCase = string.Empty;
                response.ListContacts = null;
            }

            finally
            {
                string debugTicket = ConfigurationManager.AppSettings["debugTicket"].ToString();

                if (debugTicket.Equals("1"))
                {
                    log.Info(sbTrace.ToString());
                }
                
            }

            return response;
        }
    }
}