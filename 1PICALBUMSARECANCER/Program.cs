using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ImgurNet;
using RedditSharp;
using RedditSharp.Things;
namespace _1PICALBUMSARECANCER
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Input Client ID");
            string ID = Console.ReadLine();
            var imgur = new Imgur(new ImgurNet.Authentication.ClientAuthentication(ID, true));
            var galleryEndpoint = new ImgurNet.ApiEndpoints.AlbumEndpoint(imgur);
            Console.WriteLine("Reddit username please");
            string username = Console.ReadLine();
            Console.WriteLine("Password please, reminder: this isn't stored anywhere");
            string password = Console.ReadLine();
            var reddit = new Reddit(username, password);
            reddit.RateLimit = WebAgent.RateLimitMode.Pace;
            Subreddit main = Subreddit.GetRSlashAll(reddit);
            List<string> alreadySeen;
            if(File.Exists("./seen.txt"))
            {
                alreadySeen = File.ReadAllLines("./seen.txt").ToList();
            }
            else
            {
                alreadySeen = new List<string>();
            }
            while (true)
            {
                foreach (var post in main.New.Take(25))
                {
                    if (alreadySeen.Contains(post.Id))
                        continue;
                    if ("imgur.com/a/".Contains(post.Url.ToString()))
                    {
                        string album = System.Text.RegularExpressions.Regex.Replace(post.Url.ToString(), @"https*://.imgur\.com/a/", "");
                        if (galleryEndpoint.GetAlbumDetailsAsync(album).Result.Data.ImagesCount == 1)
                        {
                            post.Comment("#s");
                        }
                    }
                    alreadySeen.Add(post.Id);
                    File.WriteAllLines("./seen.txt", alreadySeen);
                }
            }
        }
    }
}
