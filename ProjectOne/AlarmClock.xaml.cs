using System;
using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using Newtonsoft.Json;

namespace ProjectOne
{
    public partial class AlarmClock : Window
    {
        private static NotifyIcon notifyIcon;
        private static AlarmClock instance;
        private DispatcherTimer timer;
        private DateTime alarmDateTime;
        private bool isAlarmSet;
        private SoundPlayer alarmSound;
        private string alarmDataFile = "Resources/alarmData.json";

        // Constructor private để thực hiện Singleton
        public AlarmClock()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeSounds();
            InitializeNotifyIcon();
            LoadAlarmData();
        }

        public static AlarmClock GetInstance()
        {
            if (instance == null || !instance.IsLoaded)
            {
                instance = new AlarmClock();
            }
            instance.Show();
            instance.WindowState = WindowState.Normal;
            return instance;
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1) // Kiểm tra mỗi giây
            };
            timer.Tick += Timer_Tick;
            timer.Start();  // Đảm bảo timer được bắt đầu
        }

        private void InitializeSounds()
        {
            alarmSound = new SoundPlayer("Resources/congrat.wav");
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("Resources/timer-icon.ico"),
                Visible = true,
                Text = "Alarm Clock - Click to manage.",
                BalloonTipTitle = "Alarm Clock Running",
                BalloonTipText = "Alarm Clock is running in the background.",
                BalloonTipIcon = ToolTipIcon.Info
            };

            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            // Hiển thị thông báo khi ứng dụng ẩn vào tray
            notifyIcon.ShowBalloonTip(3000); // Hiển thị thông báo trong 3 giây khi ứng dụng ẩn vào tray
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal; // Khôi phục lại cửa sổ nếu nó đang thu nhỏ
            Activate();
        }

        private void ShowNotification(string title, string message)
        {
            // Hiển thị thông báo nhỏ trong khay hệ thống
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(3000); // 3 giây
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (isAlarmSet && DateTime.Now >= alarmDateTime)
            {
                TriggerAlarm();
            }
        }

        // Tải dữ liệu báo thức từ tệp JSON
        private void LoadAlarmData()
        {
            if (File.Exists(alarmDataFile))
            {
                try
                {
                    var alarmData = JsonConvert.DeserializeObject<AlarmData>(File.ReadAllText(alarmDataFile));
                    if (alarmData != null)
                    {
                        alarmDateTime = alarmData.AlarmTime;
                        isAlarmSet = alarmData.IsAlarmSet;
                        StatusLabel.Content = $"Alarm set for {alarmDateTime:yyyy-MM-dd HH:mm}";
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Failed to load alarm data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        // Lưu dữ liệu báo thức vào tệp JSON
        private void SaveAlarmData()
        {
            try
            {
                var alarmData = new AlarmData
                {
                    AlarmTime = alarmDateTime,
                    IsAlarmSet = isAlarmSet
                };
                File.WriteAllText(alarmDataFile, JsonConvert.SerializeObject(alarmData));
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Failed to save alarm data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Sự kiện đặt giờ báo thức khi người dùng nhấn nút Set
        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(HourTextBox.Text.Trim(), out int hour) &&
                int.TryParse(MinuteTextBox.Text.Trim(), out int minute))
            {
                if (hour >= 0 && hour < 24 && minute >= 0 && minute < 60)
                {
                    DateTime currentTime = DateTime.Now;
                    DateTime alarmDateTimeToday = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, hour, minute, 0);

                    // Nếu giờ báo thức đã qua, đặt báo thức vào ngày hôm sau
                    alarmDateTime = alarmDateTimeToday <= currentTime ? alarmDateTimeToday.AddDays(1) : alarmDateTimeToday;
                    isAlarmSet = true;

                    // Tính thời gian còn lại cho báo thức
                    TimeSpan timeUntilAlarm = alarmDateTime - currentTime;
                    StatusLabel.Content = $"Alarm set for {alarmDateTime:yyyy-MM-dd HH:mm} (in {timeUntilAlarm.Hours}h {timeUntilAlarm.Minutes}m {timeUntilAlarm.Seconds}s)";

                    System.Windows.MessageBox.Show($"Alarm set successfully for {alarmDateTime:HH:mm}.\nTime left: {timeUntilAlarm.Hours} hours, {timeUntilAlarm.Minutes} minutes, {timeUntilAlarm.Seconds} seconds.", "Alarm Set", MessageBoxButton.OK, MessageBoxImage.Information);
                    SaveAlarmData(); // Lưu trạng thái báo thức
                }
                else
                {
                    System.Windows.MessageBox.Show("Please enter a valid time (HH:MM format). Hours: 0-23, Minutes: 0-59.", "Invalid Time", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Invalid input! Please enter numeric values for hours and minutes.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Kích hoạt báo thức khi đến giờ
        private void TriggerAlarm()
        {
            // Đặt lại trạng thái báo thức
            isAlarmSet = false;
            StatusLabel.Content = "Alarm Triggered!";

            // Chạy âm thanh báo thức
            alarmSound.Play();

            // Hiển thị thông báo trong khay hệ thống
            ShowNotification("Alarm", "Time's up! Alarm is ringing!");

            // Thông báo pop-up khi báo thức kích hoạt
            System.Windows.MessageBox.Show("Time's up! Alarm is ringing!", "Alarm", MessageBoxButton.OK, MessageBoxImage.Information);

            // Xóa dữ liệu báo thức trong tệp
            DeleteAlarmData();

            // Cập nhật lại giao diện
            SaveAlarmData(); // Lưu lại trạng thái báo thức đã được kích hoạt
        }

        private void DeleteAlarmData()
        {
            // Xóa tệp alarmData nếu có
            if (File.Exists(alarmDataFile))
            {
                try
                {
                    File.Delete(alarmDataFile);
                    StatusLabel.Content = "No alarm set.";
                    // Cập nhật lại giao diện sau khi xóa dữ liệu
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Failed to delete alarm data: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (this.WindowState == WindowState.Minimized)
            {
                this.Hide();
                notifyIcon.ShowBalloonTip(3000); // Hiển thị thông báo trong 3 giây khi ứng dụng ẩn vào tray
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

        // Cập nhật hiển thị placeholder khi giờ hoặc phút trống
        private void HourTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            HourPlaceholder.Visibility = string.IsNullOrEmpty(HourTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void MinuteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MinutePlaceholder.Visibility = string.IsNullOrEmpty(MinuteTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }

    // Lớp chứa dữ liệu báo thức
    public class AlarmData
    {
        public DateTime AlarmTime { get; set; }
        public bool IsAlarmSet { get; set; }
    }
}
