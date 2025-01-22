using PMS.Application.Common.Pagins;
using PMS.Application.Request.Configuration.Command;
using PMS.Application.Request.Configuration.Query;
using PMS.Domain.Models;
using PMS.Helpers;
using PMS.Models;
using PMS.ViewModel;

namespace PMS.Application.Request.Configuration
{
    public interface IConfigurationService
    {
        Task<PagedList<MedicineListViewModel>> MedicineList(MedicineList request);
        Task<PagedList<Client>> GetAllClient(GetAllClient request);
        Task<PagedList<Supplier>> GetSupplier(GetSupplier request);
        Task<IEnumerable<MedicineListByNameViewModel>> MedicineListByName(MedicineListByName request);
        Task<PagedList<ClientWiseMedicineViewModel>> ClientWiseMedicine(GetClientWiseMedicine request);
        Task<Result> UpdateClient(UpdateClient request);
        Task<Result> AddClient(AddClient request);
        Task<IEnumerable<PopularMedicineViewModel>> PopularMedicine();
        Task<IEnumerable<Division>> GetDivision();
    }
}
