using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Map
{
    static class MapLoader
    {
       
        public static Map LoadMapFromJson(String path)
        {
            try
            {
                using (StreamReader r = new StreamReader(path))
                {

                    string json = r.ReadToEnd();

                    return JsonConvert.DeserializeObject<Map>(json);
                }
            }catch(Exception ex){
                //TODO handleError
                return null;
            }
        }
    }
}
