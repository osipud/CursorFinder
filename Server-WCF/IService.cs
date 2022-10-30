using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using Server_WCF.Models;


namespace Server_WCF
{
    // ПРИМЕЧАНИЕ. Можно использовать команду "Переименовать" в меню "Рефакторинг", чтобы изменить имя интерфейса "IService" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        Task<IList<CursorPosition>> GetAllCursorPositions();
        [OperationContract]
        Task AddNewCursorPositionAsync(int xPos, int Ypos, MouseActionType actionType);
        [OperationContract]
        bool StartRecord();
        [OperationContract]
        bool StopRecord();
        [OperationContract]
        int Auth(bool isAdmin);
        [OperationContract]
        Task<bool> ClearDb(int userAuthToken);
        [OperationContract]
        bool IsUSerAdmin(int userAuthToken);
        [OperationContract]
        Task<int> GetDbRecordsCount();
        [OperationContract]
        void EnableNotification();
        [OperationContract]
        void DisableNotification();



    }
        
}
