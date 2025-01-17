﻿using Newtonsoft.Json;

namespace MyCineList.Domain.Entities{
    public class ImageMovie
    {
        public ImageMovie(){ 
            ImdbPrimaryImageUrl = string.Empty;
            ConsidererToResizingFunction = true;
        }

        public ImageMovie(int id, string imdbPrimaryImageUrl, int width, int height) 
        {
            this.ID = id;
            this.ImdbPrimaryImageUrl = imdbPrimaryImageUrl;
            this.Width = width;
            this.Height = height;
            ConsidererToResizingFunction = true;
        }

        [JsonIgnore]
        public int ID { get; set; }

        [JsonProperty("url")]
        public string ImdbPrimaryImageUrl { get; set; }

        [JsonIgnore]
        public string? SmallImageUrl { get; set; }

        [JsonIgnore]
        public string? MediumImageUrl { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonIgnore]
        public bool ConsidererToResizingFunction { get; set; }
    }
}