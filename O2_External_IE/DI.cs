using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;

namespace O2.External.IE
{
    class DI
    {

        static DI()
        {
            config = PublicDI.config;
            log = PublicDI.log;
            reflection = PublicDI.reflection;
        }

        // DI which will need to be injected 

        public static KO2Config config { get; set; }
        public static IO2Log log { get; set; }

        public static KReflection reflection;

    }
}