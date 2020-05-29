using ChatClient.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;
 


namespace ChatClient
{
    /// <summary>
    /// Interaction logic for pAuthorization.xaml
    /// </summary>
    /// 

    public partial class pAuthorization : Page, IServiceChatCallback
    {
        private Page pageRegistration;
        ServiceChatClient client;
        MainWindow mainWindow;

        int avtorization;
        Frame mainWindowFrame;

        public pAuthorization(MainWindow _mainWindow, Frame _mainWindowFrame, ServiceChatClient _client)
        {
            mainWindowFrame = _mainWindowFrame;
            mainWindow = _mainWindow;
            client = _client;
            InitializeComponent();
        }

        public pAuthorization(MainWindow _mainWindow, ServiceChatClient _client)
        {
            mainWindow = _mainWindow;
            client = _client;
            InitializeComponent();
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

        public void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string login = txtLogin.Text;
            string password = txtPassword.Password;
            checkAuthorization(login, password);
        }

        public void checkAuthorization(string login, string password)
        {
            avtorization = client.UserAuthorization(login, password);

            if (avtorization != -1)
            {
                MessageBox.Show("You authorised");
                mainWindow.setUserId(avtorization);
            }
            else
                MessageBox.Show("Wrong login or password");
        }

        private void btnToRegistration(object sender, RoutedEventArgs e)
        {
            pageRegistration = new pRegistration(mainWindow, mainWindowFrame, client);
            mainWindowFrame.Content = pageRegistration;
        }
    }
}
