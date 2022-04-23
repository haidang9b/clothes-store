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
    public class BillRepository: IBillRepository
    {
        private readonly ClothingContext _dbContext;
        public BillRepository(ClothingContext dataContext)
        {
            _dbContext = dataContext;
        }

        public async Task<bool> ChangeStatus(int id, EStatusBill eStatus)
        {
            var bill = await _dbContext.bills.Where(b => b.id == id).FirstOrDefaultAsync();
            bill.status = eStatus;
            bill.updateDate = DateTimeOffset.Now;
            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<TrackingDto> GetBillDetail(int id)
        {
            var trackingDto = new TrackingDto();
            var bill = await _dbContext.bills.Include(b => b.user).Where(b => b.id == id).FirstOrDefaultAsync();
            if(bill == null)
            {
                return null;
            }
            trackingDto.user = bill.user.Username;
            trackingDto.id = bill.id;
            trackingDto.numberPhone = bill.numberPhone;
            trackingDto.address = bill.address;
            trackingDto.status = bill.status;
            trackingDto.totalPrice = 0;
            trackingDto.nameReceiver = bill.nameReceiver;
            trackingDto.updateDate = bill.updateDate;
            trackingDto.createdDate = bill.createdDate;
            trackingDto.billDetails = await _dbContext.billDetails.Include(b => b.product).Where(b => b.bill_id == id).ToListAsync();
            return trackingDto;
        }

        public async Task<IEnumerable<Bill>> GetBills()
        {
            return await _dbContext.bills.Include(b => b.user).OrderByDescending(b => b.id).ToListAsync();
        }

        public async Task<int> GetLastBillByUserID(int id)
        {
            var item = await _dbContext.bills.Where(x => x.user_id == id).OrderByDescending(x => x.id).FirstOrDefaultAsync();
            return item.id;
        }

        public async Task<double> GetTotalPrice(int id)
        {
            var billDetails = await _dbContext.billDetails.Include(x => x.product).Where(x => x.bill_id ==id).ToListAsync();
            if (billDetails.Count == 0)
                return 0;
            var totalPrice = 0.0;
            foreach(var item in billDetails)
            {
                totalPrice += item.product.price;
            }
            return totalPrice;

        }

        public async Task<bool> InsertBill(CartDto cart, User user)
        {
            var bill = new Bill
            {
                user_id = user.Id,
                nameReceiver = cart.nameReceiver,
                status = Enums.EStatusBill.Confirm,
                address = cart.address,
                numberPhone = cart.numberPhone,
                createdDate = DateTimeOffset.Now,
                updateDate = DateTimeOffset.Now
            };
            _dbContext.bills.Add(bill);
            await _dbContext.SaveChangesAsync();
            var lastBill = await _dbContext.bills.Where(b => b.user_id == user.Id).OrderByDescending(b => b.id).FirstOrDefaultAsync();
            foreach(var item in cart.products)
            {
                if (checkValueProduct(item) == false)
                    continue;
                _dbContext.billDetails.Add(new BillDetail
                {
                    bill_id = lastBill.id,
                    product_id = item,
                    quantity = 1
                });
                ReduceProduct(item);
            }
            return _dbContext.SaveChanges() > 0;
        }



        private bool checkValueProduct(int id)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.id == id);
            return product.quantity > 0;
        }
        private void ReduceProduct(int id)
        {
            var product = _dbContext.products.FirstOrDefault(p => p.id == id);
            product.quantity -= 1;
            _dbContext.SaveChanges();
        }

        public async Task<IEnumerable<OrderExport>> GetBillDetailByTime(ExportDataDto data)
        {
            var start = new DateTimeOffset(DateTimeOffset.Parse(data.start).DateTime, TimeSpan.Zero);
            var end = new DateTimeOffset(DateTimeOffset.Parse(data.end).AddDays(1).DateTime, TimeSpan.Zero);
            var orders = _dbContext.bills.Include(x => x.billDetails).Include(x => x.user).Where(x => x.createdDate <= end && x.createdDate >= start).ToList();
            var result = new List<OrderExport>();
            foreach(var item in orders)
            {
                result.Add(new OrderExport
                {
                    id = item.id,
                    username = item.user.Username,
                    createdDate = item.createdDate,
                    updateDate = item.updateDate,
                    nameReceiver = item.nameReceiver,
                    numberPhone = item.numberPhone,
                    address = item.address,
                    status = item.status,
                    totalCost = await GetTotalCostBill(item.id)
                });
            }
            return result;
        }

        private async Task<double> GetTotalCostBill(int idBill)
        {
            var data = await _dbContext.billDetails.Include(x => x.product).Where(x => x.bill_id == idBill).ToListAsync();
            var result = 0.0;
            foreach(var item in data)
            {
                result += item.product.price;
            }
            return result;
        }
    }
}
