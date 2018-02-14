using RulesService.Domain.Models;
using RulesService.Domain.Repositories;

namespace RulesService.Domain.Services.Rules.Validation.Invariants
{
    internal class ContentTypeCreateRuleInvariant : ICreateRuleInvariant
    {
        private const string CodeConst = "R004";

        private readonly IContentTypeRepository contentTypeRepository;

        public ContentTypeCreateRuleInvariant(IContentTypeRepository contentTypeRepository)
        {
            this.contentTypeRepository = contentTypeRepository;
        }

        public string Code => ContentTypeCreateRuleInvariant.CodeConst;

        public InvariantResult IsValid(CreateRule obj)
        {
            ContentTypeKey contentTypeKey = ContentTypeKey.New(obj.TenantId, obj.ContentTypeCode);
            ContentType contentType = this.contentTypeRepository.GetById(contentTypeKey).GetAwaiter().GetResult();

            // Validate content type.
            if (contentType == null)
            {
                return InvariantResult.ForInvalid(this.Code, string.Format(InvariantResources.R004, obj.TenantId, obj.ContentTypeCode));
            }

            return InvariantResult.ForValid(this.Code);
        }
    }
}