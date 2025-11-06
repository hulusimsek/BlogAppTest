using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Interfaces.Infrastructure
{
    public interface IFaviconService
    {
        Task<FaviconSet> GenerateFaviconsAsync(Stream fileStream, string fileName);
    }
}
