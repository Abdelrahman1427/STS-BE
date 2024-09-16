using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using STS.BusinessLogic.Services.Shared;
using STS.Core.Entities;
using STS.Core.Interface.IDefinitionServices;
using STS.Core.Interfaces.Shared.Repository;
using STS.DataTransferObjects.DTOs.Product;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Interfaces;
using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;


namespace STS.BusinessLogic.Services.DefinitionServices
{
    public class ProductService : CrudService<Product>, IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _context;

        public ProductService(IUnitOfWork unitOfWork, IHttpContextAccessor context) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _context = context;
        }

        public async Task<IPagination<Product>> GetPageAsync(PagingDTO<FilterProductDTO> paginationDto)
        {

            Expression<Func<Product, bool>> predicate = c => c.Id !=null;

            if (!string.IsNullOrWhiteSpace(paginationDto.Filter.CategoryId?.ToString()) && paginationDto.Filter.CategoryId != 0)
            predicate = f => paginationDto.Filter.CategoryId == f.CategoryId;

            var entity = await _unitOfWork.GetRepositoryAsync<Product>()
                                                              .GetListPaginitionAsync(
                                                                    predicate: predicate,
                                                                    include: x=>x.Include(src =>src.Category),
                                                                    index: paginationDto.PageIndex,
                                                                    size: paginationDto.PageSize,
                                                                    sorting: paginationDto?.SortingDTO
                                                                  );
            return entity;
        }

    }
}
