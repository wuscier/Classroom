using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.sdk_wrap
{
    internal static class SdkInterop
    {
        const string DLL_NAME = "sdk_wrap.dll";

        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool InitSdkWrap(FuncCallback callback);

        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool UninitSdkWrap();

        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        internal static extern SDKError InitSdk(InitParam initParam);








    }
}
