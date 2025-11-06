using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly ApplicationDbContext _context;

        public TagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // ID'ye göre etiket getirme
        public async Task<Tag?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        // Slug'a göre etiket getirme
        public async Task<Tag?> GetBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Tags
                .FirstOrDefaultAsync(x => x.Slug == slug, cancellationToken);
        }

        // Birden fazla etiket ID'sine göre etiketleri getirme
        public async Task<List<Tag>> GetByIdsAsync(List<Guid> tagIds, CancellationToken cancellationToken = default)
        {
            return await _context.Tags
                .Where(x => tagIds.Contains(x.Id))
                .ToListAsync(cancellationToken);
        }

        // Tüm etiketleri getirme
        public async Task<List<Tag>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Tags.ToListAsync(cancellationToken);
        }

        // Etiketin slug'ı olup olmadığını kontrol etme
        public async Task<bool> ExistsBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _context.Tags.AnyAsync(x => x.Slug == slug, cancellationToken);
        }

        // Yeni etiket oluşturma
        public async Task<Tag> CreateAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            await _context.Tags.AddAsync(tag, cancellationToken);
            return tag;
        }

        // Etiketi güncelleme
        public Task UpdateAsync(Tag tag, CancellationToken cancellationToken = default)
        {
            _context.Tags.Update(tag);
            return Task.CompletedTask;
        }

        // Etiketi silme
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var tag = await _context.Tags.FindAsync(new object[] { id }, cancellationToken);
            if (tag != null)
            {
                _context.Tags.Remove(tag);
            }
        }
    }

}
