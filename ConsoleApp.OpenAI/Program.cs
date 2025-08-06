using ConsoleApp.OpenAI;
using Microsoft.AI.Foundry.Local;

var email =
    """
    Subject: Bugs Found in User Registration Feature - Task DEV-234
    
    Hi Mark,
    
    I've completed testing on the user registration feature you developed (Task DEV-234) and found several issues that need to be addressed:
    
    BUGS IDENTIFIED:
    1. Email validation not working - accepts invalid email formats
    2. Password strength indicator shows incorrect colors
    3. Success message displays even when registration fails
    4. Form doesn't clear after successful submission
    
    SEVERITY:
    - High: Email validation issue (blocks user registration)
    - Medium: Password indicator and success message bugs
    - Low: Form clearing issue
    
    Please fix these issues and let me know when ready for re-testing. The feature is currently blocked from release.
    
    Thanks,
    Amanda Chen
    QA Engineer
    """;

var alias = "phi-3-mini-4k";

Console.WriteLine($"Starting local foundry with model alias: {alias}");
var manager = await FoundryLocalManager.StartModelAsync(aliasOrModelId: alias);

var model = await manager.GetModelInfoAsync(aliasOrModelId: alias);
Console.WriteLine($"Model: {model!.ModelId}");
Console.WriteLine($"Endpoint: {manager!.Endpoint}");
Console.WriteLine($"Starting inference ...");

var service = new InferenceService();

try
{
    var result = await service.CompleteAsync(
        manager.ApiKey,
        manager.Endpoint,
        model!.ModelId,
        Instructions.EmailSummarizer,
        email);

    Console.WriteLine(Environment.NewLine);
    Console.WriteLine(result);
    Console.WriteLine(Environment.NewLine);
    Console.WriteLine($"Inference completed successfully.{Environment.NewLine}");

}
catch (Exception e)
{
    Console.WriteLine($"An error occurred during inference: {e.Message}");
    throw;
}
finally
{
    Console.WriteLine($"Stopping local foundry service...");
    await manager.StopServiceAsync();
    Console.WriteLine($"Local foundry service stopped.");
}