
namespace Domain_Layer.Exceptions
{
    public sealed class BadRequestExceptions(List<string> errors): Exception("Validation Failed")
    {
        public List<string> Errors { get; } = errors;
    }
}
