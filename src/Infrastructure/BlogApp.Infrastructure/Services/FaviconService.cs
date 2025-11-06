using BlogApp.Application.Interfaces.Infrastructure;
using BlogApp.Application.Interfaces.Web;
using BlogApp.Domain.Entities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Infrastructure.Services
{
    public class FaviconService : IFaviconService
    {
        private readonly IFileStorageService _fileStorage;

        public FaviconService(IFileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        public async Task<FaviconSet> GenerateFaviconsAsync(Stream fileStream, string fileName)
        {
            var sizes = new[] { 16, 32, 180, 192, 512 };
            var favicons = new FaviconSet();

            using var image = await Image.LoadAsync(fileStream);

            foreach (var size in sizes)
            {
                using var resized = image.Clone(ctx => ctx.Resize(size, size));
                var faviconFileName = $"favicon-{size}x{size}.png";
                using var ms = new MemoryStream();
                await resized.SaveAsPngAsync(ms);
                ms.Position = 0;

                var path = await _fileStorage.SaveFileAsync(ms, faviconFileName, "uploads/favicons");

                switch (size)
                {
                    case 16: favicons.Favicon16 = path; break;
                    case 32: favicons.Favicon32 = path; break;
                    case 180: favicons.Favicon180 = path; break;
                    case 192: favicons.Favicon192 = path; break;
                    case 512: favicons.Favicon512 = path; break;
                }
            }

            return favicons;
        }
    }

}
