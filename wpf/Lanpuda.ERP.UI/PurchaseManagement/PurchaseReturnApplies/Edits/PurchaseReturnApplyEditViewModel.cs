using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Selects;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Edits
{
    public partial class PurchaseReturnApplyEditViewModel : EditViewModelBase<PurchaseReturnApplyEditModel>
    {
        private readonly ISupplierAppService _supplierAppService;
        private readonly IPurchaseReturnApplyAppService _purchaseReturnApplyAppService;
        private readonly IServiceProvider _serviceProvider;

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }

        public Dictionary<string, PurchaseReturnReason> ReturnReasonSource { get; set; }

        public ObservableCollection<SupplierDto> SupplierSource { get; set; }


        public PurchaseReturnApplyEditViewModel(
            IPurchaseReturnApplyAppService purchaseReturnApplyAppServic,
            IServiceProvider serviceProvider,
            ISupplierAppService supplierAppService
            )
        {
            _purchaseReturnApplyAppService = purchaseReturnApplyAppServic;
            _serviceProvider = serviceProvider;
            _supplierAppService = supplierAppService;
            ReturnReasonSource = EnumUtils.EnumToDictionary<PurchaseReturnReason>();
            Model = new PurchaseReturnApplyEditModel();

            SupplierSource = new ObservableCollection<SupplierDto>();
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var suppliers = await this._supplierAppService.GetAllAsync();
                foreach (var item in suppliers)
                {
                    this.SupplierSource.Add(item);
                }

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "退货申请-编辑";
                    Guid id = (Guid)Model.Id;
                    var result = await _purchaseReturnApplyAppService.GetAsync(id);
                    Model.Number = result.Number;
                    Model.SelectedSupplier = this.SupplierSource.Where(m => m.Id == result.SupplierId).FirstOrDefault();
                    Model.ReturnReason = result.ReturnReason;
                    Model.Description = result.Description;
                    Model.Remark = result.Remark;

                    this.Model.Details.Clear();
                    foreach (var detail in result.Details)
                    {
                        PurchaseReturnApplyDetailEditModel detailModel = new PurchaseReturnApplyDetailEditModel();
                        detailModel.Id = detail.Id;
                        detailModel.PurchaseStorageDetailId = detail.PurchaseStorageDetailId;
                        detailModel.Quantity = detail.Quantity;
                        detailModel.Batch = detail.Batch;
                        detailModel.PurchaseStorageNumber = detail.PurchaseStorageNumber;
                        detailModel.ProductNumber = detail.ProductNumber;
                        detailModel.ProductName = detail.ProductName;
                        detailModel.ProductSpec = detail.ProductSpec;
                        detailModel.ProductUnitName = detail.ProductUnitName;
                        detailModel.Remark = detail.Remark;

                        this.Model.Details.Add(detailModel);
                    }
                }
                else
                {
                    this.PageTitle = "退货申请-新建";
                    this.Model.Number = "系统自动生成";
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow != null)
            {
                this.Model.Details.Remove(Model.SelectedRow);
            }
        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null || Model.Id == Guid.Empty)
            {
                await Create();
            }
            else
            {
                await Update();
            }
            if (this.OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
            this.Close();
        }


        public bool CanSaveAsync()
        {
            bool hasError = HasErrors();
            if (hasError == true) return !hasError;
            if (Model.Details.Count == 0)
            {
                return false;
            }
            return true;
        }


        [Command]
        public void ShowPurchaseStorageDetailSelectWindow()
        {
            PurchaseStorageDetailMultipleSelectViewModel? viewModel = _serviceProvider.GetService<PurchaseStorageDetailMultipleSelectViewModel>();
            if (viewModel != null)
            {
                WindowService.Title = "选择要退货的采购入库单";
                viewModel.SaveCallback = OnPurchaseStorageDetailSelectedCallback;
                if (this.Model.SelectedSupplier != null)
                {
                    viewModel.SupplierName = this.Model.SelectedSupplier.FullName;
                }
                WindowService.Show("PurchaseStorageDetailMultipleSelectView", viewModel);
            }
        }

        private void OnPurchaseStorageDetailSelectedCallback(ICollection<PurchaseStorageDetailSelectDto> purchaseStorageDetails)
        {
            foreach (var item in purchaseStorageDetails)
            {
                PurchaseReturnApplyDetailEditModel detail = new PurchaseReturnApplyDetailEditModel();
                detail.PurchaseStorageDetailId = item.Id;
                detail.StorageQuantity = item.Quantity;
                detail.Quantity = 0;
                detail.ProductNumber = item.ProductNumber;
                detail.ProductName = item.ProductName;
                detail.ProductSpec = item.ProductSpec;
                detail.ProductUnitName = item.ProductUnitName;
                detail.PurchaseStorageNumber = item.PurchaseStorageNumber;
                detail.Batch = item.Batch;
                this.Model.Details.Add(detail);
            }
        }

        private async Task Create()
        {
            try
            {
                this.IsLoading = true;
                PurchaseReturnApplyCreateDto dto = new PurchaseReturnApplyCreateDto();
                if (Model.SelectedSupplier != null)
                {
                    dto.SupplierId = Model.SelectedSupplier.Id;
                }

                dto.ReturnReason = Model.ReturnReason;
                dto.Description = Model.Description;
                dto.Remark = Model.Remark;
                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseReturnApplyDetailCreateDto detailDto = new PurchaseReturnApplyDetailCreateDto();
                    var detail = Model.Details[i];
                    detailDto.PurchaseStorageDetailId = detail.PurchaseStorageDetailId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Remark = detail.Remark;
                    dto.Details.Add(detailDto);
                }
                await _purchaseReturnApplyAppService.CreateAsync(dto);
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }

        private async Task Update()
        {
            try
            {
                this.IsLoading = true;
                PurchaseReturnApplyUpdateDto dto = new PurchaseReturnApplyUpdateDto();
                if (Model.SelectedSupplier != null)
                {
                    dto.SupplierId = Model.SelectedSupplier.Id;
                }
                dto.ReturnReason = Model.ReturnReason;
                dto.Description = Model.Description;
                dto.Remark = Model.Remark;

                for (int i = 0; i < Model.Details.Count; i++)
                {
                    PurchaseReturnApplyDetailUpdateDto detailDto = new PurchaseReturnApplyDetailUpdateDto();
                    var detail = Model.Details[i];
                    detailDto.PurchaseStorageDetailId = detail.PurchaseStorageDetailId;
                    detailDto.Quantity = detail.Quantity;
                    detailDto.Remark = detail.Remark;
                    dto.Details.Add(detailDto);
                }

                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _purchaseReturnApplyAppService.UpdateAsync((Guid)Model.Id, dto);
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}

