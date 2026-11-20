using System;
using System.Collections.Generic;
using System.Linq;
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
        const int selectedUserId = 1;

        try
        {
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                string json = client.DownloadString(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                List<Post> posts = JsonSerializer.Deserialize<List<Post>>(options: options, json: json);

                if (posts == null)
                {
                    Console.WriteLine("The server returned an empty response.");
                    return;
                }

                // LINQ filtering
                List<Post> userPosts = posts.Where(p => p.UserId == selectedUserId).ToList();

                Console.WriteLine($"=== Posts of user {selectedUserId} ===");
                foreach (Post post in userPosts)
                {
                    Console.WriteLine($"Post ID: {post.Id}");
                    Console.WriteLine($"Title:   {post.Title}");
                    Console.WriteLine();
                }

                Console.WriteLine($"Total posts of user {selectedUserId}: {userPosts.Count}");
            }
        }
        catch (WebException ex)
        {
            Console.WriteLine($"Network error: {ex.Message}");
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
