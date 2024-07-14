namespace OpenAIApiTest.Interfaces;

public interface IOpenAIService
{
    Task<CompletionResponseMessage> GenerateWorkoutPlan();

    Task<string> GenerateMealPlan();
}