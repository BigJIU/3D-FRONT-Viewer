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

}