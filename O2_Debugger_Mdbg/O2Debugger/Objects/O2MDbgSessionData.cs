// This file is part of the OWASP O2 Platform (http://www.owasp.org/index.php/OWASP_O2_Platform) and is released under the Apache 2.0 License (http://www.apache.org/licenses/LICENSE-2.0)
using System;
using System.Collections.Generic;
using O2.Debugger.Mdbg.Debugging.CorDebug;
using O2.Debugger.Mdbg.Debugging.CorPublish;
using O2.Debugger.Mdbg.Debugging.MdbgEngine;
using O2.Debugger.Mdbg.OriginalMdbgCode.mdbg;
using FluentSharp.CoreLib;

namespace O2.Debugger.Mdbg.O2Debugger.Objects
{
    public class O2MDbgSessionData
    {
        private O2MDbg o2MDbg;

        public MDbgProcess mdbgProcess { get { return o2MDbg.shell.Debugger.Processes.Active; } }

        public O2MDbgSessionData(O2MDbg _o2MDbg)
        {
            o2MDbg = _o2MDbg;
        }

        public List<MDbgAppDomain> AppDomains
        {
            get
            {
                var appDomains = new List<MDbgAppDomain>();
                try
                {
                    foreach (MDbgAppDomain appDomain in mdbgProcess.AppDomains)
                        appDomains.Add(appDomain);
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.AppDomains");
                }
                return appDomains;
            }
        }

        public List<CorAssembly> Assemblies
        {
            get
            {
                var assemblies = new List<CorAssembly>();
                try
                {
                    if (mdbgProcess.notNull())
                        foreach (MDbgAppDomain appDomain in mdbgProcess.AppDomains)
                            foreach (CorAssembly assembly in appDomain.CorAppDomain.Assemblies)
                                assemblies.Add(assembly);
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.Assemblies");
                }
                return assemblies;
            }
        }

        public List<MDbgModule> Modules
        {
            get
            {
                var modules = new List<MDbgModule>();
                try
                {

                    foreach (MDbgModule module in mdbgProcess.Modules)
                        modules.Add(module);
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.Modules");
                }
                return modules;
            }
        }

        public O2MDbgThread CurrentThread
        {
            get {
                try
                {
                    return new O2MDbgThread(DI.o2MDbg.ActiveProcess.Threads.Active);
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.CurrentThread");
                }
                return null;
            }
        }

        public List<O2MDbgThread> Threads
        {
            get
            {
                var threads = new List<O2MDbgThread>();
                try
                {
                    foreach (MDbgThread thread in mdbgProcess.Threads)
                        threads.Add(new O2MDbgThread(thread));
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.Threads");
                }
                return threads;
            }
        }

        public List<CorPublishProcess> Processes
        {
            get
            {
                var processes = new List<CorPublishProcess>();
                try
                {

                    mdbgCommandsCustomizedForO2.ProcessEnumCmd("", processes.Add);
                }
                catch (Exception ex)
                {

                    DI.log.ex(ex, "in O2MDbgSessionData.getManagedProcesses");
                }
                return processes;
            }
        }

