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

namespace ChatClient.Pages
{
    /// <summary>
    /// Interaction logic for pRegistration.xaml
    /// </summary>
    /// 

    public partial class pRegistration : Page, IServiceChatCallback
    {
        private Page pageAuthorization;
        Frame mainWindowFrame;
        ServiceChatClient client;
        MainWindow mainWindow;  

        public pRegistration(MainWindow _mainWindow, Frame _mainWindowFrame, ServiceChatClient _client)
        {
            InitializeComponent();
            mainWindowFrame = _mainWindowFrame;
            mainWindow = _mainWindow;
            client = _client;
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
        }

        public void MsgCallback(string msg)
        {
            throw new NotImplementedException();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string username = txtRegUsername.Text;
            string email = txtRegEmail.Text;
            string password = txtRegPassword.Password;
            //MessageBox.Show((username + " " + email + " " + password));
            bool registration = client.UserRegistration(username, email, password);
            if (registration)
            {
                btnToAutorization(sender, e);
                MessageBox.Show("You successfully registered");
            }
            else
            {
                MessageBox.Show("This username or e-mail already exists");
            }
        }

        private void btnToAutorization(object sender, RoutedEventArgs e)
        {
            pageAuthorization = new pAuthorization(mainWindow, mainWindowFrame, client);
            mainWindowFrame.Content = pageAuthorization;
        }
    }
}