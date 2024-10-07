namespace VerifyService.Models.Dtos
{
    public class CourseFormRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime EnrollmentStart { get; set; }
        public DateTime EnrollmentEnd { get; set; }
        public DateTime TeachingStart { get; set; }
        public DateTime TeachingEnd { get; set; }
    }
}