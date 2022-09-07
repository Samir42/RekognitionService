using RekognitionService.Application.Interfaces;
using RekognitionService.Core.Interfaces;

namespace RekognitionService.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            return await _categoryRepository.GetAll();
        }
    }
}