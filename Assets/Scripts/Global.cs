using Assets.Scripts.Map;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.UI;


public class Global : MonoBehaviour
{

    // Main Map object with all informations about current map states
    private Assets.Scripts.Map.Map gameMap;
    private ObjectRenderer objectRenderer = new ObjectRenderer();

    private CameraBehavior cameraBehavior;

    public Light MapElementHighlight;

    public List<Assets.Scripts.Map.MapObject> listOfMapObjects { get; set; } 
    public Transform prefab_trees,prefab_grass, prefab_water, prefab_sand, prefab_archer, prefab_swordsman, prefab_mutant, prefab_horseman, prefab_castle;
    private WinNotificationUI winNotification;
 
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
            if (mapObjectAtPos.ownerID == highlightedObject.ownerID) return;
            // something is standing in this place
            Debug.Log("something is standing in this place, I decide to attack this");

            mapObjectAtPos.AttackThisObject(highlightedObject.attack);
            if (mapObjectAtPos.health <= 0)
            {
                //scenario of win
                if(mapObjectAtPos.objType == 2)
                {
                    winNotification.ShowWinAdnotacion(highlightedObject.ownerID);
                }


                Destroy(mapObjectAtPos.instance);

                mapPos.x = mapObjectAtPos.x;
                mapPos.y = mapObjectAtPos.y;

                listOfMapObjects.Remove(mapObjectAtPos);
            }
            else
            {
                highlightedObject.isReadyToMove = false;
                return;
            }

            // TODO: detect clicking on castles
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
            // unselect object
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


        winNotification = (WinNotificationUI)GameObject.Find("WinNotificationPanel").GetComponent(typeof(WinNotificationUI));
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

        //reset all isReadyToUse fields of current player
        foreach(Assets.Scripts.Map.MapObject figure in listOfMapObjects)
        {
            if (figure.ownerID == this.UserTurn)
                figure.isReadyToMove = true;
        }


        Debug.Log(this.userTurn);
    }

    private void MoveFigure(Assets.Scripts.Map.MapObject figure, Vector2 newPos, string terrainType=null)
     {   
        if (!CanFigureMoveTo(figure,newPos,terrainType))
        {
            // can't go that far or is used now
            return;
        }



        figure.x = (int)newPos.x;
        figure.y = (int)newPos.y;
        figure.isReadyToMove = false;
        objectRenderer.UpdateObjects();
    }


    private bool CanFigureMoveTo(Assets.Scripts.Map.MapObject figure, Vector2 newPos, string terrainType)
    {
        if (figure is Castle) { return false; }
        if (!figure.isReadyToMove){ return false; }//is used now
        if (newPos == new Vector2(figure.x, figure.y)) { return false; }
        if (terrainType == "terrain:water") { return false; }

        var currentPos = new Vector2(figure.x, figure.y);
        var maxDistance = ((Assets.Scripts.Map.IMilitaryUnit)figure).MovementRange;
        return Vector2.Distance(currentPos, newPos) <= maxDistance;
    }

    public void HandleMouseOverMapElement(Vector2 coords, Vector3 realWorldPos,string terrainType=null)
    {
        if ((IMilitaryUnit)highlightedObject == null || highlightedObject.objType == 2) { return; }

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
