using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lanpuda.ERP.BasicData.ProductUnits.Edits
{
    public class ProductUnitEditViewModel : EditViewModelBase<ProductUnitEditModel>
    {
        private readonly IProductUnitAppService _productUnitAppService;
        public Func<Task>? OnCloseCallbackAsync { get; set; }


        public ProductUnitEditViewModel(IProductUnitAppService productUnitAppService)
        {
            _productUnitAppService = productUnitAppService;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            if (Model.Id !=null)
            {
                try
                {
                    if (Model.Id == null) return;
                    this.IsLoading = true;
                    Guid id = (Guid)Model.Id;
                    var result = await _productUnitAppService.GetAsync(id);
                    this.Model.Name = result.Name;
                    this.Model.Number = result.Number;
                    this.Model.Remark = result.Remark;
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
                ProductUnitCreateDto dto = new ProductUnitCreateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                await _productUnitAppService.CreateAsync(dto);
                if (OnCloseCallbackAsync != null)
                {
                    await OnCloseCallbackAsync();
                    if (CurrentWindowService != null)
                        CurrentWindowService.Close();
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
                ProductUnitUpdateDto dto = new ProductUnitUpdateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _productUnitAppService.UpdateAsync((Guid)Model.Id, dto);
                if (OnCloseCallbackAsync != null)
                {
                    await OnCloseCallbackAsync();
                    if (CurrentWindowService != null)
                        CurrentWindowService.Close();
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
    }
}
