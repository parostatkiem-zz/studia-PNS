using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Map
{
    static class MapLoader
    {
        public static string mapString = @"{
    ""hight"": 2,
    ""width"": 2,
  ""mapElements"": [
    {
      ""terrainType"": 0,
      ""mapObject"": {
            ""objType"":1,
        }
    },
    {
     ""terrainType"": 0,
      ""mapObject"": {
            ""objType"":2,
            ""attack"":15
        }
    },
    {
     ""terrainType"": 0,
     ""mapObject"": {
            ""objType"":3,
            ""attack"":15
        }
    },
    {
      ""terrainType"": 0,
      ""mapObject"": {
            ""objType"":4,
            ""attack"":15
        }
    }
  ]}";
        public static Map LoadMapFromJson(String path)
        {
            return JsonConvert.DeserializeObject<Map>(mapString);
        }
    }
}
