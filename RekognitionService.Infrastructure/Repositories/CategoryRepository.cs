using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using RekognitionService.Core.Interfaces;
using RekognitionService.Domain.Models;

namespace RekognitionService.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DynamoDBContext _context;

        public CategoryRepository(IAmazonDynamoDB dynamoDbClient)
        {
            _context = new DynamoDBContext(dynamoDbClient);
        }

        public async Task<IEnumerable<string>> GetFileNamesByCategoryAsync(string category)
        {
            var details = await _context.ScanAsync<RekognitionDetails>(new List<ScanCondition>
                {
                    new ScanCondition("Category", ScanOperator.Equal, category)
                }).GetRemainingAsync();

            return details.Select(x => x.Filename).ToList();
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            var details = await _context.ScanAsync<RekognitionDetails>(new List<ScanCondition> {})
                .GetRemainingAsync();

            return details.Select(s=> s.Category).Distinct().ToList();
        }
    }
}
