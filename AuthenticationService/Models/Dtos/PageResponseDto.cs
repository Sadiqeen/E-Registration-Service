using AutoMapper;

namespace AuthenticationService.Models.Dtos
{
    public class PageResponseDto<TQuery, TModel> : PageFilterDto
    {
        public int Total { get; set; }
        public List<TModel> Data { get; set; }

        public PageResponseDto()
        {

        }

        public PageResponseDto(IMapper mapper, IQueryable<TQuery> query, int? page, int? pageSize)
        {
            Page = page;
            PageSize = pageSize;
            Total = query.Count();

            if (page > 0 && pageSize > 0)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            Data = mapper.Map<List<TModel>>(query);
        }
    }
}