// ------------------------------------------------------------------------------------------------
// <copyright file="NLogWriter.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace QuieroCasa.InterfacesCRM.Business.Commons.Logs
{ 
    public class NLogWriter
    {
        private static Logger logger;

        public NLogWriter(string name)
        {
            logger = LogManager.GetLogger(name);
        }
        public void Error(object obj)
        {
            logger.Log(LogLevel.Error, obj);
        }
        public void Error(object obj, Exception exception)
        {
            if (obj != null)
                logger.Log(LogLevel.Error, exception, obj.ToString());
            else
                logger.Log(LogLevel.Error, exception);
        }
        public void Info(object obj)
        {
            logger.Log(LogLevel.Info, obj);
        }
        public void Info(object obj, Exception exception)
        {
            if (obj != null)
                logger.Log(LogLevel.Info, exception, obj.ToString());
            else
                logger.Log(LogLevel.Info, exception);
        }
    }
}