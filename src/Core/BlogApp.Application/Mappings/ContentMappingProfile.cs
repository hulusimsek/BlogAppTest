using AutoMapper;
using BlogApp.Application.DTOs.BlogComment;
using BlogApp.Application.DTOs.BlogPost;
using BlogApp.Application.DTOs.Contact;
using BlogApp.Application.DTOs.PageSection;
using BlogApp.Application.DTOs.LawyerProfile;
using BlogApp.Application.DTOs.ServiceCategory;
using BlogApp.Application.DTOs.SiteSettings;
using BlogApp.Application.DTOs.Tag;
using BlogApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Application.Mappings
{
    public class ContentMappingProfile : Profile
    {
        public ContentMappingProfile()
        {
            // SiteSettings
            CreateMap<SiteSettings, SiteSettingsDto>();
            CreateMap<FaviconSet, FaviconSetDto>();

            CreateMap<UpdateSiteSettingsDto, SiteSettings>();
            CreateMap<FaviconSetDto, FaviconSet>();


            // HomePageSection Mappings
            CreateMap<PageSection, PageSectionDto>();
            CreateMap<UpdatePageSectionDto, PageSection>();

            CreateMap<LawyerProfile, LawyerProfileDto>()
                .ForMember(dest => dest.Specializations,
                    opt => opt.MapFrom(src => src.Specializations
                        .OrderBy(s => s.DisplayOrder)
                        .Select(s => new LawyerSpecializationDto
                        {
                            Name = s.ServiceCategory.Name,
                            Slug = s.ServiceCategory.Slug,
                            IconName = s.ServiceCategory.IconName,
                            DisplayOrder = s.DisplayOrder
                        })
                        .ToList()))
                .ForMember(dest => dest.CareerHistory,
                    opt => opt.MapFrom(src => src.CareerHistory
                        .OrderByDescending(c => c.DisplayOrder)));

            CreateMap<CareerHistory, CareerHistoryDto>();
            CreateMap<CreateLawyerProfileDto, LawyerProfile>();
            CreateMap<UpdateLawyerProfileDto, LawyerProfile>();

            // ServiceCategory
            CreateMap<ServiceCategory, ServiceCategoryDto>();

            CreateMap<CreateServiceCategoryDto, ServiceCategory>()
                // Slug ve Id gibi özel ayarlamaları handler'da yapacağımız için basit map
                .ForMember(dest => dest.BlogPosts, opt => opt.Ignore())
                .ForMember(dest => dest.ServiceDetail, opt => opt.Ignore()); // detail'ı elle eşle

            CreateMap<UpdateServiceCategoryDto, ServiceCategory>();

            // ServiceDetail
            CreateMap<ServiceDetail, ServiceDetailDto>()
                .ForMember(dest => dest.ProcessFlow,
                    opt => opt.MapFrom(src => src.ProcessFlowSteps))
                .ForMember(dest => dest.Faqs,
                    opt => opt.MapFrom(src => src.Faqs))
                .ForMember(dest => dest.Testimonials,
                    opt => opt.MapFrom(src => src.Testimonials));

            CreateMap<CreateServiceDetailDto, ServiceDetail>()
                .ForMember(d => d.ProcessFlowSteps, opt => opt.Ignore())
                .ForMember(d => d.Faqs, opt => opt.Ignore())
                .ForMember(d => d.Testimonials, opt => opt.Ignore());

            CreateMap<UpdateServiceDetailDto, ServiceDetail>()
                .ForMember(dest => dest.ProcessFlowSteps,
                    opt => opt.MapFrom(src => src.ProcessFlow))
                .ForMember(dest => dest.Faqs,
                    opt => opt.MapFrom(src => src.Faqs))
                .ForMember(dest => dest.Testimonials,
                    opt => opt.MapFrom(src => src.Testimonials));

            // ServiceCategoryDetailDto (Category + Detail)
            CreateMap<ServiceCategory, ServiceCategoryDetailDto>()
                .ForMember(dest => dest.ServiceDetail,
                    opt => opt.MapFrom(src => src.ServiceDetail));

            // İç içe listeler (FaqItem, ProcessFlowStep, TestimonialItem)
            // Entity -> DTO (okuma)
            CreateMap<ServiceFaqItem, ServiceFaqItemDto>();
            CreateMap<ProcessFlowStep, ProcessFlowStepDto>();
            CreateMap<TestimonialItem, TestimonialItemDto>();

            // DTO -> Entity (create/update)
            CreateMap<ServiceFaqItemDto, ServiceFaqItem>();
            CreateMap<ProcessFlowStepDto, ProcessFlowStep>();
            CreateMap<TestimonialItemDto, TestimonialItem>();

            // BlogPost
            CreateMap<BlogPost, BlogPostDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Name : null))
                .ForMember(dest => dest.CategorySlug,
                    opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Slug : null))
                // Tags mapping: BlogPostTag.Tag -> TagDto
                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(pt => pt.Tag)));

            CreateMap<BlogPost, BlogPostDetailDto>()
                .ForMember(dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Name : null))
                .ForMember(dest => dest.CategorySlug,
                    opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Slug : null))


                .ForMember(dest => dest.Tags,
                    opt => opt.MapFrom(src => src.Tags.Select(pt => pt.Tag))); // Tag entity'sini TagDto’ya maple

            CreateMap<Tag, TagDto>();

            CreateMap<ServiceCategory, MinimalCategoryDto>();


            CreateMap<CreateBlogPostDto, BlogPost>()
                            .ForMember(dest => dest.ServiceCategoryId, opt => opt.MapFrom(src => src.ServiceCategoryId))
                            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
                            .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Summary))
                            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
                            .ForMember(dest => dest.FeaturedImageUrl, opt => opt.MapFrom(src => src.FeaturedImageUrl))
                            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AuthorName))
                            .ForMember(dest => dest.AuthorImageUrl, opt => opt.MapFrom(src => src.AuthorImageUrl))
                            .ForMember(dest => dest.PublishDate, opt => opt.MapFrom(src => src.PublishDate))
                            .ForMember(dest => dest.IsPublished, opt => opt.MapFrom(src => src.IsPublished))
                            .ForMember(dest => dest.MetaTitle, opt => opt.MapFrom(src => src.MetaTitle))
                            .ForMember(dest => dest.MetaDescription, opt => opt.MapFrom(src => src.MetaDescription))
                            .ForMember(dest => dest.MetaKeywords, opt => opt.MapFrom(src => src.MetaKeywords))
                            .ForMember(dest => dest.Tags, opt => opt.Ignore()) // Etiketler için özel bir işlem yapacağız
                            .ForMember(dest => dest.Faqs, opt => opt.MapFrom(src => src.BlogFaqItems))
                            .ForMember(dest => dest.ViewCount, opt => opt.MapFrom(src => 0)) // Başlangıçta 0
                            .ForMember(dest => dest.DisplayOrder, opt => opt.MapFrom(src => 0)); // Başlangıçta sıralama 0


            CreateMap<UpdateBlogPostDto, BlogPost>()
                .ForMember(dest => dest.Faqs, opt => opt.MapFrom(src => src.BlogFaqItems));

            CreateMap<BlogPostFaqItemDto, BlogFaqItem>().ReverseMap();

            // Entity -> DTO
            CreateMap<BlogComment, BlogCommentDto>();
            CreateMap<BlogComment, BlogCommentSummaryDto>();

            // DTO -> Entity
            CreateMap<CreateBlogCommentDto, BlogComment>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.CommentDate, opt => opt.MapFrom(src => DateTime.UtcNow));

            CreateMap<UpdateBlogCommentDto, BlogComment>();

            // Entity -> DTO
            CreateMap<Tag, TagDto>();

            // DTO -> Entity
            CreateMap<CreateTagDto, Tag>();
            CreateMap<UpdateTagDto, Tag>();

            // Contact
            CreateMap<ContactInfo, ContactInfoDto>();
            CreateMap<UpdateContactInfoDto, ContactInfo>()
                 .ForMember(dest => dest.Faqs, opt => opt.MapFrom(src => src.Faqs))
;

            CreateMap<ContactInfoFaqItemDto, ContactInfoFaqItem>().ReverseMap();


            CreateMap<ContactFormSettings, ContactFormSettingsDto>().ReverseMap();

            CreateMap<WorkingHour, WorkingHourDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>()
                .ForMember(dest => dest.WorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
                .ReverseMap();

            CreateMap<ContactRequest, ContactRequestDto>();
            CreateMap<CreateContactRequestDto, ContactRequest>();

            CreateMap<AppointmentRequestDto, AppointmentRequest>();

        }
    }

}
