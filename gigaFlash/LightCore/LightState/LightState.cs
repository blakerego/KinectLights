using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing; 

namespace gigaFlash
{
    public class LightState : ILightState
    {
        
        #region Constructors 
        public LightState(int pNumberOfLights)
            : this(pNumberOfLights, new Connection()) { }

        public LightState(int pNumberOfLights, Connection pConnection)
        {
            mNumberOfLights = pNumberOfLights;
            mLights = new List<Light>();
            for (int i = 0; i < pNumberOfLights; i++)
            {
                Light l = new Light(0, 0, 0);
                mLights.Add(l); 
            }
            mConnection = pConnection; 
        }
        #endregion

        #region Packet Header
        byte[] GetPacketHeaderAsByteArray()
        {            
            byte[] h1 = BitConverter.GetBytes(0x4adc0104);
            byte[] h2 = BitConverter.GetBytes((ushort)0x0001);
            byte[] h3 = BitConverter.GetBytes((ushort)0x0101);
            byte[] h4 = new byte[4];
            byte[] h5 = new byte[1];
            byte[] nullByte = new byte[] { 0 }; 
            byte[] h6 = new byte[2];
            byte[] h7 = BitConverter.GetBytes(-1);
            byte[] h8 = new byte[1];
            IEnumerable<byte> myBytes = h1.Concat(h2).Concat(h3).Concat(h4).Concat(h5).Concat(nullByte).Concat(h6).Concat(h7).Concat(h8);
            return myBytes.ToArray(); 
        }
        #endregion 

        #region Public Methods
        public void Update(Color color)
        {
            foreach (Light light in mLights)
            {
                light.Red = color.R;
                light.Green = color.G;
                light.Blue = color.B; 
            }
            Update(); 
        }
        public void Update()
        {
            //We need a byte array that has a slot for each color of for each light.
            byte[] lightArray = new byte[3 * mLights.Count];

            //Initialize the array based on the lights in our collection. 
            foreach (Light light in mLights)
            {
                int addr = 3 * mLights.IndexOf(light);
                lightArray[addr] = (byte)light.Red;
                lightArray[addr + 1] = (byte)light.Green;
                lightArray[addr + 2] = (byte)light.Blue;
            }

            IEnumerable<byte> outputList = GetPacketHeaderAsByteArray().Concat(lightArray);
            byte[] outString = outputList.ToArray<byte>();
            mConnection.Send(outString);
        }

        public void Clear()
        {
            foreach (Light l in Lights)
            {
                l.Red = 0;
                l.Blue = 0;
                l.Green = 0;
            }
            Update(); 
        }
        #endregion

        #region Members / Properties
        protected int mNumberOfLights;

        protected Connection mConnection;

        protected List<Light> mLights;
        public List<Light> Lights
        {
            get { return mLights; }
            set { mLights = value; }
        }
        #endregion 
    
    }
}
