using System;
using System.IO;
using System.Threading.Tasks;

namespace PonsfordAlarm2.Helpers
{
    public class SaveDataHelper
    {

        public SaveDataHelper()
        {

        }

        public string ReadData(string name)
        {
            try
            {
                
                var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), $"{name}.json");
                if (path == null || !File.Exists(path))
                {
                    return null;
                }
                string FileData = string.Empty;
                using (var reader = new StreamReader(path, true))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        FileData = line;
                    }
                }

                return FileData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task SaveData(string name, string data)
        {
            var fileName = $"{name}.json";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, fileName);

            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                using (var writer = File.CreateText(path))
                {
                    await writer.WriteLineAsync(data);
                }
            }
            else
            {
                File.Delete(path);
                using (var writer = File.CreateText(path))
                {
                    await writer.WriteLineAsync(data);
                }
            }
        }

    }
}
