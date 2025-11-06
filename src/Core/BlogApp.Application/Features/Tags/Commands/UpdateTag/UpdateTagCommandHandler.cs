using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.Common.Utilities;
using BlogApp.Application.DTOs.Tag;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.UpdateTag
{
    public class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Result<TagDto>>
    {
        private readonly ITagRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TagDto>> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetByIdAsync(request.Data.Id, cancellationToken);
            if (tag == null)
                return Result<TagDto>.Failure("Tag bulunamadı.");

            var currentSlug = tag.Slug;

            _mapper.Map(request.Data, tag);

            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Name);
            if (baseSlug != currentSlug)
            {
                // Benzersizlik kontrolü yapılacak
                var uniqueSlug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);
                tag.Slug = uniqueSlug;
            }

            
            await _repository.UpdateAsync(tag, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var dto = _mapper.Map<TagDto>(tag);
            return Result<TagDto>.Success(dto);
        }

        private async Task<string> EnsureUniqueSlugAsync(string slug, CancellationToken cancellationToken)
        {
            var newSlug = slug;
            int counter = 2;

            while (await _repository.ExistsBySlugAsync(newSlug, cancellationToken))
            {
                newSlug = $"{slug}-{counter++}";
            }

            return newSlug;
        }
    }

}
