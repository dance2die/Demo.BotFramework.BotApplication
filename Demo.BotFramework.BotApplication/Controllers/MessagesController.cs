using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Services.Description;
using Microsoft.Bot.Connector;
//using Microsoft.Bot.Connector.Utilities;
using Newtonsoft.Json;

namespace Demo.BotFramework.BotApplication
{
	[BotAuthentication]
	public class MessagesController : ApiController
	{
		/// <summary>
		/// POST: api/Messages
		/// Receive a activity from a user and reply to it
		/// </summary>
		public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
		{
			ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
			if (activity.Type == ActivityTypes.Message)
			{
				// calculate something for us to return
				int length = (activity.Text ?? string.Empty).Length;
				// return our reply to the user
				Activity reply = activity.CreateReply($"You sent {activity.Text} which was {length} characters");
				await connector.Conversations.ReplyToActivityAsync(reply);
			}
			else
			{
				HandleSystemMessage(activity);
			}
			var response = Request.CreateResponse(HttpStatusCode.OK);
			return response;
		}

		private Activity HandleSystemMessage(Activity activity)
		{
			if (activity.Type == "Ping")
			{
				Activity reply = activity.CreateReply();
				reply.Type = "Ping";
				return reply;
			}
			else if (activity.Type == "DeleteUserData")
			{
				// Implement user deletion here
				// If we handle user deletion, return a real activity
			}
			else if (activity.Type == "BotAddedToConversation")
			{
			}
			else if (activity.Type == "BotRemovedFromConversation")
			{
			}
			else if (activity.Type == "UserAddedToConversation")
			{
			}
			else if (activity.Type == "UserRemovedFromConversation")
			{
			}
			else if (activity.Type == "EndOfConversation")
			{
			}

			return null;
		}
	}
}