using BusinessLayer.Interfaces.SystemAdmin;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.ResponseModels.ViewModels.StoreOwner;
using BusinessLayer.Services;
using DataAcessLayer.Interfaces;
using DataAcessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static DataAcessLayer.Models.User;

namespace BusinessLayer.Services.SystemAdmin
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        public async Task<StoreOwnerViewModel> Login(LoginModel login)
        {
            var cashier = await _unitOfWork.UserRepository 
                .Get().Where(x => x.Username == login.Username && x.Password == login.Password)
                .Where(x => x.Status == UserStatus.Enabled)
                .Select(x => new StoreOwnerViewModel()
                {
                    Id = x.Id,
                    Username = x.Username,
                    Name = x.Name,
                    Email = x.Email,
                    Phone = x.Phone
                }).FirstOrDefaultAsync();
            return cashier;
        }
    }
}
