// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using O2.Kernel.WCF.classes;
using O2.Kernel.WCF.Interfaces;

namespace O2.Kernel.WCF
{
    public class WCF_DI
    {
        static WCF_DI()
        {
            log = PublicDI.log;
            reflection = PublicDI.reflection; 
            config = PublicDI.config;            
        }

        // DI Targets
        //public static IReflectionASCX reflection { get; set; }
        public static KReflection reflection { get; set; }
        public static IO2Log log { get; set; }
        public static KO2Config config { get; set; }

        public static O2Shell o2Shell { get; set; }
        

        public static O2GenericWcfHost<IO2WcfKernelMessage> o2WcfKernelMessage { get; set; }

    }
}
