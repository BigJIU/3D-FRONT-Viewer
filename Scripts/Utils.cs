using UnityEngine;

public static class Utils
{
    public static Vector3 ToVec3(this float[] arrs)
    {
        return new Vector3(arrs[0], arrs[1], arrs[2]);
    }

    public static Quaternion ToQuaternion(this float[] arrs)
    {
        return new Quaternion(arrs[0], arrs[1], arrs[2],arrs[3]);
    }

    public static Vector3 GetObjectSize(GameObject g)  
    {  
        Vector3 realSize = Vector3.zero;  
          
        Mesh mesh = g.GetComponent<MeshFilter>().mesh;  
        if(mesh==null)  
        {  
            return realSize;  
        }  
        // 它模型网格的大小  
        Vector3 meshSize = mesh.bounds.size;          
        // 它的放缩  
        Vector3 scale = g.transform.lossyScale;  
        // 它在游戏中的实际大小  
        realSize = new Vector3(meshSize.x*scale.x, meshSize.y*scale.y, meshSize.z*scale.z);  
          
        return realSize;  
    }

    public static float[,] To44(this float[] arrs)
    {
        return new float[4, 4]
        {
            { arrs[0],arrs[4],arrs[8],arrs[12]}, 
            { arrs[1],arrs[5],arrs[9],arrs[13]}, 
            { arrs[2],arrs[6],arrs[10],arrs[14]}, 
            { arrs[3],arrs[7],arrs[11],arrs[15]}
        };
    }

    public static void EditFloorFormat(string path)
    {
        System.IO.File.AppendAllText(path,"\r\n");
    }

}