using BusinessObjects;
using System.Collections.Generic;

namespace Services.Interfaces
{
    public interface INewsArticleService
    {
        List<NewsArticle> GetNewsArticlesByPeriod(DateTime startDate, DateTime endDate);
        List<NewsArticle> GetNewsArticlesByAccountId(short accountId);
        List<NewsArticle> GetNewsArticles();
        NewsArticle? GetNewsArticleById(string id);
        void SaveNewsArticle(NewsArticle article);
        void UpdateNewsArticle(NewsArticle article);
        void DeleteNewsArticle(NewsArticle article);
        List<NewsArticle> GetPublishedNewsArticles();

    }
}