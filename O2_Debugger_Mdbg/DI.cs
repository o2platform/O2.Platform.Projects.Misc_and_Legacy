// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using O2.Debugger.Mdbg.O2Debugger.Objects;
using O2.External.O2Mono;

namespace O2.Debugger.Mdbg
{
    internal class DI
    {
        static DI()
        {
            log = PublicDI.log;            
            config = new KO2Config();
            reflection = new KReflection();
            showMdbgDebugMessages = false;
        }

        // DI Targets
        public static KReflection reflection { get; set; }
        public static KO2Log log { get; set; }        
        public static KO2Config config { get; set; }

        public static O2MonoCecil o2MonoCecil;


        // Global Objects
        public static O2MDbg o2MDbg { get; set; }
        public static bool showMdbgDebugMessages { get; set; }
    }
}
