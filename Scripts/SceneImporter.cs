
using System.Collections.Generic;
using System.IO;

using NewFrontData;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.UI;

public class SceneImporter : MonoBehaviour
{
    // Start is called before the first frame update
    //public static FrontDataStructure data;
    public NewFrontDataStructure data;
    public List<Node> avaRoom;
    public Texture2D floor;
    public Texture2D wall;
    public string houseId;
    public bool existWall;
    public bool rawSource;

    public static bool raw;

    [TextArea] public string stringJson;
    //public static Dictionary<string,Furniture> furnitureDic = new Dictionary<string, Furniture>(); //key:uid value:furniture
    //public static Dictionary<>

    public void Generate(string j = null)
    {
        
        //string houseId = "1_0_3_final_2";
            //"0b24a2e3-bb37-45b7-a397-282c76876132";
            //0adb88db-02a3-43ca-9a69-76dcf25740d6

            if (!string.IsNullOrEmpty(j))
            {
                stringJson = j;
            }

        
        string path =
            $@"D:\3D-Front\house\house\{houseId}\house.json";
        if (string.IsNullOrEmpty(stringJson))
        {
            data = JsonConvert.DeserializeObject<NewFrontDataStructure>(File.ReadAllText(path));
        }
        else
        {
            data = JsonConvert.DeserializeObject<NewFrontDataStructure>(stringJson);
        }

        raw = rawSource;
        // data = JsonUtility.FromJson<NewFrontDataStructure>(File.ReadAllText(path));
        //Debug.Log(data.furniture[0].title);
        //SceneBuilder.Build(data);
        // foreach (var fur in data.furniture)
        // {
        //     furnitureDic.Add(fur.uid,fur);
        // }
        // SceneBuilder.RoomBuild(data.scene.room[1]);
        avaRoom = new List<Node>();
        Debug.Log("Room JID:"+data.levels[0].nodes[0].modelId);
        foreach (Node node in data.levels[0].nodes)
        {
            if (node.roomTypes != null&&node.nodeIndices.Length>0)
            {
                avaRoom.Add(node);
                break;
            }
        }

        Config.floorTexture = floor;
        Config.wallTexture = wall;
    }

    public void NewRoom(GameObject json)
    {
        Generate(json.GetComponent<InputField>().text);
        SceneBuilder.NewRoomBuild(data,avaRoom[0],existWall);
    }
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Generate();
            SceneBuilder.NewRoomBuild(data,avaRoom[0],existWall);
        }

    }

    public static int getMeshByUid(string uid)
    {
        // for (int i = 0; i < data.mesh.Length; i++)
        // {
        //     if (data.mesh[i].uid == uid) return i;
        // }

        return 0;
    }
    
    // public static Texture2D GetTexrture2DFromPath(string imgPath)
    // {
    //     //读取文件
    //     FileStream fs = new FileStream(imgPath,FileMode.Open,FileAccess.Read);
    //     int byteLength = (int)fs.Length;
    //     byte[] imgBytes = new byte[byteLength];
    //     fs.Read(imgBytes,0,byteLength);
    //     fs.Close();
    //     fs.Dispose();
    //     //转化为Texture2D
    //     Image img = Image.FromStream(new MemoryStream(imgBytes));
    //     Texture2D t2d = new Texture2D(img.Width,img.Height);
    //     img.Dispose();
    //     t2d.LoadImage(imgBytes);
    //     t2d.Apply();
    //     return t2d;
    // }



}
