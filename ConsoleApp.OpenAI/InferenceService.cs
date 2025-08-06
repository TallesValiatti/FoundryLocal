using System.ClientModel;
using OpenAI;
using OpenAI.Chat;

namespace ConsoleApp.OpenAI;

public class InferenceService
{
    private OpenAIClient GetClient(string apikey, Uri endpoint)
    {
        ApiKeyCredential key = new ApiKeyCredential(apikey);

        OpenAIClient client = new OpenAIClient(key, new OpenAIClientOptions
        {
            Endpoint = endpoint
        });

        return client;
    }
    
    public virtual async Task<string> CompleteAsync(
        string apikey,
        Uri endpoint,
        string model,
        string instructions,
        string content)
    {
        var openAiClient = GetClient(apikey, endpoint);
      
        var client = openAiClient.GetChatClient(model);
        
        var chat = new List<ChatMessage>()
        {
            new SystemChatMessage(instructions),
            new UserChatMessage(content)
        };
        
        var result = await client.CompleteChatAsync(chat);

        return result.Value.Content[0].Text;
    }
}