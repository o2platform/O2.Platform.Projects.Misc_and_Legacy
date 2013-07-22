// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.WinForms.Controls;
using O2.ImportExport.OunceLabs.Ozasmt_OunceV6;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_FindingsCreator : UserControl
    {
        public ascx_FindingsCreator()
        {
            ascx_FindingsViewer.o2AssessmentLoadEngines.Add(new O2AssessmentLoad_OunceV6());
            ascx_FindingsViewer.o2AssessmentSave = new O2AssessmentSave_OunceV6();
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ascx_FindingsCreator_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void llMakeTraceIntoFindings_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            makeDynamicTraceIntoRealFinding();            
        }

              
    }
}
