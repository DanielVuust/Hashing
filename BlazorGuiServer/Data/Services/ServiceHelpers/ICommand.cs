using FluentResults;

namespace BlazorGuiServer.Data.Services.ServiceHelpers
{
    public interface ICommand
    {
        public Result Execute();
    }
}
