using System.Collections;
using System.Collections.Generic;
using System.IO;
using Dummiesman;
using FrontData;
using UnityEngine;

public class SceneImporter : MonoBehaviour
{
    // Start is called before the first frame update
    //public static FrontDataStructure data;
    public static Dictionary<string,Furniture> furnitureDic = new Dictionary<string, Furniture>(); //key:uid value:furniture
    public static FrontDataStructure data;
    void Awake()
    {
        string path =
            @"D:\3D-Front\3D-FRONT\3D-FRONT\00ad8345-45e0-45b3-867d-4a3c88c2517a.json";
        data = JsonUtility.FromJson<FrontDataStructure>(File.ReadAllText(path));
        //Debug.Log(data.furniture[0].title);
        //SceneBuilder.Build(data);


        foreach (var fur in data.furniture)
        {
            furnitureDic.Add(fur.uid,fur);
        }
        SceneBuilder.RoomBuild(data.scene.room[1]);
    }

    public static int getMeshByUid(string uid)
    {
        for (int i = 0; i < data.mesh.Length; i++)
        {
            if (data.mesh[i].uid == uid) return i;
        }

        return 0;
    }
    

}
