using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace wcf_chat
{
    public class InteractionDB : I_InteractionDB
    {
        private string connectionStr = "server=localhost;user=root;database=PM_DB;password=root";
        //string connectionStr = "server = localhost;user = root; database = test_pm_database; password = root";
        private MySqlConnection connection = null;
        private MySqlCommand command;
        private string commandStr = "";
        private bool openingConnection = false;

        public InteractionDB()
        {
            Connect();
        }

        public InteractionDB(string connectionStr)
        {
            this.connectionStr = connectionStr;
            Connect();
        }

        public InteractionDB(MySqlConnection connection)
        {
            this.connection = connection;
            openingConnection = true;
        }

        private void Connect()
        {
            connection = new MySqlConnection(connectionStr);
            connection.Close();
        }

        public InteractionDB(I_InteractionDB idb)
        {
            //connection = ((InteractionDB) idb).connection;
            //connectionStr = ((InteractionDB)idb).connectionStr;
        }

        public bool AddToDB (string table, string values)
        {
            if (!openingConnection)
            {
                connection.Open();
            }
            string toTable = "";
            switch (table)
            {
                case "users" :
                    toTable = "users (login, username, pass)";
                    break;

                case "messages" :
                    toTable = "messages (user_one, user_two, message)";
                    break;

                case "projects":
                    toTable = "projects (p_name, manager, methodology)";
                    break;

                case "tasks":
                    toTable = "tasks (project, t_name, developer, complexity, deadline)";
                    break;
            }
            commandStr = "INSERT INTO " + toTable + " values(" + values + ")";
            command = new MySqlCommand(commandStr, connection);
            Console.WriteLine(commandStr);
            bool check_add = command.ExecuteNonQuery() == 1;
            if (openingConnection)
            {
                connection.Close();
            }
            return check_add;
        }

        public int GetIDByUsername(string username)
        {
            if (!openingConnection)
            {
                connection.Open();
            }   
            openingConnection = true;
            commandStr = ("Select id from users where login = '" + username + "'");
            Console.WriteLine(commandStr);
            command = new MySqlCommand(commandStr, connection);
            MySqlDataReader reader = command.ExecuteReader();
            int id = -1;
            if (reader.Read())
            {
                id =  Convert.ToInt32(reader[0].ToString());

            }
            connection.Close();
            openingConnection = false;
            return id;
        }

        public ServerUser CheckAutorization(string login, string password)
        {
            ServerUser user = null;
            connection.Open();
            openingConnection = true;
            commandStr = ("Select * from users where login = '" + login + "' and pass = '" + password + "'");
            Console.WriteLine(commandStr);
            command = new MySqlCommand(commandStr, connection);
            MySqlDataReader reader = command.ExecuteReader();
            bool read = reader.Read();
            if (read)
            {
                user = new ServerUser();
                Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
                Console.WriteLine(read);
                user.ID = Convert.ToInt32(reader[0].ToString());
                user.Name = reader[2].ToString();
            }
            connection.Close();
            openingConnection = false;
            return user;
        }

        public List<string> GetUserContactListUsernames (int id)
        {
            List<string> list = null;
            return list;
        }

        public List<Project> GetAllProjects()
        {
            connection.Open();
            openingConnection = true;
            commandStr = ("select project_id, p_name, manager, methodology, deadline, p_status, creation_date from projects");
            Console.WriteLine(commandStr);
            command = new MySqlCommand(commandStr, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Project> projects = new List<Project>();
            while (reader.Read())
            {
                Project project = new Project();
                project.id = Convert.ToInt32(reader[0].ToString());
                Console.WriteLine(reader[0].ToString());
                project.name = reader[1].ToString();
                Console.WriteLine(reader[1].ToString());
                project.manager = reader[2].ToString();
                Console.WriteLine(reader[2].ToString());
                project.methodology = reader[3].ToString();
                Console.WriteLine(reader[3].ToString());
                project.deadline = reader[4].ToString();
                project.status = reader[5].ToString();
                project.creation_date = reader[6].ToString();
                projects.Add(project);
                Console.WriteLine(project.id + " " + project.name + " " + project.manager
                    + " " + project.methodology + " " + project.deadline + " " + project.status + " " + project.creation_date);
            }
            connection.Close();
            openingConnection = false;
            return projects;
        }

        public List<Task> GetTasksInProject(string projectId)
        {
            connection.Open();
            openingConnection = true;
            commandStr = ("select task_id, t_name, p_name, developer, complexity, tasks.deadline, tasks.p_status, tasks.creation_date " +
                "from tasks join projects on project = project_id where project_id = " + projectId + ";");
            Console.WriteLine(commandStr);
            command = new MySqlCommand(commandStr, connection);
            MySqlDataReader reader = command.ExecuteReader();
            List<Task> tasks = new List<Task>();
            while (reader.Read())
            {
                Task task = new Task();
                task.id = Convert.ToInt32(reader[0].ToString());
                task.name = reader[1].ToString();
                task.project = reader[2].ToString();
                task.developer = reader[3].ToString();
                task.complexity = Convert.ToInt32(reader[4].ToString());
                task.deadline = reader[5].ToString();
                task.status = reader[6].ToString();
                task.creation_date = reader[7].ToString();
                tasks.Add(task);
            }
            connection.Close();
            openingConnection = false;
            return tasks;
        }

        public void closeDB()
        {
            connection.Close();
        }

        ~InteractionDB()
        {
            //connection.Close();
        }
    }

}
