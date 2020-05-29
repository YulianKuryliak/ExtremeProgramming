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
using System.Windows.Threading;
using ChatClient.ServiceChat;
using System.Windows.Media.Animation;
using ChatClient.Pages;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    {
        ServiceChatClient client;
        int userId = -1;

        private Page pageAuthorization;
        private Page pageRegistration;
        private pChat pageChat;
        private Page pageProjects;

        public double mainFrameOpacity;

        bool StateClosed = true;

        public MainWindow()
        {
            InitializeComponent();

            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
            pageAuthorization = new pAuthorization(this, mainFrame, client);

            mainFrameOpacity = 1;
            mainFrame.Content = pageAuthorization;
        }

        public void setUserId(int id)
        {
            userId = id;
            changeButtonAccess();
        }

        private void disconnect()
        {
            client.Disconnect(userId);
        }

        private void changeButtonAccess()
        {
            b_hMenuChats.IsEnabled = !b_hMenuChats.IsEnabled;
            b_hMenuProjects.IsEnabled = !b_hMenuProjects.IsEnabled; 
        }

        private void ButtonMenu_Click(object sender, RoutedEventArgs e)
        {
            if (StateClosed)
            {
                Storyboard sb = this.FindResource("OpenMenu") as Storyboard;
                sb.Begin();
            }
            else
            {
                Storyboard sb = this.FindResource("CloseMenu") as Storyboard;
                sb.Begin();
            }

            StateClosed = !StateClosed;
        }

        private void b_hMenuProjects_click(object sender, RoutedEventArgs e)
        {
            pageProjects = new pProjects(client, userId);
            mainFrame.Content = pageProjects;
        }

        private void b_hMenuChats_click(object sender, RoutedEventArgs e)
        {
            pageChat = new pChat(client, userId);
            mainFrame.Content = pageChat;
        }

        public void MsgCallback(string msg)
        {
            pageChat.MsgCallback(msg);
        }
    }
}
