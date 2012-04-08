//---------------------------------------------------------------------
//  This file is part of the CLR Managed Debugger (mdbg) Sample.
// 
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//---------------------------------------------------------------------


// These interfaces serve as an extension to the BCL's SymbolStore interfaces.
using System.Diagnostics.SymbolStore;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace O2.Debugger.Mdbg.Debugging.CorSymbolStore
{
    // Interface does not need to be marked with the serializable attribute

    /// <include file='doc\ISymENCUpdate.uex' path='docs/doc[@for="SymbolLineDelta"]/*' />
    [StructLayout(LayoutKind.Sequential)]
    public struct SymbolLineDelta
    {
        private SymbolToken mdMethod;
        private int delta;
    } ;

    /// <include file='doc\ISymScope.uex' path='docs/doc[@for="ISymbolScope"]/*' />
    [
        ComVisible(false)
    ]
    public interface ISymbolEncUpdate
    {
        /// <include file='doc\ISymENCUpdate.uex' path='docs/doc[@for="ISymbolEncUpdate.UpdateSymbolStore"]/*' />
        void UpdateSymbolStore(IStream stream, SymbolLineDelta[] symbolLineDeltas);

        /// <include file='doc\ISymENCUpdate.uex' path='docs/doc[@for="ISymbolEncUpdate.GetLocalVariableCount"]/*' />
        int GetLocalVariableCount(SymbolToken mdMethodToken);

        /// <include file='doc\ISymENCUpdate.uex' path='docs/doc[@for="ISymbolEncUpdate.GetLocalVariables"]/*' />
        ISymbolVariable[] GetLocalVariables(SymbolToken mdMethodToken);
    }
}
