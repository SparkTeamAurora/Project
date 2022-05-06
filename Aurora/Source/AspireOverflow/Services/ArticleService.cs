using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{

    public class ArticleService 
    {
        private static IArticleRepository database;

        private static ILogger<ArticleService> _logger;

        public ArticleService(ILogger<ArticleService> logger)
        {
            _logger = logger;
            database = ArticleRepositoryFactory.GetArticleRepositoryObject(logger);

        }

        public bool CreateArticle(Article article)
        {
            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {

                article.CreatedOn = DateTime.Now;

                return database.AddArticle(article);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle()", exception, article));
                return false;
            }
        }



        public bool UpdateArticle(Article article, int CurrentUser)
        {

            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                var ExistingArticle = GetArticlesByArticleStatusId(1).Where(Item => Item.ArtileId == article.ArtileId).First();
                if (ExistingArticle == null) throw new ItemNotFoundException($"Unable to Find any Article with ArticleId:{article.ArtileId}");
                article.UpdatedOn = DateTime.Now;
                article.UpdatedBy = CurrentUser;
                ExistingArticle = article;

                return database.UpdateArticle(ExistingArticle);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle()", exception, article));
                return false;
            }
        }


        public bool ChangeArticleStatus(int ArticleId, int ArticleStatusID, int UpdatedByUserId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                return database.UpdateArticle(ArticleId, ArticleStatusID, UpdatedByUserId);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "ChangeArticleStatus()", exception), ArticleId, ArticleStatusID);

                throw exception;
            }

        }

        public bool DeleteArticleByArticleId(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {  if(GetArticlesByArticleStatusId(1).Where(item =>item.ArtileId==ArticleId).First() ==null) throw new ItemNotFoundException("Only Draft Articles will be deleted");
                return database.DeleteArticle(ArticleId); //only Draft article will be deleted
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "DeleteArticleByArticleId()", exception, ArticleId));

               return false;
            }
        }
        public Article GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                return database.GetArticleByID(ArticleId);
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById()", exception, ArticleId));

                throw exception;
            }

        }

        public IEnumerable<Article> GetLatestArticles()
        {
            try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.CreatedOn);
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLatestArticles()", exception));
                throw exception;
            }
        }

        public IEnumerable<Article> GetTrendingArticles()
        {
            try
            {
                var data = (database.GetLikes().GroupBy(item => item.ArticleId)).OrderByDescending(item => item.Count());

                var ListOfArticleId = (from item in data select item.First().ArticleId).ToList();
                var ListOfArticles = GetArticles().ToList();
                var TrendingArticles = new List<Article>();
                foreach (var Id in ListOfArticleId)
                {
                    TrendingArticles.Add(ListOfArticles.Find(item => item.ArtileId == Id));
                }
                return TrendingArticles;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetTrendingArticles()", exception));
                throw exception;
            }
        }


        public IEnumerable<Article> GetArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                return GetArticles().Where(item=>item.CreatedBy==UserId).ToList();
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById()", exception, UserId));

                throw exception;
            }

        }


        public IEnumerable<Article> GetArticles()
        {

            try
            {
                var ListOfArticles = database.GetArticles();
                return ListOfArticles;
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw exception;
            }


        }


        public IEnumerable<Article> GetArticlesByTitle(string Title)
        {
            Validation.ValidateTitle(Title);
            try
            {
                return GetArticles().Where(article => article.Title.Contains(Title));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle()", exception, Title));

                throw exception;
            }

        }



        public IEnumerable<Article> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentNullException("AuthorName value can't be null");
            try
            {
                return GetArticles().Where(article => article.User.FullName.Contains(AuthorName));
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle()", exception, AuthorName));

                throw exception;
            }

        }

        public IEnumerable<Article> GetArticlesByArticleStatusId(int ArticleStatusID)
        {

            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
                return GetArticles().Where(item => item.ArticleStatusID == ArticleStatusID).ToList();
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "ChangeArticleStatus()", exception), ArticleStatusID);

                throw exception;
            }

        }






        //Article Comment
        public bool CreateComment(ArticleComment comment)
        {
            Validation.ValidateArticleComment(comment);
            try
            {
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateComment()", exception), comment);
                return false;
            }
        }


        public IEnumerable<ArticleComment> GetComments(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                return database.GetComments().Where(comment => comment.ArticleId == ArticleId);

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetComments()", exception, ArticleId));
                throw exception;
            }
        }


        public bool AddLikeToArticle(int ArticleId, int UserId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                if (database.GetLikes().Where(item => item.UserId == UserId && item.ArticleId == ArticleId) != null) throw new Exception("Unable to Add like to same article with same UserID");
                var ArticleLike = new ArticleLike();
                ArticleLike.ArticleId = ArticleId;
                ArticleLike.UserId = UserId;
                return database.AddLike(ArticleLike);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "AddLikeToArticle()", exception, ArticleId, UserId));
                return false;
            }
        }


        public int GetLikesCount(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ArticleLikes = database.GetLikes().Count(item => item.ArticleId == ArticleId);
                return ArticleLikes;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLikesCount()", exception));
                throw;
            }
        }

    }
}