using Newtonsoft.Json;
using PlanetTweaks2.UI;
using System;
using System.IO;
using UnityEngine;

namespace PlanetTweaks2
{
    public partial class Settings
    {
        private static T[] Arr<T>(T item, int count)
        {
            var arr = new T[count];
            for (int i = 0; i < count; i++)
                arr[i] = item;
            return arr;
        }

        public string _comment_kr = "불 행성과 얼음 행성의 색은 얼불춤 기본 설정에 저장됩니다.";
        public string _comment_en = "Fire Planet and Ice Planet's Colors are Saved in Adofai Built-in Settings";
        public bool shortcutCtrl          = false;
        public bool shortcutAlt           = false;
        public bool shortcutShift         = false;
        public KeyCode shortcutKey        = KeyCode.F1;
        public Color thirdPlanetColor     = Colors.greenColor;
        public float[] planetAlphaArr     = Arr(1f, 3);
        public Color[] tailColorArr       = Arr(Colors.disableColor, 3);
        public float[] tailAlphaArr       = Arr(1f, 3);
        public Color[] ringColorArr       = Arr(Colors.disableColor, 3);
        public float[] ringAlphaArr       = Arr(.4f, 3);
        public bool thirdSamurai          = false;
        public bool thirdEmoji            = false;
        [JsonIgnore]
        public PlanetImage[] imageArr     = new PlanetImage[3];
        public Vector2[] imagePositionArr = new Vector2[3];
        public Vector2[] imageSizeArr     = Arr(Vector2.one * 100, 3);
        public bool[] imageFixRotationArr = new bool[3];

        public void Save(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var filePath = Path.Combine(dir, "Settings.json");

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ColorJsonConverter());
            jsonSettings.Converters.Add(new Vector2JsonConverter());
            jsonSettings.Formatting = Formatting.Indented;

            var result = JsonConvert.SerializeObject(this, jsonSettings);

            File.WriteAllText(filePath, result);

            for (int i = 0; i < imageArr.Length; i++)
            {
                foreach (var file in Directory.GetFiles(dir))
                    if (Path.GetFileNameWithoutExtension(file) == $"PlanetImage_{i}")
                        File.Delete(file);
                imageArr[i]?.Save(Path.Combine(dir, $"PlanetImage_{i}" + imageArr[i].GetExtension()));
            }
        }

        public static Settings Load(string dir)
        {
            var filePath = Path.Combine(dir, "Settings.json");

            if (!File.Exists(filePath))
                return new Settings();

            var jsonSettings = new JsonSerializerSettings();
            jsonSettings.Converters.Add(new ColorJsonConverter());
            jsonSettings.Converters.Add(new Vector2JsonConverter());

            var content = File.ReadAllText(filePath);

            var settings = JsonConvert.DeserializeObject<Settings>(content, jsonSettings);

            for (int i = 0; i < settings.imageArr.Length; i++)
            {
                string imageFile = null;
                foreach (var file in Directory.GetFiles(dir))
                    if (Path.GetFileNameWithoutExtension(file) == $"PlanetImage_{i}")
                    {
                        imageFile = file;
                        break;
                    }
                if (imageFile != null)
                    settings.imageArr[i] = PlanetImage.Create(imageFile);
            }

            return settings;
        }

        public class ColorJsonConverter : JsonConverter<Color>
        {
            public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var arr = serializer.Deserialize<float[]>(reader);

                return new Color(arr[0], arr[1], arr[2], arr[3]);
            }

            public override void WriteJson(JsonWriter writer, Color value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, new float[] { value.r, value.g, value.b, value.a });
            }
        }

        public class Vector2JsonConverter : JsonConverter<Vector2>
        {
            public override Vector2 ReadJson(JsonReader reader, Type objectType, Vector2 existingValue, bool hasExistingValue, JsonSerializer serializer)
            {
                var arr = serializer.Deserialize<float[]>(reader);

                return new Vector2(arr[0], arr[1]);
            }

            public override void WriteJson(JsonWriter writer, Vector2 value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, new float[] { value.x, value.y });
            }
        }
    }
}
