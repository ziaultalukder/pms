using Dapper;
using PMS.Application.Request.Category;
using PMS.Context;
using PMS.Helpers.Interface;
using PMS.ViewModel;

namespace PMS.Helpers.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        public CategoryService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;
        }

        public async Task<IEnumerable<MedicalDepartmentNameViewModel>> GetMedicalDepartmentName()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "GetMedicalDepartmentName";
                DynamicParameters parameter = new DynamicParameters();

                var result = await context.QueryAsync<MedicalDepartmentNameViewModel>(query);
                return result.ToList();
            }
        }
    }
}
