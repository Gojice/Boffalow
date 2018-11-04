﻿/*
 * Developer: Ramtin Jokar [ Ramtinak@live.com ] [ My Telegram Account: https://t.me/ramtinak ]
 * 
 * Github source: https://github.com/ramtinak/InstagramApiSharp
 * Nuget package: https://www.nuget.org/packages/InstagramApiSharp
 * 
 * IRANIAN DEVELOPERS
 */

using System.Collections.Generic;
using Newtonsoft.Json;
namespace InstagramApiSharp.Classes
{
    public class InstaBroadcastTopLiveStatusResponse
    {
        [JsonProperty("broadcast_status_items")]
        public List<InstaBroadcastStatusItem> BroadcastStatusItems { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class InstaBroadcastStatusItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        [JsonProperty("broadcast_status")]
        public string BroadcastStatus { get; set; }
        [JsonProperty("viewer_count")]
        public float ViewerCount { get; set; }
        [JsonProperty("has_reduced_visibility")]
        public bool HasReducedVisibility { get; set; }
        [JsonProperty("cover_frame_url")]
        public string CoverFrameUrl { get; set; }
    }

}
