using Dummiesman;
using UnityEngine;

namespace NewFrontData
{
    public class SceneBuilder
    {

        public static Transform rooom;
        // public static void Build(FrontDataStructure data)
        // {
        //     var furniture = data.scene.room[1].children;
        //     foreach (var model in furniture)
        //     {
        //         if (model.replace_jid == null)
        //         {
        //             Debug.Log("continue");
        //             continue;
        //         }
        //         Debug.Log($"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj");
        //         var modelPath = $"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj";
        //         var mtlPath = $"{Config.ModelPath}{model.replace_jid}\\model.mtl";
        //         var m = new OBJLoader().Load(modelPath, mtlPath);
        //         m.transform.position = model.pos.ToVec3();
        //         m.transform.rotation = model.rot.ToQuaternion();
        //         m.transform.localScale = model.scale.ToVec3();
        //     }
        // }
        //
        // public static void RoomBuild(Room roomData)
        // {
        //     var furniture = roomData.children;
        //     foreach (var model in furniture)
        //     {
        //         if (model.replace_jid == null)//environment elements
        //         {
        //             Debug.Log($"env:{model.instanceid}");
        //         }else //furnitures
        //         {
        //             Debug.Log($"fur:{model.instanceid}");
        //             var modelPath = $"{Config.ModelPath}{model.replace_jid}\\normalized_model.obj";
        //             var mtlPath = $"{Config.ModelPath}{model.replace_jid}\\model.mtl";
        //             var m = new OBJLoader().Load(modelPath, mtlPath);
        //             m.transform.position = model.pos.ToVec3();
        //             m.transform.rotation = model.rot.ToQuaternion();
        //             
        //             Vector3 fundSize = getFundSize(m);
        //             m.transform.localScale = new Vector3(model.replace_bbox.xLen/fundSize.x,model.replace_bbox.zLen/fundSize.y,model.replace_bbox.yLen/fundSize.z);
        //             Debug.Log(m.transform.localScale);
        //             
        //             //fundSize = getFundBbox(model.@ref);
        //             //m.transform.localScale = fundSize;
        //             // Debug.Log(m.transform.localScale);
        //         }
        //     }
        // }

        // public static Vector3 getFundSize(GameObject g)
        // {
        //     MeshFilter[] fList = g.GetComponentsInChildren<MeshFilter>();
        //     Vector3 result = new Vector3(0, 0, 0);
        //     foreach (MeshFilter f in fList)
        //     {
        //         result = Vector3.Max(result, f.mesh.bounds.size);
        //     }
        //     //fList[0].mesh.bounds.size
        //     result = result * 100;
        //     return result;
        // }
        //
        // public static Vector3 getFundBbox(string @ref)
        // {
        //     Furniture f = SceneImporter.furnitureDic[@ref];
        //     return new Vector3(f.size[0],f.size[1],f.size[2]);
        // }
        
        //Build functions for new 3D Front data
        public static void NewRoomBuild(NewFrontDataStructure data, Node node,bool wall, Transform parent = null)
        {

            if (rooom != null)
            {
                foreach (var r in rooom.GetComponents<Transform>())
                {
                    MonoBehaviour.Destroy(r.gameObject);
                }
            }
            rooom = (new GameObject($"{node.modelId}")).transform;
            
            parent = rooom;
            //FIXME
            if (wall)
            {
                NewWallBuild(data.id,node, parent);
            }
            

            foreach (int nodeid in node.nodeIndices)
            {
                //Debug.Log("Onbuilding "+ nodeid);
                for (int d = 0; d < data.levels[0].nodes.Length; d++)
                {
                    if (data.levels[0].nodes[d].id.Split('_')[1] == $"{nodeid}")
                    {
                        NewFurBuild(data.levels[0].nodes[d], parent);
                    }
                }
                
            }
            //NewFurBuild(node);
        }

        public static void NewWallBuild(string houseId, Node node, Transform parent)
        {
            Debug.Log("Building wall and floor");
            string wallPath = $"{Config.HomePath}{houseId}\\{node.modelId}w.obj";
            string floorPath = $"{Config.HomePath}{houseId}\\{node.modelId}f.obj";
            string wallMtl = $"{Application.dataPath}\\wall.mtl";
            string floorlMtl = $"{Application.dataPath}\\floor.mtl";
            
            Utils.EditFloorFormat(floorPath);
            //f1bbcb16-3303-4832-b568-5cb573d22e1e
            var wall = new OBJLoader().Load(wallPath,wallMtl,null);
            wall.transform.SetParent(parent);
            //fd3f9e0e-7fe5-4b23-8652-bd6e52aeb253
            var floor = new OBJLoader().Load(floorPath,floorlMtl,null);
            floor.transform.SetParent(parent);
            //wall.transform.position = new Vector3(0, 0, 0);
            //floor.transform.position = new Vector3(0,0,0);
        }

        public static void NewFurBuild(Node node, Transform parent)
        {
            // Debug.Log($"Furniture JID:{node.modelId}\n" +
            //           $"Furniture Id:{node.id}");
            string objPath = $"{Config.ObjectPath}{node.modelId}\\{node.modelId}.obj";
            string mtlPath = $"{Config.ObjectPath}{node.modelId}\\{node.modelId}.mtl";
            //new OBJLoader().Load(objPath,mtlPath,Utils.To44(node.transform));
            if (node.type != "Room")
            {
                string mname = SceneImporter.raw ? "raw" : "normalized";
                objPath = $"{Config.ModelPath}{node.modelId}\\{mname}_model.obj";
            mtlPath = $"{Config.ObjectPath}{node.modelId}\\model.mtl";

            float[,] innerTrans = new float[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    innerTrans[i, j] = node.transform[i+j*4];
                    // innerTrans[i, j] = node.transform[i][j];
                }
            }

                GameObject furnitureTemp = new OBJLoader().Load(objPath, mtlPath, innerTrans);
                furnitureTemp.name = node.id;
                furnitureTemp.transform.SetParent(parent);
// Debug.Log("ID not found");



            }


        }
        
        
        
        
    }
}