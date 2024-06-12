using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.QualityManagement.FinalInspections.Edits
{
    public class FinalInspectionEditViewModel : EditViewModelBase<FinalInspectionEditModel>
    {
        private readonly IFinalInspectionAppService _finalInspectionAppService;
        public Func<Task>? OnCloseCallbackAsync { get; set; }


        public FinalInspectionEditViewModel(IFinalInspectionAppService finalInspectionAppService)
        {
            _finalInspectionAppService = finalInspectionAppService;
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
                    var result = await _finalInspectionAppService.GetAsync(id);
                    this.Model.Number = result.Number;
                    this.Model.BadQuantity = result.BadQuantity;
                    this.Model.Description = result.Description;
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
            else
            {
                this.Model.Number = "系统自动生成";
            }

        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            await UpdateAsync();
            if (this.OnCloseCallbackAsync != null)
            {
                await OnCloseCallbackAsync();
            }
            this.Close();
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                FinalInspectionUpdateDto dto = new FinalInspectionUpdateDto();
                dto.BadQuantity = Model.BadQuantity;
                dto.Description = Model.Description;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _finalInspectionAppService.UpdateAsync((Guid)Model.Id, dto);
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
