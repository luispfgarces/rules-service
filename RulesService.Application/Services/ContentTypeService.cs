using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RulesService.Application.Dto.ContentTypes;
using RulesService.Application.Exceptions;
using RulesService.Domain.Models;
using RulesService.Domain.Models.Factories;
using RulesService.Domain.Repositories;

namespace RulesService.Application.Services
{
    internal class ContentTypeService : IContentTypeService
    {
        private readonly IContentTypeFactory contentTypeFactory;

        private readonly IContentTypeRepository contentTypeRepository;

        public ContentTypeService(
            IContentTypeRepository contentTypeRepository,
            IContentTypeFactory contentTypeFactory)
        {
            this.contentTypeRepository = contentTypeRepository;
            this.contentTypeFactory = contentTypeFactory;
        }

        public async Task<ContentTypeDto> Add(ContentTypeDto contentTypeDto)
        {
            if (contentTypeDto == null)
            {
                throw new ArgumentNullException(nameof(contentTypeDto));
            }

            ContentType contentType = this.contentTypeFactory.CreateContentType(
                contentTypeDto.TenantId,
                contentTypeDto.Code,
                contentTypeDto.Name);

            await this.contentTypeRepository.Add(contentType);

            return this.ConvertToDto(contentType);
        }

        public async Task<IEnumerable<ContentTypeDto>> GetAll(Guid tenantId)
        {
            IEnumerable<ContentType> contentTypes = await this.contentTypeRepository.GetAll();

            return contentTypes.Where(ct => ct.Key.TenantId == tenantId).Select(ct => this.ConvertToDto(ct));
        }

        public async Task<ContentTypeDto> GetBy(Guid tenantId, int code)
        {
            ContentTypeKey key = ContentTypeKey.New(tenantId, code);
            return await this.contentTypeRepository.GetById(key)
                .ContinueWith(contentTypeTask =>
                {
                    ContentType contentType = contentTypeTask.GetAwaiter().GetResult();

                    if (contentType != null)
                    {
                        return this.ConvertToDto(contentType);
                    }

                    return null;
                });
        }

        public async Task<ContentTypeDto> Update(ContentTypeDto contentTypeDto)
        {
            ContentTypeKey key = ContentTypeKey.New(contentTypeDto.TenantId, contentTypeDto.Code);
            ContentType conditionType = await this.contentTypeRepository.GetById(key);

            if (conditionType == null)
            {
                throw new NotFoundException(FormattableString.Invariant($"{nameof(ContentType)} was not found. Key = {key}"));
            }

            conditionType.Name = contentTypeDto.Name;

            return await this.contentTypeRepository.Update(conditionType)
                .ContinueWith(tenantTask =>
                {
                    tenantTask.GetAwaiter().GetResult();
                    return this.ConvertToDto(conditionType);
                });
        }

        private ContentTypeDto ConvertToDto(ContentType ct) => new ContentTypeDto
        {
            Code = ct.Key.Code,
            Name = ct.Name,
            TenantId = ct.Key.TenantId
        };
    }
}