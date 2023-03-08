using FluentResults;

namespace BlazorGuiServer.Data.Management.Services.ServiceHelpers
{
    public interface ICommand
    {
        public Result Execute();
    }
}
