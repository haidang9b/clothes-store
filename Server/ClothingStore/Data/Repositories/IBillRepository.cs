using ClothingStore.Entities.Dtos;
using ClothingStore.Entities.Models;
using ClothingStore.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClothingStore.Data.Repositories
{
    public interface IBillRepository
    {
        Task<IEnumerable<Bill>> GetBills();
        Task<TrackingDto> GetBillDetail(int id);
        Task<bool> InsertBill(CartDto cart, User user);
        Task<bool> ChangeStatus(int id, EStatusBill eStatus);
        Task<double> GetTotalPrice(int id);
        Task<int> GetLastBillByUserID(int id);
        Task<IEnumerable<OrderExport>> GetBillDetailByTime(ExportDataDto data);
    }
}
