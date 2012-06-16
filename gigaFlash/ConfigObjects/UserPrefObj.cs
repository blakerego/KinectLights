using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace gigaFlash.ConfigObjects
{
    public class UserPrefObj
    {
        public string Username;

        public string DateLastModfied;

        public string DateCreated;

        public List<ColorConfig> Colors = new List<ColorConfig>();

        public List<RoomConfig> RoomConfigs = new List<RoomConfig>();

        public static string ConfigPath = "C:/svn/configs/";
    }
}
