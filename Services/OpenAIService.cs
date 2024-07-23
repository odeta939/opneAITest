using System.Net.Http.Headers;
using OpenAIApiTest.Configurations;
using OpenAIApiTest.Interfaces;

namespace OpenAIApiTest.Services;

public class OpenAIService : IOpenAIService
{

    private readonly Utils.HttpClientWrapper _client;
    
    public OpenAIService(HttpClient client, OpenAIConfiguration configuration)
    {   
        _client = new Utils.HttpClientWrapper(client);
        
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", configuration.ApiToken);
        client.BaseAddress = new Uri(configuration.BaseAddress);
    }

    public async Task<CompletionResponseMessage> GenerateWorkoutPlan()
    {
        const string api = "v1/chat/completions";
        var requestBody = new CompletionRequestMessage();
        requestBody.AddSystemMessage("You are a professional trainer. Data should be returned in json format. " +
                                     "The Json should strictly follow this format: " +
                                     "{\n  \"name\": \"Push-up\",\n  \"howToPerform\": \"Place your hands on the ground, " +
                                     "shoulder-width apart, and lower your body until your chest nearly touches the floor. " +
                                     "Push back up to the starting position.\",\n  \"muscleGroup\": \"Chest, Triceps, " +
                                     "Shoulders\",\n  \"repetitions\": \"15\",\n  \"weightType\": \"Bodyweight\",\n  \"kilograms\": \"0\"\n} " );
        requestBody.AddUserMessage("Give me list of 3 exercises for lower body for experienced level. Give me a description for each" +
                                   "as well as repetitions, type of weights and kilograms. If it's a body weight exercise say that kilograms is 0 ");

        try
        {
            var response = await _client.PostDeserializedAsync<CompletionResponseMessage>(api, requestBody);
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> GenerateMealPlan()
    {
        throw new NotImplementedException();
    }
    
}