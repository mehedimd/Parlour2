using Domain.Enums.AppEnums;
using Domain.ViewModel.UserAccount;

namespace Domain.Utility;

public static class AppUtility
{
    public const string DefaultPassword = "123456";
    public static RegisterViewModel GetUserModel(string userName, string name, string email, string mobile, string password, string confirmPassword, string photoUrl)
    {
        var userModel = new RegisterViewModel
        {
            Mobile = mobile,
            Email = email,
            FullName = name,
            Password = password,
            ConfirmPassword = confirmPassword,
            UserName = userName,
            PhotoUrl = photoUrl
        };
        if (string.IsNullOrEmpty(email))
        {
            email = mobile + "@mail.com";
            userModel.Email = email;
            userModel.IsMaskEmail = true;
        }
        else
        {
            userModel.UserName = string.IsNullOrEmpty(userModel.UserName) ? email : userModel.UserName;
        }

        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            userModel.Password = !string.IsNullOrEmpty(mobile) ? mobile : email;
            userModel.ConfirmPassword = !string.IsNullOrEmpty(mobile) ? mobile : email;
        }

        return userModel;
    }


    public static int GetMinutesFromDateDifference(DateTime minuend, DateTime subtrahend)
    {
        var timeDifference = minuend - subtrahend;
        var totalMinutes = (int)timeDifference.TotalMinutes;

        return totalMinutes;
    }

    public static string GetPasswordChangeMessage(string email, string newPassword, string message = null)
    {
        message = string.IsNullOrEmpty(message) ? "User Account Password Has been Reset Successfully based on your Request, please login and change your password ASAP." : message;
        var body = $"<h1>Congratulations</h1><br/>" +
                   $"{message} <br/>" +
                   $"User ID: <b>{email} </b><br/>" +
                   $"New Password: <b>{newPassword}</b>";
        return body;
    }

    public static DateTime AddTimeSpanToDate(DateTime date, string time)
    {
        String attendanceTime = new String(time);
        var times = attendanceTime.Split(":");
        var timeSpan = new TimeSpan(0, Convert.ToInt16(times[0]), Convert.ToInt16(times[1]), 0);
        var dateTime = date.Add(timeSpan);
        return dateTime;
    }

    public static Tuple<int, int, int> YearMonthDiff(DateTime startDate, DateTime endDate)
    {
        int monthDiff = ((endDate.Year * 12) + endDate.Month) - ((startDate.Year * 12) + startDate.Month) + 1;
        int years = (int)Math.Floor((decimal)(monthDiff / 12));
        int months = monthDiff % 12;
        return Tuple.Create(monthDiff, years, months); // TotalMonts, Year, Months
    }

    public static int DaysDiffernce(DateTime startDate, DateTime endDate)
    {
        TimeSpan diff = startDate - endDate;
        int daysDiff = (int)diff.TotalDays;
        return daysDiff;
    }

    public static int DaysDiffernceOnlyDate(DateTime startDate, DateTime endDate)
    {
        // Remove the time component from both dates
        startDate = startDate.Date;
        endDate = endDate.Date;

        TimeSpan diff = startDate - endDate;
        int daysDiff = (int)diff.TotalDays;
        return daysDiff;
    }

    public static bool OneYearHasPassed(DateTime startDate, DateTime endDate)
    {
        TimeSpan difference = endDate - startDate;
        return difference.TotalDays >= 365;
    }

    public static IEnumerable<DateTime> DateRangeList(DateTime startingDate, DateTime endingDate)
    {
        if (endingDate < startingDate)
        {
            throw new ArgumentException("endingDate should be after startingDate");
        }
        var ts = endingDate - startingDate;
        for (int i = 0; i <= ts.TotalDays; i++)
        {
            yield return startingDate.AddDays(i);
        }
    }

    public static string GetRelationText(string relation)
    {
        if (relation == RelationEnum.F.ToCharToString()) return $"{RelationEnum.F.GetDescription()}";
        if (relation == RelationEnum.M.ToCharToString()) return $"{RelationEnum.M.GetDescription()}";
        if (relation == RelationEnum.B.ToCharToString()) return $"{RelationEnum.B.GetDescription()}";
        if (relation == RelationEnum.S.ToCharToString()) return $"{RelationEnum.S.GetDescription()}";
        if (relation == RelationEnum.W.ToCharToString()) return $"{RelationEnum.W.GetDescription()}";
        if (relation == RelationEnum.H.ToCharToString()) return $"{RelationEnum.H.GetDescription()}";
        if (relation == RelationEnum.O.ToCharToString()) return $"{RelationEnum.O.GetDescription()}";
        if (relation == RelationEnum.D.ToCharToString()) return $"{RelationEnum.D.GetDescription()}";
        if (relation == RelationEnum.T.ToCharToString()) return $"{RelationEnum.T.GetDescription()}";

        return "--";
    }
}