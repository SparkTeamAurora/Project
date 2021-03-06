using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using AspireOverflow.Models;
namespace AspireOverflow.Services
{
    public static class Validation
    {
        public static bool ValidateQuery(Query query)
        {
            if (query == null) throw new ValidationException("Query should not be null");
            if (String.IsNullOrEmpty(query.Title)) throw new ValidationException("Title cannot be null or empty");
            if (String.IsNullOrEmpty(query.Content)) throw new ValidationException("content cannot be null or empty");
            if (query.Title.Length > 100) throw new ValidationException("Title length must be less than 100 charcter");
            if (!query.IsActive) throw new ValidationException("IsActive must be true");
            if (query.IsSolved) throw new ValidationException("IsSolved must be false");
            else return true;
        }

        public static bool ValidateQueryComment(QueryComment Comment)
        {
            if (Comment == null) throw new ValidationException("Comment should not be null");
            if (Comment.QueryId <= 0) throw new ValidationException("Query Id  must be greater than 0");
            if (String.IsNullOrEmpty(Comment.Comment)) throw new ValidationException("Comment cannot be null or empty");
            if (Comment.Comment.Length > 500) throw new ValidationException("Comment must be less than 500 characters");
            if (Comment.Code != null && Comment.Code.Length > 1000) throw new ValidationException("Code must be less than 1000 characters");
            else return true;
        }

        public static bool ValidateArticle(Article article)
        {
            if (article == null) throw new ValidationException("Article should not be null");

            if (String.IsNullOrEmpty(article.Title)) throw new ValidationException("Title cannot be null or empty");
            if (String.IsNullOrEmpty(article.Content)) throw new ValidationException("content cannot be null or empty");
            if (article.Title.Length > 100) throw new ValidationException("Title length must be less than 100 charcter");
            if (article.ArticleStatusID <= 0 && article.ArticleStatusID > 2) throw new ValidationException("ArticlestatusID must be less than 2");
            else return true;
        }

        public static bool ValidateArticleComment(ArticleComment Comment)
        {
            if (Comment == null) throw new ValidationException("Comment should not be null");
            if (Comment.ArticleId <= 0) throw new ValidationException("Article Id  must be greater than 0");
            if (String.IsNullOrEmpty(Comment.Comment)) throw new ValidationException("Comment cannot be null or empty");
            if (Comment.Comment.Length > 100) throw new ValidationException("Comment must be less than 100 characters");
            else return true;
        }

        public static bool ValidateUser(User user)
        {
            if (user == null) throw new ValidationException("User should not be null");
            if (user.VerifyStatusID != 3) throw new ValidationException($"VerifyStatus must be 3  VerifyStatusId:{user.VerifyStatusID}");
            if (user.IsReviewer) throw new ValidationException($"IsReviewer must be false");
            if (user.GenderId <= 0 && user.GenderId > 2) throw new ValidationException($"Gender ID must be 1 or 2");
            if (user.UserRoleId != 2) throw new ValidationException($"UserRoleId must be equal to 2 UserRole:{user.UserRoleId}");
            if (!ValidateUserCredentials(user.EmailAddress, user.Password)) return false;
            else return true;
        }
        public static bool ValidateSpam(Spam spam)
        {
            if (spam == null) throw new ValidationException("Spam should not be null");
            if (spam.QueryId <= 0) throw new ValidationException("Query Id must be greater than 0");
            if (spam.UserId <= 0) throw new ValidationException("UserId must be greater than 0");
            if (spam.VerifyStatusID <= 0 || spam.VerifyStatusID > 3) throw new ValidationException("Verify Status Id must be between 0 and 3");
            if (String.IsNullOrEmpty(spam.Reason)) throw new ValidationException("Spam Reason cannot be null or empty");
            else return true;
        }
        public static bool ValidateUserCredentials(String Email, String Password)
        {
            var mail = new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");
            var password = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            if (!mail.IsMatch(Email)) throw new ValidationException($"Email format is incorrect EmailEntered:{Email}");
            if (!password.IsMatch(Password)) throw new ValidationException($"Password must be at least 4 characters, no more than 8 characters, and must include at least one upper case letter, one lower case letter,  one numeric digit and one special character. Password Entered:{Password}");
            else return true;
        }

        public static bool ValidateEmail(String Email)
        {
            var mail = new Regex("^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$");
            if (!mail.IsMatch(Email)) throw new ValidationException($"Email format is incorrect EmailEntered:{Email}");
            else return true;
        }

        public static bool ValidateArticleLike(ArticleLike Like)
        {
            if (Like == null) throw new ValidationException("ArticleLike Object cannot be null");
            if (Like.ArticleId <= 0) throw new ValidationException($"Article Id must be greater than 0 where ArticleId:{Like.ArticleId}");
            if (Like.UserId <= 0) throw new ValidationException($"User Id must be greater than 0 where UserId:{Like.UserId}");
            else return true;

        }

    }
}