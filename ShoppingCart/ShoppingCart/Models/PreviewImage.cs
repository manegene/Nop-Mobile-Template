using System.Runtime.Serialization;
using Xamarin.Forms.Internals;

namespace habahabamall.Models
{

    /// <summary>
    /// Model for preview image.
    /// </summary>
    ///
    [DataContract]
    [Preserve(AllMembers = true)]
    public class PreviewImage
    {
        public virtual string GetFileExtensionFromMimeType(string mimeType)
        {
            if (mimeType == null)
            {
                return null;
            }

            string[] parts = mimeType.Split('/');
            string lastPart = parts[parts.Length - 1];
            switch (lastPart)
            {
                case "pjpeg":
                    lastPart = "jpg";
                    break;
                case "x-png":
                    lastPart = "png";
                    break;
                case "x-icon":
                    lastPart = "ico";
                    break;
            }

            return lastPart;
        }

        [DataMember(Name = "id")] public int Id { get; set; }

        [DataMember(Name = "src")] public string SeoFilename { get; set; }

        public string MimeType { get; set; }

        private string SeoType => GetFileExtensionFromMimeType(MimeType);
        private string Productimage => $"{Id:0000000}_{SeoFilename}.{SeoType}";

        public string Image => App.BaseImageUrl + Productimage;


        [DataMember(Name = "product_id")] public int product_id { get; set; }




    }
}