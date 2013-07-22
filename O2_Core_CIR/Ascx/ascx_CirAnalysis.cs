// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.IO;
using System.Windows.Forms;
//using O2.Core.CIR.CirObjects;
using FluentSharp.CoreLib.API;
using FluentSharp.WinForms;
using FluentSharp.WinForms.Utils;
using O2.Core.CIR.CirObjects;


namespace O2.Core.CIR.Ascx
{
    public partial class ascx_CirAnalysis : UserControl
    {
        public ascx_CirAnalysis()
        {
            InitializeComponent();
        }

        private void DropObject1_eDnDAction_ObjectDataReceived_Event(object oObject)
        {
            
                                       loadFileOrFolder(oObject);
                                   
        }

        public void loadFileOrFolder(object fileOrFolder)
        {
            this.invokeOnThread(
                () =>
                    {
                        if (cbClearPreviousO2CirData.Checked)
                        {
                            cirDataAnalysis = new CirDataAnalysis();
                            lbLoadedO2CirDataFiles.Items.Clear();
                        }
                        O2Thread.mtaThread(
                            () =>
                                {

                                    if (Directory.Exists(fileOrFolder.ToString()))
                                    {
                                        foreach (String sFile in Directory.GetFiles(fileOrFolder.ToString()))
                                        {
                                            loadO2CirDataFile(sFile, false, false /*decompileCodeIfNoPdb*/);
                                            Application.DoEvents();
                                        }
                                        raiseSetCirDataAnalysisO2Message();
                                    }
                                    else
                                        loadO2CirDataFile(fileOrFolder.ToString(), true, false /*decompileCodeIfNoPdb*/);
                                });
                    });
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {
            if (false == DesignMode)
            {
                //PublicDI.o2MessageQueue.onMessages += o2MessageQueue_onMessages;
                KO2MessageQueue.getO2KernelQueue().onMessages += o2MessageQueue_onMessages;
            }
        }

        private void DropObject1_Load(object sender, EventArgs e)
        {

        }

        private void lbLoadedO2CirDataFiles_DragDrop(object sender, DragEventArgs e)
        {
            loadFileOrFolder(Dnd.tryToGetFileOrDirectoryFromDroppedObject(e));
        }

        private void lbLoadedO2CirDataFiles_DragEnter(object sender, DragEventArgs e)
        {
            Dnd.setEffect(e);            
        }

        private void llClearLoadedData_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clearLoadedData();
        }
               
    }
}
