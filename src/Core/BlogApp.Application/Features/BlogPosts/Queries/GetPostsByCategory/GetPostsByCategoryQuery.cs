using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.BlogPosts.Queries.GetPostsByCategory
{
    public class GetPostsByCategoryQuery : IRequest<Result<List<BlogPostDto>>>
    {
        public string categorySlug { get; set; } = null!;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public bool includeUnPublishedPost { get; set; } = false;
        public Guid? ExcludePostId { get; set; } // İlgili yazıları getirirken mevcut yazıyı hariç tutmak için

    }
}
