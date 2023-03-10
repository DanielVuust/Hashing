using FluentResults;

namespace RSAApi
{
    public interface IDto
    {
        public Result<bool> Validate();
    }
}
