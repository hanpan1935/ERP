using Lanpuda.ERP.ProductionManagement.Boms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace Lanpuda.ERP.ProductionManagement.WorkOrders
{
    public class WorkOrderManager : DomainService
    {
        private readonly IBomRepository _bomRepository;
        public WorkOrderManager(IBomRepository bomRepository) 
        {
            _bomRepository = bomRepository;
        }


        public async Task<List<WorkOrderMaterial>> GetBomDetailAsync(WorkOrder workOrder)
        {
            var queryable = await _bomRepository.WithDetailsAsync();
            queryable = from m in queryable
                        where
                        m.ProductId == workOrder.ProductId
                        orderby m.CreationTime descending
                        select m;
            var bom = await AsyncExecuter.FirstOrDefaultAsync(queryable);

            List<WorkOrderMaterial> workOrderMaterials = new List<WorkOrderMaterial>();

            if (bom != null)
            {
                foreach (var item in bom.Details)
                {
                    WorkOrderMaterial workOrderMaterial = new WorkOrderMaterial(GuidGenerator.Create());
                    workOrderMaterial.WorkOrderId = workOrder.Id;
                    workOrderMaterial.ProductId = item.ProductId;
                    workOrderMaterial.BomQuantity = item.Quantity;
                    workOrderMaterial.Quantity = workOrder.Quantity * item.Quantity;
                    workOrderMaterial.Sort = item.Sort;
                    workOrderMaterials.Add(workOrderMaterial);
                }
            }
            return workOrderMaterials;
        }
    }
}
