using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;
using System;
using System.Threading.Tasks;

namespace Lanpuda.ERP.QualityManagement.ArrivalInspections.Edits
{
    public class ArrivalInspectionEditViewModel : EditViewModelBase<ArrivalInspectionEditModel>
    {
        private readonly IArrivalInspectionAppService _arrivalInspectionAppService;
        public Func<Task>? Refresh { get; set; }


        public ArrivalInspectionEditViewModel(IArrivalInspectionAppService arrivalInspectionAppService)
        {
            _arrivalInspectionAppService = arrivalInspectionAppService;
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
                    var result = await _arrivalInspectionAppService.GetAsync(id);
                    this.Model.Number = result.Number;
                    this.Model.BadQuantity = result.BadQuantity;
                    this.Model.Description = result.Description;
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
            await UpdateAsync();
            if (Refresh != null)
            {
                await Refresh();
                this.Close();
            }
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
                ArrivalInspectionUpdateDto dto = new ArrivalInspectionUpdateDto();
                dto.BadQuantity = Model.BadQuantity;
                dto.Description = Model.Description;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _arrivalInspectionAppService.UpdateAsync((Guid)Model.Id, dto);

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
