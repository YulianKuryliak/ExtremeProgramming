using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using wcf_chat;
using Moq;
using MySql.Data.MySqlClient;
using System.Data;

namespace Backend.Tests
{
    [TestClass]
    public class ServiceChatTests
    {

        [TestMethod]
        public void CheckAuthorizationWithNotNullDataFromDatabase()
        {
            string login = "login";
            string password = "password12341231";
            string name = "name";
            int expID = 100;

            var mock = new Mock<I_InteractionDB>();
            mock.Setup(a => a.CheckAutorization(login, password)).Returns(new ServerUser() { ID = expID, Name = name });
            ServiceChat serviceChat = new ServiceChat(mock.Object);

            int rez = serviceChat.UserAuthorization(login, password);

            Assert.AreEqual(rez, expID, "Function of avthorization returned value {0} but not {1}", rez, expID);
        }

        [TestMethod]
        public void IntegrationCheckAuthorization()
        {
            string login = "testLogin";
            string password = "testPass";
            int expID = 1;
            ServiceChat serviceChat = new ServiceChat();


            int rez = serviceChat.UserAuthorization(login, password);

            Assert.AreEqual(rez, expID, "Function of avthorization returned value {0} but not {1}", rez, expID);
        }

        /* [TestMethod]
         public void CheckAddToUserList()
         {
             string login = "testLogin";
             string password = "testPass";
             string name = "name";
             int expID = 1;

             var mock = new Mock<ServiceChat>();
             mock.Setup(a => a.CheckAuto).Returns(new ServerUser() { ID = expID, Name = name });

             ServiceChat serviceChat = new ServiceChat(mock.Object);
             serviceChat.UserAuthorization(login, password);


             int rez = serviceChat.UserAuthorization(login, password);


             int rez = serviceChat.UserAuthorization(login, password);

             Assert.AreEqual(rez, expID, "Function of avthorization returned value {0} but not {1}", rez, expID);
         }
         */

        /*[TestMethod]
        public void TestUsingMsgCallback()
        {
            string login = "login";
            string password = "password12341231";
            string name = "name";
            int expID = 100;

            var mock = new Mock<IServerChatCallback>();
            ServiceChat serviceChat = new ServiceChat(mock.Object);


            int rez = serviceChat.UserAuthorization(login, password);

            Assert.AreEqual(rez, expID, "Function of avthorization returned value {0} but not {1}", rez, expID);
        }
        */

        [TestMethod]
        public void IntegrationCheckAddToDatabase()
        {
            string table = "users";
            string login = "login122311";
            string password = "password123412311211";
            string name = "name1231112";
            int expID = 100;

            //var mock = new Mock<InteractionDB>();
            string connectionStr = "server = localhost;user = root; database = test_pm_database; password = root";
            //string connectionStr = "server=localhost;user=root;database=PM_DB;password=root";
            MySqlConnection connection = new MySqlConnection(connectionStr);
            connection.Close();
            connection.Open();
            
            //string commandStr = "create table users ( id INT, login   varchar(50), username varchar(30), pass    varchar(100)); ";
            //MySqlCommand command = new MySqlCommand("drop table users", connection);
            //MySqlCommand command = new MySqlCommand(commandStr, connection);

            InteractionDB interactionDB = new InteractionDB(connection);

            bool rez = interactionDB.AddToDB(table, "'" + login + "','" + name + "','" + password + "'");
            
            connection.Open();
            MySqlCommand command = new MySqlCommand("Select * from " + table + " where login = '" + login + "' and pass = '" + password + "'", connection);
            MySqlDataReader reader = command.ExecuteReader();

            Assert.IsTrue(rez, "Function AddToDB returned value 'false'");
            Assert.IsTrue(reader.Read(), "User exist in table ");
            connection.Close();
        }

        [TestMethod]
        public void IntegrationCheckGetUserByID()
        {
            string table = "users";
            string login = "login1241";
            string password = "password111";
            string name = "name112";
            int expID = 100;

            string connectionStr = "server = localhost;user = root; database = test_pm_database; password = root";
            MySqlConnection connection = new MySqlConnection(connectionStr);
            connection.Close();
            connection.Open();

            string commandStr = "insert into users(id, login, pass, username) values (" + expID.ToString() + ",'"  + login + "','" + password + "','" + name + "'";

            InteractionDB interactionDB = new InteractionDB(connection);

            int rez = interactionDB.GetIDByUsername(name);

            Assert.AreEqual(rez, expID, "expID is {0} but no {1}", expID, rez);
            connection.Close();
        }
        //написати тести такого самого типу
    }
}
