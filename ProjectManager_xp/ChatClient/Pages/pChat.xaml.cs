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
    /// Interaction logic for pChat.xaml
    /// </summary>
    public partial class pChat : Page, IServiceChatCallback
    {
        bool isConnected = false;
        ServiceChatClient client;
        int userId;
        string currentUser;

        public pChat(ServiceChatClient _client, int _userId)
        {
            InitializeComponent();
            client = _client;
            userId = _userId;
            //GetUserContacts();
        }

        /*private void GetUserCOntacts()
        {
            client.UserContactListUsernames(userId);

        }*/



        private void SetUsernameField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SelectUser(txtfindUsername.Text);
            }
        }

        private void SelectUser(string username)
        {
            currentUser = txtfindUsername.Text;
            lbChat.Items.Clear();
            ShowDialog();
        }

        private void ShowDialog()
        {
            // запит у бд за повідомленнями
            lbChat.Visibility = Visibility.Visible;
            tbMessage.Visibility = Visibility.Visible;
            messagePageLabel.Visibility = Visibility.Hidden;
        }

        /*
            вивести діалог з юзером при виборі його у меню або через find
         */

        /*
         * Вивести збоку усіх юзерів з якими контактував користувач
         */
        /*private void changeContactList()
        {
            < ListViewItem Width = "200" Height = "50" >
   
                           < StackPanel Orientation = "Horizontal" >
    
                                < Button Name = "b_hMenuChats" Content = "Chats" IsEnabled = "False" Command = "{Binding b_hMenuChat_click}" HorizontalAlignment = "Right" Click = "b_hMenuChats_click" ></ Button >
               
                                           < !--< TextBlock Text = "TextFild 2" HorizontalAlignment = "Right" ></ TextBlock > -->
                   
                                           </ StackPanel >
                   
                                       </ ListViewItem >
        }*/

        // вивести у діалог з юзером
        public void MsgCallback(string msg)
        {
            //lbChat.Items.Add("< Button x: Name = \"bConnDicon\" Click = \"Button_Click\" Content = \"" + (msg) + "\" Margin = \"10,10,0,0\" />");

            Button b = new Button();
            string sender = msg.Substring(0, msg.IndexOf(';'));
            msg = msg.Substring(msg.IndexOf(';') + 1, (msg.Length - sender.Length-1));

            b.Name = "a";
            //b.Click = 
            b.Content = msg;
            if (currentUser != sender) {
                b.Visibility = Visibility.Collapsed;
                //позначити у меню користувачів нове повідомлення
            }
            lbChat.Items.Add(b);
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);
        }

        //переробити на find user
        private void tbMessage_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (client != null)
                {
                    client.SendMsg(tbMessage.Text, userId, currentUser); // відправити ід отримувача
                    Button b = new Button();
                    b.Name = "a";
                    b.Content = DateTime.Now.ToShortTimeString() + ", Me : " + tbMessage.Text;
                    lbChat.Items.Add(b);
                    lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]);

                    tbMessage.Text = string.Empty;
                }
            }
        }

        private void bFindUser(object sender, RoutedEventArgs e)
        {
            if (txtfindUsername.Text != "Find User")
            {
                SelectUser(txtfindUsername.Text);
            }
        }
    }
}
