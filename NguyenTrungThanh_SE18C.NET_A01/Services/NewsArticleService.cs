using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Collections.Generic;

namespace Services
{
    public class NewsArticleService : INewsArticleService
    {
        private readonly INewsArticleRepository _newsArticleRepository;
        public List<NewsArticle> GetPublishedNewsArticles()
        {
            return _newsArticleRepository.GetPublishedNewsArticles();
        }
        public NewsArticleService(INewsArticleRepository newsArticleRepository)
        {
            _newsArticleRepository = newsArticleRepository;
        }

        public void DeleteNewsArticle(NewsArticle article)
        {
            _newsArticleRepository.DeleteNewsArticle(article);
        }

        public NewsArticle? GetNewsArticleById(string id)
        {
            return _newsArticleRepository.GetNewsArticleById(id);
        }

        public List<NewsArticle> GetNewsArticles()
        {
            return _newsArticleRepository.GetNewsArticles();
        }

        public void SaveNewsArticle(NewsArticle article)
        {
            _newsArticleRepository.SaveNewsArticle(article);
        }

        public void UpdateNewsArticle(NewsArticle article)
        {
            _newsArticleRepository.UpdateNewsArticle(article);
        }

        public List<NewsArticle> GetNewsArticlesByAccountId(short accountId)
        {
            return _newsArticleRepository.GetNewsArticlesByAccountId(accountId);
        }
        public List<NewsArticle> GetNewsArticlesByPeriod(DateTime startDate, DateTime endDate)
        {
            return _newsArticleRepository.GetNewsArticlesByPeriod(startDate, endDate);
        }

    }
}