using BlogApp.Application.Common;
using BlogApp.Application.DTOs.ServiceCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Queries.GetServiceCategoryById
{
    public class GetServiceCategoryByIdQuery : IRequest<Result<ServiceCategoryDetailDto>>
    {
        public Guid Id { get; set; }
    }
}
