using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MySql.Data.MySqlClient.Memcached;

namespace ChatClient.Pages
{
    /// <summary>
    /// Interaction logic for pProjects.xaml
    /// </summary>
    public partial class pProjects : Page
    {
        
        //private BindingList<Task> tasks;
        //private BindingList<Project> projects;
        ServiceChatClient client;
        int userId;
        string activeProject;
        ChatClient.ServiceChat.Project[] projects;
        ChatClient.ServiceChat.Task[] tasks;
        string activeProj;

        public pProjects(ServiceChatClient _client, int _userId)
        {
            InitializeComponent();
            client = _client;
            userId = _userId;
            SetProjectList();

        }

        public void SetProjectList()
        {
            projects = client._GetAllProjects();
            //lbMain.ItemsSource = pprojects;
            lbMain.ItemsSource = projects;
        }

        private void bFindProject(object sender, RoutedEventArgs e)
        {

        }

        private void SetProjectnameField_KeyDown(object sender, KeyEventArgs e)
        {

        }

        void HideProjects()
        {
            projectsGrid.Visibility = Visibility.Hidden;
        }

        void ShowProjects()
        {
            projectsGrid.Visibility = Visibility.Visible;
        }

        void OpenTasks(string id)
        {
            tasksGrid.Visibility = Visibility.Visible;
            tasks = client.GetTasksInProject(id);
            if (tasks != null)
            {
                lbTasks.ItemsSource = tasks;
            }
        }

        void HideTasks()
        {
            tasksGrid.Visibility = Visibility.Hidden;
        }

        void OpenNewProjectForm()
        {
            newProjectsGrid.Visibility = Visibility.Visible;
        }

        void HideNewProjectFrom()
        {
            newProjectsGrid.Visibility = Visibility.Hidden;
        }

        private void ibMainProjectsButton_click(object sender, RoutedEventArgs e)
        {
            HideProjects();
            activeProj = (sender as Button).Tag.ToString();
            OpenTasks(activeProj);
        }

        private void ibMainTasksButton_click(object sender, RoutedEventArgs e)
        {

        }

        private void bCreateProject(object sender, RoutedEventArgs e)
        {
            HideProjects();
            OpenNewProjectForm();
        }

        private void bAddProject(object sender, RoutedEventArgs e)
        {
            Project project = new Project()
            {
                name = txtProjectName.Text,
                manager = txtProjectManager.Text,
                methodology = txtProjectMethodology.Text,
                deadline = txtProjectDeadline.Text
            };
            
            if (client.AddProject("'" + project.name + "', '" + project.manager + "', '" + project.methodology + "'"))
            {
                MessageBox.Show("Project Added");
                HideNewProjectFrom();
                ShowProjects();
            }
        }

        private void addTasks(object sender, RoutedEventArgs e)
        {
            OpenNewTaskFrom();
            HideTasks();
        }

        private void OpenNewTaskFrom()
        {
            newTaskGrid.Visibility = Visibility.Visible;
        }

        private void HideNewtaskForm()
        {
            newTaskGrid.Visibility = Visibility.Hidden;
        }

        private void bCreateTask(object sender, RoutedEventArgs e)
        {
            Task task = new Task()
            {
                name = txtTaskName.Text,
                project = activeProj,
                developer = txtTaskDeveloper.Text,
                complexity = Int32.Parse(txtTaskComplexity.Text),
                deadline = txtTaskDeadline.Text
            };

            if(client.AddTask("'" + task.project + "', '" + task.name + "', '" + task.developer + "', '" + task.complexity + "', '" + task.deadline + "'"))
            {
                MessageBox.Show("Task added");
                HideNewtaskForm();
                ShowProjects();
            }
        }
    }
}
