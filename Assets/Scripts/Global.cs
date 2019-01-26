using Assets.Scripts.Map;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Global : MonoBehaviour
{

    // Main Map object with all informations about current map states
    private Assets.Scripts.Map.Map gameMap;
    private ObjectRenderer objectRenderer = new ObjectRenderer();

    private CameraBehavior cameraBehavior;


    public Light MapElementHighlight;
    public List<Assets.Scripts.Map.MapObject> listOfMapObjects { get; set; } 
    public Transform prefab_trees,prefab_grass, prefab_water, prefab_sand, prefab_archer, prefab_swordsman, prefab_mutant, prefab_horseman, prefab_castle;
 
    private int userTurn = 0;


    private int UserTurn 
    {
        get{ return userTurn; }
        set
        {
            userTurn = value;
        }
    }

    public Camera mainCamera;


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

    public void HandleMapElementClick(Vector2 mapPos, string terrainType=null)
    {
        if (highlightedObject==null)
        {
            Debug.Log("no figure is selected");
            return;
        }

        var mapObjectAtPos = listOfMapObjects.FindLast(obj => (float)obj.x == mapPos.x && (float)obj.y == mapPos.y);

        if (mapObjectAtPos != null)
        {
            // something is standing in this place
            Debug.Log("something is standing in this place");
            // TODO: detect clicking on castles
            return;
        }

        MoveFigure(highlightedObject, mapPos,terrainType);

    }

    public void HandleFigureHighlight(Assets.Scripts.Map.MapObject selectedObj)
      { 
        if (selectedObj == null)
        {
            Debug.LogError("Selected object not found in the list");
            return;
        }

        if (selectedObj == highlightedObject)
        {
            Debug.Log("Unselecting object");

            cameraBehavior.ResetCamera();

            highlightedObject.isHighlighted = false;
            highlightedObject = null;
            objectRenderer.UpdateObjects();
            return;
        }

        if ( selectedObj.ownerID != userTurn)
        {
            // player wants to select the unit he doesn't own 
            return;
        }

        cameraBehavior.SetCameraOverTransform(selectedObj.instance.transform);


        highlightedObject = selectedObj;
        foreach(var obj in listOfMapObjects)
        {
            obj.isHighlighted = false;
        }
        highlightedObject.isHighlighted = true;

        objectRenderer.UpdateObjects();
    }

    // Use this for initialization
    void Start () {
        MapElementHighlight.enabled = false;
        cameraBehavior = mainCamera.GetComponent<CameraBehavior>();
        listOfMapObjects = new List<Assets.Scripts.Map.MapObject>();

        var filter= gameObject.AddComponent<MeshFilter>();
        gameObject.AddComponent<MeshRenderer>();
        this.gameMap = Assets.Scripts.Map.MapLoader.LoadMapFromJson(Assets.Scripts.Map.GlobalMapConfig.JsonMapPath);

        //set prefabs for objectRenderer
        objectRenderer.setPrefabs(prefab_archer, prefab_swordsman, prefab_mutant, prefab_horseman, prefab_castle);

        var mapRenderer = new MapRenderer(GameMap,prefab_trees, prefab_grass, prefab_water, prefab_sand, listOfMapObjects, objectRenderer);
        mapRenderer.RenderTheMap();
        UserTurn = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndTurn()
    {

        var numberOfPlayers = 2;
        this.UserTurn = (this.UserTurn + 1) % numberOfPlayers;
        if (highlightedObject != null)
        { HandleFigureHighlight(highlightedObject); }

        Debug.Log(this.userTurn);
    }

    private void MoveFigure(Assets.Scripts.Map.MapObject figure, Vector2 newPos, string terrainType=null)
     {
    
        if (!CanFigureMoveTo(figure,newPos,terrainType))
        {
            // can't go that far
            return;
        }
        figure.x = (int)newPos.x;
        figure.y = (int)newPos.y;
        objectRenderer.UpdateObjects();
    }

    private bool CanFigureMoveTo(Assets.Scripts.Map.MapObject figure, Vector2 newPos, string terrainType)
    {
        if (newPos == new Vector2(figure.x, figure.y)) { return false; }
        if (terrainType == "terrain:water") { return false; }

        var currentPos = new Vector2(figure.x, figure.y);
        var maxDistance = ((Assets.Scripts.Map.IMilitaryUnit)figure).MovementRange;
        return Vector2.Distance(currentPos, newPos) <= maxDistance;
    }

    public void HandleMouseOverMapElement(Vector2 coords, Vector3 realWorldPos,string terrainType=null)
    {
        if ((IMilitaryUnit)highlightedObject == null) { return; }

        if (CanFigureMoveTo(highlightedObject, coords,terrainType))
        {
            MapElementHighlight.color = Color.green;
        }
        else
        {
            MapElementHighlight.color = Color.red;
        }
        MapElementHighlight.transform.position = realWorldPos + new Vector3(0, 0.3f, 0);
        MapElementHighlight.enabled = true;
    
    }
}
