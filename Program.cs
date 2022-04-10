
   
using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "//nlog.config").GetCurrentClassLogger();


        class Posts
        {
            public void AddPost()
            {
                   var db = new BloggingContext();
                Console.WriteLine("Select the blog you would like to post to:");

                var query = db.Blogs.OrderBy(b => b.BlogId);
                foreach (var item in query)
                {
                    Console.WriteLine(item.BlogId.ToString() + ") " + item.Name);
                    }
                    
                var option = Int32.Parse(Console.ReadLine());
                if(option.Equals(typeof(string))){
                    logger.Error("Not a valid integer for blog");
                } 
                else if(option != db.Blogs.Find(option).BlogId){
                    logger.Error("Blog not found with that Blog Id");
                }

                else{
                
                Console.WriteLine($"Please enter title");
                var title = Console.ReadLine();
                if(title.Length == 0){
                    logger.Error("Post title cannot be null");
                }
                else{
                    Console.WriteLine("Please enter content of post");
                    var content = Console.ReadLine();
                    var post = new Post{ Title = title, Content = content};
                    db.AddPost(db.Blogs.Find(option), post);
                      
                    logger.Info($"Post added - {content}");
                }
                }

                
            }

            public void ReadPosts()
            {
                var db = new BloggingContext();
                Console.WriteLine("Select the blog you would like to view the posts of");
                var query = db.Blogs.OrderBy(b => b.BlogId);
                Console.WriteLine("0) Display all posts");
                foreach (var item in query)
                {
                    Console.WriteLine(item.BlogId.ToString() + ") " + item.Name);
                    }
                var option = Int32.Parse(Console.ReadLine());
                if(option == 0){
                    var order = db.Posts.OrderBy(b => b.BlogId);
                    foreach(Post p in order){
                        p.Blog.Name = db.Blogs.Find(p.BlogId).Name;
                
                          Console.WriteLine(p.Display());
                        }
                        logger.Info($"{db.Posts.Count()} posts returned");
                    }
                    
                
                else{
               foreach(Post p in db.Posts.Where(p => option.Equals(p.BlogId))){
                   p.Blog.Name = db.Blogs.Find(option).Name;
                       Console.WriteLine(p.Display());
                    }
                    logger.Info($"{db.Posts.Where(p => option.Equals(p.BlogId)).Count()} Posts Returned");
                    
                }
            }
        }
        class Blogs
        {
            public void RunReadBlog()
            {

                var db = new BloggingContext();

                var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");

                 foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
            }

            public void RunCreateBlog()
            {
               Console.Write("Enter a name for a new Blog: ");
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                var db = new BloggingContext();
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
            }



        }

        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                
                

                Console.WriteLine("1. Display all blogs");
                Console.WriteLine("2. Add Blog");
                Console.WriteLine("3. Create Post");
                Console.WriteLine("4. Display Posts");
                string selection = Console.ReadLine();

                if(selection == "1"){
                    Blogs newCreate = new Blogs();
                    newCreate.RunReadBlog();

                
               
                }else if(selection == "2"){
                  Blogs newCreate = new Blogs();
                  newCreate.RunCreateBlog();
                }else if(selection == "3"){
                    Posts newCreate = new Posts();
                    newCreate.AddPost();
                }else if(selection == "4")
                {
                    Posts newCreate = new Posts();
                    newCreate.ReadPosts();
                }

                
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}