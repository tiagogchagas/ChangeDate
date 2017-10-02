using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChangeDate.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    public class ChangeDateModel
    {
        private const long YearToMinuteRef = 525600;
        private const long MonthToMinuteRef = 43800;
        private const long DayToMinuteRef = 1440;
        private const long HourToMinuteRef = 60;

        private string dateTime;

        /// <summary>
        /// Represents current date and time in format dd/mm/yyyy.
        /// </summary>
        public string DateTime
        {
            get { return this.day.ToString().PadLeft(2, '0') + "/" + this.month.ToString().PadLeft(2, '0') + "/" + this.year.ToString() + " " + this.hour.ToString().PadLeft(2, '0') + ":" + this.minutes.ToString().PadLeft(2, '0'); }
            set { dateTime = this.day.ToString().PadLeft(2, '0') + "/" + this.month.ToString().PadLeft(2, '0') + "/" + this.year.ToString() + " " + this.hour.ToString().PadLeft(2, '0') + ":" + this.minutes.ToString().PadLeft(2, '0'); }
        }

        private long minutes;

        /// <summary>
        /// Represents long value that contains current minute value.
        /// </summary>
        public long Minutes
        {
            get { return this.minutes; }
            set
            {
                this.minutes = value;
                if (this.minutes >= 60)
                {
                    this.minutes = 0;
                    this.Hour = this.hour + 1;
                }
                else if (this.minutes == -1)
                {
                    this.minutes = 59;
                    this.Hour = this.hour - 1;
                }
            }
        }

        private long hour;

        /// <summary>
        /// Represents long value that contains current hour value.
        /// </summary>
        public long Hour
        {
            get { return this.hour; }
            set
            {
                this.hour = value;
                if (this.hour >= 24)
                {
                    this.hour = 0;
                    this.Day = this.day + 1;
                }
                else if (this.hour == -1)
                {
                    this.hour = 23;
                    this.Day = this.day - 1;
                }
            }
        }

        private long day;

        /// <summary>
        /// Represents long value that contains current day value.
        /// </summary>
        public long Day
        {
            get { return this.day; }
            set
            {
                this.day = value;
                if (this.day > 0)
                {
                    if (this.month == 02)
                    {
                        if (this.day > 28)
                        {
                            this.day = 01;
                            this.Month = this.month + 01;
                        }
                    }
                    else if (this.month == 01 || this.month == 03 || this.month == 05 || this.month == 07
                            || this.month == 08 || this.month == 10 || this.month == 12)
                    {
                        if (this.day > 31)
                        {
                            this.day = 01;
                            this.Month = this.month + 01;
                        }
                    }
                    else if (this.month == 04 || this.month == 06 || this.month == 09 || this.month == 11)
                    {
                        if (this.day > 30)
                        {
                            this.day = 01;
                            this.Month = this.month + 01;
                        }
                    }
                }
                else
                {
                    if (this.month == 03)
                    {
                        this.Month = this.month - 1;
                        this.day = 28;
                    }
                    else if (this.month == 02 || this.month == 04 || this.month == 06 || this.month == 08
                            || this.month == 09 || this.month == 11 || this.month == 01)
                    {
                        this.day = 31;
                        this.Month = this.month - 01;
                    }
                    else if (this.month == 05 || this.month == 07 || this.month == 10 || this.month == 12)
                    {
                        this.day = 30;
                        this.Month = this.month - 01;
                    }
                }

            }
        }

        private long month;
        /// <summary>
        /// Represents long value that contains current month value.
        /// </summary>
        public long Month
        {
            get { return this.month; }
            set
            {
                this.month = value;

                if (this.month > 12)
                {
                    this.month = 01;
                    this.year = this.year + 1;
                }
                else if (this.month <= 0)
                {
                    this.month = 12;
                    this.year = this.year - 01;
                }
            }
        }

        private long year;
        /// <summary>
        /// Represents long value that contains current year value.
        /// </summary>
        public long Year
        {
            get { return year; }
            set { year = value; }
        }

        public ChangeDateModel()
        {
            //Establish default date.
            day = 01;
            month = 01;
            year = 1900;
            hour = 00;
            minutes = 00;
        }

        /// <summary>
        /// Adds minutes to a specified date.
        /// </summary>
        /// <param name="date">Date that will receives specified minutes.</param>
        /// <param name="op">Operantion value. Inform '+' char value to add minutes or '-' char to subtract minutes.</param>
        /// <param name="value">Value in minutes that will be added (or subtract) in the specified date.</param>
        /// <returns>Return new DateTime.</returns>
        public string ChangeDate(string date, char op, long value)
        {
            try
            {
                long _receiveMinute = Math.Abs(value);
                string[] separator = date.Split(' ');
                string justDate = separator[0];
                string justTime = separator[1];

                string[] dateSeparator = justDate.Split('/');
                string[] timeSeparator = justTime.Split(':');

                day = long.Parse(dateSeparator[0]);
                if (day > 31)
                    day = 01;

                month = long.Parse(dateSeparator[1]);
                if (month > 12)
                    month = 01;

                year = long.Parse(dateSeparator[2]);

                hour = long.Parse(timeSeparator[0]);
                if (hour > 23)
                    hour = 0;

                minutes = long.Parse(timeSeparator[1]);
                if (minutes > 59)
                    minutes = 0;

                return NewDate(op, _receiveMinute);

            }
            catch (Exception ex)
            {
                return "500";
            }
        }

        private string NewDate(char op, long value)
        {
            string _returnValue = "";
            
            //Verifica o tipo de operação
            switch (op)
            {
                case '+':
                    _returnValue = DoSumOperation(value);
                    break;
                case '-':
                    _returnValue = DoSubtractionOperation(value);
                    break;
                default:
                    _returnValue = "500";
                    break;
            }
            return _returnValue;
        }

        private string DoSubtractionOperation(long value)
        {
            long _localValue = value;

            while (_localValue >= YearToMinuteRef)
            {
                _localValue = SubYear(_localValue);
            }
            while (_localValue >= MonthToMinuteRef)
            {
                _localValue = SubMonth(_localValue);
            }
            while (_localValue >= DayToMinuteRef)
            {
                _localValue = SubDay(_localValue);
            }
            while (_localValue >= HourToMinuteRef)
            {
                _localValue = SubHour(_localValue);
            }

            while (_localValue > 0)
            {
                _localValue = SubMinutes(_localValue);
            }

            return this.DateTime;
        }

        private string DoSumOperation(long value)
        {
            long _localValue = value;

            while (_localValue >= YearToMinuteRef)
            {
                _localValue = AddYear(_localValue);
            }
            while (_localValue >= MonthToMinuteRef)
            {
                _localValue = AddMonth(_localValue);
            }
            while (_localValue >= DayToMinuteRef)
            {
                _localValue = AddDay(_localValue);
            }
            while (_localValue >= HourToMinuteRef)
            {
                _localValue = AddHour(_localValue);
            }

            while (_localValue > 0)
            {
                _localValue = AddMinutes(_localValue);
            }

            this.DateTime = this.day.ToString().PadLeft(2, '0') + "/" + this.month.ToString().PadLeft(2, '0') + "/" + this.year.ToString() + " " + this.hour.ToString().PadLeft(2, '0') + ":" + this.minutes.ToString().PadLeft(2, '0');

            return this.DateTime;
        }

        /// <summary>
        /// Subtract minutes in current date/time and returns remaining minutes of total initial minutes adds.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long SubMinutes(long minutes)
        {
            this.Minutes = this.minutes - 1;
            long newMinutesValue = minutes - 1;
            return newMinutesValue;
        }

        /// <summary>
        /// Add minutes in current date/time and returns remaining minutes of total initial minutes adds.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long AddMinutes(long minutes)
        {
            this.Minutes = this.minutes + 1;
            long newMinutesValue = minutes - 1;
            return newMinutesValue;
        }

        /// <summary>
        /// Subtract hours in current date/time and return remaining minutes of total initial minutes subtracts.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will subtract in current date/time.</param>
        /// <returns>Remaining value of total initial minutes subtract.</returns>
        private long SubHour(long minutes)
        {
            this.Hour = this.hour - 1;
            long newMinutesValue = minutes - HourToMinuteRef;
            return newMinutesValue;
        }

        /// <summary>
        /// Add hours in current date/time and returns remaining minutes of total initial minutes adds. Ex.: AddHours(110) : returns 50.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long AddHour(long minutes)
        {
            this.Hour = this.hour + 1;
            long newMinutesValue = minutes - HourToMinuteRef;
            return newMinutesValue;
        }

        /// <summary>
        /// Subtract a day in current date/time and returns remaining minutes of total initial minutes adds.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will subtract in current date/time.</param>
        /// <returns>Remaining value of total initial minutes subtract.</returns>
        private long SubDay(long minutes)
        {
            this.Day = day - 1;
            long newMinutesValue = minutes - DayToMinuteRef;
            return newMinutesValue;
        }


        /// <summary>
        /// Add day in current date/time and returns remaining minutes of total initial minutes adds.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long AddDay(long minutes)
        {
            this.Day = day + 1;
            long newMinutesValue = minutes - DayToMinuteRef;
            return newMinutesValue;
        }

        /// <summary>
        /// Sub month in current date/time and returns remaining minutes of total initial minutes subtracts. 
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will subtract in current date/time.</param>
        /// <returns>Remaining value of total initial minutes subtracts.</returns>
        private long SubMonth(long minutes)
        {
            this.Month = month - 1;
            long newMinutesValue = minutes - MonthToMinuteRef;
            return newMinutesValue;
        }

        /// <summary>
        /// Add month in current date/time and returns remaining minutes of total initial minutes adds.
        /// </summary>
        /// <param name="minutes">Long value that represents the minutes that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long AddMonth(long minutes)
        {
            this.Month = month + 1;
            long newMinutesValue = minutes - MonthToMinuteRef;
            return newMinutesValue;
        }

        /// <summary>
        /// Subtract year in current date/time and returns remaining minutes of total initial minutes subtract.
        /// </summary>
        /// <param name="minutes">Long value that represents the year that will subtract in current date/time.</param>
        /// <returns>Remaining value of total initial minutes subtract.</returns>
        private long SubYear(long minutes)
        {
            this.Year = year - 1;
            long newMinutesValue = minutes - YearToMinuteRef;
            return newMinutesValue;
        }


        /// <summary>
        /// Add year in current date/time and returns remaining minutes of total initial minutes adds. Ex.: AddMinutes(61) : returns 01.
        /// </summary>
        /// <param name="minutes">Long value that represents the year that will adds in current date/time.</param>
        /// <returns>Remaining value of total initial minutes adds.</returns>
        private long AddYear(long minutes)
        {
            this.Year = year + 1;
            long newMinutesValue = minutes - YearToMinuteRef;
            return newMinutesValue;
        }

    }

}