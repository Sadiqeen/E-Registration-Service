namespace CourseService.Models.Dtos
{
    public class CourseResponseDto : CourseFormRequestDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CreatedByUserId { get; set; }
    }
}