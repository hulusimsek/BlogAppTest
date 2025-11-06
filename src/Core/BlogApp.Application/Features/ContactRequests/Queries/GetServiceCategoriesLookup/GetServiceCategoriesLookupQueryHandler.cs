using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Queries.GetServiceCategoriesLookup
{
    public class GetServiceCategoriesLookupQueryHandler
        : IRequestHandler<GetServiceCategoriesLookupQuery, List<ServiceCategoryLookupDto>>
    {
        private readonly IServiceCategoryRepository _serviceCategoryRepository;

        public GetServiceCategoriesLookupQueryHandler(IServiceCategoryRepository serviceCategoryRepository)
        {
            _serviceCategoryRepository = serviceCategoryRepository;
        }

        public async Task<List<ServiceCategoryLookupDto>> Handle(
            GetServiceCategoriesLookupQuery request,
            CancellationToken cancellationToken)
        {
            var result = await _serviceCategoryRepository.GetActiveProjectedAsync(
                x => new ServiceCategoryLookupDto
                {
                    Id = x.Id,
                    Name = x.Name
                },
                cancellationToken);
            return result;
        }
    }
}
