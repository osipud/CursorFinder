using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Drawing;
using Point = System.Drawing.Point;
using Client_WPF.ServiceDB;
using System.Runtime.InteropServices;
using System.Windows.Threading;
using System.Windows.Controls;
using Control = System.Windows.Forms.Control;

namespace Client_WPF.Controllers
{
    internal class ServiceDBController
    {
        private Point _lastPoint;
        private ServiceClient _client;
        private const float _distanceForUpdatePX = 10;

        public static System.Drawing.Point Position { get; set; }


        public ServiceDBController()
        {
            DistanceForUpdate = (float)Math.Pow(_distanceForUpdatePX, 2);
        }
        public int UserToken { get; private set; }

        private float DistanceForUpdate { get; }

        public ServiceClient ServiceClient
        {
            get
            {
                if (_client == null)
                    _client = new ServiceClient("NetTcpBinding_IService");
                return _client;
            }
        }
        /// <summary>
        /// try/catch необходим для вылавливания ошибок при попытке получить данные без подключения к серверу, чтобы приложение не падало
        /// </summary>
        /// <returns></returns>
        public async Task<List<CursorPosition>> GetCursorPositions()
        {
            try
            {
                return (await ServiceClient.GetAllCursorPositionsAsync()).ToList();
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {

                return new List<CursorPosition>();
            }
        }

        public async Task<int> Auth(bool isAmin)
        {
            try
            {
                UserToken = await Task.Run(() => ServiceClient.Auth(isAmin));
                return UserToken;
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException e)
            {

                return -1;
            }

        }
        public async Task UpdateCursorPositionAsync(int xPos, int yPos, MouseActionType mouseActionType) => await ServiceClient.AddNewCursorPositionAsync(xPos, yPos, mouseActionType);
        public async Task UpdateCursorPositionAsync(Point point, MouseActionType mouseActionType) => await UpdateCursorPositionAsync(point.X, point.Y, mouseActionType);
        public async Task<bool> EnableNotifictionsAsync()
        {
            try
            {
                await ServiceClient.EnableNotificationAsync();
                return true;

            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                return false;
            }
        }
        public async Task<bool> DisableNotifictionsAsync()
        {
            try
            {
                await ServiceClient.DisableNotificationAsync();
                return true;

            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                return false;
            }
        }

        public async void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var point = Control.MousePosition;

            


            UpdateLastPoint(point);
            await UpdateCursorPositionAsync(point, GetCurrentMouseActionType(e));
        }
        public async Task<bool> StartRecording()
        {
            try
            {
                return await ServiceClient.StartRecordAsync();
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                return false;
            }
        }
        public async Task<bool> StoptRecording()
        {
            try
            {
                return await ServiceClient.StopRecordAsync();
            }
            catch (System.ServiceModel.CommunicationObjectFaultedException)
            {
                return false;
            }
        }

        public async Task<bool> RecordOut()
        {

            var point = Control.MousePosition;
            if (GetDistanceBeetweenPoints(point) < DistanceForUpdate)
            {
                return false;
            }
            else
            {
                UpdateLastPoint(point);
                await UpdateCursorPositionAsync(point, MouseActionType.Shift);
            }

            return false;

        }
        
        public async Task Window_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var point = Control.MousePosition;
            if (GetDistanceBeetweenPoints(point) < DistanceForUpdate) return;
            UpdateLastPoint(point);
            await UpdateCursorPositionAsync(point, MouseActionType.Shift);
        }
        public bool IsMyAccountAdmin() => ServiceClient.IsUSerAdmin(UserToken);

        public async Task<int> GetDbRecordsCount()
        {
            try
            {
                return await ServiceClient.GetDbRecordsCountAsync();
            }
            catch (System.ServiceModel.EndpointNotFoundException)
            {

                return -1;
            }
        }

        public async Task ClearDbRecords() => await ServiceClient.ClearDbAsync(UserToken);
        private void UpdateLastPoint(Point newPoint) => _lastPoint = newPoint;
        private float GetDistanceBeetweenPoints(Point point) => (float)(Math.Pow((point.X - _lastPoint.X), 2) + Math.Pow((point.Y - _lastPoint.Y), 2));

        private MouseActionType GetCurrentMouseActionType(MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                return MouseActionType.LeftButtonClick;
            if (e.MiddleButton == MouseButtonState.Pressed)
                return MouseActionType.MiddleButtonClick;
            return MouseActionType.RightButtonClick;
        }
    }

}
