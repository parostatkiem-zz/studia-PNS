using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace Assets.Scripts.Map
{
    public class MapObjectConverter : JsonConverter
    {
        static JsonSerializerSettings SpecifiedSubclassConversion = new JsonSerializerSettings() { ContractResolver = new MapObjectSpecifiedConcreteClassConverter() };

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(MapObject));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            switch (jo["objType"].Value<int>())
            {
                case 1:
                    return null;
                //    return JsonConvert.DeserializeObject<EmptyField>(jo.ToString(), SpecifiedSubclassConversion);
                case 2:
                    return JsonConvert.DeserializeObject<Castle>(jo.ToString(), SpecifiedSubclassConversion);
                case 3:
                    return JsonConvert.DeserializeObject<Warrior>(jo.ToString(), SpecifiedSubclassConversion);
                case 4:
                    return JsonConvert.DeserializeObject<Archer>(jo.ToString(), SpecifiedSubclassConversion);
                case 5:
                    return JsonConvert.DeserializeObject<HorseMan>(jo.ToString(), SpecifiedSubclassConversion);
                //case 6:
                //    return JsonConvert.DeserializeObject<GoldMine>(jo.ToString(), SpecifiedSubclassConversion);
                default:
                    throw new Exception();
            }
            throw new NotImplementedException();
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException(); // won't be called because CanWrite returns false
        }
    }
}