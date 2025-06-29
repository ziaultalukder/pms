﻿using PMS.Application.Common.Pagins;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account.Query;
using PMS.Application.Request.Configuration.Query;
using PMS.Domain.Models;
using PMS.Helpers;
using PMS.ViewModel;

namespace PMS.Application.Request.Account
{
    public interface IAccountsService
    {
        Task<Result> ActiveAndDeActiveUser(ActiveAndDeActiveUser request);
        Task<Result> AddOrEditUser(AddOrEditUser request);
        Task<ProfileViewModel> AdminProfile(GetAdminProfile request);
        Task<Result> ChangePassword(ChangePassword request);
        Task<IEnumerable<ClientUserViewModel>> GetClientUsers(GetClientUsers request);
        Task<Result> SendToken(SendToken request);
        Task<object> UserLogin(UserLogin request);
    }
}
