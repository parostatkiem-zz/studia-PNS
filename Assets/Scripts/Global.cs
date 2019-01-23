using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Global : MonoBehaviour
{

    // Main Map object with all informations about current map states
    private Assets.Scripts.Map.Map gameMap;
    private ObjectRenderer objectRenderer = new ObjectRenderer();
    private List<Assets.Scripts.Map.MapObject> listOfMapObjects = new List<Assets.Scripts.Map.MapObject>();
    public Transform prefab_grass, prefab_water, prefab_sand, prefab_archer, prefab_swordsman, prefab_mutant, prefab_horseman, prefab_castle;

    private int userTurn = 0;

    private Assets.Scripts.Map.MapObject highlightedObject;
   
    public Assets.Scripts.Map.MapObject ListOfMapObjects
    {
        get
        {
            return this.ListOfMapObjects;
        }
    }
    public Assets.Scripts.Map.Map GameMap{
        get{
            return this.gameMap;
        }
    }

    public void HandleMapElementClick(Vector2 mapPos)
    {
        if (this.highlightedObject==null)
        {
            Debug.Log("no figure is selected");
            return;
        }

        var mapObjectAtPos = listOfMapObjects.FindLast(obj => (float)obj.x == mapPos.x && (float)obj.y == mapPos.y);

        if (mapObjectAtPos == null)
        {
            // something is standing in this place
            Debug.Log("something is standing in this place");
            return;
        }

        highlightedObject.x = (int)mapPos.x;
        highlightedObject.y = (int)mapPos.y;
    }

    public void HandleFigureHighlight(Vector2 mapPos)
    { 
        var selectedObj = listOfMapObjects.FindLast(obj => (float)obj.x == mapPos.x && (float)obj.y  == mapPos.y);
        if (selectedObj == null)
        {
            return;
        }
        if (selectedObj == highlightedObject)
        {
            // unnselect
            highlightedObject = null;
        }

    }

    // Use this for initialization
    void Start () {
       var filter= gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        this.gameMap = Assets.Scripts.Map.MapLoader.LoadMapFromJson(Assets.Scripts.Map.GlobalMapConfig.JsonMapPath);

        //set prefabs for objectRenderer
        objectRenderer.setPrefabs(prefab_archer, prefab_swordsman, prefab_mutant, prefab_horseman, prefab_castle);

        //  Instantiate(prefab_grass, new Vector3(0, 0.5f, 0), Quaternion.identity);
        var mapRenderer = new MapRenderer(GameMap, prefab_grass, prefab_water, prefab_sand, listOfMapObjects, objectRenderer);
        mapRenderer.RenderTheMap();
        // filter.mesh = MapRenderer.RenderTheMap();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void endTurn()
    {
        this.userTurn = (this.userTurn + 1) % 2;
        Debug.Log(this.userTurn);
    }
}
