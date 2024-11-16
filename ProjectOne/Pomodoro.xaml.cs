using System;
using System.Media;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Forms;

namespace ProjectOne
{
    public partial class Pomodoro : Window
    {
        private static Pomodoro instance;
        private DispatcherTimer timer;
        private TimeSpan countdownTime;
        private bool isStudying;
        private int cycles;
        private SoundPlayer soundPlayer;
        private SoundPlayer congratSound;
        private bool hasPlayedSound;
        private NotifyIcon notifyIcon;

        // Constructor private để thực hiện Singleton
        private Pomodoro()
        {
            InitializeComponent();
            InitializeTimer();
            InitializeSounds();
            InitializeNotifyIcon();
        }

        public static Pomodoro GetInstance()
        {
            if (instance == null || !instance.IsLoaded)
            {
                instance = new Pomodoro();
            }
            instance.Show();
            instance.WindowState = WindowState.Normal;
            return instance;
        }

        private void InitializeTimer()
        {
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        private void InitializeSounds()
        {
            soundPlayer = new SoundPlayer("Resources/army-ringtones.wav");
            congratSound = new SoundPlayer("Resources/congrat.wav");
            hasPlayedSound = false;
        }

        private void InitializeNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new System.Drawing.Icon("Resources/timer-icon.ico"),
                Visible = true,
                Text = "Pomodoro Timer - Click to manage."
            };
            notifyIcon.DoubleClick += NotifyIcon_DoubleClick;

            // Hiển thị thông báo khi ứng dụng ẩn vào tray
            notifyIcon.BalloonTipTitle = "Pomodoro Minimized";
            notifyIcon.BalloonTipText = "Pomodoro Timer is running in the background.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = WindowState.Normal; // Khôi phục lại cửa sổ nếu nó đang thu nhỏ
            Activate();
        }
        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                // Ẩn cửa sổ khi minimize
                Hide();
                notifyIcon.ShowBalloonTip(3000); // Hiển thị thông báo trong 3 giây
            }
            else if (WindowState == WindowState.Normal)
            {
                // Hiển thị lại cửa sổ khi không minimize
                Show();
            }
        }

        private void ShowNotification(string title, string message)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(3000); // 3 giây
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (countdownTime.TotalSeconds > 0)
            {
                countdownTime = countdownTime.Add(TimeSpan.FromSeconds(-1));
                CountdownTimer.Text = countdownTime.ToString(@"mm\:ss");
                notifyIcon.Text = isStudying ? $"Studying... {CountdownTimer.Text}" : $"On break... {CountdownTimer.Text}";
            }
            else
            {
                HandleSessionEnd();
            }
        }

        private void HandleSessionEnd()
        {
            if (!hasPlayedSound)
            {
                soundPlayer.Play(); // âm thanh
                hasPlayedSound = true;
            }
            timer.Stop();

            if (isStudying)
            {
                cycles--;
                if (cycles > 0)
                {
                    StatusText.Text = "Study session complete! Click Start to begin break.";
                    isStudying = false;
                    ShowNotification("Session Complete", "Study session completed. Time for a break!");
                }
                else
                {
                    StatusText.Text = "All study sessions completed!";
                    congratSound.Play(); // âm thanh cuối
                    StartButton.IsEnabled = true; // Kích hoạt nút bắt đầu
                    notifyIcon.Text = "All study sessions completed!";
                    ShowNotification("Pomodoro Complete", "Congratulations! All study sessions are completed.");
                    return;
                }
            }
            else
            {
                StatusText.Text = "Break session complete! Click Start to resume studying.";
                isStudying = true;
                ShowNotification("Break Over", "Break session completed. Back to study!");
            }

            CountdownTimer.Text = countdownTime.ToString(@"mm\:ss");
            StartButton.IsEnabled = true;
            hasPlayedSound = false;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (cycles > 0)
            {
                StartSession();
            }
            else if (int.TryParse(StudyTimeTextBox.Text, out int studyMinutes) &&
                     int.TryParse(BreakTimeTextBox.Text, out int breakMinutes) &&
                     int.TryParse(CyclesTextBox.Text, out cycles) &&
                     cycles > 0)
            {
                StartNewSession(studyMinutes);
            }
            else
            {
                System.Windows.MessageBox.Show("Please enter valid times and ensure cycles are greater than 0.", "Input Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void StartNewSession(int studyMinutes)
        {
            countdownTime = TimeSpan.FromMinutes(studyMinutes);
            StatusText.Text = "Studying...";
            CountdownTimer.Text = countdownTime.ToString(@"mm\:ss");
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            StartButton.IsEnabled = false;
            isStudying = true;
            hasPlayedSound = false;

            PauseButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Visible;

            notifyIcon.Text = $"Studying... {CountdownTimer.Text}";
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            ResetTimer();
        }

        private void ResetTimer()
        {
            timer.Stop();
            countdownTime = TimeSpan.Zero;
            CountdownTimer.Text = "00:00";
            StatusText.Text = "Stopped.";
            StartButton.IsEnabled = true;
            hasPlayedSound = false;

            PauseButton.Visibility = Visibility.Collapsed;
            StopButton.Visibility = Visibility.Collapsed;

            notifyIcon.Text = "Pomodoro stopped.";
        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            if (timer.IsEnabled)
            {
                timer.Stop();
                StatusText.Text = "Paused.";
                PauseButton.Content = "Resume";
                notifyIcon.Text = "Pomodoro paused.";
            }
            else
            {
                timer.Start();
                StatusText.Text = isStudying ? "Studying..." : "On Break...";
                PauseButton.Content = "Pause";
                notifyIcon.Text = isStudying ? $"Studying... {CountdownTimer.Text}" : $"On break... {CountdownTimer.Text}";
            }
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Study, Break, and Cycle times submitted!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void StartSession()
        {
            countdownTime = isStudying
                ? TimeSpan.FromMinutes(double.Parse(StudyTimeTextBox.Text))
                : TimeSpan.FromMinutes(double.Parse(BreakTimeTextBox.Text));

            StatusText.Text = isStudying ? "Studying..." : "On break...";
            CountdownTimer.Text = countdownTime.ToString(@"mm\:ss");
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            StartButton.IsEnabled = false;
            hasPlayedSound = false;

            PauseButton.Visibility = Visibility.Visible;
            StopButton.Visibility = Visibility.Visible;
            notifyIcon.Text = isStudying ? $"Studying... {CountdownTimer.Text}" : $"On break... {CountdownTimer.Text}";
        }

        protected override void OnClosed(EventArgs e)
        {
            // Dừng timer khi cửa sổ đóng
            timer.Stop();
            notifyIcon.Visible = false; // Ẩn biểu tượng tray
            base.OnClosed(e);
        }
    }
}
