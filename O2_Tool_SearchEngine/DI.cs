// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.WinForms.Interfaces;
using FluentSharp.WinForms.Utils;


namespace O2.Tool.SearchEngine
{
    class DI
    {
        static DI()
        {
            log = PublicDI.log;// new WinFormsUILog();            
            config = PublicDI.config; //new KO2Config();
            
            searchEngineAPI = new  FluentSharp.CoreLib.API.SearchEngine();            
            o2MessageQueue = KO2MessageQueue.getO2KernelQueue();            
        }
        
        public static IO2Log log { get; set;}
        public static KO2Config config { get; set;}
        public static  FluentSharp.CoreLib.API.SearchEngine searchEngineAPI { get; set; }        

        public static IO2MessageQueue o2MessageQueue;

        //public static KReflection reflection = new KReflection();
    }
}
