using Kernel;
using LogReader.Database;
using LogReader.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Threading;

namespace LogReader.ViewModels
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private LogContext logContext;

        public ObservableCollection<Node> Logs { get; private set; }

        public List<LogLevel> LogLevels { get; set; }

        private LogLevel selectedLogLevel;

        public LogLevel SelectedLogLevel
        {
            get
                => selectedLogLevel;
            set
            {
                if (selectedLogLevel == value)
                {
                    return;
                }

                selectedLogLevel = value;
                UpdateLogs();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateLogs()
        {
            Logs = GetLogsTree();
            OnPropertyChanged("Logs");
        }

        private void SetUpdateTimer()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler((s, e) => UpdateLogs());
            dispatcherTimer.Interval = new TimeSpan(0, 0, 15);
            dispatcherTimer.Start();
        }

        public LogViewModel(LogContext context)
        {
            LogLevels = new List<LogLevel>()
            {
                LogLevel.Information,
                LogLevel.Warning,
                LogLevel.Error
            };

            selectedLogLevel = LogLevel.Information;

            logContext = context;

            Logs = GetLogsTree();

            SetUpdateTimer();
        }

        private ObservableCollection<Node> GetLogsTree()
        {
            var nodes = new ObservableCollection<Node>();

            // Getting single logs (no children, no parents)
            var logs = logContext.Logs.OrderByDescending(x => x.Time);

            var parentless = logs.Where(x => x.ParentId == null);

            var childrenOfParentless = logs.Except(parentless).Where(x => (parentless.Select(x => x.Id).Contains(x.ParentId.Value)));

            var single = parentless.Where(x => !childrenOfParentless.Select(child => child.ParentId ?? Guid.Empty).Contains(x.Id) && x.Level == selectedLogLevel);

            // Getting other log chains
            var notSingle = logs.Except(single);
            var children = notSingle.Where(x => !(notSingle.Select(x => x.ParentId)).Contains(x.Id) && x.Level == selectedLogLevel);

            // Adding logs to tree
            foreach (var child in children)
            {
                nodes.Add(GetParentsChain(notSingle, child));
            }

            foreach (var log in single)
            {
                nodes.Add(new Node() { Log = log });
            }

            return nodes;
        }

        private Node GetParentsChain(IQueryable<LogMessage> logs, LogMessage log)
        {
            if (log.ParentId != null)
            {
                var parentLog = logs.Where(x => x.Id == log.ParentId).FirstOrDefault();

                var parentNode = new ObservableCollection<Node>();

                parentNode.Add(GetParentsChain(logs, parentLog));

                return new Node()
                {
                    Log = log,
                    Nodes = parentNode
                };
            }

            return new Node() { Log = log };
        }
    }
}
