using Newtonsoft.Json;

namespace ArchitecturalStudioTradition.Model.Facebook
{
    public class FacebookPictureData
    {
        public FacebookPicture Data { get; set; }
    }

    public class FacebookPicture
    {
        public int Height { get; set; }
        public int Width { get; set; }
        [JsonProperty("is_silhouette")]
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }

}
