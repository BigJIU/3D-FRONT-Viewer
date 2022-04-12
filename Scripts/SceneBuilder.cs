using Dummiesman;
using UnityEngine;

namespace FrontData
{
    public class SceneBuilder
    {
        public static void Build(FrontDataStructure data)
        {
            var furniture = data.scene.room[1].children;
            foreach (var model in furniture)
            {
                if (model.replace_jid == null)
                {
                    Debug.Log("continue");
                    continue;
                }
                Debug.Log($"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj");
                var modelPath = $"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj";
                var mtlPath = $"{Config.ModelPath}{model.replace_jid}\\model.mtl";
                var m = new OBJLoader().Load(modelPath, mtlPath);
                m.transform.position = model.pos.ToVec3();
                m.transform.rotation = model.rot.ToQuaternion();
                m.transform.localScale = model.scale.ToVec3();
            }
        }

        public static void RoomBuild(Room roomData)
        {
            var furniture = roomData.children;
            foreach (var model in furniture)
            {
                if (model.replace_jid == null)//environment elements
                {
                    Debug.Log($"env:{model.instanceid}");
                }else //furnitures
                {
                    Debug.Log($"fur:{model.instanceid}");
                    var modelPath = $"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj";
                    var mtlPath = $"{Config.ModelPath}{model.replace_jid}\\model.mtl";
                    var m = new OBJLoader().Load(modelPath, mtlPath);
                    m.transform.position = model.pos.ToVec3();
                    m.transform.rotation = model.rot.ToQuaternion();
                    
                    Vector3 fundSize = getFundSize(m);
                    m.transform.localScale = new Vector3(model.replace_bbox.xLen/fundSize.x,model.replace_bbox.zLen/fundSize.y,model.replace_bbox.yLen/fundSize.z);
                    Debug.Log(m.transform.localScale);
                    
                    //fundSize = getFundBbox(model.@ref);
                    //m.transform.localScale = fundSize;
                    // Debug.Log(m.transform.localScale);
                }
            }
        }

        public static Vector3 getFundSize(GameObject g)
        {
            MeshFilter[] fList = g.GetComponentsInChildren<MeshFilter>();
            Vector3 result = new Vector3(0, 0, 0);
            foreach (MeshFilter f in fList)
            {
                result = Vector3.Max(result, f.mesh.bounds.size);
            }
            //fList[0].mesh.bounds.size
            result = result * 100;
            return result;
        }

        public static Vector3 getFundBbox(string @ref)
        {
            Furniture f = SceneImporter.furnitureDic[@ref];
            return new Vector3(f.size[0],f.size[1],f.size[2]);
        }
        
    }
}