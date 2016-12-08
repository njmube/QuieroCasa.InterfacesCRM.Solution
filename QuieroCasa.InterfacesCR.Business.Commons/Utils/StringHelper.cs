using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuieroCasa.InterfacesCRM.Business.Commons.Utils
{
    public class StringHelper
    {
        public static string GetURLConnectionString(string connectionString)
        {
            try
            {
                if (!connectionString.Contains("Url"))
                {
                    return string.Empty;
                }

                string url = string.Empty;
                string[] configurations = connectionString.Split(';');

                foreach (string c in configurations)
                {
                    if(c.Contains("Url"))
                    {
                        url = c.Split('=')[1];
                        return url;
                    }
                }

                return url;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}