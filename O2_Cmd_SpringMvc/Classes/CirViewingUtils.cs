// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.Interfaces;
using FluentSharp.WinFormUI.Utils;
using O2.Core.CIR.Ascx;

namespace O2.Cmd.SpringMvc.Classes
{
    public class CirViewingUtils
    {
        public static void openCirDataFileInCirViewerControl(ICirDataAnalysis cirDataAnalysis, string cirViewerControlName)
        {
            if (cirDataAnalysis != null)
            {
                var ascxCirViewer = (ascx_CirDataViewer) O2AscxGUI.getAscx(cirViewerControlName);
                if (ascxCirViewer != null)
                {
                    ascxCirViewer.loadCirDataAnalysisObject(cirDataAnalysis);
                    ascxCirViewer.updateCirDataStats();
                }
            }

        }

       
    }
}
