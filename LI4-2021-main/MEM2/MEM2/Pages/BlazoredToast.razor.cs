using MEM2.Config;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MEM2.Pages
{
    public partial class  BlazoredToast
    {
        [CascadingParameter] private BlazoredToasts ToastsContainer { get; set; }
        [Parameter] public Guid ToastId { get; set; }
        [Parameter] public ToastSettings ToastSettings { get; set; }

        private void Close()
        {
            ToastsContainer.RemoveToast(ToastId);
        }

    }
}
