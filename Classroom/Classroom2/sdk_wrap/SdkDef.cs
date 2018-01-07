using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Classroom.sdk_wrap
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate void FuncCallback(int callbackId, IntPtr data);

    enum SDKError
    {
        SDKERR_SUCCESS = 0,///< Success Result
        SDKERR_NO_IMPL,///< Not support this feature now 
        SDKERR_WRONG_USEAGE,///< Wrong useage about this feature 
        SDKERR_INVALID_PARAMETER,///< Wrong parameter 
        SDKERR_MODULE_LOAD_FAILED,///< Load module failed 
        SDKERR_MEMORY_FAILED,///< No memory allocated 
        SDKERR_SERVICE_FAILED,///< Internal service error 
        SDKERR_UNINITIALIZE,///< Not initialize before use 
        SDKERR_UNAUTHENTICATION,///< Not Authentication before use
        SDKERR_NORECORDINGINPROCESS,///< No recording in process
        SDKERR_TRANSCODER_NOFOUND,///< can't find transcoder module
        SDKERR_VIDEO_NOTREADY,///< Video service not ready
        SDKERR_NO_PERMISSION,///< No premission to do this
        SDKERR_UNKNOWN,///< Unknown error 
        SDKERR_OTHER_SDK_INSTANCE_RUNNING,
    };

    [StructLayout(LayoutKind.Sequential,CharSet = CharSet.Ansi)]
    internal struct InitParam
    {

    }
}
