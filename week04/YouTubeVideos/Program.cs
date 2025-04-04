using System;
using System.Collections.Generic;

// Comment class to track commenter name and text
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

// Video class to track video information and comments
public class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    private List<Comment> Comments { get; } = new List<Comment>();

    public Video(string title, string author, int length)
    {
        Title = title;
        Author = author;
        LengthInSeconds = length;
    }

    public void AddComment(string commenterName, string commentText)
    {
        Comments.Add(new Comment(commenterName, commentText));
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length: {LengthInSeconds} seconds");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");

        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine($"- {comment.CommenterName}: {comment.CommentText}");
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create list to hold videos
        List<Video> videos = new List<Video>();

        // Create and populate first video
        Video video1 = new Video("C# Tutorial for Beginners", "Programming Master", 600);
        video1.AddComment("JohnDoe", "Great tutorial!");
        video1.AddComment("JaneSmith", "Very helpful, thanks!");
        video1.AddComment("CodeNewbie", "When will you make part 2?");
        videos.Add(video1);

        // Create and populate second video
        Video video2 = new Video("ASP.NET Core Crash Course", "Web Dev Simplified", 1200);
        video2.AddComment("WebDeveloper", "Clear explanations");
        video2.AddComment("BackendGuy", "Could you cover middleware next?");
        video2.AddComment("Student123", "Perfect timing for my project");
        video2.AddComment("DotNetFan", "Love your teaching style!");
        videos.Add(video2);

        // Create and populate third video
        Video video3 = new Video("Entity Framework Explained", "Data Access Pro", 900);
        video3.AddComment("DatabaseAdmin", "Finally someone who explains migrations well");
        video3.AddComment("JuniorDev", "This saved my project!");
        video3.AddComment("TechLead", "Good overview of EF Core");
        videos.Add(video3);

        // Create and populate fourth video
        Video video4 = new Video("Design Patterns in C#", "Software Architect", 1500);
        video4.AddComment("SeniorDev", "Excellent real-world examples");
        video4.AddComment("TeamLead", "My team will watch this");
        video4.AddComment("OOPFan", "Singleton pattern explained perfectly");
        videos.Add(video4);

        // Display information for all videos
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}