using CurrieTechnologies.Razor.SweetAlert2;
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
    }
}