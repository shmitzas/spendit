using System.Text.Json;
using System.Text;
using WebApp.Models.Bills;

namespace WebApp.Services
{
    public class BillsService
    {
        private readonly HttpClient _httpClient;
        public BillsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        private async Task<StringContent> SerializeObj(object obj)
        {
            string json = JsonSerializer.Serialize(obj);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }
        public async Task<IEnumerable<Bill>> GetBills(Guid userId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Bill[]>($"/api/bills/{userId}");
            }
            catch
            {
                return new List<Bill>();
            }
        }
        public async Task<Bill> GetBill(Guid userId, Guid billId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<Bill>($"/api/bills/{userId}/{billId}");
            }
            catch
            {
                return new Bill();
            }
        }
        public async Task<bool> AddBill(NewBill bill)
        {
            try
            {
                var content = await SerializeObj(bill);
                var res = await _httpClient.PostAsync("/api/bills", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> UpdateBill(Bill bill)
        {
            try
            {
                var content = await SerializeObj(bill);
                var res = await _httpClient.PutAsync("/api/bills", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateBillStatus(Bill bill)
        {
            try
            {
                var content = await SerializeObj(bill);
                var res = await _httpClient.PutAsync("/api/bills/status", content);
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteBill(Guid userId, Guid billId)
        {
            try
            {
                var res = await _httpClient.DeleteAsync($"/api/bills/{userId}/{billId}");
                if (res.IsSuccessStatusCode)
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
