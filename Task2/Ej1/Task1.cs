using System;
using System.Net;
using System.Text.Json;

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Website { get; set; }
}

public class Program
{
    public static void Main()
    {
        const string url = "https://jsonplaceholder.typicode.com/users/1";

        try
        {
            using (var client = new WebClient())
            {
                client.Encoding = System.Text.Encoding.UTF8;
                string json = client.DownloadString(url);

                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                User user = JsonSerializer.Deserialize<User>(json, options);

                if (user == null)
                {
                    Console.WriteLine("The server returned an empty response.");
                    return;
                }

                Console.WriteLine("=== User information ===");
                Console.WriteLine($"ID:       {user.Id}");
                Console.WriteLine($"Name:     {user.Name}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Email:    {user.Email}");
                Console.WriteLine($"Phone:    {user.Phone}");
                Console.WriteLine($"Website:  {user.Website}");
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
