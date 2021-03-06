// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;

namespace O2.RnD.SpringMVCAnalyzer
{
    class DI
    {
        public static IO2Log log = PublicDI.log;
        public static KO2Config config = PublicDI.config;
        public static KReflection reflection = PublicDI.reflection;
    }
}
