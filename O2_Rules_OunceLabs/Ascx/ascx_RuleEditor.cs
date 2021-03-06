// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Windows.Forms;
using FluentSharp.CoreLib.Interfaces;

namespace O2.Rules.OunceLabs.Ascx
{
    public partial class ascx_RuleEditor : UserControl
    {
        public ascx_RuleEditor()
        {
            InitializeComponent();
        }

        private void btSaveRuleChanges_Click(object sender, EventArgs e)
        {
            saveCurrentRule();
        }

        private void ascx_RuleEditor_Load(object sender, EventArgs e)
        {
            onLoad();
        }

   
        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btSaveChangesToAllRules_Click(object sender, EventArgs e)
        {
            saveCurrentRules();
        }

        private void controlUnsavedChanges(object sender, EventArgs e)
        {
            laUnsavedChanges.Visible = true;
            laDataSaved.Visible = false;
        }

        private void btSetRuleTypeAsNotASink_Click(object sender, EventArgs e)
        {
            cbRuleType.Text = O2RuleType.NotASink.ToString();
            if (btSaveRuleChanges.Enabled)
                btSaveRuleChanges_Click(null, null);
            else
                if (btSaveChangesToAllRules.Enabled)
                    btSaveChangesToAllRules_Click(null, null);           
        }       
    }
}
