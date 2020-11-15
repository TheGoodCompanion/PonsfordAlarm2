using PonsfordAlarm2.Models;
using PonsfordAlarm2.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PonsfordAlarm2.ViewModels
{
    public class AlarmViewModel
    {
        private AlarmRepository _repo;

        public ICommand AddAlarmCommand => new Command(AddAlarm);
        public ObservableCollection<Alarm> Alarms {get;set;}
        public string Name { get; set; }
        public TimeSpan AlarmTime { get; set; }

        public AlarmViewModel()
        {
            _repo = new AlarmRepository();

            Alarms = new ObservableCollection<Alarm>();
            foreach(var alarm in _repo.GetAllAlarms())
            {
                Alarms.Add(alarm);
            }
        }

        public async void AddAlarm()
        {
            await _repo.UpdateAlarms(new Alarm().New(Name, AlarmTime));
            Alarms = new ObservableCollection<Alarm>();
            foreach (var alarm in _repo.GetAllAlarms())
            {
                Alarms.Add(alarm);
            }
            new Page1().ReturnToList();
        }

    }
}
