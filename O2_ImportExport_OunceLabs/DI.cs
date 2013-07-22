// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;

namespace O2.ImportExport.OunceLabs
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;
            reflection = PublicDI.reflection;  // new O2FormsReflectionASCX();
            config = PublicDI.config;
        }

        // DI Targets
        //public static IReflectionASCX reflection { get; set; }
        public static KReflection reflection { get; set; }
        public static IO2Log log { get; set; }        
        public static KO2Config config { get; set; }
        
    }
}
