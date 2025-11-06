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

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryBySlug
{
    public class GetServiceCategoryBySlugQueryHandler : IRequestHandler<GetServiceCategoryBySlugQuery, Result<ServiceCategoryDetailDto>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetServiceCategoryBySlugQueryHandler(IServiceCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ServiceCategoryDetailDto>> Handle(GetServiceCategoryBySlugQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetBySlugAsync(request.Slug, cancellationToken);
            if (category == null)
            {
                return Result<ServiceCategoryDetailDto>.Failure("Kategori bulunamadı.");
            }

            var dto = _mapper.Map<ServiceCategoryDetailDto>(category);
            return Result<ServiceCategoryDetailDto>.Success(dto);
        }
    }
}
