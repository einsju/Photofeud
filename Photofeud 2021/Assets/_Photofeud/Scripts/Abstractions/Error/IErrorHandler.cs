namespace Photofeud.Abstractions.Error
{
    public interface IErrorHandler
    {
        void HandleError(string message);
    }
}
