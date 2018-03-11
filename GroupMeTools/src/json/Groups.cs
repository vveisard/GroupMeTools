// class file contains all POCOS required to deserialize JSON recieved from https://api.groupme.com/v3/groups?token=ACCESS_TOKEN

using System.Net;

using Newtonsoft.Json;

namespace OriginalSINe.GroupMeAPI
{
    /// <summary>
    /// Deserialized JSON recieved from https://api.groupme.com/v3/groups?token=ACCESS_TOKEN;
    /// </summary>
    public class Groups
    {
        #region Public, Properties

        public GroupsResponse[] response { get; set; }

        public Meta meta { get; set; }

        #endregion

        #region Static, Methods

        static public string URLGet(string token)
        => UtilityNet.URLGet(token, "groups");

        /// <summary>
        /// Get <see cref="Groups"/>;
        /// </summary>
        static public Groups Get(WebClient webClient, string token)
        => JsonConvert.DeserializeObject<Groups>(UtilityNet.JSONGet(webClient, URLGet(token)));

        #endregion
    }

    /// <summary>
    /// Enclosed field in <see cref="Groups"/>;
    /// </summary>
    public class GroupsResponse
    {
        public int id { get; set; }
    }

    /// <summary>
    /// Enclosed field in <see cref="GroupsResponse"/>;
    /// </summary>
    public class GroupsResponseMessages
    {
        public int count { get; set; }
        public int last_message_id { get; set; }
    }
}
