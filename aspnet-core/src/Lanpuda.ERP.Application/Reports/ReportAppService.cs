using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using Lanpuda.ERP.Reports.Dtos.Home;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.Reports
{
    [Authorize]
    public class ReportAppService : ERPAppService , IReportAppService
    {
        private readonly IWorkOrderRepository _workOrderRepository;
        private readonly IWorkOrderStorageApplyRepository _workOrderStorageApplyRepository;
        private readonly IWorkOrderStorageDetailRepository _workOrderStorageDetailRepository;
        public ReportAppService(
            IWorkOrderRepository workOrderRepository,
            IWorkOrderStorageDetailRepository workOrderStorageDetailRepository,
            IWorkOrderStorageApplyRepository workOrderStorageApplyRepository)
        {
            _workOrderRepository = workOrderRepository;
            _workOrderStorageApplyRepository = workOrderStorageApplyRepository;
            _workOrderStorageDetailRepository = workOrderStorageDetailRepository;
        }

        [Authorize]
        public async Task<HomeDto> GetHomeDataAsync(DateTime startDate)
        {
            var list = await _workOrderRepository.GetListAsync(
                m => m.StartDate.Year == startDate.Year &&
                m.StartDate.Month == startDate.Month &&
                m.StartDate.Day == startDate.Day 
                , true);
            var results = ObjectMapper.Map<List<WorkOrder>, List<HomeWorkOrderDto>>(list);

            foreach (var item in results)
            {
                var queryable = await _workOrderStorageDetailRepository.WithDetailsAsync();
                queryable = queryable.Where(m => m.WorkOrderStorage.WorkOrderStorageApply.WorkOrderId == item.Id && m.WorkOrderStorage.IsSuccessful == true);
                var storageDetaiList = await AsyncExecuter.ToListAsync(queryable);
                var sum = storageDetaiList.Sum(m => m.Quantity);
                item.WorkOrderStorageQuantity = sum;
            }
            HomeDto homeDto = new HomeDto();
            homeDto.WorkOrders = results;
            return homeDto;
        }
       
    }
}
