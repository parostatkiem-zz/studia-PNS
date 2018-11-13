using Newtonsoft.Json;

namespace Assets.Scripts.Map
{
    public enum TerrainType
    {
        grass = 0,
        water = 1,
        forest = 2,
        hills = 3
    }

    public class EmptyField : MapObject
    {

    }

    public class Castle : MapObject
    {

    }

    public class Warrior : MapObject
    {

    }

    public class Archer : MapObject
    {

    }

    public class HorseMan : MapObject
    {

    }

    public class GoldMine : MapObject
    {

    }

    [JsonConverter(typeof(MapObjectConverter))]
    public abstract class MapObject
    {
        public int objType { get; set; }
        public bool fraction { get; set; }
        public string name { get; set; }
        public int health { get; set; }
        public int deffend { get; set; }
        public int attack { get; set; }
    }


    public class MapElement
    {
        public TerrainType terrainType;
        public MapObject mapObject;
    }

    public class Map
    {
        public int hight;
        public int width;
        public MapElement[] mapElements;
    }
}