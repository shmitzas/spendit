using CurrieTechnologies.Razor.SweetAlert2;
using System;
using WebApp.Models.Bills;

namespace WebApp.Services
{
    public class AlertsService
    {
        private readonly SweetAlertService _alerts;
        public AlertsService(SweetAlertService alerts)
        {
            _alerts = alerts;
        }
        public async Task InvokeSuccess(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Success",
                Text = message,
                Icon = SweetAlertIcon.Success,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task InvokeSuccessTimer(string message, int time)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Success",
                Text = message,
                Icon = SweetAlertIcon.Success,
                Timer = time,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task InvokeWarning(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Warning",
                Text = message,
                Icon = SweetAlertIcon.Warning,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task InvokeError()
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Error",
                Text = "Oops... An error occured, please try again.",
                Icon = SweetAlertIcon.Error,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task InvokeCustomError(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Error",
                Text = message,
                Icon = SweetAlertIcon.Error,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task InvokeCustomWarning(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Warning",
                Text = message,
                Icon = SweetAlertIcon.Warning,
                ConfirmButtonText = "Ok",
            });
        }
        public async Task<bool> InvokeBillsReminder(List<BillReminder> bills)
        {
            var billsHtml = new List<string>();
            if(bills.Count() > 0)
            {
                foreach(var bill in bills)
                {
                    
                    billsHtml.Add(
                        $@"
                        <tr>
                            <td class=""text-start"">{bill.Description}</td>
                            <td>{bill.DueDate}</td>
                            <td>{bill.DueIn}</td>
                        </tr>
                        ");
                }
            }
            var res = await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Upcoming bills",
                Icon = SweetAlertIcon.Info,
                ConfirmButtonText = "Details",
                Html = $@"
                        <div class='container'>
                            <div class=""pt-2"">
                                <table class=""table table-hover"">
                                    <thead class=""border-bottom-1"">
                                        <tr>
                                            <th scope=""col"" class=""col-md-6 col-4 text-start"">Description</th>
                                            <th scope=""col"" class=""col-md-3 col-4 text-center text-primary"">Due date</th>
                                            <th scope=""col"" class=""col-md-3 col-4 text-center text-primary"">Due in</th>
                                        </tr>
                                    </thead>
                                    <tbody>
                                        {string.Join("", billsHtml)}
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        ",
                Width = "col-xl-4 col-lg-6 col-md-8 col-12",
                ShowCloseButton = true,
            }); 
            return res.IsConfirmed;
        }
        public async Task<bool> InvokeConfirmation()
        {
            var res = await _alerts.FireAsync(new SweetAlertOptions
            {
                Title = "Are you sure?",
                Text = "This action is irreversible! Are you sure you want to do this?",
                Icon = SweetAlertIcon.Warning,
                ShowCancelButton = true,
                ConfirmButtonText = "Yes",
                CancelButtonText = "No"
            });
            return res.IsConfirmed;
        }
        public async Task InvokeCustomToastSuccess(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Toast = true,
                Title = message,
                Icon = SweetAlertIcon.Success,
                Position = SweetAlertPosition.TopEnd,
                ShowConfirmButton = false,
                Timer = 3000,
                TimerProgressBar = true
            });
        }
        public async Task InvokeCustomToastWarning(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Toast = true,
                Title = message,
                Icon = SweetAlertIcon.Warning,
                Position = SweetAlertPosition.TopEnd,
                ShowConfirmButton = false,
                Timer = 3000,
                TimerProgressBar = true
            });
        }
        public async Task InvokeCustomToastError(string message)
        {
            await _alerts.FireAsync(new SweetAlertOptions
            {
                Toast = true,
                Title = message,
                Icon = SweetAlertIcon.Error,
                Position = SweetAlertPosition.TopEnd,
                ShowConfirmButton = false,
                Timer = 3000,
                TimerProgressBar = true
            });
        }
    }
}