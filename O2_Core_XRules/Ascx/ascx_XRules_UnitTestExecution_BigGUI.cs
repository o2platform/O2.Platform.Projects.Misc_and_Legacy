// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.WinForms.Utils;
using O2.Core.XRules.Classes;

namespace O2.Core.XRules.Ascx
{
    public partial class ascx_XRules_UnitTestExecution_BigGUI : UserControl
    {
        public ascx_XRules_UnitTestExecution_BigGUI()
        {
            InitializeComponent();
        }

        private void llLoadAssembliesToLookForUnitTests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var assembliesWithUnitTests = UnitTestSupport.getAssembliesWithUnitTest(directory_targetAssemblies.getFiles());
            UnitTestExecutionViewHelpers.addAssembliesWithUnitTestsToTreeView(assembliesWithUnitTests, tvAssembliesToLookForUnitTests, true);            
        }
                
        private void ascx_XRules_UnitTestExecution_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void llMapXRulesForUnitTests_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            mapLoadedAssembliesIntoXRules();
            //UnitTestExecutionViewHelpers.mapAssembliesIntoXRules(tvAssembliesToLookForUnitTests, tvXRules);
        }
            
        private void btExecuteSelected_Click(object sender, EventArgs e)
        {
            tvXRules_executeSelectedNode();
        }       
        
        private void tvAssembliesToLookForUnitTests_DragDrop(object sender, DragEventArgs e)
        {
            handleDrop(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }
       
        private void tvAssembliesToLookForUnitTests_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void llClearFlowLayoutPanelWithResults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            flowLayoutPanelWithResults.Controls.Clear();
        }

        private void tvXRules_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void tvXRules_DoubleClick(object sender, EventArgs e)
        {
            tvXRules_executeSelectedNode();
        }                
    }
}
