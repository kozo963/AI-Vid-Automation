using System;
using System.Collections.Generic;

namespace AI_Vid_Automation.Models;

public partial class StoryData
{
    public int Id { get; set; }

    public int VideoDataId { get; set; }

    public string StoryBoard { get; set; } = null!;

    public virtual VideoData VideoData { get; set; } = null!;
}
