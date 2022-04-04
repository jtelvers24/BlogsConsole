
   
using System;
using NLog.Web;
using System.IO;
using System.Linq;

namespace BlogsConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "//nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                var name = Console.ReadLine();

                var blog = new Blog { Name = name };

                var db = new BloggingContext();

                Console.WriteLine("1. Display all blogs");
                Console.WriteLine("2. Add Blog");
                Console.WriteLine("3. Create Post");
                Console.WriteLine("4. Display Posts");
                string selection = Console.ReadLine();

                if(selection == "1"){
                    var query = db.Blogs.OrderBy(b => b.Name);

                Console.WriteLine("All blogs in the database:");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                }else if(selection == "2"){
                    Console.Write("Enter a name for a new Blog: ");
                
                db.AddBlog(blog);
                logger.Info("Blog added - {name}", name);
                }else{
                    Console.WriteLine("Selection not found or Selection has not been created");
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