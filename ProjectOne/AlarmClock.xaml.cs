using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;

namespace ProjectOne
{
    public partial class AlarmClock : Window
    {
        private DispatcherTimer timer;
        private TimeSpan alarmTime;
        private bool isAlarmSet;
        private SoundPlayer alarmSound;

        public AlarmClock()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeSound();
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void InitializeSound()
        {
            alarmSound = new SoundPlayer("Resources/congrat.wav");
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Lấy giờ hiện tại
            DateTime currentTime = DateTime.Now;
            CurrentTimeLabel.Content = $"Current Time: {currentTime:HH:mm:ss}";

            // Kiểm tra nếu đã đặt báo thức
            if (isAlarmSet && currentTime.TimeOfDay >= alarmTime)
            {
                TriggerAlarm();
            }
        }

        private void SetAlarmButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(HourTextBox.Text, out int hour) &&
                int.TryParse(MinuteTextBox.Text, out int minute) &&
                hour >= 0 && hour < 24 &&
                minute >= 0 && minute < 60)
            {
                alarmTime = new TimeSpan(hour, minute, 0);
                isAlarmSet = true;
                StatusLabel.Content = $"Alarm set for {alarmTime:hh\\:mm}";
                System.Windows.MessageBox.Show($"Alarm set for {alarmTime:hh\\:mm}", "Alarm Set", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter a valid time (HH:MM).", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TriggerAlarm()
        {
            isAlarmSet = false;
            StatusLabel.Content = "Alarm Triggered!";
            alarmSound.Play();
            System.Windows.MessageBox.Show("Time's up! Alarm is ringing!", "Alarm", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void HourTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            HourPlaceholder.Visibility = string.IsNullOrEmpty(HourTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }

        private void MinuteTextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            MinutePlaceholder.Visibility = string.IsNullOrEmpty(MinuteTextBox.Text) ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
