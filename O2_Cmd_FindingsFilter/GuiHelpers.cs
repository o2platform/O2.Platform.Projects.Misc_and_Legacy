// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using FluentSharp.REPL.Controls;
using FluentSharp.REPL.Utils;
using FluentSharp.WinFormUI.Utils;
using O2.Cmd.FindingsFilter.Filters;

namespace O2.Cmd.FindingsFilter
{
    public class GuiHelpers
    {        

      /*  public static void openScriptEditor()
        {

            
        }*/

        public static void gui()
        {
            O2Thread.mtaThread(
                () =>
                    {                        
                        if (O2AscxGUI.launch("Findings Filter GUI"))
                        {
                            openFindingsViewerAndSourceCodeEditor();
                            // enable opening findings file references
                            HandleO2MessageOnSD.setO2MessageFileEventListener();
                            /*KO2MessageQueue.getO2KernelQueue().onMessages += o2Message => HandleO2MessageOnSD.
                                                                                              o2MessageHelper_Handle_IM_FileOrFolderSelected
                                                                                              (o2Message,
                                                                                               null);*/

                        }
                    });
        }

        [O2CmdHide]
        public static void openFindingsViewerAndSourceCodeEditor()
        {
            var sourceCodeEditorControlName = "Scripts Editor for Findings Filter";
            var findingsViewerControlName = "Findings Filter";

            openFindingsFilterControl(findingsViewerControlName);

            openSourceCodeEditorControl(sourceCodeEditorControlName);

            loadSampleScripts(sourceCodeEditorControlName, typeof(CustomScripts));
            O2DockUtils.setDockContentPosition(findingsViewerControlName);
        }

        [O2CmdHide]
        public static void openFindingsFilterControl(string findingsViewerControlName)
        {
            O2AscxGUI.openAscx(typeof(Ascx.ascx_FindingsFilter), O2DockState.Document,
                                               findingsViewerControlName);
        }

        [O2CmdHide]
        public static void openSourceCodeEditorControl(string sourceCodeEditorControlName)
        {
            O2AscxGUI.openAscx(typeof(SourceCodeEditor), O2DockState.Document,
                                               sourceCodeEditorControlName);

        }

        [O2CmdHide]
        public static void loadSampleScripts(string findingsViewerControlName, Type typeWithSampleScripts)
        {
            //findingsViewerControlName.invokeOnAscx("loadSampleScripts", new object[] {  typeWithSampleScripts});
            //findingsViewerControlName.invokeOnAscx("compileSourceCode");
            O2AscxGUI.invokeOnAscxControl(findingsViewerControlName, "loadSampleScripts", new object[] {  typeWithSampleScripts});
            O2AscxGUI.invokeOnAscxControl(findingsViewerControlName, "compileSourceCode");
        }
    }
}
