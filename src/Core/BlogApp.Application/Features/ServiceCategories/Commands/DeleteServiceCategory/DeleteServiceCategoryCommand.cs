using BlogApp.Application.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ServiceCategories.Commands.DeleteServiceCategory
{
    public class DeleteServiceCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
    }
}
