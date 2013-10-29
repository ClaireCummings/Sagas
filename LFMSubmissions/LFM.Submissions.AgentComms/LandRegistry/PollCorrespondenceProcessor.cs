using System;
using LFM.Submissions.InternalMessages.LandRegistry.Commands;
using NServiceBus.Saga;

namespace LFM.Submissions.AgentComms.LandRegistry
{
    /// <summary>
    /// TODO:  Is this a good design -- we're tracking all correspondance from Land Registry ever sent to a customer in one Saga
    /// -- actually I don't think we'll need the CorrespondenceSent message in practice as the live outstanding requests service
    /// won't return the same item of correspondance if we tick "ShowOnlyNewResponses".
    /// </summary>
    public class PollCorrespondenceProcessor : Saga<PollCorrespondenceSagaData>, IAmStartedByMessages<PollCorrespondence>
    {
        public ICorrespondencePoller CorrespondencePoller { get; set; }

        public override void ConfigureHowToFindSaga()
        {
            ConfigureMapping<PollCorrespondence>(s => s.Username, m=> m.Username);
        }

        public void Handle(PollCorrespondence message)
        {
            this.Data.Username = message.Username;

            Console.WriteLine("Gateway received message PollCorrespondence MessageId: " + message.MessageId);

            CorrespondencePoller.MessageId = message.MessageId;
            CorrespondencePoller.Username = message.Username;
            CorrespondencePoller.Password = message.Password;

            if (CorrespondencePoller.Poll())
            {
                var responseMessage = CorrespondencePoller.Response;

                if (responseMessage != null)
                {
                    responseMessage.Username = message.Username;
                    responseMessage.Password = message.Password;
                    responseMessage.CorrespondenceId = message.MessageId;
                    Bus.Publish(responseMessage);
                }
            }
        }
    }
}