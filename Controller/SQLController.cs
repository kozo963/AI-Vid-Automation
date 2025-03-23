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

        public static List<VideoData> GetVideoDataToDo()
        {
            return context.VideoData.Where(x => x.IsDone == false || x.IsDone == null).ToList();
        }
    }
}
