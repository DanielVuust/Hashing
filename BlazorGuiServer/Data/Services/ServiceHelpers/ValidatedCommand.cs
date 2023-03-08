using FluentResults;

namespace BlazorGuiServer.Data.Services.ServiceHelpers
{
    /// <summary>
    ///     ValidatedCommand is the same as ICommand but adds the ability to validate before execution.
    /// </summary>
    public abstract class ValidatedCommand : ICommand
    {
        protected bool Validated = false;

        public abstract Result Validate();
        public abstract Result Execute();

        public Result ExecuteWithValidation()
        {
            var result = Validate();
            if (result.IsFailed)
            {
                return result;
            }
            return Execute();
        }

    }
}
