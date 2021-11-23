using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternshipApplicationServer.Service
{
    public static class Notifications
    {
        public static async ValueTask ToastSuccess(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ToastShow", "successPopUp", message);
        }

        public static async ValueTask ToastError(this IJSRuntime jSRuntime, string message)
        {
            await jSRuntime.InvokeVoidAsync("ToastShow", "errorPopUp", message);
        }
    }
}
