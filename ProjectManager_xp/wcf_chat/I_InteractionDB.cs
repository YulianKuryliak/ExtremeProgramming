using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wcf_chat
{
    public interface I_InteractionDB
    {
        bool AddToDB(string table, string values);

        int GetIDByUsername(string username);

        ServerUser CheckAutorization(string login, string password);

        List<string> GetUserContactListUsernames(int id);

        List<Project> GetAllProjects();

        List<Task> GetTasksInProject(string projectId);

        void closeDB();
    }
}
