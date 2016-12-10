// ------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHelper.cs" company="Inmobiliaria Quiero Casa, S.A. de C.V.">
//  Copyright (c) 2016-2017 INMOBILIARIA QUIERO CASA, S.A. de C.V., All rights reserved.
// </copyright>
// ------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace QuieroCasa.InterfacesCRM.Business.Commons.Exceptions
{
    public class ExceptionHelper
    {
        public static string GetErrorMessage(Exception error, bool returnWithStackTrace)
        {
            if (error.InnerException is FaultException)
            {
                if (returnWithStackTrace)
                {
                    return ((FaultException)error.InnerException).ToString();
                }
                else
                {
                    return ((FaultException)error.InnerException).Message;
                }
            }
            else if (error.InnerException != null)
            {
                if (returnWithStackTrace)
                {
                    return error.InnerException.ToString();
                }
                else
                {
                    return error.InnerException.Message;
                }
            }
            else
            {
                if (returnWithStackTrace)
                {
                    return error.ToString();
                }
                else
                {
                    return error.Message;
                }
            }
        }
    }
}