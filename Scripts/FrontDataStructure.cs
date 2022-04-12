using System;
using System.Collections.Generic;

namespace FrontData
{


    [Serializable]
    public class FrontDataStructure
    {

        public string uid;
        public string jobid;
        public string design_version;
        public string code_version;
        public float[] north_vector;
        public Furniture[] furniture;
        public Mesh[] mesh;
        public Material[] material;
        public Light[] lights;
        public Extension extension;
        public Scene scene;
        public Object[] groups;
        public string[] materialList;
        public string version;

    }
    [Serializable]
    public class Furniture
    {
        public string uid;
        public string jid;
        public string[] aid;
        public string title;
        public string type;
        public float[] size;
    }
    [Serializable]
    public class Mesh
    {
        public string[] aid;
        public string jid;
        public string uid;
        public float[] xyz;
        public float[] normal;
        public float[] uv;
        public float faces;
        public string material;
        public string type;
        public string constructid;
        public string instanceid;
    }
    [Serializable]
    public class Material
    {
        public string uid;
        public string[] aid;
        public string jid;
        public string texture;
        public string normaltexture;
        public int[] color;
        public float seamWidth;
        public bool useColor;
        public float[] normalUVTransform;
        public string[] contentType;
    }
    [Serializable]
    public class Light
    {
        public string uid;
        public string nodeType;
        public string roomId;
        public float[] src_position;
        public float[] direction;
        public float[] up_vector;
        public int color_temperature;
        public int multiplier;
        public bool DoubleSided;
        public bool skylightPortal;
        public float size0;
        public float size1;
        public int type;
        public bool enabled;
        public int units;
        public bool affectSpecular;
        public string hostInstanceId;
    }
    [Serializable]
    public class Extension
    {
        //hard to describe
        
    }
    [Serializable]
    public class Scene
    {
        public string @ref;
        public float[] pos;
        public float[] rot;
        public float[] scale;
        public Room[] room;
    }
    [Serializable]
    public class Room
    {
        public string type;
        public string instanceid;
        public float[] pos;
        public float[] rot;
        public float[] scale;
        public Model[] children;
        public int empty;
    }
    [Serializable]
    public class Model
    {
        public string @ref;
        public float[] pos;
        public float[] rot;
        public float[] scale;
        public string instanceid;
        public string replace_jid;
        public ModelBox replace_bbox;
    }

    [Serializable]
    public class ModelBox
    {
        public float xLen;
        public float yLen;
        public float zLen;
    }

}