using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;
using System.Xml.Linq;

namespace fsmAPI.Models
{
    public class Post
    {
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string Content { get; set; }
        public string Likes { get; set; }
        public string Comments { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
        public string IsPublished { get; set; }
        public string ImageUrl { get; set; }
        public string ExternalUrl { get; set; }
        public string Location { get; set; }
    }

    public class NewPost
    {   
        public int AuthorId { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public List<IFormFile> Files { get; set; }
    }
}