        public List<String> getAppDomains()
        {
            var appDomains = new List<String>();
            try
            {
                foreach (MDbgAppDomain appDomain in AppDomains)
                {
                    appDomains.Add(appDomain.CorAppDomain.Name);
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in O2MDbgSessionData.getAppDomains");
            }
            return appDomains;
        }

        public List<String> getAssemblies()
        {
            var assemblies = new List<String>();
            try
            {

                foreach (CorAssembly assembly in Assemblies)
                    if (!assemblies.Contains(assembly.Name))
                        assemblies.Add(assembly.Name);
            }
            catch (Exception ex)
            {

                DI.log.ex(ex, "in O2MDbgSessionData.getAssemblies");
            }
            return assemblies;
        }

        internal MDbgModule getModule(string moduleName)
        {
            foreach (MDbgModule module in Modules)
                if (module.CorModule.Name == moduleName)
                    return module;
            return null;
        }

        public List<String> getModules()
        {
            var modules = new List<String>();
            try
            {

                foreach (MDbgModule module in Modules)
                    if (!modules.Contains(module.CorModule.Name))
                        modules.Add(module.CorModule.Name);
            }
            catch (Exception ex)
            {

                DI.log.ex(ex, "in O2MDbgSessionData.getModules");
            }
            return modules;
        }

        public List<String> getThreads()
        {
            var threads = new List<String>();
            try
            {

                foreach (O2MDbgThread thread in Threads)
                    threads.Add(thread.MdbgThread.Id.ToString());
            }
            catch (Exception ex)
            {

                DI.log.ex(ex, "in O2MDbgSessionData.getThreads");
            }
            return threads;
        }

        public Dictionary<int, string> getManagedProcesses()   // <processID, processName>
        {
            var processes = new Dictionary<int, string>();
            try
            {

                foreach (CorPublishProcess process in Processes)
                    processes.Add(process.ProcessId, process.DisplayName);
            }
            catch (Exception ex)
            {

                DI.log.ex(ex, "in O2MDbgSessionData.getManagedProcesses");
            }
            return processes;
        }
        
        public Dictionary<string, O2MDbgVariable> getVariableDictionary(O2MDbgVariable variableToGet)
        {
            var variablesDictionary = new Dictionary<string, O2MDbgVariable>();
            try
            {
                foreach(var variable in getCurrentFrameVariable(variableToGet))
                {
                    if (false == variablesDictionary.ContainsKey(variable.name))
                        variablesDictionary[variable.name] = variable;
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getVariableDictionary()");
            }
            return variablesDictionary;
        }

        public Dictionary<string, O2MDbgVariable> getVariablesDictionary()
        {
            var variablesDictionary = new Dictionary<string, O2MDbgVariable>();
            try
            {
                var currentFrameVariables = getCurrentFrameVariables(0, true);
                foreach (var variable in currentFrameVariables)
                    if (false == variablesDictionary.ContainsKey(variable.name))
                        variablesDictionary[variable.name] = variable;
            }
            catch (Exception ex)
            {
                DI.log.ex(ex,"in getVariablesDictionary()");
            }
            return variablesDictionary;

        }

        public List<O2MDbgVariable> getCurrentFrameVariables(int expandDepth, bool canDoFunceval)
        {
            var o2MDbgVariables = new List<O2MDbgVariable>();
            try
            {
                //MDbgFrame frame = DI.o2MDbg.ActiveProcess.Threads.Active.CurrentFrame;

                // Debugger variables
                /*MDbgProcess activeProcess = DI.o2MDbg.ActiveProcess;
                foreach (MDbgDebuggerVar dv in activeProcess.DebuggerVars)
                {
                var v = new MDbgValue(activeProcess, dv.CorValue);
                DI.log.debug(dv.Name + "=" + v.GetStringValue(expandDepth, canDoFunceval));                    
                }*/
                if (o2MDbg.ActiveProcess.IsRunning == false)
                {
                    MDbgFrame frame = DI.o2MDbg.ActiveProcess.Threads.Active.CurrentFrame;
                    MDbgFunction function = frame.Function;

                    var vars = new List<MDbgValue>();


                    MDbgValue[] vals = function.GetActiveLocalVars(frame);
                    if (vals != null)
                        vars.AddRange(vals);
                    vals = function.GetArguments(frame);
                    if (vals != null)
                        vars.AddRange(vals);

                    var functionFullName = function.FullName;
                    //var functionAssembly = function.Module.CorModule.Assembly.Name;
                    foreach (MDbgValue mdbgValue in vars)
                    {
                        //var valueAssembly = ""mdbgValue.CorValue.ExactType.Class.Module.Assembly.Name;
                        /*if (mdbgValue.TypeName.IndexOf("System.Web") > -1)
                        {
                            CorType cor = mdbgValue.CorValue.ExactType;
                            var corclas = cor.Class;
                            //corclas.Module.Assembly.Name
                        }*/
                        o2MDbgVariables.Add(new O2MDbgVariable(mdbgValue, functionFullName, ""));
                    }
                }
            }
            catch (Exception ex)
            {
                if (false == DI.o2MDbg.AnimateOnStepEvent && ex.Message.IndexOf("zoobie") >-1)
                    DI.log.ex(ex, "in showFrameVariables");
            }
            return o2MDbgVariables;
        }

        internal List<O2MDbgVariable> getCurrentFrameVariable(O2MDbgVariable o2MDbgVariable)
        {
            var o2MDbgVariables = new List<O2MDbgVariable>();
            try
            {
                MDbgFrame frame = DI.o2MDbg.ActiveProcess.Threads.Active.CurrentFrame;
                MDbgValue mbbgValue = DI.o2MDbg.ActiveProcess.ResolveVariable(o2MDbgVariable.fullName, frame);
                if (mbbgValue != null && mbbgValue.IsComplexType)
                {
                    try
                    {
                        // add properties
                        var valueType = mbbgValue.TypeName;
                        var valueAssembly = mbbgValue.CorValue.ExactType.Class.Module.Assembly.Name;
                        //var properties2 = DI.reflection.getProperties(DI.reflection.getType(valueAssembly,valueType));
                        var properties = DI.reflection.getProperties(DI.reflection.getType(valueAssembly, valueType));
                        foreach (var property in properties)
                            o2MDbgVariables.Add(new O2MDbgVariable(property, o2MDbgVariable));
                    }
                    catch (Exception ex)
                    {
                        DI.log.ex(ex, "in getCurrentFrameVariable , add properties");
                    }
                    // add values
                    try
                    {
                        foreach (MDbgValue mdbgValue in mbbgValue.GetFields())
                            if (mdbgValue != null)
                                o2MDbgVariables.Add(new O2MDbgVariable(mdbgValue, o2MDbgVariable));
                    }
                    catch (Exception ex)
                    {
                        DI.log.ex(ex, "in getCurrentFrameVariable , add values");
                    }
                }
            }
            catch (Exception ex)
            {
                DI.log.ex(ex, "in getCurrentFrameVariable");
            }
            return o2MDbgVariables;
        }
    }    
}
