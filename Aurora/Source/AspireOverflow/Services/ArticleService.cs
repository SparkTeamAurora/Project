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
                article.Image=System.Convert.FromBase64String(article.ImageString);
                article.CreatedOn = DateTime.Now;

                return database.AddArticle(article);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "CreateArticle(Article article)", exception, article));
                return false;
            }
        }



        public bool UpdateArticle(Article article, int CurrentUser)
        {

            if (!Validation.ValidateArticle(article)) throw new ValidationException("Given data is InValid");
            try
            {
                var ExistingArticle = GetArticles().Where(Item => Item.ArtileId == article.ArtileId && Item.ArticleStatusID==1).First();
                if (ExistingArticle == null) throw new ItemNotFoundException($"Unable to Find any Article with ArticleId:{article.ArtileId}");
                article.UpdatedOn = DateTime.Now;
                article.UpdatedBy = CurrentUser;
                ExistingArticle = article;

                return database.UpdateArticle(ExistingArticle);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " UpdateArticle(Article article, int CurrentUser)", exception, article));
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
                _logger.LogError(HelperService.LoggerMessage("ArticleService", " ChangeArticleStatus(int ArticleId, int ArticleStatusID, int UpdatedByUserId)", exception), ArticleId, ArticleStatusID);

                throw exception;
            }

        }

        public bool DeleteArticleByArticleId(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {  if(GetArticles().Where(item =>item.ArtileId==ArticleId && item.ArticleStatusID==1).First() ==null) throw new ItemNotFoundException("Only Draft Articles will be deleted");
                return database.DeleteArticle(ArticleId); //only Draft article will be deleted
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "DeleteArticleByArticleId(int ArticleId)", exception, ArticleId));

               return false;
            }
        }
        public object GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var article= database.GetArticleByID(ArticleId);
                return new {
                    articleId=article.ArtileId,
                    PublishedDate =article.Datetime,
                    title=article.Title,
                    AuthorName=article.User.FullName,
                    content=article.Content,
                    image=article.Image,
                    Likes= GetLikesCount(article.ArtileId),
                    comments=GetComments(article.ArtileId) 
                };
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticleById(int ArticleId)", exception, ArticleId));

                throw exception;
            }

        }

        public IEnumerable<object> GetLatestArticles()
        {
            try
            {
                var ListOfArticles = GetArticles().OrderByDescending(article => article.CreatedOn);
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetLatestArticles()", exception));
                throw exception;
            }
        }

        public IEnumerable<Object> GetTrendingArticles()
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
                return TrendingArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetTrendingArticles()", exception));
                throw exception;
            }
        }


        public IEnumerable<object> GetArticlesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfArticles= GetArticles().Where(item=>item.CreatedBy==UserId).ToList();
                  return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
                
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByUserId(int UserId)", exception, UserId));

                throw exception;
            }

        }


        private IEnumerable<Article> GetArticles()
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

        
        public IEnumerable<Object> GetListOfArticles()
        {

            try
            {
                var ListOfArticles = database.GetArticles();
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticles()", exception));
                throw exception;
            }


        }


        public IEnumerable<object> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ValidationException("Article Title cannot be null or empty");
            try
            {
                var ListOfArticles= GetArticles().Where(article => article.Title.Contains(Title));
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByTitle(string Title)", exception, Title));

                throw exception;
            }

        }



        public IEnumerable<object> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) throw new ArgumentNullException("AuthorName value can't be null");
            try
            {
                var ListOfArticles= GetArticles().Where(article => article.User.FullName.Contains(AuthorName));
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByAuthor(string AuthorName)", exception, AuthorName));

                throw exception;
            }

        }

        
        public IEnumerable<object> GetArticlesByReviewerId(int ReviewerId)
        {
            if (ReviewerId <=0 ) throw new ArgumentException($"ReviewerId must be greater than 0 While ReviewerId:{ReviewerId}");
            try
            {
                var ListOfArticles= GetArticles().Where(article => article.ReviewerId==ReviewerId);
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }
            catch (Exception exception)
            {

                _logger.LogError(HelperService.LoggerMessage("ArticleService", " GetArticlesByReviewerId(int ReviewerId)", exception, ReviewerId));

                throw exception;
            }

        }

        public IEnumerable<object> GetArticlesByArticleStatusId(int ArticleStatusID)
        {

            if (ArticleStatusID <= 0 && ArticleStatusID > 4) throw new ArgumentException($"Article Status Id must be between 0 and 4 ArticleStatusID:{ArticleStatusID}");
            try
            {
              
                var ListOfArticles= GetArticles().Where(item => item.ArticleStatusID == ArticleStatusID).ToList();
                  if(ArticleStatusID==2) ListOfArticles.Where(Item =>Item.ArticleStatusID==3);
                return ListOfArticles.Select(Article => new{
                    ArticleId =Article.ArtileId,
                    title=Article.Title,
                      AuthorName=Article.User.FullName,
                    content=Article.Content,
                    image=Article.Image,
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("ArticleService", "GetArticlesByArticleStatusId(int ArticleStatusID)", exception), ArticleStatusID);

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


        public IEnumerable<Object> GetComments(int ArticleId)
        {
            if (ArticleId <= 0) throw new ArgumentException($"Article Id must be greater than 0 where ArticleId:{ArticleId}");
            try
            {
                var ListOfComments= database.GetComments().Where(comment => comment.ArticleId == ArticleId);
                return ListOfComments.Select(Item => new {
                       CommentId = Item.ArticleCommentId,
                    Message = Item.Comment,
                    Name = Item.User.FullName,
                    ArticleId = Item.ArticleId

                });

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
                if (database.GetLikes().ToList().Find(item => item.UserId == UserId && item.ArticleId == ArticleId) != null) throw new Exception("Unable to Add like to same article with same UserID");
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