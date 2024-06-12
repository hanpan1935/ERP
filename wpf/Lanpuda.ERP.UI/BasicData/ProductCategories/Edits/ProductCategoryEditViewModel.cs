using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.BasicData.ProductCategories.Edits
{
    public class ProductCategoryEditViewModel : EditViewModelBase<ProductCategoryEditModel>
    {
        private readonly IProductCategoryAppService _productCategoryAppService;
        public Func<Task>? Refresh { get; set; }


        public ProductCategoryEditViewModel(IProductCategoryAppService productCategoryAppService)
        {
            _productCategoryAppService = productCategoryAppService;
        }

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            if (Model.Id != null)
            {
                try
                {
                    if (Model.Id == null) return;
                    this.IsLoading = true;
                    Guid id = (Guid)Model.Id;
                    var result = await _productCategoryAppService.GetAsync(id);
                    this.Model.Name = result.Name;
                    this.Model.Number = result.Number;
                    this.Model.Remark = result.Remark;
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


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductCategoryCreateDto dto = new ProductCategoryCreateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                await _productCategoryAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.Close();
                }
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


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                ProductCategoryUpdateDto dto = new ProductCategoryUpdateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _productCategoryAppService.UpdateAsync((Guid)Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.Close();
                }
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
