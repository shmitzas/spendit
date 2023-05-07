using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using REST_API.Data;
using REST_API.Models.Bills;

namespace REST_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillsController : Controller
    {
        private readonly ApiDbContext _DbContext;
        public BillsController(ApiDbContext DbContext)
        {
            _DbContext = DbContext;
        }

        [HttpGet]
        [Route("{userId:guid}")]
        public async Task<IActionResult> GetAllBills([FromRoute] Guid userId)
        {
            try
            {
                var bills = await _DbContext.Bills.Where(t => t.UserId == userId).OrderBy(t => t.DueDate).ToListAsync();
                return Ok(await ConvertToBillList(bills));
            }
            catch
            {
                return NotFound("No Bills found");
            }
        }
        [HttpGet]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> GetBill([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var bill = await _DbContext.Bills.Where(t => t.UserId == userId && t.Id == id).SingleAsync();

                return Ok(await ConvertToBill(bill));
            }
            catch
            {
                return NotFound("Bill not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBill(NewBill billInfo)
        {
            var reminders = "";
            if (billInfo.Reminders.Count() > 0)
            {
                reminders = JsonConvert.SerializeObject(billInfo.Reminders);
            }

            try
            {
                var bill = new BillToDb()
                {
                    Id = Guid.NewGuid(),
                    UserId = billInfo.UserId,
                    CategoryId = billInfo.CategoryId,
                    Description = billInfo.Description,
                    DueDate = billInfo.DueDate,
                    Amount = billInfo.Amount,
                    Reminders = reminders,
                    Currency = billInfo.Currency,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await _DbContext.Bills.AddAsync(bill);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return BadRequest("Wrong bill details");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBill(Bill billInfo)
        {
            try
            {
                var bill = await _DbContext.Bills.Where(t => t.UserId == billInfo.UserId && t.Id == billInfo.Id).SingleAsync();
                var billDbInfo = await ConvertBillToDb(billInfo);

                bill.CategoryId = billDbInfo.CategoryId;
                bill.Description = billDbInfo.Description;
                bill.DueDate = billDbInfo.DueDate;
                bill.Amount = billDbInfo.Amount;
                bill.Currency = billDbInfo.Currency;
                bill.Reminders = billDbInfo.Reminders;
                bill.UpdatedAt = DateTime.Now;

                _DbContext.Bills.Update(bill);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound("Wrong bill details");
            }
        }

        [HttpPut ("status")]
        public async Task<IActionResult> UpdateBillStatus(Bill billInfo)
        {
            try
            {
                var bill = await _DbContext.Bills.Where(t => t.UserId == billInfo.UserId && t.Id == billInfo.Id).SingleAsync();
                bill.IsPaid = billInfo.IsPaid;
                _DbContext.Bills.Update(bill);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound("Wrong bill details");
            }
        }

        [HttpDelete]
        [Route("{userId:guid}/{id:guid}")]
        public async Task<IActionResult> DeleteBbill([FromRoute] Guid userId, [FromRoute] Guid id)
        {
            try
            {
                var bill = await _DbContext.Bills.Where(t => t.UserId == userId && t.Id == id).SingleAsync();
                _DbContext.Remove(bill);
                await _DbContext.SaveChangesAsync();
                return Ok();
            }
            catch
            {
                return NotFound("Bill does not exist");
            }
        }
        private async Task<Bill> ConvertToBill(BillToDb billToDb)
        {
            var reminders = new List<DateTime>();

            if (!String.IsNullOrEmpty(billToDb.Reminders))
            {
                try
                {
                    reminders = JsonConvert.DeserializeObject<List<DateTime>>(billToDb.Reminders);
                    if (reminders != null && reminders.Count() > 1)
                    {
                        reminders = reminders.Order().ToList();
                    }
                }
                catch { }
            }

            return new Bill
            {
                Id = billToDb.Id,
                UserId = billToDb.UserId,
                CategoryId = billToDb.CategoryId,
                Description = billToDb.Description,
                Amount = billToDb.Amount,
                Currency = billToDb.Currency,
                DueDate = billToDb.DueDate,
                Reminders = reminders,
                IsPaid = billToDb.IsPaid,
                CreatedAt = billToDb.CreatedAt,
                UpdatedAt = billToDb.UpdatedAt
            };
        }
        private async Task<List<Bill>> ConvertToBillList(List<BillToDb> billToDbList)
        {
            var billList = new List<Bill>();
            foreach(var billToDb in billToDbList)
            {
                var reminders = new List<DateTime>();

                if (!String.IsNullOrEmpty(billToDb.Reminders))
                {
                    try
                    {
                        reminders = JsonConvert.DeserializeObject<List<DateTime>>(billToDb.Reminders);
                        if (reminders != null && reminders.Count() > 1)
                        {
                            reminders = reminders.Order().ToList();
                        }
                    }
                    catch { }
                }

                billList.Add(new Bill
                {
                    Id = billToDb.Id,
                    UserId = billToDb.UserId,
                    CategoryId = billToDb.CategoryId,
                    Description = billToDb.Description,
                    Amount = billToDb.Amount,
                    Currency = billToDb.Currency,
                    DueDate = billToDb.DueDate,
                    Reminders = reminders,
                    IsPaid = billToDb.IsPaid,
                    CreatedAt = billToDb.CreatedAt,
                    UpdatedAt = billToDb.UpdatedAt
                });
            }
            return billList;
        }
        private async Task<BillToDb> ConvertBillToDb(Bill bill)
        {
            var reminders = "";

            if (bill.Reminders != null && bill.Reminders.Count() > 0)
            {
                if (bill.Reminders.Count() > 1)
                {
                    bill.Reminders = bill.Reminders.Order().ToList();
                }
                reminders = JsonConvert.SerializeObject(bill.Reminders);
            }

            return new BillToDb
            {
                Id = bill.Id,
                UserId = bill.UserId,
                CategoryId = bill.CategoryId,
                Description = bill.Description,
                Amount = bill.Amount,
                Currency = bill.Currency,
                DueDate = bill.DueDate,
                Reminders = reminders,
                IsPaid = bill.IsPaid,
                CreatedAt = bill.CreatedAt,
                UpdatedAt = bill.UpdatedAt
            };
        }
    }
}
