using SmartQA.DTO;
using SmartQA.Helpers;
using SmartQA.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace SmartQA.Controllers
{
    public class HomeController : Controller
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        List<TopicModels> TopicList = new List<TopicModels>();
        List<GenericType> AnswerTypeList = new List<GenericType>();
        List<TestModels> Tests = new List<TestModels>();
        public ActionResult Index()
        {
         
            DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/UsersFiles/"));
            if (!dir.Exists)
            {
                Directory.CreateDirectory(Server.MapPath("~/UsersFiles/"));
            }
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Contact(ContactModels contact)
        {
            contact.Subject = 0;
           if(Request.Form["subject"].ToLower() == "General Customer Service".ToLower())
           {
               contact.Subject = 1;
           }
           if (Request.Form["subject"].ToLower() == "Suggestions".ToLower())
           {
               contact.Subject = 2;
           }
           if (Request.Form["subject"].ToLower() == "Product Support".ToLower())
           {
               contact.Subject = 3;
           }

            DataAccess dbWork = new DataAccess(connectionString);
            dbWork.AddContact(contact);

            return View();
        }

        public ActionResult EditTopic(string TopicName)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            TopicList = dbWork.GetAllTopics();
            ViewBag.Topics = TopicList;
            TopicModels Topic = TopicList.Where(x => x.TopicName == TopicName).FirstOrDefault();
            return View(Topic);
            

        }

        [HttpPost]
        public ActionResult EditTopic(TopicModels topic)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = topic.TopicName + extension;
                topic.PhotoName = fileName;
                var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                file.SaveAs(path);
            }
            dbWork.UpdateTopic(topic);
            return RedirectToAction("Topics");


        }

        public ActionResult Topics()
        {
            DataAccess dbWork = new DataAccess(connectionString);
            ViewBag.Message = "Add topic";
            int PageCount = dbWork.GetTopcCountNR();


            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "TopicName";
            info.SortDirection = "ascending";
            info.PageSize = 8;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(PageCount / info.PageSize))) + 1;
            info.CurrentPageIndex = 1;
            TopicList = dbWork.GetTopics(info.PageSize, info.CurrentPageIndex);
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.SortingPagingInfo = info;
            return View(TopicList);
        }

   
        public ActionResult DeleteTopic(int topicId)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            dbWork.DeleteTopic(topicId);

            return RedirectToAction("Topics");
        }

        [HttpPost]
        public ActionResult Topics(SortingPagingInfo info)
        {
            ViewBag.Message = "Add Topic";
            DataAccess dbWork = new DataAccess(connectionString);
            int offset = ((info.CurrentPageIndex - 1) * info.PageSize) + 1;
            TopicList = dbWork.GetTopics(info.PageSize, offset);

            ViewBag.SortingPagingInfo = info;
            ViewBag.UserId = User.Identity.GetUserId();
            return View(TopicList);

        }


        public ActionResult Recent()
        {
            DataAccess dbWork = new DataAccess(connectionString);
            int totalTestCount = dbWork.GetQuizCount();
            ViewBag.TestCount = totalTestCount;

            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "Tests";
            info.SortDirection = "ascending";
            info.PageSize = 3;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(totalTestCount / info.PageSize))) + 1;
            info.CurrentPageIndex = 1;
            Tests = dbWork.GetTests(info.PageSize);
            ViewBag.SortingPagingInfo = info;
            ViewBag.userId = User.Identity.GetUserId();
            return View(Tests);
        }

        [HttpPost]
        public ActionResult Recent(SortingPagingInfo info)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            int totalTestCount = dbWork.GetQuizCount();
            ViewBag.TestCount = totalTestCount;

            info.PageSize = 3 * info.CurrentPageIndex;
            Tests = dbWork.GetTests(info.PageSize);
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.SortingPagingInfo = info;
            return View(Tests);
        }

        public ActionResult CreateTopic()
        {
            DataAccess dbWork = new DataAccess(connectionString);
            TopicList = dbWork.GetAllTopics();
            ViewBag.Topics = TopicList;
            return View();
        }

        [HttpPost]
        public ActionResult CreateTopic(TopicModels topic)
        {
            HttpPostedFileBase file = Request.Files[0];
            if (file.ContentLength > 0)
            {
                var extension = Path.GetExtension(file.FileName);
                var fileName = topic.TopicName + extension;
                topic.PhotoName = fileName;
                var path = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                file.SaveAs(path);
            }
            else
            {
                topic.PhotoName = "default.jpg";
            }

            DataAccess dbWork = new DataAccess(connectionString);
            dbWork.SaveTopicDB(topic, User.Identity.Name);

            return RedirectToAction("Topics");
        }
        public ActionResult CreateQuiz()
        {
            DataAccess dbWork = new DataAccess(connectionString);
            TopicList = dbWork.GetAllTopics();
            AnswerTypeList = dbWork.GetAnswerType();

            ViewBag.Topics = new SelectList(TopicList, "ID", "TopicName");
            ViewBag.AnswersNumbers = new SelectList(AnswerTypeList, "ID", "Value");
            if (TempData["CurrentQuizZ"] != null)
            {
                TestModels test = TempData["CurrentQuizZ"] as TestModels;
                ViewData["error"] = "Insert a valid Quiz.";

                return View(test);
            }

            return View();
        }

        [HttpPost]
        public ActionResult CreateQuiz(TestModels test)
        {
            string id = User.Identity.Name;
            test.NumberOfAnswerForQuestion += 1;
            for (int i = 0; i < Request.Files.Count; i++)
            {
                HttpPostedFileBase hpf = Request.Files[i] as HttpPostedFileBase;
                if (hpf.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(hpf.FileName);
                    test.FileName = fileName;
                    var extension = Path.GetExtension(hpf.FileName);
                    var onlyFileName = Path.GetFileNameWithoutExtension(hpf.FileName);

                    var newfilename = onlyFileName + "_" + id + "_" + test.TopicID.ToString() + extension;

                    var path = Path.Combine(Server.MapPath("~/TestDocuments/"), newfilename);
                    test.QuizPathOnServer = Path.Combine("~/TestDocuments/", newfilename);
                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/TestDocuments/"));
                    if (!dir.Exists)
                    {
                        Directory.CreateDirectory(Server.MapPath("~/TestDocuments/"));
                    }
                    hpf.SaveAs(path);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult PersonalQuizzes(string UserId)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            int PageCount = dbWork.GetQuizNR(User.Identity.GetUserId());


            SortingPagingInfo info = new SortingPagingInfo();
            info.SortField = "QuizName";
            info.SortDirection = "ascending";
            info.PageSize = 6;
            info.PageCount = Convert.ToInt32(Math.Ceiling((double)(PageCount / info.PageSize))) + 1;
            info.CurrentPageIndex = 1;
            string userId = User.Identity.GetUserId();
            Tests = dbWork.GetTestsByUser(userId, info.PageSize, info.CurrentPageIndex);
            ViewBag.UserId = userId;
            ViewBag.SortingPagingInfo = info;
            return View(Tests);
        }

        [HttpPost]
        public ActionResult PersonalQuizzes(SortingPagingInfo info)
        {
            DataAccess dbWork = new DataAccess(connectionString);
            int offset = ((info.CurrentPageIndex - 1) * info.PageSize) + 1;
            Tests = dbWork.GetTestsByUser(User.Identity.GetUserId(), info.PageSize, offset);

            ViewBag.SortingPagingInfo = info;
            ViewBag.UserId = User.Identity.GetUserId();
            return View(Tests);

        }

        public ActionResult BuildQuiz()
        {
            TestModels newTest = new TestModels();
            newTest.Questions = new List<QuestionModels>();
            QuestionModels question = new QuestionModels();
            question.Answers = new List<AnswerModels>();
            AnswerModels answerDefault1 = new AnswerModels();
            AnswerModels answerDefault2 = new AnswerModels();
            AnswerModels answerDefault3 = new AnswerModels();
            AnswerModels answerDefault4 = new AnswerModels();
            AnswerModels answerDefault5 = new AnswerModels();
            question.Answers.Add(answerDefault1);
            question.Answers.Add(answerDefault2);
            question.Answers.Add(answerDefault3);
            question.Answers.Add(answerDefault4);
            question.Answers.Add(answerDefault5);

            newTest.Questions.Add(question);
            ViewBag.Index = 0;
            return View(newTest);
        }
        [HttpPost]
        public ActionResult BuildQuiz(TestModels test)
        {
            TestModels newTest = new TestModels();
            newTest.Questions = new List<QuestionModels>();
            QuestionModels question = new QuestionModels();
            question.Answers = new List<AnswerModels>();
            AnswerModels answerDefault1 = new AnswerModels();
            AnswerModels answerDefault2 = new AnswerModels();
            AnswerModels answerDefault3 = new AnswerModels();
            AnswerModels answerDefault4 = new AnswerModels();
            AnswerModels answerDefault5 = new AnswerModels();
            question.Answers.Add(answerDefault1);
            question.Answers.Add(answerDefault2);
            question.Answers.Add(answerDefault3);
            question.Answers.Add(answerDefault4);
            question.Answers.Add(answerDefault5);

            newTest.Questions.Add(question);

            return View(newTest);
        }
    }
}