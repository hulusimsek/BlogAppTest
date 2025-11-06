using AutoMapper;
using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.Contact;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Queries.Filter
{
    public class FilterContactRequestsQueryHandler
                : IRequestHandler<FilterContactRequestsQuery, Result<List<ContactRequestDto>>>
    {
        private readonly IMapper _mapper;

        public FilterContactRequestsQueryHandler(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<Result<List<ContactRequestDto>>> Handle(FilterContactRequestsQuery request, CancellationToken cancellationToken)
        {
            var query = request.ContactRequests.AsQueryable();

            // 🔍 FullName ile filtreleme
            if (!string.IsNullOrWhiteSpace(request.FullName))
                query = query.Where(c => c.FullName.Contains(request.FullName, System.StringComparison.OrdinalIgnoreCase));

            // 📧 Email ile filtreleme
            if (!string.IsNullOrWhiteSpace(request.Email))
                query = query.Where(c => c.Email.Contains(request.Email, System.StringComparison.OrdinalIgnoreCase));

            // ✅ Okunmuş/Okunmamış talepler
            if (request.IsRead.HasValue)
                query = query.Where(c => c.IsRead == request.IsRead.Value);

            // ✅ Yanıtlanmış/Yanıtlanmamış talepler
            if (request.IsReplied.HasValue)
                query = query.Where(c => c.IsReplied == request.IsReplied.Value);

            // 📅 Tarih aralığı filtreleme
            if (request.RequestDateFrom.HasValue)
                query = query.Where(c => c.RequestDate >= request.RequestDateFrom.Value);

            if (request.RequestDateTo.HasValue)
                query = query.Where(c => c.RequestDate <= request.RequestDateTo.Value);

            // 📝 Subject ile filtreleme
            if (!string.IsNullOrWhiteSpace(request.Subject))
                query = query.Where(c => c.Subject.Contains(request.Subject, System.StringComparison.OrdinalIgnoreCase));

            // 🔽 Sıralama
            // Sıralama in-memory
            var list = query.ToList(); // EF Core sorgusu çalışıyor, veri belleğe geliyor

            list = (request.SortBy?.ToLower()) switch
            {
                "subject" => request.Descending ? list.OrderByDescending(c => c.Subject).ToList() : list.OrderBy(c => c.Subject).ToList(),
                "email" => request.Descending ? list.OrderByDescending(c => c.Email).ToList() : list.OrderBy(c => c.Email).ToList(),
                "requestdate" => request.Descending ? list.OrderByDescending(c => c.RequestDate).ToList() : list.OrderBy(c => c.RequestDate).ToList(),
                _ => request.Descending ? list.OrderByDescending(c => c.RequestDate).ToList() : list.OrderBy(c => c.RequestDate).ToList()
            };

            // DTO'ya dönüşüm
            var dtoList = _mapper.Map<List<ContactRequestDto>>(list);
            return Task.FromResult(Result<List<ContactRequestDto>>.Success(dtoList));
        }
    }


}
