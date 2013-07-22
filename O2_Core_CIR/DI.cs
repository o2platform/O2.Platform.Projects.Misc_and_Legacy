// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using System.IO;
//using O2.Core.CIR.CirObjects;
using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;


namespace O2.Core.CIR
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;            
            reflection = PublicDI.reflection;  // new O2FormsReflectionASCX();
            config = PublicDI.config;
            defaultDirectoryForCirCreationQueue = Path.Combine(DI.config.O2TempDir, "_temp_CirCreationQueue");
            defaultDirectoryForCreatedCirFiles = Path.Combine(DI.config.O2TempDir,"_temp_CreatedCirFiles");            
        }

        // DI Targets
        //public static IReflectionASCX reflection { get; set; }
        public static KReflection reflection { get; set; }
        public static IO2Log log { get; set; }        
        public static KO2Config config { get; set; }


        // local global vars
     //   public static ICirData cirData;
        public static string defaultDirectoryForCirCreationQueue { get; set; }
        public static string defaultDirectoryForCreatedCirFiles { get; set; }
        public static string CONST_NEED_SIGNATURE = "[need signature]";        
    }
}
