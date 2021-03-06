using System;
using System.Timers;
using OneStreamWebUI.Mvvm.ViewModel;
using Timer = System.Timers.Timer;

namespace OneStreamMvvmBlazor.Client
{
    public class ClockViewModel : ViewModelBase, IDisposable
    {
        private readonly Timer _timer;
        private DateTime _dateTime = DateTime.Now;

        public ClockViewModel()
        {
            _timer = new Timer(TimeSpan.FromSeconds(1).TotalMilliseconds);
            _timer.Elapsed += TimerOnElapsed;
            _timer.Start();
        }

        public DateTime DateTime
        {
            get => _dateTime;
            set => Set(ref _dateTime, value);
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(true);
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            DateTime = DateTime.Now;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _timer.Dispose();
        }

        ~ClockViewModel()
        {
            Dispose(false);
        }
    }
}
