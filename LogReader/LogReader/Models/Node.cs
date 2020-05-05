using System.Collections.ObjectModel;

namespace LogReader.Models
{
    public class Node
    {
        public Log Log { get; set; }
        public ObservableCollection<Node> Nodes { get; set; }
    }
}
