using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PonsfordAlarm2.Helpers;
using PonsfordAlarm2.Models;

namespace PonsfordAlarm2.Repositories
{
    public class AlarmRepository
    {

        private SaveDataHelper _saveDataHelper;

        public AlarmRepository()
        {
            _saveDataHelper = new SaveDataHelper();
        }

        public List<Alarm> GetAllAlarms()
        {
            var alarmsJson = _saveDataHelper.ReadData("alarms");
            if(alarmsJson != null)
            {
                var listAlarms = JsonConvert.DeserializeObject<List<Alarm>>(alarmsJson);
                return listAlarms;
            }
            else
            {
                return new List<Alarm>();
            }

        }

        public async Task<bool> UpdateAlarms(Alarm alarm)
        {
            var alarms = GetAllAlarms();

            alarms.Add(alarm);
            var alarmsJson = JsonConvert.SerializeObject(alarms);
            await _saveDataHelper.SaveData("alarms", alarmsJson);

            return true;
        }

    }
}
