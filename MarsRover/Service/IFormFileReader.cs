namespace MarsRover.Service
{
    public interface IFormFileReader
    {
        IEnumerable<string> ReadFormFile(IFormFile formFile);
    }
}
