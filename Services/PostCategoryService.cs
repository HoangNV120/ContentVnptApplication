using ContentVnptApplication.Helpers;
using ContentVnptApplication.Models;
using ContentVnptApplication.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContentVnptApplication.Services
{
    public class PostCategoryService
    {
        private AppDbContext db = new AppDbContext();

        public List<PostCategoryViewModel> GetAll()
        {
            return db.PostCategories
                .Where(x => !x.IsDeleted)
                .OrderByDescending(x => x.Id)
                .Select(x => new PostCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug,
                })
                .ToList();
        }

        public PostCategory GetById(int id)
        {
            return db.PostCategories.Find(id);
        }

        public bool IsExistTitle(string title, int? id = null)
        {
            return db.PostCategories.Any(x =>
                x.Title == title &&
                !x.IsDeleted &&
                x.Id != (id ?? 0));
        }

        public void Create(PostCategory model)
        {
            model.IsDeleted = false;
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = model.CreatedAt;

            db.PostCategories.Add(model);
            db.SaveChanges();

            model.Slug = SlugHelperService.Generate(model.Title) + "-" + model.Id;
            db.SaveChanges();
        }

        public bool Update(PostCategory model)
        {
            var cat = db.PostCategories.Find(model.Id);
            if (cat == null) return false;

            cat.Title = model.Title;
            cat.Slug = SlugHelperService.Generate(model.Title) + "-" + model.Id;
            cat.UpdatedAt = DateTime.Now;

            db.SaveChanges();
            return true;
        }

        public void Delete(int id)
        {
            var cat = db.PostCategories.Find(id);
            if (cat == null) return;

            cat.IsDeleted = true;
            cat.DeletedAt = DateTime.Now;
            cat.UpdatedAt= cat.DeletedAt;

            db.SaveChanges();
        }

        public List<PostCategory> GetMenu()
        {
            return db.PostCategories.ToList();
        }

        public PostCategory GetBySlug(string slug)
        {
            return db.PostCategories
                .FirstOrDefault(x => x.Slug == slug);
        }

        public PagedViewModel<PostCategoryViewModel> GetPaged(string keyword, int page, int pageSize)
        {
            var query = db.PostCategories.Where(x => !x.IsDeleted);

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
                .Select(x => new PostCategoryViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Slug = x.Slug
                })
                .ToList();

            return new PagedViewModel<PostCategoryViewModel>
            {
                Items = items,
                Page = page,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages
            };
        }
    }
}