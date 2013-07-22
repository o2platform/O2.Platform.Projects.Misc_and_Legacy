// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.WinForms.Interfaces;
using FluentSharp.WinForms.Utils;
using O2.External.O2Mono.MonoCecil;
//using O2.Interfaces.ExternalDlls;

//using O2.Kernel.InterfacesBaseImpl;

namespace O2.External.O2Mono
{
    internal class DI
    {
        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            reflection = new O2FormsReflectionASCX();
            cecilUtils = new CecilUtils();
            monoCecil = new O2MonoCecil();
            assemblyAnalysis = new AssemblyAnalysisImpl();
        }

        public static KO2Config config { get; set; }
        public static IO2Log log { get; set; }
        public static O2FormsReflectionASCX reflection { get; set; }

        // local classes
        public static CecilUtils cecilUtils { get; set; }
        public static IAssemblyAnalysis assemblyAnalysis;// = new AssemblyAnalysisImpl();                
        public static O2MonoCecil monoCecil { get; set; }

    }
}
