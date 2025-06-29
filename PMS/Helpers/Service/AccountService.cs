using Dapper;
using Microsoft.IdentityModel.Tokens;
using PMS.Application.Request.Account;
using PMS.Application.Request.Account.Command;
using PMS.Application.Request.Account.Query;
using PMS.Context;
using PMS.Domain.Models;
using PMS.Helpers.Interface;
using PMS.ViewModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PMS.Helpers.Service
{
    public class AccountService : IAccountsService
    {
        private readonly DapperContext _dapperContext;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;

        public AccountService(DapperContext dapperContext, IConfiguration configuration, ICurrentUserService currentUserService)
        {
            _dapperContext = dapperContext;
            _configuration = configuration;
            _currentUserService = currentUserService;
        }
        public async Task<Result> AddOrEditUser(AddOrEditUser request)
        {
            if (string.IsNullOrEmpty(request.Mobile))
            {
                return Result.Failure(new List<string> { "Mobile No is required" });
            }
            using (var context = _dapperContext.CreateConnection())
            {
                if (request.Id == 0)
                {
                    string queryForCheckUserId = "SELECT * FROM Users where Mobile = '" + request.Mobile + "'";
                    var userData = await context.QueryFirstOrDefaultAsync<Users>(queryForCheckUserId);
                    if (userData != null)
                    {
                        return Result.Failure(new List<string> { "Mobile No Already Exist" });
                    }
                }

                request.Password = MD5Encryption.GetMD5HashData(request.Password);

                string query = "SP_Users";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Name", request.Name, DbType.String, ParameterDirection.Input);
                parameter.Add("@Emaill", request.Email, DbType.String, ParameterDirection.Input);
                parameter.Add("@Mobile", request.Mobile, DbType.String, ParameterDirection.Input);
                parameter.Add("@ClientId", _currentUserService.ClientId, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@Password", request.Password, DbType.String, ParameterDirection.Input);
                parameter.Add("@IsClientUser", "Y", DbType.String, ParameterDirection.Input);
                /*parameter.Add("@IsClientUser", _currentUserService.ClientId == 0 ? null : "Y", DbType.String, ParameterDirection.Input);*/
                parameter.Add("@IsActive", request.IsActive, DbType.String, ParameterDirection.Input);
                parameter.Add("@Status", request.Status, DbType.String, ParameterDirection.Input);
                parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                parameter.Add("@Result", "", DbType.String, ParameterDirection.Output);

                var result = await context.ExecuteAsync(query, parameter);
                if (result > 0)
                {
                    return Result.Success("User Save Success");
                }
                else
                {
                    return Result.Failure(new List<string> { "user save failed" });
                }
            }
        }
        public async Task<ProfileViewModel> AdminProfile(GetAdminProfile request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "SELECT [Id] ,[Name] ,[ShopName] ,[Address] ,[ContactNo] ,[Email] ,[CreateDate] FROM [dbo].[Client] WHERE Id = " + _currentUserService.ClientId;
                var res = await context.QueryFirstOrDefaultAsync<ProfileViewModel>(query);

                var sql = "select Id, Name, Email, Mobile, IsActive from Users where ClientId =" + _currentUserService.ClientId + " and IsClientUser = 'Y'";
                var userlist = context.Query<ClientUserViewModel>(sql).ToList();

                res.ClientUsers = userlist;

                return res;
            }
        }
        public async Task<Result> ChangePassword(ChangePassword request)
        {
            try
            {
                using (var context = _dapperContext.CreateConnection())
                {
                    var parameter = new { Id = request.Id };
                    string query = "select * from Users where Id = @Id ";
                    var user = await context.QueryFirstOrDefaultAsync<Users>(query, parameter);
                    if (user == null)
                    {
                        return Result.Failure(new List<string> { "Invalid Current User!!!!" });
                    }

                    request.OldPassword = MD5Encryption.GetMD5HashData(request.OldPassword);
                    if (user.Password == request.OldPassword)
                    {
                        var updateQryParameter = new { Password = MD5Encryption.GetMD5HashData(request.NewPassword), UserId = request.Id };
                        string updatePasswordQuery = "update Users set Password = @Password where Id = @UserId ";
                        var res = await context.QueryAsync(updatePasswordQuery, updateQryParameter);

                        return Result.Success("success");
                    }
                    else
                    {
                        return Result.Failure(new List<string> { "Invalid Old Password!!!!" });
                    }
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }
        public async Task<Result> SendToken(SendToken request)
        {
            try
            {
                using (var context = _dapperContext.CreateConnection())
                {
                    var tokenCode = TokenGenerator();
                    string query = "SP_Token";
                    DynamicParameters parameter = new DynamicParameters();
                    parameter.Add("@id", 0, DbType.Int32, ParameterDirection.Input);
                    parameter.Add("@Code", tokenCode, DbType.String, ParameterDirection.Input);
                    parameter.Add("@ContactNo", request.ContactNo, DbType.String, ParameterDirection.Input);
                    parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                    parameter.Add("@MESSAGE", "", DbType.String, ParameterDirection.Output);
                    var response = await context.ExecuteAsync(query, parameter);

                    var result = parameter.Get<string>("@MESSAGE");
                    string conactNo = "88" + request.ContactNo;

                    if (!string.IsNullOrEmpty(result))
                    {
                        string smsBody = "E-Village verification code is " + tokenCode;
                        var data = Helper.SendSms(conactNo, smsBody);
                        //var sendEmail = Helper.SendEmail(cmnToken.UserEmail, "SaRa Seller Registration-Token", smsBody, null);
                        //var sendEmail = Helper.SendEmail(cmnToken.UserContactNo, "SaRa Seller Registration-Token", smsBody, null);
                    }
                    return Result.Success(result);
                }
            }
            catch (Exception ex)
            {
                return Result.Failure(new List<string> { ex.Message });
            }
        }
        public string TokenGenerator()
        {
            Random rnd = new Random();
            return (rnd.Next(100000, 999999)).ToString();
        }
        public async Task<object> UserLogin(UserLogin request)
        {
            if (string.IsNullOrEmpty(request.ContactNo))
            {
                return Result.Failure(new List<string> { "Contact No is required" });
            }
            if (string.IsNullOrEmpty(request.Password))
            {
                return Result.Failure(new List<string> { "Password No is required" });
            }

            using (var context = _dapperContext.CreateConnection())
            {
                var parameter = new { ContactNO = request.ContactNo };
                string query = "SELECT Id, Name, Email, Mobile, ClientId, Password, IsActive, Status, ISNULL(IsClientUser, 'N') IsClientUser FROM Users where Mobile = @ContactNo";
                var userData = await context.QueryFirstOrDefaultAsync<Users>(query, parameter);
                if (userData.IsActive == "N")
                {
                    return Result.Failure(new List<string> { "Your Account Is DeActivated!!" });
                }
                if (userData != null)
                {
                    /*var hasPass = Helper.HashPassword(request.Password, userData.PasswordKey);*/

                    request.Password = MD5Encryption.GetMD5HashData(request.Password);

                    if (userData.Password == request.Password)
                    {
                        var user = new UsersViewModel
                        {
                            Id = userData.Id,
                            Email = userData.Email,
                            Name = userData.Name,
                            ClientId = userData.ClientId,
                        };
                        var token = GenerateJWTToken(user);

                        return new
                        {
                            succeed = true,
                            token = token,
                            name = userData.Name,
                            IsClientUser = userData.IsClientUser,
                        };
                    }
                    else
                    {
                        return Result.Failure(new List<string> { "UserId and Password Dose Not Match" });
                    }
                }
                else
                {
                    return Result.Failure(new List<string> { "User Not Found" });
                }
            }
        }
        public string GenerateJWTToken(UsersViewModel user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.ClientId.ToString()),
            };

            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddHours(3),
                signingCredentials: new SigningCredentials
                (
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"])),
                    SecurityAlgorithms.HmacSha256Signature)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
        private async Task<string> GenerateJwtToken(UsersViewModel user)
        {
            var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Email, user.ClientId.ToString()),
                };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<IEnumerable<ClientUserViewModel>> GetClientUsers(GetClientUsers request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                var sql = "select Id, Name, Email, Mobile, IsActive from Users where ClientId ="+_currentUserService.ClientId+ " and IsClientUser = 'Y'";
                return context.Query<ClientUserViewModel>(sql).ToList();
            }
        }

        public async Task<Result> ActiveAndDeActiveUser(ActiveAndDeActiveUser request)
        {
            using (var context = _dapperContext.CreateConnection())
            {
                string query = "ActiveAndDeActiveUser";
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@Id", request.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@IsActive", request.IsActive, DbType.String, ParameterDirection.Input);
                parameter.Add("@CreateBy", _currentUserService.UserId, DbType.String, ParameterDirection.Input);
                parameter.Add("@Message", "", DbType.String, ParameterDirection.Output);
                var result = await context.ExecuteAsync(query, parameter);
                return Result.Success(parameter.Get<string>("@Message"));
            }
        }
    }
}
