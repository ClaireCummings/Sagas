using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus.Saga;


namespace LFM.Submissions.GovGateway.LandRegistry
{
    /// <summary>
    /// TODO:  Is this a good design -- we're tracking all correspondance from Land Registry ever sent to a customer in one Saga
    /// -- actually I don't think we'll need the CorrespondenceSent message in practice as the live outstanding requests service
    /// won't return the same item of correspondance if we tick "ShowOnlyNewResponses".
    /// </summary>
    public class PollCorrespondenceProcessor : Saga<PollCorrespondenceSagaData>, IAmStartedByMessages<PollCorrespondence>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PollCorrespondence>(s => s.Username, m=> m.Username);

        }
        public void Handle(PollCorrespondence message)
        {
            this.Data.Username = message.Username;

           
            Console.WriteLine("Gateway received message PollCorrespondence MessageId: " + message.MessageId);

            var sender = new CorrespondencePollSender
                {
                    MessageId = message.MessageId,
                    Username = message.Username,
                    Password = message.Password
                };

            var response = sender.Poll();
            
            var responseMessage = CorrespondencePollResponseAnalyser.GetCorrespondencePollResponse(response);
            
            if (responseMessage != null)
            {
                responseMessage.Username = message.Username;
                responseMessage.Password = message.Password;
                responseMessage.CorrespondenceId = message.MessageId;
                Bus.Publish(responseMessage);                       
            }
        }
    }

    public class PollCorrespondenceSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string Username { get; set; }
  }
}
