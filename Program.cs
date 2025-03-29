using AI_Vid_Automation.Controller;
using AI_Vid_Automation.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
internal class Program
{
    static void Main(string[] args)
    {
        //for (int i = 0; i < 3; i++)
        //{
        //    FillDBWithIdeaAndContext();
        //}

        //for (int i = 0; i < 3; i++)
        //{
        //    FillStoryBoards();
        //}

        SeleniumController
    }

    private static void FillStoryBoards()
    {
        List<StoryData> storyDatas = new List<StoryData>();
        VideoData videoData = SQLController.GetVideoToDo();
        videoData.IsDone = true;

        Aiprompt aiprompt = SQLController.GetPrompt(2002);

        string prompt = ModifyPrompt(aiprompt.Prompt, videoData.Text);

        string jsonData = DeepSeekController.GetAnswerAsync(prompt).Result;
        

        JsonNode rootNode = JsonNode.Parse(jsonData);
        try
        {
            JsonNode jsonNode = rootNode?["StoryBoard"]?.AsArray();

            for (int i = 0; i < jsonNode.AsArray().Count; i++)
            {
                storyDatas.Add(new StoryData()
                {
                    VideoDataId = videoData.Id,
                    StoryBoard = jsonNode[i]["prompt"]?.GetValue<string>(),
                });
            }

            SQLController.AddStoryBoards(storyDatas);
            Console.WriteLine("Story boards added");
            videoData.IsStoryBoard = true;
            SQLController.UpdateVideoData(videoData);
            Console.WriteLine("Videodata updated");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error" + ex);
        }
    }

    private static string ModifyPrompt(string prompt, string text)
    {
        return prompt.Replace("!@#!@#", text);
    }

    private static void FillDBWithIdeaAndContext()
    {
        VideoData videoData = new VideoData();
        Aiprompt aiprompt = SQLController.GetPrompt(1);

        List<VideoData> videoDatas = SQLController.GetAllVideos();

        string prompt = ModifyPrompt(aiprompt.Prompt, string.Join(";",videoDatas.Select(x => x.Idea)));
        
        string jsonData = DeepSeekController.GetAnswerAsync(aiprompt.Prompt).Result;

        JsonNode rootNode = JsonNode.Parse(jsonData);
        try
        {
            JsonNode jsonNode = rootNode?["viral_vid"]?.AsArray()[0];

            if (jsonNode != null)
            {
                // Populate the VideoData object with values from the JSON
                videoData.Idea = jsonNode["idea"]?.GetValue<string>();
                videoData.Text = jsonNode["text"]?.GetValue<string>();
                videoData.IsDone = false;
                videoData.EnviromentPrompt = jsonNode["environment_prompt"]?.GetValue<string>();
                videoData.IsStoryBoard = false;

                Console.WriteLine(videoData.Text);
                Console.WriteLine("\n\n\n\n");
                // Add the VideoData object to the database
                SQLController.AddVideoData(videoData);
                Console.WriteLine("New VideoData added to DB.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid JSON data received.");
        }
    }
}