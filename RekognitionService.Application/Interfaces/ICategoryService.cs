namespace RekognitionService.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<string>> GetAll();
    }
}