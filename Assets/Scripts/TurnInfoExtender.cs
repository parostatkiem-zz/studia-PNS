using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts
{
    class TurnInfoExtender : MonoBehaviour
    {
        public Text infoField;

        public string redTurn = "Turn: Red";
        public string blueTurn = "Turn Blue";

        public void Start()
        {
            infoField.text = redTurn;
        }

            public void updateField()
        {
            if (infoField.text.Equals(redTurn))
                infoField.text = blueTurn;
            else
                infoField.text = redTurn;
        }
    }
}
