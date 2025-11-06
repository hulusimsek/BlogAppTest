using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.Tag;
using BlogApp.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.Tags.Queries.GetTagBySlug
{
    public class GetTagBySlugQueryHandler : IRequestHandler<GetTagBySlugQuery, Result<TagDto>>
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public GetTagBySlugQueryHandler(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<TagDto>> Handle(GetTagBySlugQuery request, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetBySlugAsync(request.Slug, cancellationToken);
            if (tag == null)
                return Result<TagDto>.Failure("Tag bulunamadı.");

            var dto = _mapper.Map<TagDto>(tag);
            return Result<TagDto>.Success(dto);
        }
    }

}
