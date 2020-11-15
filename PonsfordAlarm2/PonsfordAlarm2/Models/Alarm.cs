using System;
using System.Collections.Generic;
using System.Text;

namespace PonsfordAlarm2.Models
{
    public class Alarm
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        private TimeSpan _alarmTime;
        public TimeSpan AlarmTime
        {
            get
            {
                return _alarmTime;
            }
            set
            {
                _alarmTime = value;
            }
        }

        public Alarm New(
            string alarmName,
            TimeSpan alarmTime)
        {
            return new Alarm
            {
                AlarmTime = alarmTime,
                Name = alarmName
            };
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
