using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace RedditCommentHistory
{
    class RedditCommentSearcher
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the username of the Reddit user you would like to check: ");
            string username = Console.ReadLine();

            Console.WriteLine("Enter the word you would like to search for: ");
            string searchWord = Console.ReadLine();

            int count = 0;
            string after = "";

            // Make requests to the Reddit API to get the user's comment history
            // The API only returns a maximum of 1000 comments at a time, so we have to make multiple requests
            // until we have retrieved all of the user's comments
            while (true)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create($"https://www.reddit.com/user/{username}/comments.json?limit=1000&after={after}");
                request.UserAgent = "CommentHistoryBot/1.0";

                // Add a delay to avoid hitting the rate limit
                System.Threading.Thread.Sleep(1000);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string json = reader.ReadToEnd();

                // Parse the JSON response
                JObject results = JObject.Parse(json);
                JArray comments = (JArray)results["data"]["children"];
                -
                // Count the number of times the user has used the search word
                foreach (JObject comment in comments)
                {
                    string body = (string)comment["data"]["body"];
                    if (body.ToLower().Contains(searchWord.ToLower()))
                    {
                        count++;
                    }
                }

                // Check if we have retrieved all of the user's comments
                // If not, set the "after" parameter to the ID of the last comment we retrieved
                // so that we can get the next batch of comments
                if (comments.Count < 1000)
                {
                    break;
                }
                else
                {
                    after = (string)comments[comments.Count - 1]["data"]["name"];
                }
            }

            Console.WriteLine($"The user has used the word '{searchWord}' {count} times in their comment history.");
        }
    }
}