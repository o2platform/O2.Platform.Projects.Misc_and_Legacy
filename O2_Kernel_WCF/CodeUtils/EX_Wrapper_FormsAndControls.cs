// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.CoreLib.API;

namespace O2.Kernel.CodeUtils
{
    public static class EX_Wrapper_FormsAndControls
    {
        public static Form openO2Gui(this O2AppDomainFactory o2AppDomainFactory)
        {
            var o2Gui = (Form) o2AppDomainFactory.proxyInvokeInstance("O2_CoreLib", "O2GuiWithDockPanel", "");
            O2Thread.staThread(() => o2Gui.ShowDialog());
            return o2Gui;
        }

        public static void closeO2Gui(this O2AppDomainFactory o2AppDomainFactory)
        {
            o2AppDomainFactory.proxyInvokeStatic("O2_CoreLib", "O2GuiWithDockPanel", "CloseThisForm");
        }

        public static Control loadControl(this O2AppDomainFactory o2AppDomainFactory, Type ascxControlToLoad,
                                          bool showAsFloat, String name)
        {
            return (Control) o2AppDomainFactory.proxyInvokeStatic(
                                 "O2_CoreLib", "O2DockPanel", "loadControl",
                                 new object[] {ascxControlToLoad, showAsFloat, name});
        }

        public static void springExec(this O2AppDomainFactory o2AppDomainFactory, string springFileToLoad)
        {
            O2Thread.staThread(() =>
                                                   o2AppDomainFactory.proxyInvokeStatic("O2_CoreLib", "SpringExec",
                                                                                        "loadConfigAndStartGUI",
                                                                                        new object[] {springFileToLoad})
                );
            //O2Kernel_Lambda.runLambdaOnNewSTAThread(() => 
            //    );            
        }

        //
        //DI.windowsForms.openAscx(typeof(ascx_Unzip), true, "O2 Tool - Unzip files");
    }
}
