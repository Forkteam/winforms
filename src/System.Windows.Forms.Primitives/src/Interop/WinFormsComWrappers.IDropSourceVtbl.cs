﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

internal partial class Interop
{
    internal unsafe partial class WinFormsComWrappers
    {
        internal static class IDropSourceVtbl
        {
            public static IntPtr Create(IntPtr fpQueryInterface, IntPtr fpAddRef, IntPtr fpRelease)
            {
                IntPtr* vtblRaw = (IntPtr*)RuntimeHelpers.AllocateTypeAssociatedMemory(typeof(IDropSourceVtbl), IntPtr.Size * 5);
                vtblRaw[0] = fpQueryInterface;
                vtblRaw[1] = fpAddRef;
                vtblRaw[2] = fpRelease;
                vtblRaw[3] = (IntPtr)(delegate* unmanaged<IntPtr, BOOL, User32.MK, HRESULT>)&QueryContinueDrag;
                vtblRaw[4] = (IntPtr)(delegate* unmanaged<IntPtr, Ole32.DROPEFFECT, HRESULT>)&GiveFeedback;

                return (IntPtr)vtblRaw;
            }

            [UnmanagedCallersOnly]
            private static HRESULT QueryContinueDrag(IntPtr thisPtr, BOOL fEscapePressed, User32.MK grfKeyState)
            {
                var inst = ComInterfaceDispatch.GetInstance<Ole32.IDropSource>((ComInterfaceDispatch*)thisPtr);
                return inst.QueryContinueDrag(fEscapePressed, grfKeyState);
            }

            [UnmanagedCallersOnly]
            private static HRESULT GiveFeedback(IntPtr thisPtr, Ole32.DROPEFFECT dwEffect)
            {
                var inst = ComInterfaceDispatch.GetInstance<Ole32.IDropSource>((ComInterfaceDispatch*)thisPtr);
                return inst.GiveFeedback(dwEffect);
            }
        }
    }
}
