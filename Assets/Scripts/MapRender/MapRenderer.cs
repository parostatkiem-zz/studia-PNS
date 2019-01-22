using System.Collections.Generic;
using UnityEngine;



public class MapRenderer :MonoBehaviour
{
    

    private float separator = 0.07f;
    private float square_xz = 1f;
    private float square_y = 0.21f;
    private float scale = 1f;

    private const float tableX = 10f;
    private const float tableZ = 8.5f; // table size is 10
    private const float tableStartX = 1 - tableX / 2f;
    private const float tableStartZ = -0.75f - tableZ / 2f;

    private Assets.Scripts.Map.Map map;
    private List<Assets.Scripts.Map.MapObject> listOfMapObjects;
    private List<Transform> prefabs;
    private ObjectRenderer objectRenderer;

    public MapRenderer(Assets.Scripts.Map.Map map, 
        Transform prefab_grass, Transform prefab_water, Transform prefab_sand,
        List<Assets.Scripts.Map.MapObject> listOfMapObjects, ObjectRenderer objectRenderer)
    {
        this.map = map;
        this.listOfMapObjects = listOfMapObjects;
        this.objectRenderer = objectRenderer;
        
        prefabs = new List<Transform> { prefab_grass,prefab_water,prefab_sand};
       
    }

    public void RenderTheMap()
    {
        int mapElementIndex = 0;


        //set default map size
        float heightOfElements = (float)map.height * (square_xz + separator);
        float widthOfElements = (float)map.width * (square_xz + separator);
        float zScale = 1f;
        float xScale = 1f;

        //find fine scale multiplayer
        if (heightOfElements > tableZ)
        {
            zScale = tableZ / heightOfElements;
        } 
        if (widthOfElements > tableX)
        {
            xScale = tableX / widthOfElements;
        }
        scale = (zScale > xScale) ? xScale : zScale;

        //set value of draw element with separator
        float mapElement = (square_xz + separator) * scale;

        //set start point for prefab 
        float startXOfMap = ( tableStartX + (tableX - (widthOfElements * scale)) / 2 ) + mapElement / 2;
        float startZOfMap = ( tableStartZ + (tableZ - (heightOfElements * scale)) / 2 ) + mapElement / 2;

        for (var x = 0; x < map.width; x++)
        {
            for (var y = 0; y < map.height; y++, mapElementIndex++)
            {
                ///map element rendering
                ///
                var elementInstance= Instantiate(
                    prefabs[(int)map.mapElements[mapElementIndex].terrainType],       
                    new Vector3(
                        x: startXOfMap + x * mapElement, 
                        y: 0.0f,
                        z: startZOfMap + y * mapElement), 
                    Quaternion.identity);

                //scale all map elements
                elementInstance.transform.localScale = new Vector3(
                    square_xz * scale,
                    square_y * scale,
                    square_xz * scale
                    );

                elementInstance.GetComponent<MapElement>().mapCords = new Vector2(x, y);

                ///map object rendering
                ///
                if(map.mapElements[mapElementIndex].mapObject != null && !(map.mapElements[mapElementIndex].mapObject is Assets.Scripts.Map.EmptyField))
                {
                    map.mapElements[mapElementIndex].mapObject.x = x;
                    map.mapElements[mapElementIndex].mapObject.y = y;
                    this.listOfMapObjects.Add(map.mapElements[mapElementIndex].mapObject);

                    this.objectRenderer.RenderObjectAtPos(
                        startXOfMap + x * mapElement, 
                        0.5f, 
                        startZOfMap + y * mapElement,
                        map.mapElements[mapElementIndex].mapObject.objType
                        );
                }

            }
        }

        this.objectRenderer.setParameters(this.map, this.listOfMapObjects, this.scale, this.square_xz, this.separator);
    }


   
}
