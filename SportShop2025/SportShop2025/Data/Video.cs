using System;
using System.Collections.Generic;

namespace SportShop2025.Data;

public partial class Video
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Url { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }
}
