using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AI_Vid_Automation.Controller
{
    public static class DeepSeekController
    {
        private static string ExtractJsonObject(string text)
        {
            // Use a regular expression to find the JSON object
            string pattern = @"\{.*\}";
            Match match = Regex.Match(text, pattern, RegexOptions.Singleline);

            if (match.Success)
            {
                return match.Value;
            }
            return null;
        }
        public static async Task<string> GetAnswerAsync(string prompt)
        {
            //OpenRouter API key
            string apiKey = File.ReadAllText($@"D:\Projects\C#\AIDATA\apiKey.txt");

            // OpenRouter API endpoint
            string apiUrl = "https://openrouter.ai/api/v1/chat/completions";

            // Create the request payload
            var requestBody = new
                {
                    model = "deepseek/deepseek-r1:free", // DeepSeek model
                    messages = new[]
                    {
                        new { role = "user", content = prompt 
                    }
                }
            };

            // Serialize the request body to JSON
            string jsonContent = Newtonsoft.Json.JsonConvert.SerializeObject(requestBody);

            using (HttpClient client = new HttpClient())
            {
                // Add headers
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                // Create the HTTP content
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // Make the POST request
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine("Response: " + responseBody);

                    JObject json = JObject.Parse(responseBody);
                    //hiConsole.WriteLine(json["choices"][0]["message"]["content"].ToString());
                    return ExtractJsonObject(json["choices"][0]["message"]["content"].ToString());
                }
                else
                {
                    Console.WriteLine("Error: " + response.StatusCode);
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Error Details: " + errorResponse);
                }
            }
            return "Error";
        }
    }
}