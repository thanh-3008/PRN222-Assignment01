using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System; 
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        private readonly FUNewsManagementDbContext _context;

        public NewsArticleRepository(FUNewsManagementDbContext context)
        {
            _context = context;
        }
        public List<NewsArticle> GetPublishedNewsArticles()
        {
            return _context.NewsArticles
                           .Include(n => n.Category)
                           .Where(a => a.NewsStatus == true)
                           .OrderByDescending(a => a.CreatedDate)
                           .ToList();
        }

        public List<NewsArticle> GetNewsArticlesByAccountId(short accountId)
        {
            return _context.NewsArticles
                           .Include(n => n.Category)
                           .Where(a => a.CreatedById == accountId)
                           .OrderByDescending(a => a.CreatedDate)
                           .ToList();
        }

        public List<NewsArticle> GetNewsArticles()
        {
            return _context.NewsArticles
                           .Include(n => n.Category)
                           .OrderByDescending(a => a.CreatedDate)
                           .ToList();
        }

        public NewsArticle GetNewsArticleById(string id)
        {
            return _context.NewsArticles
                           .Include(a => a.Category)       
                           .Include(a => a.CreatedBy)      
                           .Include(a => a.NewsTags)       
                               .ThenInclude(nt => nt.Tag)  
                           .FirstOrDefault(a => a.NewsArticleId == id);
        }

        public void SaveNewsArticle(NewsArticle article)
        {
            _context.NewsArticles.Add(article);
            _context.SaveChanges();
        }

        public void UpdateNewsArticle(NewsArticle article)
        {
            var existingTags = _context.NewsTags.Where(nt => nt.NewsArticleId == article.NewsArticleId);
            _context.NewsTags.RemoveRange(existingTags);
            _context.NewsArticles.Update(article);
            _context.SaveChanges();
        }

        public void DeleteNewsArticle(NewsArticle article)
        {
            _context.NewsArticles.Remove(article);
            _context.SaveChanges();
        }

        public List<NewsArticle> GetNewsArticlesByPeriod(DateTime startDate, DateTime endDate)
        {
            return _context.NewsArticles
                           .Include(a => a.Category)
                           .Include(a => a.CreatedBy) 
                           .Where(a => a.CreatedDate.Value.Date >= startDate.Date && a.CreatedDate.Value.Date <= endDate.Date)
                           .OrderByDescending(a => a.CreatedDate)
                           .ToList();
        }
    }
}