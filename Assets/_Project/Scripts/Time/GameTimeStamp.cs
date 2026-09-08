using UnityEngine;


public enum  Season
{
   Spring,
   Summer,
   Fall,
   Winter
}
public enum DayOfTheWeek
{
    Saturday,
    Sunday,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday
}
public class GameTimeStamp
{
    private int day;
    private int month;
    private int year;
    private int hour;
    private int minute;
    private Season season;

    public int Day => day;
    public int Month => month;
    public int Year => year;
    public int Hour => hour;
    public int Minute => minute;
    public Season Season => season;

    public DayOfTheWeek GetDayOfTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;
        int dayIndex = daysPassed % 7;
        return (DayOfTheWeek)(dayIndex);
    }
    public GameTimeStamp(int day, int year, int hour, int minute, Season season)
    {
        this.day = day;
        this.year = year;
        this.hour = hour;
        this.minute = minute;
        this.season = season;
    }
    public GameTimeStamp(GameTimeStamp timeStamp)
    {
        this.day = timeStamp.day;
        this.year = timeStamp.year;
        this.hour = timeStamp.hour;
        this.minute = timeStamp.minute;
        this.season = timeStamp.season;

    }

    public void UpdateClock()
    {
        minute ++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }
        if (hour >= 24)
        {
            hour = 0;
            day++;
        }
        if (day > 30)
        {
            day = 1;
            if(season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
          
        }
    }
    public static int HoursToMinutes(int hours)
    {
        return hours * 60;
    }
    public static int DaysToHours(int days)
    {
        return days * 24;
    }
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }
    public static int YearsToDays(int years)
    {
        return years * 4 * 30;
    }

    public static int CompareTwoTimeStamps(GameTimeStamp timeStampOne, GameTimeStamp timeStampTwo)
    {
        int timeStampOneHours = DaysToHours(YearsToDays(timeStampOne.year)) + DaysToHours(SeasonsToDays(timeStampOne.season)) + DaysToHours(timeStampOne.day)+timeStampOne.hour;
        int timeStampTwoHours = DaysToHours(YearsToDays(timeStampTwo.year)) + DaysToHours(SeasonsToDays(timeStampTwo.season)) + DaysToHours(timeStampTwo.day) + timeStampTwo.hour;

        int difference = (timeStampTwoHours - timeStampOneHours);
        return Mathf.Abs(difference);
    }
}
