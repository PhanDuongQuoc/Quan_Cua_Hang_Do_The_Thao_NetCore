using SportShop2025.Data;

namespace SportShop2025.ViewModel
{
    public class HomeViewModel
    {
        public Product Product { get; set; } = new Product();
        public Video video { get; set; } = new Video(); 
        public Blog blog { get; set; } = new Blog();
        public List<Slide> slides { get; set; } = new List<Slide>();
        public List<Product> products { get; set; } = new List<Product>();
        public List<Video> videos { get; set; } = new List<Video> { new Video() };
        public List<Blog> blogs { get; set; } = new List<Blog> { new Blog() };

    }
}
