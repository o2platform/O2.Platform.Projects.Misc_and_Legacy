// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;

namespace O2.Core.FileViewers
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;// new WinFormsUILog();            
            config = PublicDI.config; //new KO2Config();    
            reflection = PublicDI.reflection;
        }

        public static IO2Log log { get; set; }
        public static KO2Config config { get; set; }
        public static KReflection reflection { get; set; }

        
    }
}
