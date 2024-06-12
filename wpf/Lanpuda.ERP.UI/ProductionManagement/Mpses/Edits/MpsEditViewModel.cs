using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using DevExpress.Mvvm.Native;
using Lanpuda.Client.Common;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.Products.Selects.SelectAll;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.ObjectMapping;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Edits
{
    public class MpsEditViewModel : EditViewModelBase<MpsEditModel>
    {
        private readonly IMpsAppService _mpsAppService;
        private readonly IObjectMapper _objectMapper;
        private readonly IServiceProvider _serviceProvider;

        public Dictionary<string, MpsType> MpsTypeSource { get; set; }

        public bool IsCanSelectProduct
        {
            get { return GetProperty(() => IsCanSelectProduct); }
            set { SetProperty(() => IsCanSelectProduct, value); }
        }

        public Func<Task>? OnCloseWindowCallbackAsync { get; set; }
        public MpsEditViewModel(
            IMpsAppService mpsAppService,
            IObjectMapper objectMapper,
            IServiceProvider serviceProvider
            )
        {
            _mpsAppService = mpsAppService;
            _objectMapper = objectMapper;
            _serviceProvider = serviceProvider;
            MpsTypeSource = EnumUtils.EnumToDictionary<MpsType>();
            IsCanSelectProduct = true;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;

                if (Model.Id != null && Model.Id != Guid.Empty)
                {
                    this.PageTitle = "生产计划-编辑";
                    if (Model.Id == null) throw new Exception("Id为空");
                    Guid id = (Guid)Model.Id;
                    var result = await _mpsAppService.GetAsync(id);
                    Model.Id = result.Id;
                    Model.Number = result.Number;
                    Model.ProductId = result.ProductId;
                    Model.ProductName = result.ProductName;
                    Model.Quantity = result.Quantity;
                    Model.StartDate = result.StartDate;
                    Model.CompleteDate = result.CompleteDate;
                    Model.Remark = result.Remark;
                    Model.MpsType = result.MpsType;
                    foreach (var item in result.Details)
                    {

                        var same = Model.MpsDetails
                            .Where(
                            m => m.ProductionDate.Day == item.ProductionDate.Day && 
                            m.ProductionDate.Month == item.ProductionDate.Month && 
                            m.ProductionDate.Year == item.ProductionDate.Year).FirstOrDefault();
                        if (same != null)
                        {
                            same.Id = item.Id;
                            same.ProductionDate = item.ProductionDate;
                            same.Quantity = item.Quantity;
                            same.Remark = item.Remark;
                        }
                        else
                        {
                            MpsDetailEditModel detaiModel = new MpsDetailEditModel(Model);
                            detaiModel.Id = item.Id;
                            detaiModel.ProductionDate = item.ProductionDate;
                            detaiModel.Quantity = item.Quantity;
                            detaiModel.Remark = item.Remark;
                            Model.MpsDetails.Add(detaiModel);
                        }
                    }

                }
                else
                {
                    this.PageTitle = "生产计划-新建";
                    var now = DateTime.Now;
                    this.Model.StartDate = new DateTime(now.Year, now.Month, now.Day);
                    this.Model.CompleteDate = new DateTime(now.Year, now.Month, now.Day);
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



            if (OnCloseWindowCallbackAsync != null)
            {
                await OnCloseWindowCallbackAsync();
            }
            CurrentWindowService.Close();

        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            if (hasError == true) return !hasError;

            if (this.Model.MpsDetails.Count == 0)
            {
                return false;
            }

            var sumValue = Model.MpsDetails.Sum(m => m.Quantity);
            if (sumValue != Model.Quantity)
            {
                return false;
            }

            return true;
        }
     


        [Command]
        public void SelectProduct()
        {
            ProductSelectAllViewModel? productSelectAllViewModel = _serviceProvider.GetService<ProductSelectAllViewModel>();
            if (productSelectAllViewModel != null)
            {
                WindowService.Title = "选择产品";
                productSelectAllViewModel.OnSelectedCallback = this.OnSelectedCallback;
                WindowService.Show("ProductSelectAllView", productSelectAllViewModel);
            }
        }

   


        [Command]
        public void Add()
        {
            MpsDetailEditModel model = new MpsDetailEditModel(this.Model);
            model.ProductionDate = DateTime.Now;
        }

        [Command]
        public void DeleteSelectedRow()
        {
            if (Model.SelectedRow == null)
            {
                return;
            }
            this.Model.MpsDetails.Remove(Model.SelectedRow);
        }


        /// <summary>
        /// 平均分配
        /// </summary>
        [Command]
        public void Avg()
        {
            var aa = (int)(this.Model.Quantity / this.Model.MpsDetails.Count);

            var bb = this.Model.Quantity % this.Model.MpsDetails.Count;

            for (int i = 0; i < Model.MpsDetails.Count; i++)
            {
                var item = Model.MpsDetails[i];
                if (i == 0)
                {
                    var total = aa + bb;
                    item.Quantity = total;

                }
                else
                {
                    item.Quantity = aa;
                }
            }

        }

        public bool CanAvg()
        {
            if (this.Model.Quantity == 0)
            {
                return false;
            }
            if (this.Model.MpsDetails.Count == 0)
            {
                return false;
            }
            return true;
        }

        [Command]
        public void Reset()
        {
            foreach (var item in Model.MpsDetails)
            {
                item.Quantity = 0;
            }
        }

        private async Task Create()
        {
            try
            {
                this.IsLoading = true;
                MpsCreateDto dto = new MpsCreateDto();
                dto.MpsType = Model.MpsType;
                dto.StartDate = Model.StartDate;
                dto.CompleteDate = Model.CompleteDate;
                dto.ProductId = Model.ProductId;
                dto.Quantity = Model.Quantity;
                dto.Remark = Model.Remark;
                foreach (var item in Model.MpsDetails)
                {
                    MpsDetailCreateDto detailDto = new MpsDetailCreateDto();
                    detailDto.ProductionDate = item.ProductionDate;
                    detailDto.Quantity = item.Quantity;
                    detailDto.Remark = item.Remark;
                    dto.Details.Add(detailDto);
                }
                await _mpsAppService.CreateAsync(dto);
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
                MpsUpdateDto dto = new MpsUpdateDto();
                dto.MpsType = Model.MpsType;
                dto.StartDate = Model.StartDate;
                dto.CompleteDate = Model.CompleteDate;
                dto.ProductId = Model.ProductId;
                dto.Quantity = Model.Quantity;
                dto.Remark = Model.Remark;
                foreach (var item in Model.MpsDetails)
                {
                    MpsDetailUpdateDto detailDto = new MpsDetailUpdateDto();
                    detailDto.Id = item.Id;
                    detailDto.ProductionDate = item.ProductionDate;
                    detailDto.Quantity = item.Quantity;
                    detailDto.Remark = item.Remark;
                    dto.Details.Add(detailDto);
                }

                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _mpsAppService.UpdateAsync((Guid)Model.Id, dto);
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

        private void OnSelectedCallback(ProductDto product)
        {
            this.Model.ProductId = product.Id;
            this.Model.ProductName = product.Name;
        }
       

    }
}

