using System;
using System.Collections.Generic;
using LFM.Submissions.Contract.NotificationsService;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus;
using NServiceBus.Saga;


namespace LFM.Submissions.GovGateway.LandRegistry
{
    /// <summary>
    /// TODO:  Is this a good design -- we're tracking all correspondance from Land Registry ever sent to a customer in one Saga
    /// -- actually I don't think we'll need the CorrespondenceSent message in practice as the live outstanding requests service
    /// won't return the same item of correspondance if we tick "ShowOnlyNewResponses".
    /// </summary>
    public class PollCorrespondenceProcessor : Saga<PollCorrespondenceSagaData>, IAmStartedByMessages<PollCorrespondence>, IHandleMessages<CorrespondenceSent>
    {
        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PollCorrespondence>(s => s.Username, m=> m.Username);
            ConfigureMapping<CorrespondenceSent>(s => s.Username, m => m.Username);

        }
        public void Handle(PollCorrespondence message)
        {
            this.Data.Username = message.Username;

            if(this.Data.CorrespondencesReceived == null)
                this.Data.CorrespondencesReceived = new Dictionary<string, bool>();
            
            if(!this.Data.CorrespondencesReceived.ContainsKey(message.MessageId))
                this.Data.CorrespondencesReceived.Add(message.MessageId,false);

            if (this.Data.CorrespondencesReceived[message.MessageId])
                return;
            
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

        public void Handle(CorrespondenceSent message)
        {
            Console.WriteLine("Gateway received {0}", message.GetType().Name);
            if(this.Data.CorrespondencesReceived.ContainsKey(message.CorrespondenceId))
                this.Data.CorrespondencesReceived[message.CorrespondenceId] = true;
            //todo otherwise raise an exception we've sent an untracked message
        }
    }

    public class PollCorrespondenceSagaData : IContainSagaData
    {
        public Guid Id { get; set; }
        public string Originator { get; set; }
        public string OriginalMessageId { get; set; }

        public string Username { get; set; }
        public Dictionary<string, bool> CorrespondencesReceived { get; set; }
    }
}
