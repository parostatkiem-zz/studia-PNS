using Newtonsoft.Json;
using UnityEngine;

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

    public class Warrior : MapObject,IMilitaryUnit
    {
        float movementRange = 1.5f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }

    public class Archer : MapObject, IMilitaryUnit
    {
        float movementRange = 1.5f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }

    public class HorseMan : MapObject, IMilitaryUnit
    {
        float movementRange =3.9f;
    public float MovementRange { get { return movementRange; } set { movementRange = value; } }

    }

    public class GoldMine : MapObject
    {

    }

    [JsonConverter(typeof(MapObjectConverter))]
    public abstract class MapObject
    {
        public GameObject instance { get; set; }
        public bool isHighlighted { get; set; }
        public int ownerID { get; set; }
        public int x { get; set; }
        public int y { get; set; }
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
        public bool hasTrees;
    }

    public class Map
    {
        public int height;
        public int width;
        public MapElement[] mapElements;
        
    }
    public interface IMilitaryUnit
    {
        float MovementRange { get; set; }
    }
}
