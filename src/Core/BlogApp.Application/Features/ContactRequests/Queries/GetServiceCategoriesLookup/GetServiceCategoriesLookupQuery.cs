using BlogApp.Application.DTOs.ServiceCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Queries.GetServiceCategoriesLookup
{
    public class GetServiceCategoriesLookupQuery : IRequest<List<ServiceCategoryLookupDto>>
    {
        // Şu anlık parametre yok, ama istersen filtre (örneğin sadece aktif/aktif olmayan) ekleyebilirsin
    }
}
