using BusinessObjects;
using System; 
using System.Collections.Generic;

namespace Repositories.Interfaces
{
    public interface INewsArticleRepository
    {
        List<NewsArticle> GetNewsArticlesByPeriod(DateTime startDate, DateTime endDate);
        List<NewsArticle> GetNewsArticles();
        NewsArticle? GetNewsArticleById(string id);
        void SaveNewsArticle(NewsArticle article);
        void UpdateNewsArticle(NewsArticle article);
        void DeleteNewsArticle(NewsArticle article);
        List<NewsArticle> GetNewsArticlesByAccountId(short accountId);
        List<NewsArticle> GetPublishedNewsArticles();   
    }
}