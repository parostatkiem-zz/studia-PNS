using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Map
{
    class MapLoader : Behaviour
    {
        private PNSLogger logger = new PNSLogger("MapLoader - class");
       
        public Map LoadMapFromJson(String path)
        {
            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string json = r.ReadToEnd();
                    logger.Log("map " + path + " loaded");
                    return JsonConvert.DeserializeObject<Map>(json);
                }
            }
            catch (Exception ex)
            {
                logger.LogException(ex, this);
                return null;
            }
        }
    }
}
