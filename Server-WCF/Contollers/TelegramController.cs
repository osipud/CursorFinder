
using Server_WCF.Models;
using System.ComponentModel.DataAnnotations;
using TeleSharp.TL;
using TLSharp.Core;

namespace Server_WCF.Contollers
{
    /// <summary>
    /// Контроллер для отправки уведомлений в телеграм
    /// Использует TLSharp(по сути обычный telegram API)
    /// </summary>
    internal class TelegramController
    {
        private FileSessionStore _store;
        private TelegramClient _client;
        private string _phoneNumber;
        public TelegramController(int apiId, string apiHash, string phoneNumber)
        {
            _store = new FileSessionStore();
            _client = new TelegramClient(apiId, apiHash);
            _phoneNumber = phoneNumber;
        }

        public async void SendMessage(TLContact contact, string message)
        {
            await _client.SendMessageAsync(new TLInputPeerUser() { UserId = contact.UserId }, message);
        }

        public async void MakeAuthAsync()
        {
            var hash = await _client.SendCodeRequestAsync(_phoneNumber);
            System.Console.WriteLine("Enter code from telegram");
            var code = System.Console.ReadLine();
            var user = await _client.MakeAuthAsync(_phoneNumber, hash, code);
        }
    }
}
