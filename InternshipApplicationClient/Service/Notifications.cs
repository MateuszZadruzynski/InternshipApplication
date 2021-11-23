using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace InternshipApplicationClient.Service
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
