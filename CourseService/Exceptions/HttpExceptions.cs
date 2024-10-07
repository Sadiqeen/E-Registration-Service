using System.Net;

namespace CourseService.Exceptions
{
    public abstract class HttpException : Exception
    {
        public abstract int StatusCode { get; }
        protected HttpException(string message) : base(message) { }
    }

    public class NotFoundException : HttpException
    {
        public override int StatusCode { get { return (int)HttpStatusCode.NotFound; } }
        public NotFoundException(string message) : base(message) { }
    }

    public class BadRequestException : HttpException
    {
        public override int StatusCode { get { return (int)HttpStatusCode.BadRequest; } }
        public BadRequestException(string message) : base(message) { }
    }

    public class UnauthorizedException : HttpException
    {
        public override int StatusCode { get { return (int)HttpStatusCode.Unauthorized; } }
        public UnauthorizedException(string message) : base(message) { }
    }

    public class InternalServerException : HttpException
    {
        public override int StatusCode { get { return (int)HttpStatusCode.InternalServerError; } }
        public InternalServerException(string message) : base(message) { }
    }
}