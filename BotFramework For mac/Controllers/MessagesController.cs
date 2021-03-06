﻿using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;

namespace BotTemplate
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// Receive a message from a user and reply to it
        /// </summary>
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            if (activity.Type == ActivityTypes.Message)
            {
                await Conversation.SendAsync(activity, () => new Dialogs.RootDialog());
            }
            else
            {
                HandleSystemMessage(activity);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }

        private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<object> result)
        {
            var activity = await result as Activity;
            // calculate something for us to return    
            int length = (activity.Text).Length;
            // return our reply to the user    
            //test    
            if (activity.Text.Contains("technology"))
            {
                await context.PostAsync("Refer C# corner website for tecnology http://www.c-sharpcorner.com/");
            }
            else if (activity.Text.Contains("morning"))
            {
                await context.PostAsync("Hello !! Good Morning , Have a nice Day");
            }
            //test    
            else if (activity.Text.Contains("night"))
            {
                await context.PostAsync(" Good night and Sweetest Dreams with Bot Application ");
            }
            else if (activity.Text.Contains("date"))
            {
                await context.PostAsync(DateTime.Now.ToString());
            }
            else
            {
                await context.PostAsync("You sent {activity.Text} which was {length} characters");
            }
            context.Wait(MessageReceivedAsync);
        }
    }
}