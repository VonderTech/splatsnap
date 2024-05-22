using System.Diagnostics;

namespace SplatSnapConsole;

internal static class Program
{
    private static async Task Main()
    {
        // Print the current working directory

        var currentDirectory = Directory.GetCurrentDirectory();
        Console.WriteLine($"Current working directory: {currentDirectory}");

        // Define the script path
        var scriptPath = @"..\..\..\scripts\start_backend.bat";

        // Start the backend using the shell script
        var processStartInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/c {scriptPath}",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        var process = new Process
        {
            StartInfo = processStartInfo
        };

        process.Start();
        Console.WriteLine("Starting Python backend...");

        // Read and display the standard output and error
        process.OutputDataReceived += (_, e) => Console.WriteLine(e.Data);
        process.ErrorDataReceived += (_, e) => Console.WriteLine(e.Data);
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        // Wait for the server to be ready
        var serverReady = false;
        using var client = new HttpClient();
        client.BaseAddress = new Uri("http://127.0.0.1:5000");

        while (!serverReady)
            try
            {
                var response = await client.GetAsync("/");
                if (response.IsSuccessStatusCode) serverReady = true;
            }
            catch (HttpRequestException)
            {
                // Server is not ready yet
                await Task.Delay(500); // Wait for a short period before retrying
            }

        Console.WriteLine("Python backend is ready.");

        // Send a GET request
        var finalResponse = await client.GetAsync("/");
        finalResponse.EnsureSuccessStatusCode();

        // Read and display the response body
        var responseBody = await finalResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Response from Flask server: {responseBody}");

        // Stop the Python server
        process.Kill();
        Console.WriteLine("Python server stopped.");
    }
}