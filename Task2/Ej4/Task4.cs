using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Post
{
    public int UserId { get; set; }
    public int Id { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
}

public class PostService
{
    private const string BaseUrl = "https://jsonplaceholder.typicode.com/posts";

    private static readonly JsonSerializerOptions ReadOptions =
        new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

    private static readonly JsonSerializerOptions WriteOptions =
        new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

    private WebClient CreateClient()
    {
        var client = new WebClient();
        client.Encoding = System.Text.Encoding.UTF8;
        client.Headers[HttpRequestHeader.ContentType] = "application/json";
        return client;
    }

    public List<Post> GetPosts()
    {
        using (var client = CreateClient())
        {
            string json = client.DownloadString(BaseUrl);
            return JsonSerializer.Deserialize<List<Post>>(json, ReadOptions) ?? new List<Post>();
        }
    }

    public Post GetPost(int id)
    {
        using (var client = CreateClient())
        {
            string json = client.DownloadString($"{BaseUrl}/{id}");
            return JsonSerializer.Deserialize<Post>(json, ReadOptions);
        }
    }

    public string CreatePost(Post post)
    {
        using (var client = CreateClient())
        {
            string json = JsonSerializer.Serialize(post, WriteOptions);
            return client.UploadString(BaseUrl, "POST", json);
        }
    }

    public string UpdatePost(Post post)
    {
        using (var client = CreateClient())
        {
            // PUT replaces the whole resource, so Id is included in the body
            string json = JsonSerializer.Serialize(post, WriteOptions);
            return client.UploadString($"{BaseUrl}/{post.Id}", "PUT", json);
        }
    }

    public string DeletePost(int id)
    {
        using (var client = CreateClient())
        {
            return client.UploadString($"{BaseUrl}/{id}", "DELETE", string.Empty);
        }
    }
}

public class Program
{
    private static readonly PostService service = new PostService();

    public static void Main()
    {
        bool running = true;

        while (running)
        {
            PrintMenu();
            string choice = Console.ReadLine();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1": DisplayAllPosts(); break;
                    case "2": DisplayPostById(); break;
                    case "3": CreatePost(); break;
                    case "4": UpdatePost(); break;
                    case "5": DeletePost(); break;
                    case "0": running = false; Console.WriteLine("Goodbye!"); break;
                    default: Console.WriteLine("Invalid option. Choose a number from 0 to 5."); break;
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine($"Network/server error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON processing error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            if (running)
            {
                Console.WriteLine();
            }
        }
    }

    private static void PrintMenu()
    {
        Console.WriteLine("===== Post Management System =====");
        Console.WriteLine("1. Display all posts");
        Console.WriteLine("2. Display a post by ID");
        Console.WriteLine("3. Create a post");
        Console.WriteLine("4. Update a post");
        Console.WriteLine("5. Delete a post");
        Console.WriteLine("0. Exit");
        Console.Write("Choose an option: ");
    }

    // ---------- Menu actions ----------

    private static void DisplayAllPosts()
    {
        List<Post> posts = service.GetPosts();
        foreach (Post p in posts)
        {
            Console.WriteLine($"[{p.Id}] (User {p.UserId}) {p.Title}");
        }
        Console.WriteLine($"Total: {posts.Count} posts");
    }

    private static void DisplayPostById()
    {
        if (!ReadId("Enter post ID: ", out int id)) return;

        Post post = service.GetPost(id);
        PrintPost(post);
    }

    private static void CreatePost()
    {
        if (!ReadId("Enter User ID: ", out int userId)) return;
        if (!ReadText("Enter title: ", "The title cannot be empty.", out string title)) return;
        if (!ReadText("Enter post content: ", "The post content cannot be empty.", out string body)) return;

        var post = new Post { UserId = userId, Title = title, Body = body };
        string response = service.CreatePost(post);

        Console.WriteLine("Post created. Server response:");
        Console.WriteLine(response);
    }

    private static void UpdatePost()
    {
        if (!ReadId("Enter the ID of the post to update: ", out int id)) return;
        if (!ReadId("Enter User ID: ", out int userId)) return;
        if (!ReadText("Enter new title: ", "The title cannot be empty.", out string title)) return;
        if (!ReadText("Enter new post content: ", "The post content cannot be empty.", out string body)) return;

        var post = new Post { Id = id, UserId = userId, Title = title, Body = body };
        string response = service.UpdatePost(post);

        Console.WriteLine("Post updated. Server response:");
        Console.WriteLine(response);
    }

    private static void DeletePost()
    {
        if (!ReadId("Enter the ID of the post to delete: ", out int id)) return;

        string response = service.DeletePost(id);
        Console.WriteLine($"Post {id} deleted. Server response: {response}");
    }

    // ---------- Helpers ----------

    private static bool ReadId(string prompt, out int id)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out id) && id > 0)
        {
            return true;
        }
        Console.WriteLine("Invalid ID. It must be a positive integer.");
        return false;
    }

    private static bool ReadText(string prompt, string errorMessage, out string text)
    {
        Console.Write(prompt);
        text = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(text))
        {
            Console.WriteLine(errorMessage);
            return false;
        }
        text = text.Trim();
        return true;
    }

    private static void PrintPost(Post post)
    {
        if (post == null)
        {
            Console.WriteLine("Post not found.");
            return;
        }
        Console.WriteLine($"ID:      {post.Id}");
        Console.WriteLine($"User ID: {post.UserId}");
        Console.WriteLine($"Title:   {post.Title}");
        Console.WriteLine($"Body:    {post.Body}");
    }
}
