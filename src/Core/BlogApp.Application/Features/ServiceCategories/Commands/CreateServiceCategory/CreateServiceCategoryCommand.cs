using BlogApp.Application.Common;
using BlogApp.Application.DTOs.ServiceCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Commands.CreateServiceCategory
{
    public class CreateServiceCategoryCommand : IRequest<Result<ServiceCategoryDto>>
    {
        public CreateServiceCategoryDto Data { get; set; } = null!;
    }
}
