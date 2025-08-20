using System.Collections.Generic;
using System.Reflection.Metadata;

namespace LstProf.Models
{
    public class HomeViewModel
    {
        public List<Project> LatestProjects { get; set; }
        public List<BlogPost> LatestBlogs { get; set; }
    }
}
