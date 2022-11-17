namespace MarsRover.Service
{
    public class InstructionsFormFileReader : IFormFileReader
    {
        public IEnumerable<string> ReadFormFile(IFormFile formFile)
        {
            if(formFile == null)
            {
                throw new ArgumentNullException(nameof(formFile));
            }
            var lines = new List<string>();
            using(var streamReader = new StreamReader(formFile.OpenReadStream()))
            {
                while (!streamReader.EndOfStream)
                {
                    lines.Add(streamReader.ReadLine() ?? String.Empty);
                }                
            }

            return lines;
            
        }
    }
}
