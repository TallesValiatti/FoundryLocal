using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

IKernelBuilder kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.AddOpenAIChatCompletion(
    "Phi-3-mini-4k-instruct-generic-cpu",
    new Uri("http://localhost:5273/v1"), 
    "");

Kernel kernel = kernelBuilder.Build();

var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();

ChatHistory history = [];
history.AddUserMessage("What's the capital of Brazil?");


var response = await chatCompletionService.GetChatMessageContentAsync(
    history,
    kernel: kernel
);

Console.WriteLine(response);
