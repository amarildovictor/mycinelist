namespace MyCineList.Domain.Entities{
    public class ImageMovie
    {
        public ImageMovie(){ 
            ImdbPrimaryImageUrl = string.Empty;
        }

        public ImageMovie(int id, string imdbPrimaryImageUrl, int width, int height) 
        {
            this.ID = id;
            this.ImdbPrimaryImageUrl = imdbPrimaryImageUrl;
            this.Width = width;
            this.Height = height;
        }

        public int ID { get; set; }

        public string ImdbPrimaryImageUrl { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }
}