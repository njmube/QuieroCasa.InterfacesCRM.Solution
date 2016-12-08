using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace QuieroCasa.InterfacesCRM.Business.Commons.Logs
{
    public class HostLogger
    {
        public static NLogWriter Get<T>() where T : class
        {
            return Get(typeof(T).FullName);
        }
        public static NLogWriter Get(string name)
        {
            try
            {
                return new NLogWriter(name);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}