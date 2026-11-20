using System;
using System.Net;
using System.Text.Json;

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string url = "https://jsonplaceholder.typicode.com/posts";

        try
        {
            // --- Input and validation ---
            Console.Write("Enter User ID: ");
            if (!int.TryParse(Console.ReadLine(), out int userId) || userId <= 0)
            {
                Console.WriteLine("Invalid User ID. It must be a positive integer.");
                return;
            }

            Console.Write("Enter title: ");
            string title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("The title cannot be empty.");
                return;
            }

            Console.Write("Enter post content: ");
            string body = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(body))
            {
                Console.WriteLine("The post content cannot be empty.");
                return;
            }

            // --- Create and serialize the object ---
            var post = new Post { UserId = userId, Title = title.Trim(), Body = body.Trim() };

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                // Id is assigned by the server, so we do not send it
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault
            };
            string json = JsonSerializer.Serialize(post, options);

            // --- Send the POST request ---
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                client.Headers[HttpRequestHeader.ContentType] = "application/json";

                string response = client.UploadString(url, "POST", json);

                Console.WriteLine();
                Console.WriteLine("=== Server response ===");
                Console.WriteLine(response);
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Network error: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
