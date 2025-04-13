using System;
using System.Collections.Generic;

namespace ExerciseTracking
{
    // Base Activity class
    public abstract class Activity
    {
        private DateTime _date;
        private int _minutes;

        public Activity(DateTime date, int minutes)
        {
            _date = date;
            _minutes = minutes;
        }

        public DateTime Date => _date;
        public int Minutes => _minutes;

        // Abstract methods to be overridden by derived classes
        public abstract double GetDistance();
        public abstract double GetSpeed();
        public abstract double GetPace();

        // Virtual method that can be overridden if needed
        public virtual string GetSummary()
        {
            return $"{_date.ToString("dd MMM yyyy")} {GetType().Name} ({_minutes} min) - " +
                   $"Distance: {GetDistance():F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min per mile";
        }
    }

    // Running class
    public class Running : Activity
    {
        private double _distance;

        public Running(DateTime date, int minutes, double distance) 
            : base(date, minutes)
        {
            _distance = distance;
        }

        public override double GetDistance() => _distance;
        public override double GetSpeed() => (_distance / Minutes) * 60;
        public override double GetPace() => Minutes / _distance;
    }

    // Cycling class
    public class Cycling : Activity
    {
        private double _speed;

        public Cycling(DateTime date, int minutes, double speed) 
            : base(date, minutes)
        {
            _speed = speed;
        }

        public override double GetDistance() => (_speed * Minutes) / 60;
        public override double GetSpeed() => _speed;
        public override double GetPace() => 60 / _speed;
    }

    // Swimming class
    public class Swimming : Activity
    {
        private int _laps;

        public Swimming(DateTime date, int minutes, int laps) 
            : base(date, minutes)
        {
            _laps = laps;
        }

        public override double GetDistance() => _laps * 50 / 1000 * 0.62; // miles
        public override double GetSpeed() => (GetDistance() / Minutes) * 60;
        public override double GetPace() => Minutes / GetDistance();
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create activities
            var activities = new List<Activity>
            {
                new Running(new DateTime(2023, 11, 3), 30, 3.0),
                new Cycling(new DateTime(2023, 11, 3), 30, 9.7),
                new Swimming(new DateTime(2023, 11, 3), 30, 20)
            };

            // Display summaries
            foreach (var activity in activities)
            {
                Console.WriteLine(activity.GetSummary());
            }
        }
    }
}