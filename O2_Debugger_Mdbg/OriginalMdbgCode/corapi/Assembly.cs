//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using System;
using System.Collections;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    /**
     * Information about an Assembly being debugged.
     */

    public sealed class CorAssembly : WrapperBase
    {
        private readonly ICorDebugAssembly m_asm;

        internal CorAssembly(ICorDebugAssembly managedAssembly)
            : base(managedAssembly)
        {
            m_asm = managedAssembly;
        }


        /** Get the process containing the Assembly. */

        public CorProcess Process
        {
            get
            {
                ICorDebugProcess proc = null;
                m_asm.GetProcess(out proc);
                return CorProcess.GetCorProcess(proc);
            }
        }

        /** Get the AppDomain containing the assembly. */

        public CorAppDomain AppDomain
        {
            get
            {
                ICorDebugAppDomain ad = null;
                m_asm.GetAppDomain(out ad);
                return new CorAppDomain(ad);
            }
        }

        /** All the modules in the assembly. */

        public IEnumerable Modules
        {
            get
            {
                ICorDebugModuleEnum emod = null;
                m_asm.EnumerateModules(out emod);
                return new CorModuleEnumerator(emod);
            }
        }

        /** Get the name of the code base used to load the assembly. */

        public String CodeBase
        {
            get
            {
                var name = new char[300];
                uint sz = 0;
                m_asm.GetCodeBase((uint) name.Length, out sz, name);
                // ``sz'' includes terminating null; String doesn't handle null,
                // so we "forget" it.
                return new String(name, 0, (int) (sz - 1));
            }
        }

        /** The name of the assembly. */

        public String Name
        {
            get
            {
                var name = new char[300];
                uint sz = 0;
                m_asm.GetName((uint) name.Length, out sz, name);
                // ``sz'' includes terminating null; String doesn't handle null,
                // so we "forget" it.
                return new String(name, 0, (int) (sz - 1));
            }
        }

        public Boolean IsFullyTrusted
        {
            get
            {
                var asm2 = m_asm as ICorDebugAssembly2;
                if (asm2 == null)
                {
                    throw new NotSupportedException("This version of the CLR does not support IsFullyTrusted");
                }

                int trustFlag;
                asm2.IsFullyTrusted(out trustFlag);
                if (trustFlag == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    } /* class Assembly */
} /* namespace debugging */
