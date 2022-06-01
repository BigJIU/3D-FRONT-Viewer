using System;
using System.Collections.Generic;

namespace NewFrontData
{


    [Serializable]
    public class NewFrontDataStructure
    {

        public string id;
        public int[] up;
        public int[] front;
        public int scaleToMeters;
        public Level[] levels;

    }
    [Serializable]
    public class Level
    {
        public string id;
        public bbox bbox;
        public Node[] nodes;
        public string title;
        public string type;
        public float[] size;
    }
    [Serializable]
    public class Node
    {
        public string id;
        public string type;
        public int valid;
        public string modelId;
        public string[] roomTypes;
        public float[] transform;
        public string[] materials;
        public int state;
        public bbox bbox;
        public int[] nodeIndices;
        public string instanceid;
    }
    [Serializable]
    public class bbox
    {
        public float[] min;
        public float[] max;
    }

}