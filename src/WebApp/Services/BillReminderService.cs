using WebApp.Models.Bills;

namespace WebApp.Services
{
    public class BillReminderService
    {
        private readonly HttpClient _httpClient;
        public BillReminderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<List<BillReminder>> CycleBills(Guid userId)
        {
            try
            {
                var billsService = new BillsService(_httpClient);
                var bills = await billsService.GetBills(userId);

                var reminders = new List<BillReminder>();
                var dateFormat = "yyyy-MM-dd HH:mm";
                var dueIn = new List<string>();
                var descriptions = new List<string>();
                var dueDates = new List<string>();
                var startMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                var lastDay = startMonth.AddMonths(1).AddDays(-1).Day;
                if (bills != null && bills.Count() > 0)
                {
                    foreach (var bill in bills)
                    {
                        if (bill.Reminders != null && bill.Reminders.Count() > 0)
                        {
                            if (bill.Reminders[0] <= DateTime.Now)
                            {
                                var billToRemind = new BillReminder();
                                billToRemind.Description = bill.Description;
                                billToRemind.DueDate = bill.DueDate.ToString(dateFormat);

                                var remainingTime = bill.DueDate - bill.Reminders[0];
                                if (remainingTime.Days > 1)
                                {
                                    billToRemind.DueIn = string.Format("{0} days, {1}h {2}min", remainingTime.Days, remainingTime.Hours, remainingTime.Minutes);
                                }
                                else
                                {
                                    billToRemind.DueIn = string.Format("{0}h {1}min", remainingTime.Hours, remainingTime.Minutes);
                                }
                                reminders.Add(billToRemind);
                                bill.Reminders.RemoveAt(0);
                                await billsService.UpdateBill(bill);
                            }
                        }
                    }
                }
                return reminders;
            }
            catch (Exception ex) 
            { 
                return new List<BillReminder>();
            }
        }
    }
}
