using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data.Entity;
using System.Data.SQLite;
using System.IO;
using System.Drawing;
using System.Diagnostics;
using System.Xml;
using System.ServiceModel.Channels;
using System.Runtime.Remoting.Contexts;
using System.Threading.Tasks;
using Server_WCF.Contollers;
using Server_WCF.Models;

namespace Server_WCF
{
    /// <summary>
    /// Сервис для сообщения с клиентом
    /// </summary>
    public class Service : IService
    {
        private CursorFinderController _cursorFinderController;
        private AuthController _authController;
        private MailController _mailController;
        private bool _isNotificationEnabled;

        /// <summary>
        /// Ленивая подгрузка контроллеров
        /// </summary>
        private CursorFinderController CursorFinderController
        {
            get
            {
                if (_cursorFinderController is null)
                {
                    _cursorFinderController = new CursorFinderController();
                    System.Console.WriteLine("HOST: Клиент соединялся к серверу");
                }
                return _cursorFinderController;
            }
        }
        private AuthController AuthController
        {
            get
            {
                if (_authController is null)
                    _authController = new AuthController();
                return _authController;
            }
        }
        private MailController MailController
        {
            get
            {
                if (_mailController is null)
                    _mailController = new MailController();
                return _mailController;
            }
        }

        public async Task AddNewCursorPositionAsync(int xPos, int Ypos, MouseActionType actionType)
        {
            await CursorFinderController.AddCursorPositionAsync(xPos, Ypos, actionType);
            SendNotificationIfRequired();
        }

        public int Auth(bool isAdmin) => AuthController.Auth(isAdmin ? UserRole.Admin : UserRole.User);

        public async Task<bool> ClearDb(int userAuthToken)
        {
            if (!AuthController.IsUserAdmin(userAuthToken)) return false;
            await _cursorFinderController.ClearDbAsync();
            return true;
        }

        public void DisableNotification()
        {
            _isNotificationEnabled = false;
            System.Console.WriteLine("HOST: Уведомления отключены!");
        }

        public void EnableNotification()
        {
            _isNotificationEnabled = true;
            System.Console.WriteLine("HOST: Уведомления включены!");
        }

        private async void SendNotification(string message) => await MailController.SendMessageAsync(message);

        /// <summary>
        /// Отправляет уведомление если выполнено условие
        /// </summary>
        private async void SendNotificationIfRequired()
        {
            var recordsCount = await _cursorFinderController.GetRecordsCountAsync();
            if (_isNotificationEnabled && recordsCount > 0 && recordsCount % 50 == 0)
                SendNotification("Общее количество записей в БД: " + recordsCount.ToString());
        }

        public async Task<IList<CursorPosition>> GetAllCursorPositions() => await CursorFinderController.GetAllCursorPositionsAsync();

        public async Task<int> GetDbRecordsCount() => await CursorFinderController.GetRecordsCountAsync();

        public bool IsUSerAdmin(int userToken) => AuthController.IsUserAdmin(userToken);

        public bool StartRecord()
        {
            System.Console.WriteLine("HOST: Начата запись");
            return true;
        }

        public bool StopRecord()
        {
            System.Console.WriteLine("HOST: Остановлена запись");
            return true;
        }
    }
}




