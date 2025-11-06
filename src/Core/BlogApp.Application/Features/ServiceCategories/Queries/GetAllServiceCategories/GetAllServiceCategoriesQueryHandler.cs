using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetAllServiceCategories
{
    public class GetAllServiceCategoriesQueryHandler : IRequestHandler<GetAllServiceCategoriesQuery, Result<List<ServiceCategoryDto>>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetAllServiceCategoriesQueryHandler(IServiceCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<ServiceCategoryDto>>> Handle(GetAllServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllActiveAsync(cancellationToken);
            var dtos = _mapper.Map<List<ServiceCategoryDto>>(categories);
            return Result<List<ServiceCategoryDto>>.Success(dtos);
        }
    }
}
