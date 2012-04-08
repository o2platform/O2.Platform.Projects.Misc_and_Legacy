//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------

using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;
using O2.Debugger.Mdbg.Debugging.CorDebug.NativeApi;

namespace O2.Debugger.Mdbg.Debugging.CorDebug
{
    public sealed class CorValueBreakpoint : CorBreakpoint
    {
        private readonly ICorDebugValueBreakpoint m_br;

        internal CorValueBreakpoint(ICorDebugValueBreakpoint breakpoint) : base(breakpoint)
        {
            m_br = breakpoint;
        }

        public CorValue Value
        {
            get
            {
                ICorDebugValue m = null;
                m_br.GetValue(out m);
                return new CorValue(m);
            }
        }
    } /* class ValueBreakpoint */
} /* namespace */
