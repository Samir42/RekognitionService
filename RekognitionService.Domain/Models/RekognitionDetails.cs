using Amazon.DynamoDBv2.DataModel;

namespace RekognitionService.Domain.Models
{
    [DynamoDBTable("RekognitionDetails")]
    public class RekognitionDetails
    {
        [DynamoDBHashKey]
        public string ID { get; set; }

        public string Category { get; set; }

        [DynamoDBGlobalSecondaryIndexHashKey]
        public string Filename { get; set; }

        public double Confidence { get; set; }
    }
}