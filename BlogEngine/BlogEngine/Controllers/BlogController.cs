﻿using BlogEngine.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogEngine.Models;

namespace BlogEngine.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogRepository context;
        public static List<BlogListViewModel> PostList = new List<BlogListViewModel>();
        public BlogController()
        {
            context = new BlogRepository(new BlogContext());
        }
        public BlogController(IBlogRepository _context)
        {
            context = _context;
        }
        public ActionResult Index()
        {
            Posts();
            return View();
        }
        public ActionResult Posts()
        {
            PostList.Clear();
            var posts = context.GetPosts();
            foreach (var post in posts)
            {
                var categories = GetCategories(post);
                var tags = GetTags(post);
                PostList.Add(new BlogListViewModel()
                {
                    Description = post.Description,
                    Body = post.Body,
                    Tittle = post.Tittle,
                    Id = post.Id,
                    PostedDate = post.PostedDate,
                    UrlOpt = post.UrlOpt,
                    Post = post,
                    Category = categories,
                    Tags = tags        
                });                
            }
            return PartialView("Posts");
        }
        public ActionResult Post(int PostId)
        {
            PostList.Clear();            
            var model = NewPost(PostId);
            return View(model);

        }
        public ActionResult Admin()
        {
            return View();
        }
        public ActionResult AddPost()
        {
            List<int> ids = new List<int>();
            int id = 0;          
            var posts = context.GetPosts();
            if (posts.Count != 0)
            {
                foreach (var post in posts)
                {
                    ids.Add(post.Id);
                }
                ids.Sort();
                id = ids.Last();
                id++;
            }
            else
            {
                id = 1;
            }
            BlogListViewModel model = new BlogListViewModel();
            model.Id = id;
            return View(model);
            
        }





        #region helper
        public IList<Post> GetPosts()
        {
            return context.GetPosts();
        }
        public IList<Category> GetCategories(Post post)
        {
            return context.GetCategory(post);
        }
        public IList<Tag> GetTags(Post post)
        {
            return context.GetTags(post);
        }
        public BlogListViewModel NewPost (int PostId)
        {
            BlogListViewModel model = new BlogListViewModel();            
            var post = context.GetPost(PostId);
            var categories = GetCategories(post);
            var tags = GetTags(post);
            model.Id = PostId;
            model.Tittle = post.Tittle;
            model.Description = post.Description;
            model.PostedDate = post.PostedDate;
            model.Category = categories;
            model.Tags = tags;
            model.Body = post.Body;
            return model;
        }
        #endregion
    }
}