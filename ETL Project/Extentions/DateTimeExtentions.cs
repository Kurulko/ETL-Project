namespace ETL_Project.Extentions;

public static class DateTimeExtentions
{
    static TimeZoneInfo EstZone => TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

    public static DateTime ConvertUtcToEst(this DateTime utcTime)
    {
        if (utcTime.Kind != DateTimeKind.Utc)
            throw new ArgumentException("The DateTime must be in UTC format.", nameof(utcTime));

        DateTime estTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, EstZone);
        return estTime;
    }

    public static DateTime ConvertEstToUtc(this DateTime estTime)
    {
        if (estTime.Kind != DateTimeKind.Unspecified)
            throw new ArgumentException("The DateTime must not be either local or UTC format.", nameof(estTime));
       
        DateTime utcTime = TimeZoneInfo.ConvertTimeToUtc(estTime, EstZone);
        return utcTime;
    }
}
