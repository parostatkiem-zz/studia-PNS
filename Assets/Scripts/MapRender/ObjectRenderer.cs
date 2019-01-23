using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour
{
    private Assets.Scripts.Map.Map map;
    private List<Assets.Scripts.Map.MapObject> listOfMapObjects;

    private List<Transform> prefabs;

    private float separator;
    private float square_xz;

    private float scale;

    public ObjectRenderer() { }

    public ObjectRenderer(Assets.Scripts.Map.Map map, List<Assets.Scripts.Map.MapObject>  listOfMapObjects, float scale, float square_xz, float separator)
    {
        setParameters(map, listOfMapObjects, scale, square_xz, separator);
    }

    public void setPrefabs(Transform prefab_archer, Transform prefab_swordsman, Transform prefab_mutant, Transform prefab_horseman, Transform prefab_castle)
    {
        this.prefabs = new List<Transform>{ prefab_castle, prefab_swordsman,  prefab_archer, prefab_horseman };
    }

    public void setParameters(Assets.Scripts.Map.Map map, List<Assets.Scripts.Map.MapObject> listOfMapObjects, float scale, float square_xz, float separator)
    {
        this.map = map;
        this.listOfMapObjects = listOfMapObjects;
        this.separator = separator;
        this.square_xz = square_xz;
        this.scale = scale;
    }

    public void RenderObjectAtPos(float xPos, float yPos, float zPos, int objTypeID, int ownerID,Vector2 mapPosition)
    {
        var elementInstance = Instantiate(
                   prefabs[objTypeID-2],
                   new Vector3(
                       x: xPos,
                       y: 0.35f,
                       z: zPos),
                   prefabs[objTypeID - 2].rotation);

      
        Color highlightColor=Color.yellow;
        if(ownerID==0)
        {
            highlightColor = Color.red;
        }

        if (ownerID == 1)
        {
            highlightColor = Color.blue;
        }
        elementInstance.GetComponent<Renderer>().material.SetColor("_Color",highlightColor);
        elementInstance.GetComponent<militaryBehavior>().mapPosition = mapPosition;

        //elementInstance.GetComponent<MapElement>().mapCords = new Vector2(x, y);
    }

    public void RenderTheMapObjects()
    {

    }
}

