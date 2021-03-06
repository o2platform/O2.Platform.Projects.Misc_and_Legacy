// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.WinFormUI.Utils;
using NUnit.Framework;
using O2.Rules.OunceLabs.Ascx;

namespace O2.Tool.RulesManager._UnitTests
{
    [TestFixture]
    public class Test_ascx_RulePackViewer
    {
        // private const string testRulePackFile = @"C:\O2\_tempDir\tmp296F.tmp_MySql_Sources_(1387)_.O2RulePack";         
        private const string rulePackViewerControlName ="Rule Pack Viewer"; 
        [SetUp]
        public void openGUI()
        {
            O2AscxGUI.openAscxAsForm(typeof(ascx_RulePackViewer), rulePackViewerControlName);
        }

        [Test]
        public void test_loadO2Packs()
        {
            var rulePackViewerControl = (ascx_RulePackViewer)O2AscxGUI.getAscx(rulePackViewerControlName);
            var thread = rulePackViewerControl.importFromLocalMySqlDatabase();
            //var thread = rulePackViewerControl.loadO2RulePack(testRulePackFile);
            thread.Join();
            thread = rulePackViewerControl.refreshRulesViewer("All", "");
            thread.Join();
            Assert.That(rulePackViewerControl.currentO2RulePack.O2Rules.Count >0);

        }

        [TearDown]
        public void closeGui()
        {
            //O2AscxGUI.waitForAscxGuiClose();
            O2AscxGUI.closeAscxParent(rulePackViewerControlName);
        }
    }
}
