// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.CoreLib.API;
using FluentSharp.WinForms.Utils;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_Breakpoints : UserControl
    {
        public ascx_Breakpoints()
        {
            InitializeComponent();
        }

        private void btCreateBreakPoint_Click(object sender, EventArgs e)
        {
            addBreakpoint(tbBreakPointSignature.Text);
        }

        private void llRefreshBreakpointList_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            refreshBreakPointList();
        }


        private void tbBreakPointSignature_TextChanged(object sender, EventArgs e)
        {
            if (cbAutoAddBreakPointsOnSignatureChange.Checked)
                addBreakpoint(tbBreakPointSignature.Text);
        }



        

        private void ascx_Breakpoints_Load(object sender, EventArgs e)
        {
            onLoad();            
        }
        

        private void ascx_Breakpoints_Enter(object sender, EventArgs e)
        {
            refreshBreakPointList();
        }

        private void lbRemoveSelectedBreakpoint_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
        }

        private void llDeleteAllBreakpoints_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            O2Thread.mtaThread(() =>
            {
                DI.o2MDbg.BreakPoints.deleteAddBreakpoints();
                refreshBreakPointList();
            });
        }

        private void cbAutoContinueOnBreakPointEvent_CheckedChanged(object sender, EventArgs e)
        {
            DI.o2MDbg.AutoContinueOnBreakPointEvent = cbAutoContinueOnBreakPointEvent.Checked;
        }

        private void lbArchivedBreakpoints_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbArchivedBreakpoints.SelectedItem is String)
            {
                var breakpointSignature = lbArchivedBreakpoints.SelectedItem.ToString();
                var lastIndexOfColon = breakpointSignature.LastIndexOf(':');
                if (lastIndexOfColon > -1)
                {
                    var fileName = breakpointSignature.Substring(0, lastIndexOfColon);
                    var lineNumberAsString = breakpointSignature.Substring(lastIndexOfColon+1);
                    int lineNumber;
                    if (Int32.TryParse(lineNumberAsString, out lineNumber))
                        O2Messages.fileOrFolderSelected(fileName, lineNumber);
                }               
            }
        }

       
          

    }
}
