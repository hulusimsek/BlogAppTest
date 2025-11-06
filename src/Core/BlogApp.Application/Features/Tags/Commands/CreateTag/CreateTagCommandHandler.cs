using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.Common.Utilities;
using BlogApp.Application.DTOs.Tag;
using BlogApp.Application.Interfaces.Persistence;
using BlogApp.Domain.Entities;
using BlogApp.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Commands.CreateTag
{
    public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Result<TagDto>>
    {
        private readonly ITagRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateTagCommandHandler(ITagRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            var baseSlug = SlugGenerator.GenerateSlug(request.Data.Name);
            var uniqueSlug = await EnsureUniqueSlugAsync(baseSlug, cancellationToken);

            var tag = _mapper.Map<Tag>(request.Data);
            tag.Slug = uniqueSlug;

            await _repository.CreateAsync(tag, cancellationToken);
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
