using STS.Core.Entities;
using STS.Core.Interface.Shared.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STS.Core.Interface.IDefinitionServices
{
    public interface IProductService : ICrudWithPaginateService<Product>
    {
    }
}
