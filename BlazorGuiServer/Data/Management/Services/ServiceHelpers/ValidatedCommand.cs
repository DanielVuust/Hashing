using FluentResults;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
{
    /// <summary>
    ///     ValidatedCommand is the same as ICommand but adds the ablity to validate before execution.
    /// </summary>
    public abstract class ValidatedCommand : ICommand
    {
        protected bool Validated = false;

        public abstract Result Validate();
        public abstract Result Execute();

        public Result ExecuteWithValidation()
        {
            var result = this.Validate();
            if (result.IsFailed)
            {
                return result;
            }
            return this.Execute();
        }

    }
}
