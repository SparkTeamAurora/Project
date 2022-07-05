using System.Data;
using System.Reflection.PortableExecutable;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;
using AspireOverflow.DataAccessLayer;
using AspireOverflow.Models;
using AspireOverflow.CustomExceptions;
using Microsoft.EntityFrameworkCore;

using AspireOverflow.DataAccessLayer.Interfaces;




namespace AspireOverflow.Services
{


    public class QueryService :IQueryService
    {
        private readonly  IQueryRepository database;

        private readonly  ILogger<QueryService> _logger;

        private readonly MailService _mailService;

        public QueryService(ILogger<QueryService> logger, MailService mailService,IQueryRepository _queryRepository)
        {
            _logger = logger;
            _mailService = mailService;
            database =_queryRepository;

        }


        public bool CreateQuery(Query query)
        {
            Validation.ValidateQuery(query);
            try
            {
                query.CreatedOn = DateTime.Now;
                var IsAddedSuccessfully = database.AddQuery(query);
                if (IsAddedSuccessfully) _mailService?.SendEmailAsync(HelperService.QueryMail("Manimaran.0610@gmail.com", query.Title!, "Query Created Successfully"));
                return IsAddedSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateQuery(Query query)", exception, query));
                return false;
            }
        }


        public bool RemoveQueryByQueryId(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsDelete: true);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "RemoveQueryByQueryId(int QueryId)", exception, QueryId));

                return false;
            }

        }


        public bool MarkQueryAsSolved(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                return database.UpdateQuery(QueryId, IsSolved: true);
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", " MarkQueryAsSolved(int QueryId)", exception, QueryId));

                return false;
            }
        }

        public Object GetQuery(int QueryID)
        {
            if (QueryID <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryID:{QueryID}");
            try
            {
                var Query = database.GetQueryByID(QueryID);
                return new
                {
                    QueryId = Query.QueryId,
                    Title = Query.Title,
                    Content = Query.Content,
                    code = Query.code,
                    Date = Query.CreatedOn,
                    RaiserName = Query.User?.FullName,
                    IsSolved=Query.IsSolved,
                    Comments = GetComments(QueryID)

                };
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQuery(int QueryId)", exception, QueryID));
                throw;
            }

        }


        private IEnumerable<Query> GetQueries()
        {
            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries()", exception));
                throw;
            }

        }

        public IEnumerable<Object> GetListOfQueries()
        {
            try
            {
                var ListOfQueries = database.GetQueries();
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                    IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries()", exception));
                throw;
            }

        }



        public IEnumerable<Object> GetLatestQueries()
        {
            try
            {
                var ListOfQueries = GetQueries().OrderByDescending(query => query.CreatedOn);

                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetLatestQueries()", exception));
                throw;
            }
        }



        public IEnumerable<Object> GetTrendingQueries()
        {
            try
            {

                var  ListOfComments= (database.GetComments().GroupBy(item => item.QueryId)).OrderByDescending(item => item.Count());

                var ListOfQueryId = (from queryComment in ListOfComments select queryComment.First().QueryId).ToList();
                var ListOfQueries = GetQueries().Where(item => !item.IsSolved).ToList();
                List<Query> TrendingQueries = new List<Query>();
                foreach (var Id in ListOfQueryId)
                {
                    var Query =ListOfQueries.Find(item => item.QueryId == Id);
                    if(Query!= null)TrendingQueries.Add(Query);
                }
               return (from Query in TrendingQueries select Query).Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                    IsSolved=item.IsSolved
                });
               
               
            }

            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetTrendingQueries()", exception));
                throw;
            }
        }



        public IEnumerable<Object> GetQueriesByUserId(int UserId)
        {
            if (UserId <= 0) throw new ArgumentException($"User Id must be greater than 0 where UserId:{UserId}");
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.CreatedBy == UserId);

                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByUserId(int UserId)", exception, UserId));
                throw;
            }

        }


        public IEnumerable<Object> GetQueriesByTitle(String Title)
        {
            if (String.IsNullOrEmpty(Title)) throw new ArgumentException(" Title value can't be null");
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.Title!.Contains(Title));
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueriesByTitle(String Title)", exception, Title));
                throw;
            }
        }


        public IEnumerable<Object> GetQueries(bool IsSolved)
        {
            try
            {
                var ListOfQueries = GetQueries().Where(query => query.IsSolved == IsSolved);
                return ListOfQueries.Select(item => new
                {
                    QueryId = item.QueryId,
                    Title = item.Title,
                    content = item.Content,
                      IsSolved=item.IsSolved
                });
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetQueries(bool IsSolved)", exception, IsSolved));
                throw;
            }
        }


        public bool CreateComment(QueryComment comment)
        {
            Validation.ValidateComment(comment);
            try
            {
                comment.CreatedOn=DateTime.Now;
                 comment.Datetime=DateTime.Now;
                return database.AddComment(comment);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "CreateComment(QueryComment comment)", exception, comment));
                return false;
            }
        }

        public IEnumerable<Object> GetComments(int QueryId)
        {
            if (QueryId <= 0) throw new ArgumentException($"Query Id must be greater than 0 where QueryId:{QueryId}");
            try
            {
                var ListOfComments = database.GetComments().Where(comment => comment.QueryId == QueryId);
                return ListOfComments.Select(item => new
                {
                    CommentId = item.QueryCommentId,
                    Message = item.Comment,
                    Name = item.User?.FullName,
                    QueryId = item.QueryId
                });

            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetComments(int QueryId)", exception, QueryId));
                throw;
            }

        }



 public bool ChangeSpamStatus(int QueryId, int VerifyStatusID)
        {
            if (QueryId <= 0) throw new ArgumentException($"QueryId  must be greater than 0  where QueryId:{QueryId}");
            if(VerifyStatusID <= 0 || VerifyStatusID > 3)throw new ArgumentException($"VerifyStatusId must be greater than 0  and less than 3 where VerifyStatusID:{VerifyStatusID}");
            try
            {
                var IsChangeSuccessfully = database.UpdateSpam(QueryId,  VerifyStatusID);
                if (IsChangeSuccessfully) _mailService?.SendEmailAsync(HelperService.SpamMail("Manimaran.0610@gmail.com","Title", "Hello" , 2));
                return IsChangeSuccessfully;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage($"QueryService", "ChangeSpamStatus(int QueryId, int VerifyStatusID)", exception, QueryId,VerifyStatusID));
                throw;
            }
        }


        public bool AddSpam(Spam spam)
        {
         Validation.ValidateSpam(spam);
            try
            {
                return database.AddSpam(spam);
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "AddSpam(int QueryId, int UserId)", exception, spam));
                return false;
            }
        }


        public IEnumerable<object> GetSpams(int VerifyStatusID)
        {
            if(VerifyStatusID <=0 || VerifyStatusID > 3) throw new ArgumentException("Verfiystatus Id must be greater than 0 and less than or eeeeeeequal to 4");
            try
            {
                var Spams = database.GetSpams().Where(item =>item.VerifyStatusID==VerifyStatusID).GroupBy(item => item.QueryId).OrderByDescending(item=>item.Count()).Select(item =>new{
                ListOfSpams =item.Select(spam =>new {
                    Name =spam.User?.FullName,
                    Reason=spam.Reason
                }),
                Query=new {
                    QueryId=item.First().QueryId,
                    Content=item.First().Query?.Content,
                    Title=item.First().Query?.Title
                }});
                return Spams;
            }
            catch (Exception exception)
            {
                _logger.LogError(HelperService.LoggerMessage("QueryService", "GetSpams()", exception));
                throw;
            }
        }
    }
}
