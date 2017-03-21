// ------------------------------------------------------------------------------------------------
// <copyright file="IncidentsManagement.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using QuieroCasa.InterfacesCRM.Data.Entities;
using QuieroCasa.InterfacesCRM.Business.Commons.Logs;
using QuieroCasa.InterfacesCRM.Business.Commons.Exceptions;
using System.Configuration;

namespace QuieroCasa.InterfacesCRM.Business.Logic
{
    public class IncidentsManagement
    {
        static readonly NLogWriter log = HostLogger.Get<ContactsManagement>();

        public string Add(IOrganizationService organizationServiceProxy, string usernameNimbus, int priorityCode, int caseOriginCode, int caseTypeCode, string urlNimbus
            , string contactId, string area, DateTime? dateTimeStart, string callerId, string timeStart, int caseCreatedBy, int identifiedContacts
            , string callId, string identifiedPersonId, int typeCall, string urlRecording)
        {
            try
            {
                List<ContactDTO> listContacts = new List<ContactDTO>();

                Incident newCase = new Incident()
                {   
                    PriorityCode = new OptionSetValue(priorityCode),
                    CaseOriginCode = new OptionSetValue(caseOriginCode),
                    CaseTypeCode = new OptionSetValue(caseTypeCode),
                    its_urldegrabacion = string.IsNullOrEmpty(urlNimbus) ? null : urlNimbus,
                    CustomerId = string.IsNullOrEmpty(contactId) ? null : new EntityReference(Contact.EntityLogicalName, new Guid(contactId)),
                    qc_areadepartamento = string.IsNullOrEmpty(area) ? null : new EntityReference("qc_area", new Guid(area)),
                    its_fechayhoradeinicio = dateTimeStart,
                    its_telefono = callerId,
                    its_horadeinicio = timeStart,
                    its_casocreadopor = new OptionSetValue(caseCreatedBy),
                    its_iddelallamada = callId,
                    its_contactosidentificados = identifiedContacts,
                    qc_personaidentificadaportelefono = string.IsNullOrEmpty(identifiedPersonId) ? null : new EntityReference(Contact.EntityLogicalName, new Guid(identifiedPersonId)),
                    its_tipodellamada = new OptionSetValue(typeCall),
                    qc_usuarionimbus = string.IsNullOrEmpty(usernameNimbus) ? null : usernameNimbus,
                    qc_urldegrabacion = string.IsNullOrEmpty(urlRecording) ? null : urlRecording
                };

                Guid incidentId = organizationServiceProxy.Create(newCase);
                return incidentId.ToString();
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del caso del contacto {0} con el siguiente mensaje: {1}", contactId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        public bool Update(IOrganizationService organizationServiceProxy, string caseId, DateTime dateTimeClosing, string urlRecording, string timeEnd)
        {
            try
            {
                Incident updateCase = new Incident()
                {
                    IncidentId = new Guid(caseId),
                    qc_urldegrabacion = urlRecording,
                    its_fechayhoradecierre = dateTimeClosing,
                    its_horadecierre = timeEnd
                };

                organizationServiceProxy.Update(updateCase);
                return true;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la actualización del caso {0} fecha y hora de cierre {1} url de grabación {2} con el siguiente mensaje: {3}", caseId, dateTimeClosing, urlRecording, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        public string Add(IOrganizationService organizationServiceProxy, string contactId, string its_personanoidentificada, string its_correoelectronico, string its_personaidentificada, string area, string telefono, int? contactosIdentificados, string its_paquete, string desarrollo, string qc_personaidentificadaporvivienda, string qc_personaidentificadaportelefono, string its_postventa, string comentarios
            , bool? sala, bool? comedor, bool? cocina, bool? patioServicio, bool? bano, bool? recamara, bool? cajonEstacionamiento, bool? bodega, bool? areasComunes, bool? otro, string especificar, DateTime? fechaNacimiento, bool? esCliente)
        {
            try
            {
                List<ContactDTO> listContacts = new List<ContactDTO>();

                string title = ConfigurationManager.AppSettings["title"].ToString();

                Incident newCase = new Incident()
                {
                    Title = title,
                    PriorityCode = new OptionSetValue(1),
                    CaseOriginCode = new OptionSetValue(3),
                    CaseTypeCode = new OptionSetValue(3),
                    its_casocreadopor = new OptionSetValue(960760001),
                    Description = comentarios,
                    CustomerId = new EntityReference(Contact.EntityLogicalName, new Guid(contactId)),
                    qc_areadepartamento = new EntityReference("qc_area", new Guid(area)),
                    its_telefono = telefono,
                    its_personanoidentificada = its_personanoidentificada,
                    its_correoelectronico = its_correoelectronico,
                    its_contactosidentificados = contactosIdentificados,
                    its_personaidentificada = string.IsNullOrEmpty(its_personaidentificada) ? null : new EntityReference(Contact.EntityLogicalName, new Guid(its_personaidentificada)),
                    qc_personaidentificadaportelefono = string.IsNullOrEmpty(qc_personaidentificadaportelefono) ? null : new EntityReference(Contact.EntityLogicalName, new Guid(qc_personaidentificadaportelefono)),
                    qc_personaidentificadaporvivienda = string.IsNullOrEmpty(qc_personaidentificadaporvivienda) ? null : new EntityReference(Contact.EntityLogicalName, new Guid(qc_personaidentificadaporvivienda)),
                    its_paquete = string.IsNullOrEmpty(its_paquete) ? null : new EntityReference("qc_paquetes", new Guid(its_paquete)),
                    its_postventa = string.IsNullOrEmpty(its_postventa) ? null : new EntityReference("qc_postventa", new Guid(its_postventa)),
                    its_sala = sala,
                    its_comedor = comedor,
                    its_cocina = cocina,
                    its_patiodeservicio = patioServicio,
                    its_bano = bano,
                    its_recamara = recamara,
                    its_cajondeestaconamiento = cajonEstacionamiento,
                    its_bodega = bodega,
                    its_areascomunes = areasComunes,
                    its_otro = otro,
                    its_otrafallaareportar = especificar,
                    qc_fechanacimientoidentificado = fechaNacimiento,
                    qc_escliente = esCliente,
                    its_desarrollo = string.IsNullOrEmpty(desarrollo) ? null : new EntityReference("qc_desarrollos", new Guid(desarrollo)),
                };

                Guid incidentId = organizationServiceProxy.Create(newCase);
                return incidentId.ToString();
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en el registro del caso del contacto {0} con el siguiente mensaje: {1}", contactId, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
        public IncidentDTO SearchById(IOrganizationService organizationServiceProxy, string incidentid)
        {
            try
            {
                IncidentDTO incident = null;

                QueryExpression sdkIncidentQuery = new QueryExpression
                {
                    EntityName = "incident",
                    ColumnSet = new ColumnSet("ticketnumber"),
                    Criteria = new FilterExpression()
                    {
                        FilterOperator = LogicalOperator.Or,
                        Conditions =
                            {
                                new ConditionExpression("incidentid", ConditionOperator.Equal, incidentid)
                            }
                    }
                };

                EntityCollection incidentCollections = organizationServiceProxy.RetrieveMultiple(sdkIncidentQuery);

                if (incidentCollections.Entities.Count > 0)
                {
                    incident = new IncidentDTO()
                    {
                        incidentid = incidentid,
                        ticketnumber = incidentCollections[0].Attributes.Contains("ticketnumber") ? incidentCollections[0].Attributes["ticketnumber"].ToString() : string.Empty
                    };
                }

                return incident;
            }
            catch (Exception ex)
            {
                log.Error(string.Format("Error en la busqueda del Caso con el incidentid {0} con el siguiente mensaje: {1}", incidentid, ExceptionHelper.GetErrorMessage(ex, false)), ex);
                throw ex;
            }
        }
    }
}