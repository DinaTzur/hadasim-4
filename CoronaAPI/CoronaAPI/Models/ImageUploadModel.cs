namespace CoronaAPI.Models
{
    public class ImageUploadModel
    {
        public int Id { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
