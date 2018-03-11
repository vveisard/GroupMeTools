using System;
using System.Collections.Generic;
using System.Net;

namespace OriginalSINe.GroupMeAPI
{
    /// <summary>
    /// Utility class for the GroupMe API;
    /// </summary>
    static public class UtilityNet
    {
        /// <summary>
        /// Get a GroupMe API url;
        /// </summary>
        static public string URLGet(string token, params string[] urlActions)
        {
            string url = __urlBase.TrimEnd('/');

            foreach (string aURLAction in urlActions)
            {
                url += "/" + aURLAction.TrimEnd('/');
            }

            return url + __urlTokenSuffix + token;
        }

        /// <summary>
        /// Add post stuff to a URL;
        /// </summary>
        static public string URLPost(this string thisURL, params KeyValuePair<string, string>[] pairs)
        {
            foreach(KeyValuePair<string, string> aPost in pairs)
            {
                if
                (
                    !string.IsNullOrWhiteSpace(aPost.Value) && 
                    !string.IsNullOrWhiteSpace(aPost.Key)
                )
                {
                    thisURL += string.Format("&{0}={1}", aPost.Key, aPost.Value);
                }
            }

            return thisURL;
        }

        /// <summary>
        /// Get the JSON string for the given action URL;
        /// </summary>
        static public string JSONGet(WebClient client, string token, params string[] urlActions)
        => JSONGet(client, URLGet(token, urlActions));

        /// <summary>
        /// Get the JSON string from a URL;
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static public string JSONGet(WebClient webClient, string url)
        {
            //Console.WriteLine("Downloading from" + url);

            return webClient.DownloadString(url); // write the result
        }

        /// <summary>
        /// Base URL;
        /// </summary>
        private const string __urlBase = "https://api.groupme.com/v3/";

        /// <summary>
        /// Url portion at the end of a url, right before the token id;
        /// </summary>
        private const string __urlTokenSuffix = "?token=";
    }
}
