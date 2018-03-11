using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace OriginalSINe.GroupMeAPI
{
    /// <summary>
    /// Entry point;
    /// </summary>
    public class Program
    {
        #region Public, Methods

        /// <summary>
        /// Entry point;
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            _webClient_.Headers[HttpRequestHeader.ContentType] = "application/json;charset=UTF-8"; // set the header

            Console.WriteLine("Enter access token...");
            _accessToken_ = Console.ReadLine(); // get the access token

            attatchmentsDumpURLsAndDownload(); // do this thing!
        }

        /// <summary>
        /// Dump all the URLs of al attachments of a group to a text file;
        /// </summary>
        static private void attatchmentsDumpURLsAndDownload(string pathDirectory = "attatchments/", string dateFormatPrefix = "yyyy-MM-dd-HH-mm")
        {
            // TODO: refactor this into multiple methods

            Directory.CreateDirectory(pathDirectory);
            string pathFileURLs = Path.Combine(pathDirectory, "urls.txt");

            Console.WriteLine("Enter group ID...");
            string groupID = Console.ReadLine(); // get the access token

            // find

            Console.WriteLine("Attatchments in group '{0}', find, start", groupID);
            List<MessagesResponseMessageAttatchment> attatchments = new List<MessagesResponseMessageAttatchment>();
            List<DateTime> dates = new List<DateTime>(); // date time for all the attatchments 
            UtilityMessages.MessagesFilter
            (
                _webClient_,
                _accessToken_,
                groupID,
                (aMessage) =>
                {
                    //Console.WriteLine(aMessage.text);

                    if (aMessage.attachments.Length != 0)
                    {
                        //Console.WriteLine("Attatchments found");

                        foreach (MessagesResponseMessageAttatchment anAttatchment in aMessage.attachments)
                        {
                            if (!string.IsNullOrWhiteSpace(anAttatchment.url)) // if there is a URL of the attatchment
                            {
                                attatchments.Add(anAttatchment); // add the attatchment
                                dates.Add(aMessage.created_at.ToDateTime()); // add date created at
                            }
                        }
                    }
                }
            );
            Console.WriteLine("Attatchments in group '{0}', find, finish", groupID);
            //Console.WriteLine("There are '{0}' in Group '{1}'", attatchments.Count, _groupID_);

            // url dump

            Console.WriteLine("Attatchments in group '{0}', dump url, start", groupID);
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(pathFileURLs))
            {
                foreach (MessagesResponseMessageAttatchment anAttatchment in attatchments)
                {
                    file.WriteLine(string.Format("{0}: {1}", anAttatchment.type, anAttatchment.url));
                }
            }
            Console.WriteLine("Attatchments in group '{0}', dump url, finish", groupID);

            // dowloading

            Console.WriteLine("Attatchments in group '{0}', download, start", groupID);
            string pathFile = ""; // path to save the file locally
            string fileExtension = "";
            string filenameServer = ""; // filename on the server
            for (int i = 0; i < attatchments.Count; i++) // for all the attatchments
            {
                filenameServer = Path.GetFileName(new Uri(attatchments[i].url).AbsolutePath); // cache the "filename" of the attatchment
                fileExtension = ".UNKNOWN"; // start unknown

                // try to find valid file extension
                for(int i_extension = 0; i_extension < _fileExtensions.Length; i_extension++)
                {
                    if (filenameServer.Contains(_fileExtensions[i_extension])) // if known
                    {
                        fileExtension = _fileExtensions[i_extension];
                    }
                }

                pathFile = string.Format("{0}_{1}{2}", dates[i].ToString(dateFormatPrefix), (attatchments.Count - i).ToString(), fileExtension); // set file path

                // download
                _webClient_.DownloadFile
                (
                    attatchments[i].url, // download the attatchment
                    Path.Combine(pathDirectory, pathFile) // // into the directory
                );
            }
            Console.WriteLine("Attatchments in group '{0}', download, finish", groupID);

            Console.ReadLine();
        }

        #endregion

        #region Private, Fields

        static private WebClient _webClient_ = new WebClient();

        static private string[] _fileExtensions = new string[]
        {
            ".png",
            ".jpeg",
            ".gif",
            ".mp4"
        };

        #endregion

        #region Static, Private, Fields

        /// <summary>
        /// Access token for this application;
        /// Should be created by the developer;
        /// </summary>
        static private string _accessToken_;

        #endregion
    }
}
