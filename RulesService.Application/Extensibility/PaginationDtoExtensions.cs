using RulesService.Domain.Repositories;

namespace RulesService.Application.Dto.Common
{
    internal static class PaginationDtoExtensions
    {
        public static Pagination ToPagination(this PaginationDto paginationDto)
        {
            return new Pagination
            {
                PageIndex = paginationDto.PageIndex,
                PageSize = paginationDto.PageSize
            };
        }
    }
}