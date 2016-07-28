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
            Console.WriteLine("Input Client ID, It is also known as an application ID");
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
            // To add a subreddit to filter from, add it to this list of items, an example would be say, "aww", "The_Donald", for example, I added two to show you the syntax
            Subreddit[] exclusions = new Subreddit[] { reddit.GetSubreddit("aww"), reddit.GetSubreddit("The_Donald") };
            while (true)
            {
                foreach (var post in main.New.Take(100))
                {
                    Console.Title = "Searching /r/all";
                    if (alreadySeen.Contains(post.Id))
                        continue;
                    if ("imugr".Contains(post.Url.ToString()))
                    {
                        Console.WriteLine(post.Id +  "is an imgur link and it's link is" + post.Url.ToString());
                        string album = post.Url.ToString().Substring(post.Url.ToString().LastIndexOf('/'));
                       
                        if (galleryEndpoint.GetAlbumDetailsAsync(album).Result.Data.ImagesCount == 1)
                        {
                            Console.Title = "Searching imgur to see if it is a 1 post album";

                            if(!exclusions.ToList().Contains(post.Subreddit))
                            {
                                post.Comment("#s");
                            }
                        }
                    }
                    Console.WriteLine("Adding ID so no reposts");
                    alreadySeen.Add(post.Id);
                    File.WriteAllLines("./seen.txt", alreadySeen);
                    
                }
            }
        }
    }
}
