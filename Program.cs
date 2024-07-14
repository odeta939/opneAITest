// See https://aka.ms/new-console-template for more information
using System;
using Microsoft.Extensions.Configuration;
using OpenAIApiTest;
using OpenAIApiTest.Configurations;
using OpenAIApiTest.Services;


public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddUserSecrets<Program>()
            .Build();

        var client = new HttpClient();
        var openAIConfiguration = new OpenAIConfiguration(configuration);
        var openAIService = new OpenAIService(client, openAIConfiguration);

        var response = await openAIService.GenerateWorkoutPlan();
        var contentRaw = response.choices[0].message.content;
        var content = System.Text.Json.JsonSerializer.Deserialize<Content>(contentRaw);
        foreach (var exercise in content.exercises)
        {
            Console.WriteLine($"Name: {exercise.name}");
            Console.WriteLine($"Description: {exercise.howToPerform}");
            Console.WriteLine($"Muscle group: {exercise.muscleGroup}");
            Console.WriteLine($"Repetitions: {exercise.repetitions}");
            Console.WriteLine($"Type of weights: {exercise.weightType}");
            Console.WriteLine($"Kilograms: {exercise.kilograms}");
            Console.WriteLine();
        }
    }
}