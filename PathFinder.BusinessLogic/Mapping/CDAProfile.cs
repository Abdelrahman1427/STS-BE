using AutoMapper;
using PathFinder.Core.Entities;
using PathFinder.DataTransferObjects.DTOs.CDA;
using PathFinder.DataTransferObjects.DTOs.City;
using PathFinder.DataTransferObjects.DTOs.Shared.Request;
using PathFinder.DataTransferObjects.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.BusinessLogic.Mapping
{
    public class CDAProfile :Profile
    {
        public CDAProfile()
        {
            CreateMap<CommunityDevlopmentAssosition, GetLookUpDefinitionDTO>().ReverseMap();
            CreateMap<Pagination<CDAdto>, Pagination<GetLookUpDefinitionDTO>>().ReverseMap();
        }
    }
}
