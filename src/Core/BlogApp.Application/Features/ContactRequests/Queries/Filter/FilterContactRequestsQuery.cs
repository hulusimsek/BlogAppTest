using BlogApp.Application.Common;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Features.ContactRequests.Queries.Filter
{
    public class FilterContactRequestsQuery : IRequest<Result<List<ContactRequestDto>>>
    {
        // Zaten önceden çekilmiş veriler
        public List<ContactRequestDto> ContactRequests { get; set; } = new();

        // Filtreleme parametreleri
        public string? FullName { get; set; } // İsim araması
        public string? Email { get; set; } // E-posta araması
        public bool? IsRead { get; set; } // Okunmuş/Okunmamış talepler
        public bool? IsReplied { get; set; } // Yanıtlanmış/Yanıtlanmamış talepler
        public DateTime? RequestDateFrom { get; set; } // Tarih aralığı (başlangıç)
        public DateTime? RequestDateTo { get; set; } // Tarih aralığı (bitiş)
        public string? SortBy { get; set; }
        public string? Subject { get; set; } // Konu araması
        public bool Descending { get; set; } = true; // Sıralama yönü

        public FilterContactRequestsQuery(List<ContactRequestDto> contactRequests)
        {
            ContactRequests = contactRequests;
        }
    }

}
