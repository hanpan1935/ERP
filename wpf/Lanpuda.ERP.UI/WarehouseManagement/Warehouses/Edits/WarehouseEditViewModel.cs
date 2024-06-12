using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.WarehouseManagement.Warehouses.Edits
{
    public class WarehouseEditViewModel : EditViewModelBase<WarehouseEditModel>
    {
        private readonly IWarehouseAppService _warehouseAppService;
        public Func<Task>? Refresh { get; set; }

        public WarehouseEditViewModel(
            IWarehouseAppService warehouseAppService
            )
        {
            _warehouseAppService = warehouseAppService;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;

                if (Model.Id != null)
                {
                    if (this.Model.Id == null || this.Model.Id == Guid.Empty) throw new Exception("Id 不能为空");
                    Guid id = (Guid)this.Model.Id;
                    var result = await _warehouseAppService.GetAsync(id);
                    this.Model.Name = result.Name;
                    Model.Number = result.Number;
                    Model.Remark = result.Remark;
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


        [Command]
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
                WarehouseCreateDto dto = new WarehouseCreateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                await _warehouseAppService.CreateAsync(dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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
                WarehouseUpdateDto dto = new WarehouseUpdateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                if (this.Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _warehouseAppService.UpdateAsync((Guid)this.Model.Id, dto);
                if (Refresh != null)
                {
                    await Refresh();
                    this.CurrentWindowService.Close();
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
