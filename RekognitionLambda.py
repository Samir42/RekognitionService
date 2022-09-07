import boto3
import json
import uuid

print('Loading function')
def lambda_handler(event, context):
    bucketName = event['Records'][0]['s3']['bucket']['name']
    fileName = event['Records'][0]['s3']['object']['key']
    
    return detect_labels_and_put_dynamoDB(fileName, bucketName)

def detect_labels_and_put_dynamoDB(photo, bucket):
    rekognitionClient=boto3.client('rekognition', 'us-east-2')
    dynamoClient = boto3.client('dynamodb')

    response = rekognitionClient.detect_labels(Image={'S3Object':{'Bucket':bucket,'Name':photo}},
        MaxLabels=10)

    print('Detected labels for ' + photo) 
    for label in response['Labels']:
        dynamoClient.put_item(
            TableName='RekognitionDetails',
            Item= {
                'ID' : {
                    'S': str(uuid.uuid4())
                },
                'Filename': {
                    'S': photo
                },
                'Category': {
                    'S' : label['Name']
                },
                'Confidence': {
                   'N': str(label['Confidence'])
                }
            })

def main():
    photo=''
    bucket=''
    label_count=detect_labels(photo, bucket)
    print("Labels detected: " + str(label_count))
