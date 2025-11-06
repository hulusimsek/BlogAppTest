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

namespace BlogApp.Application.Features.Tags.Queries.GetTagById
{
    public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, Result<TagDto>>
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public GetTagByIdQueryHandler(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<TagDto>> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
        {
            var tag = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (tag == null)
                return Result<TagDto>.Failure("Tag bulunamadı.");

            var dto = _mapper.Map<TagDto>(tag);
            return Result<TagDto>.Success(dto);
        }
    }

}
