using BusinessLayer.Interfaces;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.ResponseModels.ViewModels;
using BusinessLayer.ViewModels;
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

namespace BusinessLayer.Services
{
    public class ProductService : BaseService, IProductService
    {
        public ProductService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<BasePagingViewModel<ProductsStoreOwnerViewModel>> GetProductList(int brandId, ProductSearchModel searchModel, PagingRequestModel paging)
        {
            var productsData = await _unitOfWork.ProductRepository
                .Get().Where(x => x.BrandId == brandId)
                .Select
                (x => new ProductsStoreOwnerViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnpackedProductId = x.UnpackedProductId,
                    UnpackedProductName = x.UnpackedProduct.Name,
                    BuyPrice = x.BuyPrice,
                    SellPrice = x.SellPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    ConversionRate = x.ConversionRate,
                    UnitLabel = x.UnitLabel,
                    LowerThreshold = x.LowerThreshold,
                    Status = (int)x.Status
                }
                ).ToListAsync();

            productsData = productsData
                        .Where(x =>
                            StringNormalizer.VietnameseNormalize(x.Name)
                            .Contains(StringNormalizer.VietnameseNormalize(searchModel.SearchTerm)))
                        .Where(x => (searchModel.MinimumBuyingPrice != null)
                                            ? x.BuyPrice >= searchModel.MinimumBuyingPrice
                                            : true)
                        .Where(x => (searchModel.MaximumBuyingPrice != null)
                                            ? x.BuyPrice <= searchModel.MaximumBuyingPrice
                                            : true)
                        .Where(x => (searchModel.MinimumSellingPrice != null)
                                            ? x.SellPrice >= searchModel.MinimumSellingPrice
                                            : true)
                        .Where(x => (searchModel.MaximumSellingPrice != null)
                                            ? x.SellPrice <= searchModel.MaximumSellingPrice
                                            : true)
                        .Where(x => (searchModel.Status != null)
                                            ? x.Status == (int)searchModel.Status
                                            : true)
                        .ToList();

            int totalItem = productsData.Count;

            productsData = productsData.Skip((paging.PageIndex - 1) * paging.PageSize)
                .Take(paging.PageSize).ToList();

            var productResult = new BasePagingViewModel<ProductsStoreOwnerViewModel>()
            {
                PageIndex = paging.PageIndex,
                PageSize = paging.PageSize,
                TotalItem = totalItem,
                TotalPage = (int)Math.Ceiling((decimal)totalItem / (decimal)paging.PageSize),
                Data = productsData
            };
            return productResult;
        }
        public async Task<ProductsStoreOwnerViewModel> GetProductById(int brandId,int productId)
        {
            var product = await _unitOfWork.ProductRepository
              .Get().Where(x => x.BrandId == brandId).Where(x=>x.Id == productId).Select
                (x => new ProductsStoreOwnerViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    UnpackedProductId = x.UnpackedProductId,
                    UnpackedProductName = x.UnpackedProduct.Name,
                    BuyPrice = x.BuyPrice,
                    SellPrice = x.SellPrice,
                    CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                    ConversionRate = x.ConversionRate,
                    UnitLabel = x.UnitLabel,
                    LowerThreshold = x.LowerThreshold,
                    Status = (int)x.Status
                }
                ).FirstOrDefaultAsync();
            return product;
        }

        public async Task<int> AddProduct(int brandId,ProductCreateModel model)
        {
            var product = new Product()
            {
                Name = model.Name,
                UnpackedProductId = model.UnpackedProductId,
                BuyPrice = model.BuyPrice,
                SellPrice = model.SellPrice,
                CategoryId = model.CategoryId,
                ConversionRate = model.ConversionRate,
                UnitLabel = model.UnitLabel,
                BrandId = brandId,
                LowerThreshold = model.LowerThreshold,
                Status = Product.ProductStatus.Selling
            };
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
            return product.Id;
        }
    }
}
