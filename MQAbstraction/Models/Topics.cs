using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MQAbstraction.Models
{
    public class Topics
    {
        public List<KeyValuePair<string, string>> TopicPairs { get; set; }

        public Topics(string config)
        {
            TopicPairs = new List<KeyValuePair<string, string>>();
            var topics = config.Split(',');
            foreach (var topic in topics)
            {
                var topicPair = topic.Split(':');
                TopicPairs.Add(new KeyValuePair<string, string>(topicPair[0], topicPair[1]));
            }
        }
    }
}
