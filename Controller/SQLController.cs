using AI_Vid_Automation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_Vid_Automation.Controller
{
    internal static class SQLController
    {
        static AividAutomationDbContext context = new AividAutomationDbContext();

        internal static Aiprompt GetPrompt(int ID)
        {
            return context.Aiprompts.Where(x => x.Id == ID).FirstOrDefault();
        }

        internal static void AddVideoData(VideoData videoData)
        {
            context.Add(videoData);
            context.SaveChanges();
        }
        internal static void UpdateVideoData(VideoData videoData)
        {
            context.VideoData.Update(videoData);
            context.SaveChanges();
        }
        internal static VideoData GetVideoToDo()
        {
            return context.VideoData.Where(x => x.IsDone == false && x.IsStoryBoard == false).FirstOrDefault();
        }

        internal static void AddStoryBoards(List<StoryData> storyDatas)
        {
            context.AddRange(storyDatas);
            context.SaveChanges();
        }

        internal static List<VideoData> GetAllVideos()
        {
            return context.VideoData.ToList();
        }
    }
}