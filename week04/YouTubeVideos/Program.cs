using System;
using System.Collections.Generic;

// Comment class to represent a single comment
public class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string name, string text)
    {
        CommenterName = name;
        CommentText = text;
    }
}

// Video class to represent a YouTube video
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> _comments = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        LengthInSeconds = length;
    }

    public void AddComment(Comment comment)
    {
        _comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return _comments.Count;
    }

    public List<Comment> GetComments()
    {
        return new List<Comment>(_comments); // Return a copy to maintain encapsulation
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create videos
        var video1 = new Video("C# Tutorial for Beginners", "ProgrammingMaster", 600);
        var video2 = new Video("Learn Python in 10 Minutes", "CodeWizard", 360);
        var video3 = new Video("ASP.NET Core Crash Course", "DotNetExpert", 720);
        var video4 = new Video("Understanding OOP Principles", "SoftwareGuru", 480);

        // Add comments to video1
        video1.AddComment(new Comment("User123", "Great tutorial!"));
        video1.AddComment(new Comment("CodeNewbie", "Very helpful for beginners like me."));
        video1.AddComment(new Comment("DevPro", "Could you make a video on advanced topics?"));

        // Add comments to video2
        video2.AddComment(new Comment("PythonLover", "Nice and concise!"));
        video2.AddComment(new Comment("DataScientist", "Good overview of Python basics."));
        video2.AddComment(new Comment("WebDev", "Would love to see a Django tutorial next."));

        // Add comments to video3
        video3.AddComment(new Comment("DotNetDev", "Excellent crash course!"));
        video3.AddComment(new Comment("FullStack", "Clear explanations, thank you!"));
        video3.AddComment(new Comment("BackendEngineer", "When will you cover Entity Framework?"));
        video3.AddComment(new Comment("AspNetCoreFan", "Best ASP.NET Core tutorial I've seen!"));

        // Add comments to video4
        video4.AddComment(new Comment("OOPStudent", "Finally understand abstraction!"));
        video4.AddComment(new Comment("JavaDev", "These principles apply to Java too."));
        video4.AddComment(new Comment("SeniorEngineer", "Good refresher on fundamentals."));

        // Put videos in a list
        List<Video> videos = new List<Video> { video1, video2, video3, video4 };

        // Display video information and comments
        foreach (var video in videos)
        {
            Console.WriteLine($"Title: {video.Title}");
            Console.WriteLine($"Author: {video.Author}");
            Console.WriteLine($"Length: {video.LengthInSeconds} seconds");
            Console.WriteLine($"Number of Comments: {video.GetNumberOfComments()}");
            Console.WriteLine("Comments:");

            foreach (var comment in video.GetComments())
            {
                Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
            }

            Console.WriteLine(); // Add blank line between videos
        }
    }
}