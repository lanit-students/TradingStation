using LogReader.Database;
using LogReader.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace LogReader.ViewModels
{
    public class LogViewModel : INotifyPropertyChanged
    {
        private readonly LogContext logContext;

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
                Logs = GetLogsTree(value);
                OnPropertyChanged("Logs");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

            Logs = GetLogsTree(selectedLogLevel);
        }

        private ObservableCollection<Node> GetLogsTree(LogLevel level)
        {
            var nodes = new ObservableCollection<Node>();

            // Getting single logs (no children, no parents)
            var logs = logContext.Logs;

            var parentless = logs.Where(x => x.ParentId == null);

            var childrenOfParentless = logs.Except(parentless).Where(x => (parentless.Select(x => x.Id).Contains(x.ParentId.Value)));

            var single = parentless.Where(x => !childrenOfParentless.Select(child => child.ParentId ?? Guid.Empty).Contains(x.Id) && x.Level == level);

            // Getting other log chains
            var notSingle = logs.Except(single);
            var children = notSingle.Where(x => !(notSingle.Select(x => x.ParentId)).Contains(x.Id) && x.Level == level);

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

        private Node GetParentsChain(IQueryable<Log> logs, Log log)
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
