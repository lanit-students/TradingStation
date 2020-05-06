using Kernel;
using System.Collections.ObjectModel;

namespace LogReader.Models
{
    public class Node
    {
        public LogMessage Log { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }
    }
}
