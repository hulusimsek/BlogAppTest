using BlogApp.Application.Common;
using BlogApp.Application.DTOs.ServiceCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetAllServiceCategories
{
    public class GetAllServiceCategoriesQuery : IRequest<Result<List<ServiceCategoryDto>>>
    {
    }
}
