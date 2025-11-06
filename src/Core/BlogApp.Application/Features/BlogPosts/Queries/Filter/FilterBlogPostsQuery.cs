using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.Filter
{
    public class FilterBlogPostsQuery : IRequest<Result<List<BlogPostDto>>>
    {
        // Filtreleme parametreleri
        public string? SearchTitle { get; set; }
        public Guid? CategoryId { get; set; }
        public string? CategorySlug { get; set; }
        public string? Tag { get; set; }
        public bool? IsPublished { get; set; } // null → filtreleme 
        public string? SortBy { get; set; } // "viewcount", "date", "title"
        public bool Descending { get; set; } = true;

        // Yeni parametre: yorumları filtrele
        public bool? IsApprovedComments { get; set; }
        // 📄 Sayfalama
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

}
