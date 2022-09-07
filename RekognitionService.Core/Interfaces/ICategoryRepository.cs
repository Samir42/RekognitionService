namespace RekognitionService.Core.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<string>> GetFileNamesByCategoryAsync(string category);

        Task<IEnumerable<string>> GetAll();
    }
}