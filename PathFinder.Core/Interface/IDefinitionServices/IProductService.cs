using Microsoft.EntityFrameworkCore.Query;
using STS.Core.Entities;
using STS.Core.Interface.Shared.IServices;
using STS.DataTransferObjects.DTOs.Product;
using STS.DataTransferObjects.DTOs.Shared.Request;
using STS.DataTransferObjects.Helpers;
using STS.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace STS.Core.Interface.IDefinitionServices
{
    public interface IProductService : ICrudService<Product>
    {
        Task<IPagination<Product>> GetPageAsync( PagingDTO<FilterProductDTO> paginationDto);

    }
}
