using System;
using ChatClient;
using ChatClient.ServiceChat;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Windows;

namespace Backend.Tests
{
    [TestClass]
    public class FrontendTests
    {
        [TestMethod]
        public void CheckAuthorizationWithNotNullDataFromDatabase()
        {
            string login = "123";
            string password = "123";

            var mock = new Mock<ServiceChatClient>();
            var mockWindow = new Mock<MainWindow>();
            mock.Setup(a => a.UserAuthorization(login, password)).Returns(1);

            pAuthorization authorization = new pAuthorization(mockWindow.Object, mock.Object);
            authorization.checkAuthorization(login, password);

            mockWindow.Verify(a => a.setUserId(2));
        }
    }
}
