namespace AuthenticationService.Models.Dtos
{
    public class PageFilterDto
    {
        public string? Search { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
    }
}