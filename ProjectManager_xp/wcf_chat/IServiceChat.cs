using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace wcf_chat
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IServiceChat" в коде и файле конфигурации.
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {

        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract]
        bool UserRegistration(string username, string login, string password);

        [OperationContract]
        int UserAuthorization(string login, string password);

        [OperationContract(IsOneWay = true)]
        void SendMsg(string msg, int id, string username);

        [OperationContract]
        List<string> UserContactListUsernames(int id);

        [OperationContract]
        List<Project> _GetAllProjects();

        [OperationContract]
        List<Task> GetTasksInProject(string projectId);

        [OperationContract]
        bool AddProject(string values);

        [OperationContract]
        bool AddTask(string task);

    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string msg);
    }

}
