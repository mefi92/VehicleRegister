using BoundaryHelper;

namespace Core.VerificationObjects
{
    public class ValidatorResult
    {
        public List<ErrorData> Errors { get; } = new List<ErrorData>();

        public bool IsValid => Errors.Count == 0;
    }
}
