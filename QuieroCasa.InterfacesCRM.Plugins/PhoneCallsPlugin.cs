using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace QuieroCasa.InterfacesCRM.Plugins
{
    public class PhoneCallsPlugin : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            try
            {
                if (context.InputParameters != null)
                {
                    if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
                    {
                        Entity entity = (Entity)context.InputParameters["Target"];

                        if (entity.LogicalName == "phonecall")
                        {
                            if (entity.Attributes.Contains("activityId") == true)
                            {
                                Guid userID = context.UserId;
                                tracingService.Trace("activityId: {0}", entity.Attributes["activityId"]);
                                tracingService.Trace("userID: {0}", userID);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tracingService.Trace("PhoneCallsPlugin Error: {0}", ex.ToString());
                context = null;
                throw;
            }
        }
    }
}