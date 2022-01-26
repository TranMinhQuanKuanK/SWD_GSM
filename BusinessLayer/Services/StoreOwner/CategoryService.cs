using BusinessLayer.Interfaces.StoreOwner;
using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.ResponseModels.ViewModels.StoreOwner;
using BusinessLayer.Services;
using DataAcessLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services.StoreOwner
{
    public class CategoryService : BaseService, ICategoryService
    {
        public CategoryService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
        //viet ham cho service
        public async Task<List<CategoriesViewModel>> GetCategoryList()
        {
            var categories = await _unitOfWork.CategoryRepository
                .Get()
                .Select
                (x => new CategoriesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
                ).ToListAsync();
            return categories;
        }
    }
}
