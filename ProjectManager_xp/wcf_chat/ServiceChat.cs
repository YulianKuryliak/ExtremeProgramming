using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.Text;
using System.Xml.Linq;

namespace wcf_chat
{
  
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {

        List<ServerUser> users = new List<ServerUser>();
        int nextId = 1;
        I_InteractionDB database;

        public ServiceChat()
        {
            database = new InteractionDB();
        }

        public ServiceChat(I_InteractionDB idb)
        {
            database = idb;
            //InteractionDB a = new InteractionDB(idb);
        }

        public int Connect(string name)
        {
            
            ServerUser user = new ServerUser() {
                ID = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            nextId++;
            foreach (var item in users)
            {
                Console.WriteLine(item);
            }
            //SendMsg(": "+user.Name+" подключился к чату!",0);
            users.Add(user);
            return user.ID;
        }

        public void Disconnect(int id)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            if (user!=null)
            {
                users.Remove(user);
                //SendMsg(": "+user.Name + " покинул чат!",0);
            }
        }

        public void SendMsg(string msg, int id, string username)
        {
            var user = users.FirstOrDefault(i => i.ID == id);
            string answer = user.Name + ";" + DateTime.Now.ToShortTimeString() + ", " + user.Name + " : " + msg;
            user = users.FirstOrDefault(i => i.Name == username);
            if (user != null)
            {
                user.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
            SaveMessageToDB(msg, id, username);
            // перевірити на існування юзера

            // записати повідомлення у бд
        }

        private void SaveMessageToDB(string msg, int id, string username)
        {
            int idRecipient = database.GetIDByUsername(username);
            database.AddToDB("messages", "'" + id + "', '" + idRecipient + "', '" + msg + "'");
        }

        public int UserAuthorization(string login, string password)
        {
            ServerUser connectedUser = database.CheckAutorization(login, password);
            if (connectedUser != null)
            {
                ServerUser item = new ServerUser()
                {
                    ID = connectedUser.ID,
                    Name = connectedUser.Name,
                    operationContext = OperationContext.Current
                };
                users.Add(item);

                return connectedUser.ID;
            }
            return -1;
        }

        public bool UserRegistration(string username, string login, string password)
        {
            string table = "users";
            string values = "'" + login + "', '" + username + "', '" + password + "'";
            return database.AddToDB(table, values);
        }

        public List<string> UserContactListUsernames(int id)
        {
            return database.GetUserContactListUsernames(id);
        }

        public List<Project> _GetAllProjects()
        {
            List<Project> p = database.GetAllProjects();
            return p;
        }

        public List<Task> GetTasksInProject(string projectId)
        {
            return database.GetTasksInProject(projectId);
        }

        public bool AddProject(string values)
        {
            string table = "projects";
            //string values = "'" + project.name + "', '" + project.manager + "', '" + project.methodology + "', '" + project.deadline + "'";
            return database.AddToDB(table, values);
        }

        public bool AddTask(string values)
        {
            string table = "tasks";
            //string values = "'" + task.project + "', '" + task.name + "', '" + task.developer + "', '" + task.complexity + "', '" + task.deadline + "'";
            return database.AddToDB(table, values);
        }
    }
}
