﻿using ApexCharts;
using LAHJA.UI.Components;
using MudBlazor;
using Shared.Constants._Messages;
using Shared.Wrapper;

namespace LAHJA.Data.UI.Components
{
    public class MessageBox
    {
        private readonly IDialogService _dialogService;

        public MessageBox(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public  async Task<bool> ShowAsync(string title,string message,string lang="ar")
        {
            var parameters = new DialogParameters
            {
                { "Title",title },
                { "Message", message },
                { "Lang", lang }
            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };
            var dialog = _dialogService.Show<DialogBox>("", parameters, options);
            var result = await dialog.Result;
            return !result.Canceled;
        }
    }
}
