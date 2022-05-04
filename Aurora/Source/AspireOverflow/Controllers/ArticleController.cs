using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using AspireOverflow.Models;
using AspireOverflow.Services;
using AspireOverflow.CustomExceptions;
using Microsoft.AspNetCore.Authorization;



namespace AspireOverflow.Controllers;
[ApiController]
[Route("[controller]/[action]")]
public class ArticleController : ControllerBase
{
    internal static ILogger<ArticleController> _logger;
    private static ArticleService _articleService;

     public ArticleController(ILogger<ArticleController> logger, ArticleService articleService)
    {
        _logger = logger ?? throw new NullReferenceException(nameof(logger));
        _articleService = articleService ?? throw new NullReferenceException(nameof(articleService));

    }

    [HttpPost]

        public async Task<ActionResult<Article>> CreateArticle(Article article)
        {
            if (article == null) return BadRequest("Null value is not supported");
            try
            { 

                //Development.Web is Enum constants which indicated the request approaching team.
                return _articleService.CreateArticle(article, DevelopmentTeam.Web) ? await Task.FromResult(Ok("Successfully Created")) : BadRequest($"Error Occured while Adding Article :{HelperService.PropertyList(article)}");
            }
            catch (ValidationException exception)
            {
                //HelperService.LoggerMessage - returns string for logger with detailed info
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateArticle), exception, article));
                return BadRequest($"{exception.Message}\n{HelperService.PropertyList(article)}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateArticle), exception, article));
                return BadRequest($"Error Occured while Adding Article :{HelperService.PropertyList(article)}");
            }
        }


    [HttpPost]
    public async Task<ActionResult<ArticleComment>> CreateComment(ArticleComment comment)
    {

        if (comment == null) return BadRequest("Null value is not supported");
        try
        {
            return _articleService.CreateComment(comment, DevelopmentTeam.Web) ? await Task.FromResult(Ok("Successfully added comment to the Article")) : BadRequest($"Error Occured while Adding Comment :{HelperService.PropertyList(comment)}");
        }  catch (ValidationException exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateComment), exception, comment));
            return BadRequest($"{exception.Message}\n{HelperService.PropertyList(comment)}");
        }

        catch (Exception exception)
        {
            _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(CreateComment), exception, comment));
            return BadRequest($"Error Occured while Adding comment :{HelperService.PropertyList(comment)}");
        }

    }

    [HttpGet]
        public async Task<ActionResult<Article>> GetArticleById(int ArticleId)
        {
            if (ArticleId <= 0) return BadRequest("Article ID must be greater than 0");
            try
            {
                return await Task.FromResult(_articleService.GetArticleById(ArticleId, DevelopmentTeam.Web));
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetArticleById), exception, ArticleId));
                return BadRequest($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetArticleById), exception, ArticleId));
                return BadRequest($"Error Occurred while getting query with QueryId :{ArticleId}");
            }
        }

        [HttpGet][Authorize]
        public async Task<ActionResult<IEnumerator<Article>>> GetAll()
        {
            try
            {
               

                var Articles = _articleService.GetArticles(DevelopmentTeam.Web);    // DevelopmentTeam.Web is a property of enum class
                 var JsonResult = HelperService.GetJsonResult(Articles);
                return await Task.FromResult(Ok(JsonResult));
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetAll), exception));
                return BadRequest("Error occured while processing your request");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerator<Article>>> GetArticleByUserId(int UserId)
        {
            if (UserId <= 0) return BadRequest("UserId must be greater than 0");
            try
            {
                var ListOfArticleByUserId = _articleService.GetArticles(DevelopmentTeam.Web);
                var JsonResult = HelperService.GetJsonResult(ListOfArticleByUserId);
                return await Task.FromResult(Ok(JsonResult));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetArticleByUserId), exception, UserId));
                return BadRequest($"Error Occured while processing your request with UserId in Articles :{UserId}");
            }

        }


        [HttpGet]
        public async Task<ActionResult<IEnumerator<Article>>> GetArticlesByTitle(string Title)
        {
            if (String.IsNullOrEmpty(Title)) return BadRequest("Title can't be null");
            try
            {
                var ListOfArticlesByTitle = _articleService.GetArticlesByTitle(null, DevelopmentTeam.Web);
                 var JsonResult = HelperService.GetJsonResult(ListOfArticlesByTitle);
                return await Task.FromResult(Ok(JsonResult));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(DevelopmentTeam.Web, nameof(GetArticlesByTitle), exception, Title));
                return BadRequest($"Error Occured while processing your request with Title :{Title}");

            }


        }

        [HttpGet]
         public async Task<ActionResult> ChangeArticleStatus(int ArticleId,int ArticleStatusID)
        {
            if (ArticleId <= 0 && ArticleStatusID <=0  && ArticleStatusID > 4 ) return BadRequest("Article ID  and Article Status ID must be greater than 0 and ArticleStatusID must be less than or equal to 4");
            try
            {
                 return _articleService.ChangeArticleStatus(ArticleId,ArticleStatusID, DevelopmentTeam.Web) ? await Task.FromResult(Ok($"Successfully updated the status of the Article :{ArticleId}")) : BadRequest($"Error Occurred while updating the status of the Article:{ArticleId}");
            }
            catch (ItemNotFoundException exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(ChangeArticleStatus), exception, ArticleId,ArticleStatusID));
                return NotFound($"{exception.Message}");
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(ChangeArticleStatus), exception, ArticleId,ArticleStatusID));
                return BadRequest($"Error Occurred while updating the status of the Article :{ArticleId}");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerator<Article>>> GetArticlesByAuthor(string AuthorName)
        {
            if (String.IsNullOrEmpty(AuthorName)) return BadRequest("Author Name can't be null");
            try
            {
                var ListOfArticlesByAuthor = _articleService.GetArticlesByTitle(AuthorName,DevelopmentTeam.Web);
               var JsonResult = HelperService.GetJsonResult(ListOfArticlesByAuthor);
                return await Task.FromResult(Ok(JsonResult));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetArticlesByAuthor), exception, AuthorName));
                return BadRequest($"Error Occured while processing your request with Title :{AuthorName}");

            }


        }

    
    [HttpGet]
        public async Task<ActionResult<IEnumerator<QueryComment>>> GetComments(int ArticleId)
        {
            if (ArticleId <= 0) return BadRequest("ArticleId must be greater than 0");
            try
            {
                var ListOfComments = _articleService.GetComments(ArticleId, DevelopmentTeam.Web);
                var JsonResult = HelperService.GetJsonResult(ListOfComments);
                return await Task.FromResult(Ok(JsonResult));
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage(nameof(ArticleController), nameof(GetComments), exception, ArticleId));
                return BadRequest($"Error Occured while processing your request with ArticleId :{ArticleId}");


            }

        }

}
