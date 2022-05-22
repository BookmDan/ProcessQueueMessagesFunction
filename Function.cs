using Amazon.Lambda.Core;
using Amazon.Lambda.SQSEvents;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;




// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace ProcessQueueMessagesFunction;

public class Function
{
    /// <summary>
    /// Default constructor. This constructor is used by Lambda to construct the instance. When invoked in a Lambda environment
    /// the AWS credentials will come from the IAM role associated with the function and the AWS region will be set to the
    /// region the Lambda function is executed in.
    /// </summary>
    public Function()
    {

    }


    /// <summary>
    /// This method is called for every Lambda invocation. This method takes in an SQS event object and can be used 
    /// to respond to SQS messages.
    /// </summary>
    /// <param name="evnt"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task FunctionHandler(SQSEvent evnt, ILambdaContext context)
    {
        foreach (var message in evnt.Records)
        {
            await ProcessMessageAsync(message, context);

        }

    }

    private async Task ProcessMessageAsync(SQSEvent.SQSMessage message, ILambdaContext context)
    {
        //Console.WriteLine("Hello");
        var client = new AmazonSQSClient();
        Console.WriteLine($"Parsing Message {message.Body}");
        processJSON(message.Body);
        await Task.CompletedTask;
    }

    private static void processJSON(String jsonFileContent)
    {
        PatientUQMessage message = JsonSerializer.Deserialize<PatientUQMessage>(jsonFileContent);
        if (message.hasInsurance)
        {
            Console.WriteLine("Patient with ID {0}: policyNumber = {1}, provider = {2}", message.patientID, message.policyNumber, message.provider);

        } else
        {
            Console.WriteLine("Patient with ID {0} does not have medical insurance", message.patientID);
        }
    }
}