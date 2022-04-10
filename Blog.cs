
using System;
using System.Linq;   
using System.Collections.Generic;
namespace BlogsConsole
{
        public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }


        public Blog(){
            Posts = new List<Post>();
        }
    }
}