using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.CreateModels.StoreOwner;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.RequestModels.SearchModels.StoreOwner;
using BusinessLayer.ResponseModel.ViewModels.StoreOwner;
using BusinessLayer.ResponseModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.StoreOwner
{
    public interface IProductService
    {
        Task<BasePagingViewModel<ProductsViewModel>> GetProductList(int brandId, ProductSearchModel searchModel, PagingRequestModel paging);
        Task<ProductsViewModel> GetProductById(int brandId, int productId);
        Task<int> AddProduct(int brandId, ProductCreateModel model);
    }
}
