// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Reflection;
using System.Windows.Forms;
using FluentSharp.WinForms.Utils;

namespace O2.Cmd.FindingsFilter.Ascx
{
    public partial class ascx_FindingsFilter : UserControl
    {
        public ascx_FindingsFilter()
        {
            beforeInitializeComponent();
            InitializeComponent();
        }

                       

        private void lbTargetAssessmentFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void lbAvailableFilters_SelectedIndexChanged(object sender, EventArgs e)
        {
            execSelectedFilter();
        }

        private void execSelectedFilter()
        {
            if (tvAvailableFilters.SelectedNode != null && tvAvailableFilters.SelectedNode.Tag is MethodInfo)
                applyFilter((MethodInfo) tvAvailableFilters.SelectedNode.Tag);
        }

        private void btApplyFilter_Click(object sender, EventArgs e)
        {
            execSelectedFilter();
        }        

        private void ascx_FindingsFilter_Load(object sender, EventArgs e)
        {
            onLoad();
        }

        private void tvAvailableFilters_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void tvAvailableFilters_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            execSelectedFilter();
        }

        private void btEditFilters_Click(object sender, EventArgs e)
        {

        }
    }
}
