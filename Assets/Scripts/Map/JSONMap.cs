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


    public class Castle : MapObject, IMilitaryUnit
    {
        private float movementRange = 0.0f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }

    public class Warrior : MapObject,IMilitaryUnit
    {
        private float movementRange = 1.5f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }

    public class Archer : MapObject, IMilitaryUnit
    {
        private float movementRange = 1.5f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }

    public class HorseMan : MapObject, IMilitaryUnit
    {
        private float movementRange =3.9f;
        public float MovementRange { get { return movementRange; } set { movementRange = value; } }
    }
    /*
    public class GoldMine : MapObject
    {

    }

    public class EmptyField : MapObject
    {

    }
    */
    [JsonConverter(typeof(MapObjectConverter))]
    public abstract class MapObject
    {
        //Map fields
        public GameObject instance { get; set; }
        public bool isHighlighted { get; set; }
        public int x { get; set; }
        public int y { get; set; }

        //Json fields
        public int ownerID { get; set; }
        public int objType { get; set; }
        public int health { get; set; }
        public int attack { get; set; }
        public bool isReadyToMove { get; set; }

        //Methods
        public void AttackThisObject(int value)
        {
            this.health -= value;
        }
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
