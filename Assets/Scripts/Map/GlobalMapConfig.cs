using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Map
{
    static class GlobalMapConfig
    {
        private static string jsonMapPath = "Assets/Maps/1.json";

        public static string JsonMapPath
        {
            get
            {
                return GlobalMapConfig.jsonMapPath;
            }
        }
    }
}
