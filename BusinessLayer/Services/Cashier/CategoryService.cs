using BusinessLayer.Interfaces.Cashier;
using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.Services;
using BusinessLayer.ViewModels;
using DataAcessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.Cashier
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //viet ham cho service
        public async Task<List<CategoriesStoreOwnerViewModel>> GetCategoryList()
        {
            var categories = await _unitOfWork.CategoryRepository
                .Get()
                .Select
                (x => new CategoriesStoreOwnerViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
                ).ToListAsync();
            return categories;
        }
    }
}
