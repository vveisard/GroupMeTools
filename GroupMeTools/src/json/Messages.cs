// class file contains all POCOS required to deserialize JSON recieved from https://api.groupme.com/v3/groups/GROUP_ID/messages?token=ACCESS_TOKEN

using System.Collections.Generic;
using System.Net;

using Newtonsoft.Json;

namespace OriginalSINe.GroupMeAPI
{
    /// <summary>
    /// Deserialized JSON recieved from https://api.groupme.com/v3/groups/GROUP_ID/messages?token=ACCESS_TOKEN;
    /// </summary>
    public class Messages
    {
        #region Public, Properties

        public MessagesResponse response { get; set; }

        public Meta meta { get; set; }

        #endregion

        #region Static, Public, Methods

        /// <summary>
        /// Get the json representing a <see cref="Messages"/> from GroupMe's servers;
        /// </summary>
        static public string JSONGet(WebClient webClient, string token, string groupID, params KeyValuePair<string, string>[] args)
        {
            return
                UtilityNet.JSONGet
                (
                    webClient,
                    URLGet(token, groupID).URLPost(args)
                );
        }

        static public string URLGet(string token, string groupID)
        => UtilityNet.URLGet(token, "groups", groupID, "messages");

        /// <summary>
        /// Get messages from a group, with some given url argumnets;
        /// </summary>
        static public Messages Get
        (
            WebClient webClient,
            string token,
            string groupID,
            params KeyValuePair<string, string>[] args
        )
        => JsonConvert.DeserializeObject<Messages>(JSONGet(webClient, token, groupID.ToString(), args));

        #endregion
    }

    /// <summary>
    /// Enclosed field in <see cref="Messages"/>;
    /// </summary>
    public class MessagesResponse
    {
        public int count { get; set; }
        public MessagesResponseMessage[] messages { get; set; }
    }

    /// <summary>
    /// Enclosed field in <see cref="MessagesResponse"/>
    /// </summary>
    public class MessagesResponseMessage
    {
        public MessagesResponseMessageAttatchment[] attachments { get; set; }

        public long created_at { get; set; }

        public string id { get; set; }

        public string text { get; set; }
    }

    /// <summary>
    /// Enclosed field in a <see cref="MessagesResponse"/>;
    /// </summary>
    /// <notes>
    /// ~This does not deserialize properly "attatchments" such as likes or mentions;
    /// However it worked for what I needed it to do, so I did not fix it;
    /// </notes>
    public class MessagesResponseMessageAttatchment
    {
        public string type { get; set; }

        public string url { get; set; }
    }
}