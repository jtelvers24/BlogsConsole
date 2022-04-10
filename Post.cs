namespace BlogsConsole
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

 public string Display(){
            return $"Blog Id: {BlogId}/nBlog: {Blog.Name}/nTitle: {Title}/nContent:{Content}/n";
        }
        public Post(){
            Blog = new Blog();
        }

       

    }
}