using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

public class TurnInfoExtenderUI : MonoBehaviour
{
    PNSLogger logger = new PNSLogger("TurnInfoExtenderUI - script");

    public Text infoField;

    public string redTurn = "Turn: Red";
    public string blueTurn = "Turn Blue";

    public void Start()
    {
        infoField.text = redTurn;
    }

    public void updateField()
    {
        logger.Log("updatting turn info UI");
        if (infoField.text.Equals(redTurn))
            infoField.text = blueTurn;
        else
            infoField.text = redTurn;
    }
}
