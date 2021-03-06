// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.CoreLib.API;
using FluentSharp.WinForms.Utils;

namespace O2.Debugger.Mdbg.O2Debugger.Ascx
{
    public partial class ascx_StartOrAttach : UserControl
    {
        public ascx_StartOrAttach()
        {
            InitializeComponent();
        }

        private void btAttachToSelectedProcess_Click(object sender, EventArgs e)
        {
            if (lvManagedProcesses.SelectedItems.Count == 1)
            {
                var processItToAttach = lvManagedProcesses.SelectedItems[0].SubItems[0].Text;
                O2MDbgUtils.attachToProcess(processItToAttach);
                updateGuiEnabledControlState();
            }
        }

        private void lvExecutablesInO2Dirs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvExecutablesInO2Dirs.SelectedItems.Count == 1)
            {
                //tbExecutableToStartAndDebug.Text = lvExecutablesInO2Dirs.SelectedItems[0].Text;
                btStartProcess_Click(null, null);
            }
        }

        private void btStartProcess_Click(object sender, EventArgs e)
        {
            var executableToStart = tbExecutableToStartAndDebug.Text;
            btStartProcess.Enabled = false;
            O2MDbgUtils.startProcessUnderDebugger(executableToStart);
            updateGuiEnabledControlState();

            Processes.Sleep(1000); // wait a bit before reenabling this button
            btStartProcess.Enabled = true;
        }



        private void lvExecutablesInO2Dirs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvExecutablesInO2Dirs.SelectedItems.Count == 1)
            {
                var listViewSubItem = lvExecutablesInO2Dirs.SelectedItems[0].SubItems[0];
                tbExecutableToStartAndDebug.Text = listViewSubItem.Tag.ToString();
            }
        }


        private void llRefreshListOfAvailableManagedProcesses_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {            
            refreshListOfAvailableManagedProcesses();            
        }

        private void tpAttachIntoProcess_Enter(object sender, EventArgs e)
        {
            O2Thread.mtaThread(refreshListOfAvailableManagedProcesses);
        }

        private void tpStartProcess_Enter(object sender, EventArgs e)
        {
            populateListViewWithExecutablesInO2Folders(lvExecutablesInO2Dirs);
        }

        private void tbExecutableToStartAndDebug_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }

        private void tbExecutableToStartAndDebug_DragDrop(object sender, DragEventArgs e)
        {
            tbExecutableToStartAndDebug.Text =  Dnd.tryToGetFileOrDirectoryFromDroppedObject(e);
            btStartProcess_Click(null,null);
        }

        private void btStartProcess_DragDrop(object sender, DragEventArgs e)
        {
            tbExecutableToStartAndDebug_DragDrop(sender, e);            
        }

        private void btStartProcess_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);
        }
        
    }
}
