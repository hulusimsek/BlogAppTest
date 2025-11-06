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

namespace BlogApp.Application.Features.Tags.Queries.GetAllTagsQuery
{
    public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, Result<List<TagDto>>>
    {
        private readonly ITagRepository _repository;
        private readonly IMapper _mapper;

        public GetAllTagsQueryHandler(ITagRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<List<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await _repository.GetAllAsync(cancellationToken);
            var dtoList = _mapper.Map<List<TagDto>>(tags);
            return Result<List<TagDto>>.Success(dtoList);
        }
    }

}
