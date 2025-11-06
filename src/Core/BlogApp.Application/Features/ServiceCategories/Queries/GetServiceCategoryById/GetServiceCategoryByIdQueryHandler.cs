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

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQueryHandler : IRequestHandler<GetServiceCategoryByIdQuery, Result<ServiceCategoryDetailDto>>
    {
        private readonly IServiceCategoryRepository _repository;
        private readonly IMapper _mapper;

        public GetServiceCategoryByIdQueryHandler(IServiceCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<ServiceCategoryDetailDto>> Handle(GetServiceCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (category == null)
            {
                return Result<ServiceCategoryDetailDto>.Failure("Kategori bulunamadı.");
            }

            var dto = _mapper.Map<ServiceCategoryDetailDto>(category);
            return Result<ServiceCategoryDetailDto>.Success(dto);
        }
    }

}
