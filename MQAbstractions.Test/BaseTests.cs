namespace MQAbstractions.Test
{
    [TestClass]
    public class BaseTests
    {
        [TestMethod]
        public void SendMessage(string message, string queueName)
        {
        }

        [TestMethod]
        public void SendMessage(string message, string topicName, string sender)
        {
            
        }

        [TestMethod]
        public void ReadFromQueue(string queueName)
        {
            
        }

        [TestMethod]
        public void ReadFromTopic(string topicName, string subscriberName)
        {
            
        }
    }
}