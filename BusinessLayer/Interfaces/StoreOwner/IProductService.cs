using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces.StoreOwner
{
    public interface IProductService
    {
        Task<BasePagingViewModel<ProductsStoreOwnerViewModel>> GetProductList(int brandId, ProductSearchModel searchModel, PagingRequestModel paging);
        Task<ProductsStoreOwnerViewModel> GetProductById(int brandId, int productId);
        Task<int> AddProduct(int brandId, ProductCreateModel model);
    }
}
