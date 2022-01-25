using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoriesStoreOwnerViewModel>> GetCategoryList();
    }
}
