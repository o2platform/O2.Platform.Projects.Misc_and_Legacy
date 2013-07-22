// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

//using O2.External.IKVM;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.REPL.Controls;
using FluentSharp.WinFormUI.Utils;
using O2.External.Python;
using O2.External.Python.Ascx;

namespace O2.Tool.Python
{
    static class Program
    {       
        static void Main(string[] args)
        {
            // load python execution engines
  //          IKVMEngine.addCurrentIKVMEnginesTo_ascx_SourceCodeEditor();
            ExternalEngines.addCurrentPythonEnginesTo_ascx_SourceCodeEditor();
            if (O2AscxGUI.launch("O2 Tool - Python"))
            {
                O2AscxGUI.openAscx(typeof (ascx_PythonCmdShell), O2DockState.Document, "Python Shell");
                //DI.reflection.loadAssembly();
                O2AscxGUI.addControlToMenu(typeof(ascx_Scripts), O2DockState.Document, "O2 Scripts");
                O2AscxGUI.addControlToMenu(typeof(SourceCodeEditor), O2DockState.Document, "Source Code Editor"); ;
                //O2AscxGUI.openAscx(DI.reflection.getType("O2_External_SharpDevelop.dll", "ascx_Scripts"));
            }

        }
    }
}
