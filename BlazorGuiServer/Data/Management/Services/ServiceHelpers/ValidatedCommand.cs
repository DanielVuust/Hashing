using FluentResults;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
{
    public abstract class ValidatedCommand : ICommand
    {
        protected bool Validated = false;

        public abstract Result Validate();
        public abstract Result Execute();

        public Result ExecuteWithValidation()
        {
            this.Validate();
            return this.Execute();
        }

    }
}
