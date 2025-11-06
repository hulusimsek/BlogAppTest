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

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetActiveMinimalCategories
{
    public class GetActiveMinimalCategoriesQueryHandler
        : IRequestHandler<GetActiveMinimalCategoriesQuery, Result<List<MinimalCategoryDto>>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetActiveMinimalCategoriesQueryHandler(IServiceCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<MinimalCategoryDto>>> Handle(GetActiveMinimalCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllActiveAsync(cancellationToken);
            var dtoList = _mapper.Map<List<MinimalCategoryDto>>(categories);
            return Result<List<MinimalCategoryDto>>.Success(dtoList);
        }
    }
}
