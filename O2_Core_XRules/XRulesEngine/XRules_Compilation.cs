// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using System.Reflection;
using FluentSharp.CoreLib.API;
using FluentSharp.CoreLib.Interfaces;
using O2.External.O2Mono.MonoCecil;

namespace O2.Core.XRules.XRulesEngine
{
    public class XRules_Compilation
    {

        public static void loadXRules(Action<List<IXRule>> onLoad, Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        var xRules = getCompiledXRules(currentTask, numberOfStepsToPerform, onStepEvent);
                        onLoad(xRules);                                                
                    });
        }

        public static List<IXRule> getCompiledXRules()
        {
            return getCompiledXRules(null, null, null);
        }

        public static List<IXRule> getCompiledXRules(Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            if (XRules_Config.xRulesDatabase != null)
            {
                var xRulesAssemblies = Files.getFilesFromDir_returnFullPath(XRules_Config.PathTo_XRulesCompiledDlls,"*.dll");
                xRulesAssemblies.AddRange(Files.getFilesFromDir_returnFullPath(XRules_Config.PathTo_XRulesCompiledDlls,"*.exe"));
                return loadXRules(xRulesAssemblies, currentTask, numberOfStepsToPerform, onStepEvent);
            }
            return new List<IXRule>();
        }

        public static void compileXRules(Action<List<IXRule>> onCompilation, Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            O2Thread.mtaThread(
                () =>
                    {
                        var xRules = compileXRules(currentTask,numberOfStepsToPerform, onStepEvent);
                        onCompilation(xRules);
                    });
        }

        public static List<IXRule> loadXRules(List<string>xRulesAssemblies, Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {           
            if (currentTask != null)
                currentTask("Loading XRules");

            var xRules = new List<IXRule>();
            foreach(var xRuleAssembly in xRulesAssemblies)
            {
                var assembly = PublicDI.reflection.getAssembly(xRuleAssembly);
                if (assembly != null)
                    xRules.AddRange(createXRulesFromAssembly(assembly));
                if (onStepEvent != null)
                    onStepEvent();
            }
            if (currentTask != null)
            currentTask("XRules Loading Complete");
            return xRules;
        }

        public static List<IXRule> compileXRules(Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            var compiledXRulesFiles = getCompiledXRulesAssemblies(currentTask,numberOfStepsToPerform, onStepEvent);
            
            currentTask("Moving XRules and its dependencies");

            numberOfStepsToPerform(compiledXRulesFiles.Count * 2);

            //var pathToAppDomainWithXRulesAssemblies = populateDirectoryWithAllDependencies(compiledXRulesFiles, onStepEvent);
            // dont add the dependencies since they are creating a prob with the cmd line tool
            Files.deleteAllFilesFromDir(XRules_Config.PathTo_XRulesCompiledDlls);
            foreach (var file in compiledXRulesFiles)
                Files.copy(file, XRules_Config.PathTo_XRulesCompiledDlls);


            var pathToAppDomainWithXRulesAssemblies = XRules_Config.PathTo_XRulesCompiledDlls;

            // special case where we don't need the O2_XRules_Database.dll file in the )CompiledDlls folder
          //  var xRulesDatabaseOriginalDll = System.IO.Path.Combine(pathToAppDomainWithXRulesAssemblies, "O2_XRules_Database.dll");
          //  if (System.IO.File.Exists(xRulesDatabaseOriginalDll))
          //      System.IO.File.Delete(xRulesDatabaseOriginalDll);

            var xRulesAssemblies = new List<string>();
            foreach (var originalDll in compiledXRulesFiles)
            {
                var dllInXRulesCompiledDllFolder = originalDll.Replace(System.IO.Path.GetDirectoryName(originalDll),
                                                                       pathToAppDomainWithXRulesAssemblies);
                xRulesAssemblies.Add(dllInXRulesCompiledDllFolder);
            }
            return loadXRules(xRulesAssemblies, currentTask, numberOfStepsToPerform, onStepEvent);
        }

        private static string populateDirectoryWithAllDependencies(List<String> compiledXRulesFiles, Action onStepEvent)
        {
            var targetDirectory = XRules_Config.PathTo_XRulesCompiledDlls;
            Files.deleteFilesFromDirThatMatchPattern(targetDirectory,"*.dll");
            foreach (var compiledFile in compiledXRulesFiles)
            {
                CecilAssemblyDependencies.populateDirectoryWithAllDependenciesOfAssembly(targetDirectory, compiledFile,
                                                                                         null);
                onStepEvent();
            }
            return targetDirectory;
        }

        public static List<String> getCompiledXRulesAssemblies(Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            currentTask("Compiling all rules together");
            numberOfStepsToPerform(1);
            // first try to scan all together
            var filesToCompile = Files.getFilesFromDir_returnFullPath(XRules_Config.PathTo_XRulesDatabase_fromO2, "*.cs", true); 			// recursive search
            foreach(var xRuleFile in Files.getFilesFromDir_returnFullPath(XRules_Config.PathTo_XRulesDatabase_fromLocalDisk, "*.cs", true)) // recursive search
                if (false == filesToCompile.Contains(xRuleFile))
                    filesToCompile.Add(xRuleFile);
            PublicDI.log.info("There are {0} XRules to Compile", filesToCompile.Count);

            var compiledXRulesAssembly = compileAllFilesTogether(filesToCompile);
            onStepEvent();
            if (compiledXRulesAssembly != null)
                return new List<String> {compiledXRulesAssembly.Location};
            
            // if we couldn't compile all at once, then compile each file individually
            PublicDI.log.error("It was not possible to compile all XRules together, going to try to compile each XRule file individually");           
            //return compileAllFilesIndividually(filesToCompile,currentTask,numberOfStepsToPerform,onStepEvent);
            return null;
        }

        public static Assembly compileAllFilesTogether(List<String> filesToCompile)
        {
            PublicDI.log.info("Compiling All XRules source code files together");
            return new CompileEngine().compileSourceFiles(filesToCompile);
        }

        public static List<String> compileAllFilesIndividually(List<String> filesToCompile, Action<string> currentTask, Action<int> numberOfStepsToPerform, Action onStepEvent)
        {
            currentTask("Compiling all rules individualy (one file at the time)");
            numberOfStepsToPerform(filesToCompile.Count);
            var compileEngine = new CompileEngine();
            PublicDI.log.info("Compiling All XRules source code files ONE at the time");
            var results = new List<String>();
            foreach (var fileToCompile in filesToCompile)
            {
                var assembly = compileEngine.compileSourceFile(fileToCompile);
                if (assembly != null)
                    results.Add(assembly.Location);
                else
                    PublicDI.log.error("In XRules_Execution.compileAllFilesIndividually, could not compile file: {0}", fileToCompile);
                onStepEvent();
            }
            return results;
        }

        public static List<IXRule> createXRulesFromAssembly(Assembly assembly)
        {
            var xRules = new List<IXRule>();
            try
            {


                if (assembly != null)
                {
                    var types = PublicDI.reflection.getTypes(assembly);
                    foreach (var type in types)
                    {

                        //                    var inters = type.GetInterfaces();

                        /*if (type is IXRule)*/
                        // if there is an interface of IXRule then this is a rule
                        if (type.GetInterface(typeof(IXRule).FullName) != null)
                        {
                            var ruleObject = PublicDI.reflection.createObject(assembly, type);
                            if (ruleObject != null && ruleObject is IXRule)
                            {
                                var xRule = (IXRule)ruleObject;
                                if (xRule.Name == "")
                                    xRule.Name = type.FullName;
                                xRules.Add(xRule);
                            }
                        }
                        //else
                        //    DI.log.info("type was not IXRule: {0}", type.FullName);
                    }
                }
            }
            catch (Exception ex)
            {
                PublicDI.log.error("in createXRulesFromAssembly: {0}", ex.Message);
            }
            return xRules;
        }
    }
}
