using System;
using System.Windows;
using System.Windows.Forms; // Import Windows Forms for NotifyIcon

namespace ProjectOne
{
    public partial class MainWindow : Window
    {
        private static NotifyIcon notifyIcon;
        private static MainWindow instance;

        public MainWindow()
        {
            InitializeComponent();

            // Kiểm tra nếu đã có instance
            if (instance == null)
            {
                instance = this; // Gán phiên bản hiện tại cho biến tĩnh
                CreateNotifyIcon();
            }
            else
            {
                // Nếu đã có instance, chỉ cần đưa nó lên
                instance.Activate();
                this.Close(); // Đóng cửa sổ hiện tại nếu đã có instance
            }
        }

        private void CreateNotifyIcon()
        {
            if (notifyIcon == null)
            {
                notifyIcon = new NotifyIcon
                {
                    Icon = new System.Drawing.Icon("Resources/timer-icon.ico"),
                    Text = "ProjectOne",
                    Visible = true
                };

                notifyIcon.ContextMenuStrip = new ContextMenuStrip();
                notifyIcon.ContextMenuStrip.Items.Add("Open", null, Open_Click);
                notifyIcon.ContextMenuStrip.Items.Add("Exit", null, (s, e) => System.Windows.Application.Current.Shutdown());
                notifyIcon.DoubleClick += (s, e) => ShowMainWindow();
            }
        }

        private void Open_Click(object sender, EventArgs e)
        {
            ShowMainWindow();
        }

        private void ShowMainWindow()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                if (instance != null && !instance.IsVisible)
                {
                    instance.Show();
                    instance.WindowState = WindowState.Normal;
                    instance.Activate();
                }
            });
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
            }
            base.OnStateChanged(e);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Ngăn cửa sổ đóng và ẩn nó thay vì đóng
            e.Cancel = true; // Hủy sự kiện đóng
            this.Hide(); // Ẩn cửa sổ
        }

        protected override void OnClosed(EventArgs e)
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
                notifyIcon = null;
            }
            base.OnClosed(e);
        }

        private void Pomodoro_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Pomodoro.GetInstance();
        }

        private void ToDoList_Click(object sender, RoutedEventArgs e)
        {
            ToDoList toDoListWindow = new ToDoList();
            toDoListWindow.ShowDialog();
        }

        private void WriteNotes_Click(object sender, RoutedEventArgs e)
        {
            Notes notesWindow = new Notes();
            notesWindow.ShowDialog();
        }

        private void Alarm_Click(object sender, RoutedEventArgs e)
        {
            AlarmClock alarmWindow = new AlarmClock();
            alarmWindow.ShowDialog();
        }
    }
}
