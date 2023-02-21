using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ежедневник
{
    internal class files
    {
        public static void MySerialize<T>(T notes, string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(notes);
            File.WriteAllText(desktop +"\\"+ filename, json);
        }
        public static T MyDeserialize<T>(string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(desktop+"\\"+filename);
            T notes = JsonConvert.DeserializeObject<T>(json);
            return notes;
        }
    }
}
