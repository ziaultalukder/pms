﻿using Azure.Core;
using Dapper;
using PMS.Application.Common.Pagins;
using PMS.Application.Request.Configuration;
using PMS.Application.Request.Configuration.Command;
using PMS.Application.Request.Configuration.Query;
using PMS.Context;
using PMS.Domain.Models;
using PMS.Helpers.Interface;
using PMS.Models;
using PMS.ViewModel;
using System.Data;

namespace PMS.Helpers.Service
{
    public class ConfigurationService : IConfigurationService
    {
        private readonly DapperContext _dapperContext;
        private readonly ICurrentUserService _currentUserService;
        string Id = "";
        string ClientId = "";
        public ConfigurationService(DapperContext dapperContext, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _currentUserService = currentUserService;
        }
        public async Task<PagedList<MedicineListViewModel>> MedicineList(MedicineList request)
        {
            try
            {
                using(var context = _dapperContext.CreateConnection())
                {
                    string conditionClause = " ";
                    string query = "select SL, ManufacturerName, BrandName, GenericName, Strength, DosageDescription, count(*) over() as TotalItems from MedicineList ";

                    if (!string.IsNullOrEmpty(request.Name))
                    {
                        query += Helper.GetSqlCondition(conditionClause, "AND") + " BrandName like '%" + request.Name.Trim() + "%' ";
                        conditionClause = " WHERE ";
                    }
                    if (!string.IsNullOrEmpty(request.GetAll) && request.GetAll.ToUpper() == "Y")
                    {
                        //query += " order by SL ";
                        request.ItemsPerPage = 0;
                    }
                    else
                    {
                        query += " order by SL OFFSET " + ((request.CurrentPage - 1) * request.ItemsPerPage) + " ROWS FETCH NEXT " + request.ItemsPerPage + " ROWS ONLY ";
                    }
                    var medicineList = await context.QueryAsync<MedicineListViewModel>(query);
                    string totalItemQuery = "SELECT  top 1 TotalItems FROM ( " + query + ")  AS result  ORDER BY TotalItems";
                    int totalItem = context.QueryFirstOrDefault<int>(totalItemQuery);
                    if (request.ItemsPerPage == 0)
                    {
                        request.ItemsPerPage = totalItem;
                    }
                    return new PagedList<MedicineListViewModel>(medicineList.ToList(), request.CurrentPage, request.ItemsPerPage, totalItem);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<PagedList<Client>> GetAllClient(GetAllClient request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string conditionClause = " ";
                string query = "SELECT Client.* , count(*) over() as TotalItems from Client ";
                if (request.Id > 0)
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " Id = " + request.Id;
                    conditionClause = " WHERE ";
                }
                if (!string.IsNullOrEmpty(request.ShopName))
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " ShopName = '" + request.ShopName.Trim() + "' ";
                    conditionClause = " WHERE ";
                }
                if (!string.IsNullOrEmpty(request.ContactNo))
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " ContactNo = '" + request.ContactNo.Trim() + "' ";
                    conditionClause = " WHERE ";
                }
                if (!string.IsNullOrEmpty(request.GetAll) && request.GetAll.ToUpper() == "Y")
                {
                    //query += " order by id desc ";
                    request.ItemsPerPage = 0;
                }
                else
                {
                    query += " order by id OFFSET " + ((request.CurrentPage - 1) * request.ItemsPerPage) + " ROWS FETCH NEXT " + request.ItemsPerPage + " ROWS ONLY ";
                }
                var clientList = await context.QueryAsync<Client>(query);
                string totalItemQuery = "SELECT  top 1 TotalItems FROM ( " + query + ")  AS result  ORDER BY TotalItems";
                int totalItem = context.QueryFirstOrDefault<int>(totalItemQuery);
                if (request.ItemsPerPage == 0)
                {
                    request.ItemsPerPage = totalItem;
                }
                return new PagedList<Client>(clientList.ToList(), request.CurrentPage, request.ItemsPerPage, totalItem);
            }
        }
        public async Task<PagedList<Supplier>> GetSupplier(GetSupplier request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string ds = ClientId;

                string conditionClause = " ";
                string query = "SELECT Supplier.* , count(*) over() as TotalItems from Supplier ";

                if (request.Id > 0)
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " Id = " + request.Id;
                    conditionClause = " WHERE ";
                }

