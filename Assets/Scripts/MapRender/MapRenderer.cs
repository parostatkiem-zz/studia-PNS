using UnityEngine;
using System.Collections.Generic;



public class MapRenderer :MonoBehaviour
{
    private float offset = 0.07f;
    private float square_size = 1f; //TODO
    private Assets.Scripts.Map.Map map;
     private List<Transform> prefabs;

    public MapRenderer(Assets.Scripts.Map.Map map, Transform prefab_grass, Transform prefab_water, Transform prefab_sand)
    {
        this.map = map;
     
        prefabs = new List<Transform> { prefab_grass,prefab_water,prefab_sand};
       
    }

    public void RenderTheMap()
    {
        int mapElementIndex = 0;
        for (var x = 0; x < map.width; x++)
        {
            for (var y = 0; y < map.height; y++, mapElementIndex++)
            {
                Instantiate(prefabs[(int)map.mapElements[mapElementIndex].terrainType],
                            new Vector3(x: x * (square_size + offset), y: 0.0f,
                                        z:  y * (square_size+offset)), Quaternion.identity);
             
            }
        }
    }
   
}
