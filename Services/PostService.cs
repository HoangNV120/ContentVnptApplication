using ContentVnptApplication.Models;
using ContentVnptApplication.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using ContentVnptApplication.ViewModel;
using System.Diagnostics;


namespace ContentVnptApplication.Services
{
    public class PostService
    {
        private AppDbContext db = new AppDbContext();


        public List<PostViewModel> GetAll()
        {
            return db.Posts
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Description = x.Description,
                    PostBrief = x.PostBrief,
                    ImageUrl = x.ImageUrl,
                    IsPublished = x.IsPublished,
                    Created = x.CreatedAt,
                    PostCategoryId = x.PostCategoryId,
                    PostCategoryTitle = x.PostCategory.Title,
                })
                .ToList();
        }

        public Post GetById(int id)
        {
            return db.Posts.Find(id);
        }

        public Post GetBySlug(string slug)
        {
            return db.Posts
                .FirstOrDefault(x => x.Slug == slug && x.IsPublished && !x.IsDeleted);
        }
        public List<Post> GetByCategory(int categoryId)
        {
            return db.Posts
                .Where(x =>
                    x.PostCategoryId == categoryId &&
                    x.IsPublished &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt)
                .ToList();
        }

        public PagedViewModel<PostViewModel> GetPaged(string keyword, int page, int pageSize)
        {
            var query = db.Posts.Where(x => !x.IsDeleted);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Title.Contains(keyword));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Created = x.CreatedAt,
                    PostBrief = x.PostBrief,
                    Description = x.Description,
                    Slug = x.Slug,
                    ImageUrl = x.ImageUrl,
                    IsPublished = x.IsPublished,
                    PostCategoryTitle = x.PostCategory.Title,
                    PostCategoryId = x.PostCategoryId,
                })
                .ToList();

            return new PagedViewModel<PostViewModel>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public PagedViewModel<PostViewModel> GetPaged(int page, int pageSize)
        {
            var query = db.Posts.Where(x => !x.IsDeleted && x.IsPublished);

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Author = x.Author,
                    Created = x.CreatedAt,
                    PostBrief = x.PostBrief,
                    Description = x.Description,
                    Slug = x.Slug,
                    ImageUrl = x.ImageUrl,
                    PostCategoryTitle = x.PostCategory.Title
                })
                .ToList();

            return new PagedViewModel<PostViewModel>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }

        public PagedViewModel<PostViewModel> GetPagedByCategory(
            int categoryId, 
            string keyword,
            int page,
            int pageSize)
        {
            var query = db.Posts
                .Where(x =>
                    !x.IsDeleted &&
                    x.IsPublished &&
                    x.PostCategoryId == categoryId);

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Title.Contains(keyword));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var items = query
                .OrderByDescending(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    PostBrief = x.PostBrief,
                    Description = x.Description,
                    Author = x.Author,
                    Created = x.CreatedAt,
                    Slug = x.Slug,
                    ImageUrl = x.ImageUrl,
                    PostCategoryTitle = x.PostCategory.Title
                })
                .ToList();

            return new PagedViewModel<PostViewModel>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
        public void Create(Post model)
        {
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = model.CreatedAt;
            model.IsDeleted = false;
            var sanitizer = new Ganss.Xss.HtmlSanitizer();
            model.Description = sanitizer.Sanitize(model.Description);

            Debug.WriteLine(model.Description);

            db.Posts.Add(model);
            db.SaveChanges();

            model.Slug = SlugHelperService.Generate(model.Title) + "-" + model.Id;
            db.SaveChanges();
        }

        public bool Update(Post model)
        {
            Debug.WriteLine(model.PostBrief);
            var post = db.Posts.Find(model.Id);
            if (post == null) return false;

            post.Title = model.Title;
            post.Author = model.Author;
            post.ImageUrl = model.ImageUrl;
            post.PostCategoryId = model.PostCategoryId;
            post.IsPublished = model.IsPublished;
            post.UpdatedAt = model.UpdatedAt;
            post.PostBrief = model.PostBrief;

            var sanitizer = new Ganss.Xss.HtmlSanitizer();
            post.Description = sanitizer.Sanitize(model.Description);
            Debug.WriteLine(post.Description);

            post.Slug = SlugHelperService.Generate(model.Title) + "-" + model.Id;

            db.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var post = db.Posts.Find(id);
            if (post == null) return;

            post.IsDeleted = true;
            post.DeletedAt = DateTime.Now;
            post.UpdatedAt = post.DeletedAt;

            db.SaveChanges();
        }

        public List<PostViewModel> GetRelatedPosts(int postId, int categoryId, int take = 5)
        {
            var related = db.Posts
                .Where(x => x.PostCategoryId == categoryId
                            && x.Id != postId
                            && x.IsPublished)
                .OrderByDescending(x => x.CreatedAt)
                .Take(take)
                .Select(x => new PostViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    PostBrief = x.PostBrief,
                    Author = x.Author,
                    Created = x.CreatedAt,
                    Slug = x.Slug,
                    ImageUrl = x.ImageUrl,
                    PostCategoryTitle = x.PostCategory.Title
                })
                .ToList();

            var relatedIds = related.Select(r => r.Id).ToList();

            if (related.Count < take)
            {
                var more = db.Posts
                    .Where(x => x.PostCategoryId != categoryId
                                && x.Id != postId
                                && x.IsPublished
                                && !relatedIds.Contains(x.Id))
                    .OrderByDescending(x => x.CreatedAt)
                    .Take(take - related.Count)
                    .Select(x => new PostViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        PostBrief = x.PostBrief,
                        Author = x.Author,
                        Created = x.CreatedAt,
                        Slug = x.Slug,
                        ImageUrl = x.ImageUrl,
                        PostCategoryTitle = x.PostCategory.Title
                    })
                    .ToList();

                related.AddRange(more);
            }

            return related;
        }
        public List<Post> LatestPosts(int count = 5)
        {
            return db.Posts
                .Where(x =>
                    x.IsPublished &&
                    !x.IsDeleted)
                .OrderByDescending(x => x.UpdatedAt)
                .Take(count)
                .ToList();
        }
    }
}