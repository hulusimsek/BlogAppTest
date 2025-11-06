using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.Filter
{
    public class FilterServiceCategoriesQuery : IRequest<Result<List<ServiceCategoryDto>>>
    {
        public List<ServiceCategory> ServiceCategories { get; set; } = new();

        // Filtreleme parametreleri
        public string? SearchName { get; set; }  // Servis adına göre arama (örn: "Ceza Hukuku")
        public bool? IsActive { get; set; }  // Servisin aktif olup olmadığı
        public bool? ShowOnHomePage { get; set; }  // Ana sayfada gösterilip gösterilmeyeceği
        public string? SortBy { get; set; }  // "name", "date", "displayorder"
        public bool Descending { get; set; } = true;  // Sıralama yönü (azalan veya artan)

        // Yeni parametreler (SEO için veya başka ihtiyaçlar için)
        public string? MetaTitle { get; set; }  // SEO meta title'ına göre filtreleme
        public string? MetaDescription { get; set; }  // SEO meta description'a göre filtreleme

        // Constructor
        public FilterServiceCategoriesQuery(List<ServiceCategory> serviceCategories)
        {
            ServiceCategories = serviceCategories;
        }
    }

}
