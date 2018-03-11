using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace OriginalSINe.GroupMeAPI
{
    static public class UtilityMessages
    {
        /// <summary>
        /// Go through all the messages in a group and invoke some <see cref="Action{Message}"/> on each (if not null);
        /// Messages are processed from recent to oldest;
        /// </summary>
        /// <returns>
        /// <see cref="IList{Message}"/> which contains all the messages, in order of recent to oldest;
        /// </returns>
        static public IList<MessagesResponseMessage> MessagesFilter(WebClient webClient, string token, string groupID, Action<MessagesResponseMessage> action = null, int limit = 100)
        {
            Group group = Group.Get(webClient, token, groupID); // get the group
            int count = group.response.messages.count; // get number of messages
            int pages = (int)Math.Ceiling(count / (double)limit); // get number of pages

            //Console.WriteLine(string.Format("Group '{0}' has '{1}' messages; '{2}' pages with '{3}' messages per pages;", group.response.id, count, pages, limit));

            string messageIDLast = ""; // id of the last message
            Messages messagePageCurrent; // the messages on the current page
            List<MessagesResponseMessage> messages = new List<MessagesResponseMessage>(); // all the messages
            for (int iPage = 0; iPage < pages; iPage++) // for the pages, recent to oldest... 0 - 180
            {
                // get the next oldest page
                messagePageCurrent = Messages.Get
                (
                    webClient,
                    token,
                    groupID,
                    new KeyValuePair<string, string>("limit", limit.ToString()),
                    new KeyValuePair<string, string>("before_id", messageIDLast) // before the oldest mesage of the current messages; 
                                                                                 // ~starts empty, so will not get added to arguments, so the most recent page will be gotten
                );
                messageIDLast = messagePageCurrent.response.messages.Last().id.ToString(); // store the last message

                // add/ invoke messages from recent -> oldest
                foreach (MessagesResponseMessage aMessage in messagePageCurrent.response.messages)
                {
                    messages.Add(aMessage);

                    action?.Invoke(aMessage); // inkoke the message

                    //Console.WriteLine(aMessage.text);
                }

                //Console.WriteLine
                //(
                //    string.Format
                //    (
                //        "Processing page '{0}'; URL: '{1}'",
                //        iPage,
                //        Messages.URLGet(token, groupID.ToString())
                //        .URLPost
                //        (
                //            new KeyValuePair<string, string>("limit", limit.ToString()),
                //            new KeyValuePair<string, string>("before_id", messageIDLast)
                //        )
                //    )
                //);
            }

            return messages;
        }
    }
}
