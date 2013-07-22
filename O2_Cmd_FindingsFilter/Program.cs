// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)

using FluentSharp.CoreLib.API;
using FluentSharp.WinForms.Utils;

namespace O2.Cmd.FindingsFilter
{
    class Program
    {
        static void Main(string[] args)
        {            
            O2CmdApi.typesWithCommands.Add(typeof (Filters.OzasmtFilters));
            O2CmdApi.typesWithCommands.Add(typeof(GuiHelpers));
            O2CmdApi.typesWithCommands.Add(typeof (Filters.OzasmtLinqUtils));
            O2CmdApi.typesWithCommands.Add(typeof (Filters.PublishToCore));
            O2CmdApi.typesWithCommands.Add(typeof(Filters.RemoveDuplicateTypeIIs));
            if (ClickOnceDeployment.isClickOnceDeployment())
                if (args.Length == 0)
                    args = new[] { "gui" };
            O2CmdExec.execArgs(args);
        }
    }
}