                if (!string.IsNullOrEmpty(request.Name))
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " Name = '" + request.Name.Trim() + "' ";
                    conditionClause = " WHERE ";
                }

                if (!string.IsNullOrEmpty(request.GetAll) && request.GetAll.ToUpper() == "Y")
                {
                    //query += " order by id desc ";
                    request.ItemsPerPage = 0;
                }
                else
                {
                    query += " order by id OFFSET " + ((request.CurrentPage - 1) * request.ItemsPerPage) + " ROWS FETCH NEXT " + request.ItemsPerPage + " ROWS ONLY ";
                }

                var supplierList = await context.QueryAsync<Supplier>(query);
                string totalItemQuery = "SELECT  top 1 TotalItems FROM ( " + query + ")  AS result  ORDER BY TotalItems";
                int totalItem = context.QueryFirstOrDefault<int>(totalItemQuery);
                if (request.ItemsPerPage == 0)
                {
                    request.ItemsPerPage = totalItem;
                }
                return new PagedList<Supplier>(supplierList.ToList(), request.CurrentPage, request.ItemsPerPage, totalItem);
            }
        }
        public async Task<IEnumerable<MedicineListByNameViewModel>> MedicineListByName(MedicineListByName request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                if(request.Name is not null)
                {
                    string qry = "declare @clientId int\r\nset @clientId = "+_currentUserService.ClientId+ ";\r\nselect \r\n\tM.SL, \r\n\tM.ManufacturerName,\r\n\tCONCAT(M.BrandName, ' - ' ,M.Strength, ' - ', DosageDescription) BrandName,\r\n\tISNULL(abc.SalesPrice, 0) SalesPrice, \r\n\tISNULL(abc.PurchasePrice, 0) PurchasePrice,\r\n\tcase \r\n\twhen ISNULL(abc.ClientId, 0) = " + _currentUserService.ClientId+"\r\n\tthen ISNULL(abc.Quantity, 0) else 0\r\n\tend as Quantity\r\nfrom (\r\n\tselect MedicineId, ClientId, Quantity, PurchasePrice, SalesPrice from ClientWiseMedicine where ClientId ="+_currentUserService.ClientId+"\r\n) abc right join MedicineList M on M.SL = abc.MedicineId\r\nwhere M.BrandName like '"+ request.Name.Trim() + "%'";
                    
                    var medicineNameList = await context.QueryAsync<MedicineListByNameViewModel>(qry);
                    return medicineNameList.ToList();
                }
                else
                {
                    return null;
                }
            }
        }
        public async Task<PagedList<ClientWiseMedicineViewModel>> ClientWiseMedicine(GetClientWiseMedicine request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string conditionClause = " ";
                string query = "select ClientWiseMedine_VW.*, count(*) over() as TotalItems from ClientWiseMedine_VW WHERE ClientId =" + _currentUserService.ClientId;
                
                /*if (!string.IsNullOrEmpty(request.MedicineName))
                {
                    query += Helper.GetSqlCondition(conditionClause, "AND") + " BrandName Like '" + request.MedicineName.Trim() + "%' ";
                    conditionClause = " WHERE ";
                }*/

                if (!string.IsNullOrEmpty(request.GetAll) && request.GetAll.ToUpper() == "Y")
                {
                    request.ItemsPerPage = 0;
                }
                else
                {
                    query += " order by Id OFFSET " + ((request.CurrentPage - 1) * request.ItemsPerPage) + " ROWS FETCH NEXT " + request.ItemsPerPage + " ROWS ONLY ";
                }
                var clientWiseMedicine = await context.QueryAsync<ClientWiseMedicineViewModel>(query);
                string totalItemQuery = "SELECT  top 1 TotalItems FROM ( " + query + ")  AS result  ORDER BY TotalItems";
                int totalItem = context.QueryFirstOrDefault<int>(totalItemQuery);
                if (request.ItemsPerPage == 0)
                {
                    request.ItemsPerPage = totalItem;
                }
                return new PagedList<ClientWiseMedicineViewModel>(clientWiseMedicine.ToList(), request.CurrentPage, request.ItemsPerPage, totalItem);
            }
        }
        public async Task<Result> UpdateClient(UpdateClient request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return Result.Failure(new List<string> { "Name is required" });
            }
            if (string.IsNullOrEmpty(request.ShopName))
            {
                return Result.Failure(new List<string> { "ShopName is required" });
            }
            if (string.IsNullOrEmpty(request.ContactNo))
            {
                return Result.Failure(new List<string> { "ContactNo is required" });
            }

            using (var context = _dapperContext.CreateConnection())
            {
                string query = "UpdateClient";
                DynamicParameters parameter = new DynamicParameters();

                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@ShopName", request.ShopName, DbType.String, ParameterDirection.Input);
                parameter.Add("@Address", request.Address, DbType.String, ParameterDirection.Input);
                parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);
                parameter.Add("@Email", request.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                int res = parameter.Get<int>("@MESSAGE");

                if (res > 0)
                {
                    return Result.Success("Update Success");
                }
                else
                {
                    return Result.Failure(new List<string> { "Client save failed" });
                }
            }
        }
        public async Task<Result> AddClient(AddClient request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Name))
                {
                    return Result.Failure(new List<string> { "Name is required" });
                }
                if (string.IsNullOrEmpty(request.ShopName))
                {
                    return Result.Failure(new List<string> { "ShopName is required" });
                }
                if (string.IsNullOrEmpty(request.ContactNo))
                {
                    return Result.Failure(new List<string> { "ContactNo is required" });
                }
                if (string.IsNullOrEmpty(request.Password))
                {
                    return Result.Failure(new List<string> { "Password is required" });
                }

                using (var context = _dapperContext.CreateConnection())
                {
                    var qryparameter = new { ContactNo = request.ContactNo };
                    string queryForCheckUserId = "select * from Client where ContactNo = @ContactNo";
                    var client = await context.QueryFirstOrDefaultAsync<Client>(queryForCheckUserId, qryparameter);
                    if (client != null)
                    {
                        return Result.Failure(new List<string> { "ContactNo Already Exist" });
                    }

                    request.Password = MD5Encryption.GetMD5HashData(request.Password);

                    string query = "SP_Client";
                    DynamicParameters parameter = new DynamicParameters();

                    parameter.Add("@Id", 0, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ShopName", request.ShopName, DbType.String, ParameterDirection.Input);
                    parameter.Add("@Address", request.Address, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);
                    parameter.Add("@Email", request.Email, DbType.String, ParameterDirection.Input);
                    parameter.Add("@Password", request.Password, DbType.String, ParameterDirection.Input);
                    parameter.Add("@CreateBy", Id, DbType.String, ParameterDirection.Input);
                    parameter.Add("@MESSAGE", "", DbType.Int32, ParameterDirection.Output);

                    var result = await context.ExecuteAsync(query, parameter);
                    int res = parameter.Get<int>("@MESSAGE");

                    return Result.Success("Save Success");
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }
        public async Task<IEnumerable<PopularMedicineViewModel>> PopularMedicine()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "PopularMedicineList";
                DynamicParameters parameter = new DynamicParameters();

                var result = await context.QueryAsync<PopularMedicineViewModel>(query);
                return result.ToList();
            }
        }
        public async Task<IEnumerable<Division>> GetDivision()
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "SELECT * FROM Division";
                var result = await context.QueryAsync<Division>(query);
                return result.ToList();
            }
        }
    }
}
