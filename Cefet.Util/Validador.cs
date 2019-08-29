using System;
using System.Collections.Generic;
using System.Text;

namespace Cefet.Util
{
    public class Validador
    {
        static Validador singleton = null;
        static readonly object padlock = new object();
        /// <summary>
        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
        /// for more details about its implementation.
        /// </summary>
        public static Validador Instance
        {
            get
            {
                lock (padlock)
                {
                    if (singleton == null)
                        singleton = new Validador();

                    return singleton;
                }
            }
        }

        Validador()
        {
        }

        public bool isNotNull(Object value)
        {
            try
            {
                value.ToString();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool isInteger(Object value)
        {
            try
            {
                int.Parse(value.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
