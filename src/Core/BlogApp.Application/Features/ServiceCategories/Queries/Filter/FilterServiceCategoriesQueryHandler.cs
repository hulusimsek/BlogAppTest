using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.ServiceCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.Filter
{
    public class FilterServiceCategoriesQueryHandler
            : IRequestHandler<FilterServiceCategoriesQuery, Result<List<ServiceCategoryDto>>>
    {
        private readonly IMapper _mapper;

        public FilterServiceCategoriesQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<List<ServiceCategoryDto>>> Handle(FilterServiceCategoriesQuery request, CancellationToken cancellationToken)
        {
            var query = request.ServiceCategories.AsQueryable();

            // 🔍 Servis adı ile filtreleme
            if (!string.IsNullOrWhiteSpace(request.SearchName))
                query = query.Where(sc => sc.Name.Contains(request.SearchName, System.StringComparison.OrdinalIgnoreCase));

            // ✅ Servis aktiflik durumu filtrelemesi
            if (request.IsActive.HasValue)
                query = query.Where(sc => sc.IsActive == request.IsActive.Value);

            // 🏠 Ana sayfada gösterilip gösterilmeyeceğini filtrele
            if (request.ShowOnHomePage.HasValue)
                query = query.Where(sc => sc.ShowOnHomePage == request.ShowOnHomePage.Value);

            // 📜 SEO filtreleri
            if (!string.IsNullOrWhiteSpace(request.MetaTitle))
                query = query.Where(sc => sc.MetaTitle.Contains(request.MetaTitle, System.StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrWhiteSpace(request.MetaDescription))
                query = query.Where(sc => sc.MetaDescription.Contains(request.MetaDescription, System.StringComparison.OrdinalIgnoreCase));

            // 📝 Yorumlar için filtreleme eklenebilir (Eğer ilgili özellik varsa)
            // Burada isterseniz `ServiceDetail`'i filtrelemek veya incelemek için ilgili logic eklenebilir

            // 🔽 Sıralama
            // Sıralama in-memory (veritabanı üzerinde sıralama yapmıyorsanız)
            query = request.SortBy?.ToLower() switch
            {
                "name" => request.Descending ? query.OrderByDescending(sc => sc.Name) : query.OrderBy(sc => sc.Name),
                "displayorder" => request.Descending ? query.OrderByDescending(sc => sc.DisplayOrder) : query.OrderBy(sc => sc.DisplayOrder),
                _ => request.Descending ? query.OrderByDescending(sc => sc.DisplayOrder) : query.OrderBy(sc => sc.DisplayOrder)
            };

            // Sonuçları listele
            var serviceCategories = query.ToList();

            // DTO'ya dönüştürme
            var dtoList = _mapper.Map<List<ServiceCategoryDto>>(serviceCategories);

            return Task.FromResult(Result<List<ServiceCategoryDto>>.Success(dtoList));
        }
    }

}
