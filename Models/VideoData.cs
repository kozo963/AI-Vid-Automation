using System;
using System.Collections.Generic;

namespace AI_Vid_Automation.Models;

public partial class VideoData
{
    public int Id { get; set; }

    public string? Idea { get; set; }

    public string? Text { get; set; }

    public bool? IsDone { get; set; }

    public string? EnviromentPrompt { get; set; }

    public bool IsStoryBoard { get; set; }

    public virtual ICollection<StoryData> StoryData { get; set; } = new List<StoryData>();
}
