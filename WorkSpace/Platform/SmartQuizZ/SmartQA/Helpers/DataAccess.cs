﻿using SmartQA.DTO;
using SmartQA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SmartQA.Helpers
{
    public class DataAccess
    {
        private string _connectionString;
        public DataAccess(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<TopicModels> GetTopics(int pageCount, int offset)
        {
            List<TopicModels> Topics = new List<TopicModels>();
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(getTopicByOffsetNr, connection, transaction))
                            {
                                command.Parameters.Add("@PageCount", SqlDbType.Int).Value = pageCount;
                                command.Parameters.Add("@Offset", SqlDbType.Int).Value = offset;

                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        TopicModels topic = new TopicModels();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            topic.ID = Convert.ToInt32(reader["ID"]);
                                            topic.TopicName = reader["TopicName"].ToString();
                                            topic.Description = reader["Description"].ToString();
                                            topic.AddedBy = reader["AddedBy"].ToString();
                                            DateTime date;
                                            DateTime.TryParse(reader["AddedTime"].ToString(), out date);
                                            topic.AddedTime = date;
                                            topic.NrOfQuestions = Convert.ToInt32(reader["NrOfQuestions"]);
                                            topic.NrOfArticles = Convert.ToInt32(reader["NrOfArticles"]);
                                            topic.PhotoName = reader["PhotoName"].ToString();
                                            topic.QuizNumber = Convert.ToInt32(reader["QuizNumber"]);
                                        }
                                        Topics.Add(topic);
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }

            return Topics;
        }

        public List<TopicModels> GetAllTopics()
        {
            List<TopicModels> Topics = new List<TopicModels>();
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(getTopicsQuery, connection, transaction))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        TopicModels topic = new TopicModels();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            topic.ID = Convert.ToInt32(reader["ID"]);
                                            topic.TopicName = reader["TopicName"].ToString();
                                            topic.Description = reader["Description"].ToString();
                                            topic.AddedBy = reader["AddedBy"].ToString();
                                            DateTime date;
                                            DateTime.TryParse(reader["AddedTime"].ToString(), out date);
                                            topic.AddedTime = date;
                                            topic.NrOfQuestions = Convert.ToInt32(reader["NrOfQuestions"]);
                                            topic.NrOfArticles = Convert.ToInt32(reader["NrOfArticles"]);
                                            topic.PhotoName = reader["PhotoName"].ToString();
                                            topic.QuizNumber = Convert.ToInt32(reader["QuizNumber"]);
                                        }
                                        Topics.Add(topic);
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }

            return Topics;
        }
        public List<TestModels> GetTestsByUser(string userId, int pageCount, int offset)
        {
            List<TestModels> Tests = new List<TestModels>();
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            string sql;
                            sql = string.Format(getTestByUser, pageCount, offset, userId);
                            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    
                                    while (reader.Read())
                                    {
                                        TestModels test = new TestModels();
                                        test.AddedByID = userId;
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            test.ID = Convert.ToInt32(reader["ID"]);
                                            test.Title = reader["Title"].ToString();
                                            test.TopicID = Convert.ToInt32(reader["TopicID"]);
                                            DateTime date;
                                            DateTime.TryParse(reader["AddedTime"].ToString(), out date);
                                            test.AddedTime = date;
                                            test.QuestionsNumber = Convert.ToInt32(reader["QuestionsNumber"]);
                                            test.TopicName = reader["TopicName"].ToString();
                                            test.Solved = Convert.ToBoolean(reader["Solved"]);
                                        }
                                        Tests.Add(test);
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }

            return Tests;
        }
        public List<TestModels> GetTests(int pag)
        {
            List<TestModels> Tests = new List<TestModels>();
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            string sql;
                            sql = string.Format(getTests, pag);
                            using (SqlCommand command = new SqlCommand(sql, connection, transaction))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {

                                    while (reader.Read())
                                    {
                                        TestModels test = new TestModels();
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            test.ID = Convert.ToInt32(reader["ID"]);
                                            test.Title = reader["Title"].ToString();
                                            test.TopicID = Convert.ToInt32(reader["TopicID"]);
                                            DateTime date;
                                            DateTime.TryParse(reader["AddedTime"].ToString(), out date);
                                            test.AddedTime = date;
                                            test.QuestionsNumber = Convert.ToInt32(reader["QuestionsNumber"]);
                                            test.TopicName = reader["TopicName"].ToString();
                                            test.Solved = Convert.ToBoolean(reader["Solved"]);
                                            test.PathTopicPicture = reader["PhotoName"].ToString();
                                            test.UserName = reader["Username"].ToString();
                                            test.AddedByID = reader["AddedByID"].ToString();
                                        }
                                        Tests.Add(test);
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }

            return Tests;
        }
        public int GetTopcCountNR()
        {
            int countNr = 0;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(getCountNrTopic, connection, transaction))
                            {
                                countNr = (Int32)command.ExecuteScalar();
                            }
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
            return countNr;
        }
        public int GetQuizNR(string UserId)
        {
            int countNr = 0;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(getTestNrByUser, connection, transaction))
                            {
                                command.Parameters.Add("@UserId", SqlDbType.NVarChar).Value = UserId;
                                countNr = (Int32)command.ExecuteScalar();
                            }
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
            return countNr;
        }
        public int GetQuizCount()
        {
            int countNr = 0;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(getTestsCount, connection, transaction))
                            {
                                countNr = (Int32)command.ExecuteScalar();
                            }
                        }

                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
            return countNr;
        }
        public int SaveQuizDB(TestModels test, string UserName)
        {
            test.ID = GetMAXIdForTable("Test");
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            test.AddedTime = DateTime.Now;
            test.AddedByID = GetUserId(UserName);
            if (test.QuizInstructions == null)
            {
                test.QuizInstructions = "Without Instactions";
            }
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(insertTest, connection, transaction))
                            {

                                command.Parameters.Add("@ID", SqlDbType.Int).Value = test.ID;
                                command.Parameters.Add("@Title", SqlDbType.NVarChar, 256).Value = test.Title;
                                command.Parameters.Add("@FileName", SqlDbType.NVarChar, 256).Value = test.FileName;
                                command.Parameters.Add("@TopicID", SqlDbType.Int).Value = test.TopicID;
                                command.Parameters.Add("@AddedById", SqlDbType.NVarChar, 128).Value = test.AddedByID;
                                command.Parameters.Add("@QuestionsNumber", SqlDbType.Int).Value = test.QuestionsNumber;
                                command.Parameters.Add("@NumberOfAnswerForQuestion", SqlDbType.Int).Value = test.NumberOfAnswerForQuestion;
                                command.Parameters.Add("@MultipleAnswersForOneQuestion", SqlDbType.Bit).Value = (test.MultipleAnswersForOneQuestion == true) ? 1 : 0;
                                command.Parameters.Add("@Description", SqlDbType.NVarChar, 1000).Value = test.Description;
                                command.Parameters.Add("@QuizInstructions", SqlDbType.NVarChar, 3000).Value = test.QuizInstructions;
                                command.Parameters.Add("@QuizPathOnServer", SqlDbType.NVarChar, 500).Value = test.QuizPathOnServer;
                                command.Parameters.Add("@StartReadAtPage", SqlDbType.Int).Value = test.StartReadAtPage;
                                command.Parameters.Add("@StopReadAtPage", SqlDbType.Int).Value = test.StopReadAtPage;
                                command.Parameters.Add("@XmlBeforeProcess", SqlDbType.NVarChar, 3000).Value = test.XmlBeforeProcess;
                                command.Parameters.Add("@Query", SqlDbType.NVarChar, 500).Value = test.Query;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                    UpdateTopicWhenQuizInserted(test);
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
            return test.ID;
        }

        public void SaveTopicDB(TopicModels topic, string UserName)
        {
            topic.ID = GetMAXIdForTable("Topic");
            topic.AddedBy = GetUserId(UserName);
            if (string.IsNullOrEmpty(topic.Description))
            {
                topic.Description = "No desciption for this topic";
            }
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(insertTopic, connection, transaction))
                            {
                                command.Parameters.Add("@ID", SqlDbType.Int).Value = topic.ID;
                                command.Parameters.Add("@TopicName", SqlDbType.NVarChar, 256).Value = topic.TopicName;
                                command.Parameters.Add("@PhotoName", SqlDbType.NVarChar, 256).Value = topic.PhotoName;
                                command.Parameters.Add("@Description", SqlDbType.NVarChar, 4000).Value = topic.Description;
                                command.Parameters.Add("@AddedBy", SqlDbType.NVarChar, 128).Value = topic.AddedBy;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }
        private string GetUserId(string UserName)
        {
            string UserId = String.Empty;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(getUserIdForCurrentUser, connection, transaction))
                            {
                                command.Parameters.Add(new SqlParameter("UserName", UserName));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {

                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            UserId = reader["ID"].ToString();
                                        }

                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return UserId;
        }

        private int GetMAXIdForTable(string tableName)
        {
            int maxIdFromDb = -1;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            getMAxIdFromTable = String.Format(getMAxIdFromTable, tableName);
                            using (SqlCommand command = new SqlCommand(getMAxIdFromTable, connection, transaction))
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            maxIdFromDb = Convert.ToInt32(reader["ID"]);
                                        }
                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }

            if (maxIdFromDb == -1)
            {
                return 1;
            }
            else
            {
                return maxIdFromDb + 1;
            }
        }

        private void UpdateTopicWhenQuizInserted(TestModels test)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(UpdateTopicTableWhenInsertQuiz, connection, transaction))
                            {
                                command.Parameters.Add("@NumberOfQuizesForTopic", SqlDbType.Int).Value = 1;
                                command.Parameters.Add("@NrOfQuestionsForTopic", SqlDbType.Int).Value = test.QuestionsNumber;
                                command.Parameters.Add("@TopicID", SqlDbType.Int).Value = test.TopicID;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }

        public void UpdateTopic(TopicModels topic)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(updateTopic, connection, transaction))
                            {
                                command.Parameters.Add("@TopicName", SqlDbType.NVarChar).Value = topic.TopicName;
                                command.Parameters.Add("@Description", SqlDbType.NVarChar).Value = topic.Description;
                                command.Parameters.Add("@PhotoName", SqlDbType.NVarChar).Value = topic.PhotoName;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }

        private void WriteErrorToDb(string ErrorMessage)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(writeErrorMessageTODB, connection, transaction))
                            {
                                command.Parameters.Add("@NumberOfQuizesForTopic", SqlDbType.NVarChar, 4000).Value = ErrorMessage;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        public List<GenericType> GetAnswerType()
        {
            List<GenericType> AnswerTypeList = new List<GenericType>();
            GenericType one = new GenericType();
            one.ID = 1; one.Value = "2 Answers (a, b)";

            GenericType two = new GenericType();
            two.ID = 2; two.Value = "3 Answers (a, b, c)";

            GenericType three = new GenericType();
            three.ID = 3; three.Value = "4 Answers (a, b, c, d)";

            GenericType four = new GenericType();
            four.ID = 4; four.Value = "5 Answers (a, b, c, d, e)";

            GenericType five = new GenericType();
            five.ID = 5; five.Value = "More than 5 answers";
            AnswerTypeList.Add(one);
            AnswerTypeList.Add(two);
            AnswerTypeList.Add(three);
            AnswerTypeList.Add(four);
            AnswerTypeList.Add(five);

            return AnswerTypeList;

        }

        public List<GenericType> GetTimeZoneList()
        {
            List<GenericType> TimeZoneList = new List<GenericType>();
            GenericType first = new GenericType()
            {
                ID = 1,
                Value = "(GMT-10:00) Hawaii"
            };

            GenericType second = new GenericType()
            {
                ID = 2,
                Value = "(GMT-09:00) Alaska"
            };
            GenericType third = new GenericType()
            {
                ID = 3,
                Value = "(GMT-08:00) Pacific Time (US &amp; Canada)"
            };
            GenericType four = new GenericType()
            {
                ID = 4,
                Value = "(GMT-07:00) Arizona"
            };
            GenericType five = new GenericType()
            {
                ID = 5,
                Value = "(GMT-07:00) Mountain Time (US &amp; Canada)"
            };
            GenericType six = new GenericType()
            {
                ID = 6,
                Value = "(GMT-06:00) Central Time (US &amp; Canada)"
            };
            GenericType seven = new GenericType()
            {
                ID = 7,
                Value = "(GMT-05:00) Eastern Time (US &amp; Canada)"
            };
            GenericType eight = new GenericType()
            {
                ID = 8,
                Value = "(GMT-05:00) Indiana (East)"
            };
            GenericType nine = new GenericType()
            {
                ID = 9,
                Value = "Central European Time (CET) UTC+1"
            };
            TimeZoneList.Add(first);
            TimeZoneList.Add(second);
            TimeZoneList.Add(third);
            TimeZoneList.Add(four);
            TimeZoneList.Add(five);
            TimeZoneList.Add(six);
            TimeZoneList.Add(seven);
            TimeZoneList.Add(eight);
            TimeZoneList.Add(nine);

            return TimeZoneList;

        }
        public string GetUserName(string Email)
        {
            string UserName = string.Empty;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(getUserNameByEmailAddres, connection, transaction))
                            {
                                command.Parameters.Add(new SqlParameter("Email", Email));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {

                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            UserName = reader["UserName"].ToString();
                                        }

                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return UserName;

        }

        public void DeleteTopic(int topicId)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(deleteTopic, connection, transaction))
                            {
                                command.Parameters.Add("@TopicId", SqlDbType.Int).Value = topicId;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }

        public void AddContact(ContactModels contact)
        {
            contact.ID = GetMAXIdForTable("Contact");
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(insertContact, connection, transaction))
                            {
                                command.Parameters.Add("@ID", SqlDbType.Int).Value = contact.ID;
                                command.Parameters.Add("@Name", SqlDbType.NVarChar, 256).Value = contact.Name;
                                command.Parameters.Add("@Subject", SqlDbType.Int).Value = contact.Subject;
                                command.Parameters.Add("@Email", SqlDbType.NVarChar, 256).Value = contact.Email;
                                command.Parameters.Add("@Message", SqlDbType.NVarChar, 4000).Value = contact.Message;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }

        public void AddUserCopy(UserViewModels userCopy)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();
                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(inserUserCopy, connection, transaction))
                            {
                                command.Parameters.Add("@ID", SqlDbType.NVarChar, 128).Value = userCopy.ID;
                                command.Parameters.Add("@Username", SqlDbType.NVarChar, 256).Value = userCopy.Username;
                                command.Parameters.Add("@Email", SqlDbType.NVarChar, 256).Value = userCopy.Email;
                                command.Parameters.Add("@Phone", SqlDbType.NVarChar, 500).Value = string.Empty;
                                command.Parameters.Add("@Pictures", SqlDbType.NVarChar, 500).Value = string.Empty;
                                command.Parameters.Add("@Company", SqlDbType.NVarChar, 256).Value = string.Empty;
                                command.Parameters.Add("@TimeZone", SqlDbType.Int).Value = 1;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }

                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }
        public UserViewModels SelectUser(string userId)
        {
            UserViewModels userCopy = new UserViewModels();
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {

                            using (SqlCommand command = new SqlCommand(selectUser, connection, transaction))
                            {
                                command.Parameters.Add(new SqlParameter("UserId", userId));
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {

                                        for (int i = 0; i < reader.FieldCount; i++)
                                        {
                                            userCopy.ID = reader["ID"].ToString();
                                            userCopy.Username = reader["Username"].ToString();
                                            userCopy.Email = reader["Email"].ToString();
                                            userCopy.Phone = reader["Phone"].ToString();
                                            userCopy.Picture = reader["Pictures"].ToString();
                                            userCopy.Company = reader["Company"].ToString();
                                            userCopy.TimeZone = Convert.ToInt32(reader["TimeZone"]);
                                        }

                                    }
                                }
                            }
                        }
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {

                }
            }
            return userCopy;  
        }

        public void  UpdateUser(UserViewModels user)
        {
            int rows = 0;
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(updateUsers, connection, transaction))
                            {
                                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value =(!string.IsNullOrEmpty(user.Phone))?user.Phone : string.Empty;
                                command.Parameters.Add("@Pictures", SqlDbType.NVarChar).Value = (!string.IsNullOrEmpty(user.Picture))? user.Picture : string.Empty;
                                command.Parameters.Add("@Company", SqlDbType.NVarChar).Value = (!string.IsNullOrEmpty(user.Company))? user.Company : string.Empty;
                                command.Parameters.Add("@TimeZone", SqlDbType.Int).Value = user.TimeZone;
                                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = user.ID;
                                rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
            if(rows != 0)
            {
                UpdateUserASP(user);
            }
        }
        private void UpdateUserASP(UserViewModels user)
        {
            if (_connectionString != string.Empty)
            {
                try
                {
                    using (SqlConnection connection = new SqlConnection(_connectionString))
                    {
                        connection.Open();

                        using (SqlTransaction transaction = connection.BeginTransaction())
                        {
                            using (SqlCommand command = new SqlCommand(updateAspUser, connection, transaction))
                            {
                                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = user.Email;
                                command.Parameters.Add("@Username", SqlDbType.NVarChar).Value = user.Username;
                                command.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = (!string.IsNullOrEmpty(user.Phone))? user.Phone : string.Empty;
                                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = user.ID;
                                int rows = command.ExecuteNonQuery();
                                transaction.Commit();
                            }
                        }
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    WriteErrorToDb(ex.Message);
                }
            }
        }

        private static string getCountNrTopic = @"Select Count(*) From Topic";
        private static string getTopicByOffsetNr = @"Select Top(@PageCount) *  From Topic Where ID >= @Offset";
        private static string getTopicsQuery = @" Select * from Topic ORDER BY TopicName";
        private static string getMAxIdFromTable = @"Select Max(ID) AS ID from {0}";
        private static string getUserIdForCurrentUser = @"Select ID AS ID From AspNetUsers Where UserName= @UserName";
        private static string insertTest = @"Insert INTO Test 
                                                       (ID, 
                                                        Title, 
                                                        [FileName], 
                                                        TopicID, 
                                                        AddedByID, 
                                                        AddedTime, 
                                                        QuestionsNumber,    
                                                        NumberOfAnswerForQuestion,
                                                        MultipleAnswersForOneQuestion, 
                                                        [Description],
                                                        QuizInstructions,
                                                        QuizPathOnServer,
                                                        StartReadAtPage,
                                                        StopReadAtPage,
                                                        XmlBeforeProcess,
                                                        Query,
                                                        Solved)                                   
                                                Values (@ID,
                                                        @Title,
                                                        @FileName,
                                                        @TopicID, 
                                                        @AddedById,
                                                        GETDATE(), 
                                                        @QuestionsNumber,
                                                        @NumberOfAnswerForQuestion, 
                                                        @MultipleAnswersForOneQuestion, 
                                                        @Description, 
                                                        @QuizInstructions,
                                                        @QuizPathOnServer,
                                                        @StartReadAtPage,
                                                        @StopReadAtPage,
                                                        @XmlBeforeProcess,
                                                        @Query,
                                                        0
                                                        )";

        private static  string UpdateNrArticlesFromTopic = @"Update Topic
                                                             Set NrOfArticles = [NrOfArticles] + @NrArticles
                                                             Where ID = @TopicID";
        private static string UpdateTopicTableWhenInsertQuiz = @"Update Topic 
                                                                   Set QuizNumber = [QuizNumber] + @NumberOfQuizesForTopic, 
                                                                       NrOfQuestions = [NrOfQuestions] + @NrOfQuestionsForTopic
                                                                 Where ID = @TopicID";
        private static string insertTopic = @" INSERT INTO Topic 
                                                   (ID,
                                                    TopicName,
                                                    Description,
                                                    AddedBy, 
                                                    AddedTime, 
                                                    NrOfQuestions,
                                                    NrOfArticles,
                                                    PhotoName,
                                                    QuizNumber)
                                            VALUES
                                                   (@ID,
                                                    @TopicName,
                                                    @Description,
                                                    @AddedBy,
                                                    GETDATE(),
                                                    0,
                                                    0,
                                                    @PhotoName,
                                                    0)";
        private static string insertContact = @" INSERT INTO Contact 
                                                   (ID,
                                                    Name,
                                                    Subject,
                                                    AddedTime, 
                                                    Email,
                                                    Message
                                                    )
                                            VALUES
                                                   (@ID,
                                                    @Name,
                                                    @Subject,
                                                    GETDATE(),
                                                    @Email,
                                                    @Message
                                                    )";
        private static string inserUserCopy = @"INSERT INTO Users
                                                   (ID,
                                                    Email,
                                                    Username,
                                                    Phone, 
                                                    Pictures,
                                                    Company,
                                                    TimeZone
                                                    )
                                            VALUES
                                                   (@ID,
                                                    @Email,
                                                    @Username,
                                                    @Phone,
                                                    @Pictures,
                                                    @Company,
                                                    @TimeZone
                                                    )";


        private string writeErrorMessageTODB = @"INSERT INTO Logger ([ErrorMessage] , [DateTime] ) VALUES (@ErrorMessage , GETDATE())";

        private static string getUserNameByEmailAddres = @"Select UserName From AspNetUsers Where Email = @Email";
        private static string updateTopic = @"Update Topic Set TopicName = @TopicName, Description = @Description, PhotoName = @PhotoName Where TopicName = @TopicName ";

        private static string deleteTopic = @"DELETE FROM Topic WHERE ID = @TopicId";
        private static string selectUser = @"Select ID, Email, Username, Phone, Pictures, Company, TimeZone FROM Users WHERE ID = @UserId";
        private static string updateUsers = @"Update Users SET Email = @Email, Username = @Username,  Phone = @Phone, Pictures = @Pictures,Company = @Company,  TimeZone = @TimeZone WHERE ID = @ID ";
        private static string updateAspUser = @"Update AspNetUsers SET Email = @Email, PhoneNumber = @Phone, UserName = @Username WHERE Id = @ID";
        private static string getTestNrByUser = @"Select Count(*) FROM Test WHERE AddedByID = @UserId";
        private static string getTestByUser = @"Select Top( {0} )  ID,Title,TopicID,AddedTime,QuestionsNumber, (Select TopicName From Topic WHERE ID = TopicID) AS TopicName, Solved From Test Where ID >= {1} AND AddedByID = '{2}'";

        private static string getTestsCount = @"Select Count('') From Test";
        private static string getTests = @"Select Top( {0} )  ID,Title,TopicID,AddedTime,QuestionsNumber, (Select TopicName From Topic WHERE ID = TopicID) AS TopicName, Solved, (Select PhotoName From Topic Where ID = TopicID) AS PhotoName, (Select UserName from AspNetUsers Where Id = AddedByID) AS Username, AddedByID From Test Order By AddedTime DESC";



       
    }
}