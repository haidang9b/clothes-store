using ClothingStore.Entities;
using ClothingStore.Entities.Dtos;
using ClothingStore.Entities.Models;
using ClothingStore.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public class BillRepository : IBillRepository
    {
        private readonly ClothingContext _dbContext;
        public BillRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<bool> ChangeStatus(int id, EStatusBill eStatus)
        {
            var bill = await _dbContext.bills.Where(b => b.Id == id).FirstOrDefaultAsync();
            bill.Status = eStatus;
            bill.UpdateDate = DateTimeOffset.Now;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<TrackingDto> GetBillDetail(int id)
        {
            var trackingDto = new TrackingDto();
            var bill = await _dbContext.bills.Include(b => b.User).Where(b => b.Id == id).FirstOrDefaultAsync();
            if (bill == null)
            {
                return null;
            }
            trackingDto.User = bill.User.Username;
            trackingDto.Id = bill.Id;
            trackingDto.NumberPhone = bill.NumberPhone;
            trackingDto.Address = bill.Address;
            trackingDto.Status = bill.Status;
            trackingDto.TotalPrice = 0;
            trackingDto.NameReceiver = bill.NameReceiver;
            trackingDto.UpdateDate = bill.UpdateDate;
            trackingDto.CreatedDate = bill.CreatedDate;
            trackingDto.BillDetails = await _dbContext.billDetails.Include(b => b.Product).Where(b => b.BillId == id).ToListAsync();
            return trackingDto;
        }

        public async Task<IEnumerable<Bill>> GetBills()
        {
            return await _dbContext.bills.Include(b => b.User).OrderByDescending(b => b.Id).ToListAsync();
        }

        public async Task<int> GetLastBillByUserID(int id)
        {
            var item = await _dbContext.bills.Where(x => x.UserId == id).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return item.Id;
        }

        public async Task<double> GetTotalPrice(int id)
        {
            var billDetails = await _dbContext.billDetails.Include(x => x.Product).Where(x => x.BillId == id).ToListAsync();
            if (billDetails.Count == 0)
                return 0;
            var totalPrice = 0.0;
            foreach (var item in billDetails)
            {
                totalPrice += item.Product.Price;
            }
            return totalPrice;

        }

        public async Task<bool> InsertBill(CartDto cart, User user)
        {
            var bill = new Bill
            {
                UserId = user.Id,
                NameReceiver = cart.NameReceiver,
                Status = Enums.EStatusBill.Confirm,
                Address = cart.Address,
                NumberPhone = cart.NumberPhone,
                CreatedDate = DateTimeOffset.Now,
                UpdateDate = DateTimeOffset.Now
            };
            _dbContext.bills.Add(bill);
            await _dbContext.SaveChangesAsync();
            var lastBill = await _dbContext.bills.Where(b => b.UserId == user.Id).OrderByDescending(b => b.Id).FirstOrDefaultAsync();
            foreach (var item in cart.Products)
            {
                if (IsValueProduct(item) == false)
                    continue;
                _dbContext.billDetails.Add(new BillDetail
                {
                    BillId = lastBill.Id,
                    ProductId = item,
                    Quantity = 1
                });
                ReduceProduct(item);
            }
            return _dbContext.SaveChanges() > 0;
        }



        private bool IsValueProduct(int id)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.Id == id);
            return product.Quantity > 0;
        }
        private void ReduceProduct(int id)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.Id == id);
            product.Quantity -= 1;
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<OrderExport>> GetBillDetailByTime(ExportDataDto data)
        {
            var start = new DateTimeOffset(DateTimeOffset.Parse(data.Start).DateTime, TimeSpan.Zero);
            var end = new DateTimeOffset(DateTimeOffset.Parse(data.End).AddDays(1).DateTime, TimeSpan.Zero);
            var orders = _dbContext.bills.Include(x => x.BillDetails).Include(x => x.User).Where(x => x.CreatedDate <= end && x.CreatedDate >= start).ToList();
            var result = new List<OrderExport>();
            foreach (var item in orders)
            {
                result.Add(new OrderExport
                {
                    Id = item.Id,
                    Username = item.User.Username,
                    CreatedDate = item.CreatedDate,
                    UpdateDate = item.UpdateDate,
                    NameReceiver = item.NameReceiver,
                    NumberPhone = item.NumberPhone,
                    Address = item.Address,
                    Status = item.Status,
                    TotalCost = await GetTotalCostBill(item.Id)
                });
            }
            return result;
        }

        private async Task<double> GetTotalCostBill(int idBill)
        {
            var data = await _dbContext.billDetails.Include(x => x.Product).Where(x => x.BillId == idBill).ToListAsync();
            var result = 0.0;
            foreach (var item in data)
            {
                result += item.Product.Price;
            }
            return result;
        }
    }
}
