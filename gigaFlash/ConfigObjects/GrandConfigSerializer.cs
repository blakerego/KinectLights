using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace gigaFlash.ConfigObjects
{
    public enum ConfigOrientation
    {
        fromString,
        fromDisk
    }

    public class GrandConfigSerializer<ConfigType>
    {
        /// <summary>
        /// Loads xml from string
        /// </summary>
        /// <param name="pXml">
        /// xmlAsString if in fromString mode. 
        /// path if in fromDiskMode
        /// </param>
        /// <returns></returns>
        public static ConfigType FromXML(string pOrigination, ConfigOrientation pOrientation)
        {
            XmlSerializer serial = new XmlSerializer(typeof(ConfigType));
            ConfigType deserializeXML = default(ConfigType); 
            if (pOrientation == ConfigOrientation.fromString)
            {
                StringReader sr = new StringReader(pOrigination);
                deserializeXML = (ConfigType)serial.Deserialize(sr);
            }
            else if (pOrientation == ConfigOrientation.fromDisk)
            {
                StreamReader sr = new StreamReader(pOrigination);
                try
                {
                    deserializeXML = (ConfigType)serial.Deserialize(sr);
                }
                catch (InvalidOperationException)
                {
                    // Deserialization failed. 
                }
                sr.Close(); 

            }
            return deserializeXML;
        }

        public static string ToXML(object passObject)
        {
            try
            {
                XmlSerializer serial = new XmlSerializer(typeof(ConfigType));
                StringWriter sw = new StringWriter();
                serial.Serialize(sw, passObject);
                return sw.ToString();
            }
            catch (Exception exc)
            {
                return exc.ToString();
            }
        }
    }
}
