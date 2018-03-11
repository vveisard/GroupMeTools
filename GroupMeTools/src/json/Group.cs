// class file contains all POCOS required to deserialize JSON recieved from https://api.groupme.com/v3/groups/GROUP_ID?token=ACCESS_TOKEN

using System.Net;

using Newtonsoft.Json;

namespace OriginalSINe.GroupMeAPI
{
    /// <summary>
    /// Deserialized JSON recieved from https://api.groupme.com/v3/groups/GROUP_ID?token=ACCESS_TOKEN;
    /// </summary>
    public class Group
    {
        #region Public, Properties

        public GroupResponse response { get; set; }

        #endregion

        #region Static, Public

        static public string JSONGet(WebClient webClient, string token, string groupID)
        => UtilityNet.JSONGet(webClient, token, "groups", groupID.ToString());

        static public string URLGet(string token, string groupID)
        => UtilityNet.URLGet(token, "groups", groupID.ToString());

        static public Group Get(WebClient webClient, string token, string groupID)
        => JsonConvert.DeserializeObject<Group>(JSONGet(webClient, token, groupID));

        #endregion
    }

    /// <summary>
    /// Enclosed field in <see cref="Group"/>;
    /// </summary>
    public class GroupResponse
    {
        #region Public, Properties

        public string id { get; set; }
        public GroupResponseMessages messages { get; set; }

        #endregion
    }

    /// <summary>
    /// Enclosed field in <see cref="GroupResponse"/>;
    /// </summary>
    public class GroupResponseMessages
    {
        public int count { get; set; }
        public string last_message_id { get; set; }
    }
}
