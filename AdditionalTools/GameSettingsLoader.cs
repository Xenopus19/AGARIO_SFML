using System;
using System.Text;
using System.Reflection;

namespace Agario.AdditionalTools;
public static class GameSettingsLoader
{
    private static string SettingsFilePath = "config.ini";

    public static void LoadConfig()
    {
        foreach(string line in File.ReadLines(SettingsFilePath))
        {
            string[] lineParts = line.Split('=');
            if(lineParts.Length == 2)
            {
                FieldInfo field = typeof(Game).GetField(lineParts[0]);
                if (field == null) continue;

                if(field.FieldType == typeof(int) && Int32.TryParse(lineParts[1], out int fieldValue))
                {
                    field.SetValue(Game.GetInstance(), fieldValue);
                }
                else if(field.FieldType == typeof(string))
                {
                    field.SetValue(Game.GetInstance(), lineParts[1]);
                }
            }
        }
    }
}
