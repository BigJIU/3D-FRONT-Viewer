//如果添加的对象上没有MeshFilter，则给对象添加一个MeshFilter

using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
//如果添加的对象上没有MeshRenderer，则给对象添加一个MeshRenderer
[RequireComponent(typeof(MeshRenderer))]
public class DrawPlane:MonoBehaviour
{
	//实际操作的MeshFilter 
	private MeshFilter filter;
	//实际的Mesh
	private Mesh mesh;
	//记录顶点信息
	private List<Vector3> vts = new List<Vector3>();
	//自身的uv坐标
	private Vector2[] uvs;
	//三角形数组
	private int[] tris;
	//x轴单位
	public int xSize;
	//y轴单位
	public int ySize;
	//x轴每单位大小，用于计算坐标
	public float xSeg;
	//y轴每单位大小，用于计算坐标
	public float ySeg;
	//x轴顶点个数 ==== xSize+1
	private int xCount;
	//y轴顶点个数 ==== ySize+1
	private int yCount;
	
	//初始化
	private void Start(){
		filter = GetComponent<MeshFilter>();
		xCount = xSize + 1;
		yCount = ySize + 1;
		//得到每个顶点的坐标
		vts = CalVts();
		//设置Mesh
		SetMesh();
	}
	

	
	//计算顶点信息，多少个顶点，每个顶点的坐标等
	private List<Vector3> CalVts(){
		//我们以挂载对象的位置作为起始点
		Vector3 pos = transform.position;
		
		List<Vector3> vals = new List<Vector3>();
		for(int i = 0 ; i < xCount ; i++){
			for(int j = 0 ; j < yCount ; j++){
				Vector3 vector =pos + new Vector3(i*xSeg,j*ySeg,0);
				vals.Add(vector);
			}
		}
		return vals;
	}

	//对Mesh进行设置
	private void SetMesh(){
		mesh = new Mesh();
		//因为每个有效点我们需要绘制两个三角形，共计六个顶点，所以对tris数组初始大小为
		tris = new int[xSize*ySize*6];
		//记录每个有效点的三角形起始下标，每个有效点需要画两个三角形，六个顶点，因此每次迭代triCount +=6
		int triCount = 0 ;
		for(int i = 0 ; i < ySize ; i++){
			for(int j = 0 ; j < xSize ; j++){
				//每个有效点绘制两个三角形
				//每个顶点，在vts中的下标索引
				int startIndex = i * xCount + j; 
				//第一个三角形，三个顶点
				tris[triCount] = startIndex ;
				//第一个三角形第二个点为初始点的正上方相邻点
				tris[triCount+1] = startIndex + xCount ;
				//第一个三角形第三个点为右边相邻的点
				tris[triCount+2] = startIndex + 1;
				
				//绘制第二个三角形，
				//第二个三角形第一个点为初始点的正上方相邻的点
				tris[triCount+3] = startIndex + xCount ;
				//第二个三角形第二个点为初始点右上方的点
				tris[triCount+4] = startIndex + xCount + 1;
				//第二个三角形第三个点为初始点的右边相邻的点
				tris[triCount+5] = startIndex + 1;
				
				triCount += 6;
			}
		}
		
		//将顶点传给mesh
		mesh.vertices = vts.ToArray();
		//将三角形的绘制序列传给mesh
		mesh.triangles = tris;
		
		//至此我们的三角形其实已经绘制成功了，但是面并没有uv坐标，如果了解uv的同学值得，纹理就是通过顶点之间uv坐标的插值来计算颜色值的。
		//因此我们还需要计算一下uv坐标
		//有多少个顶点，就有多少个uv坐标
		//uv坐标的范围在0-1之间，其实就相当于给顶点坐标做一个归一化处理
		uvs = new Vector2[vts.Count];
		float xOffset = 1.0f / xCount;
		float yOffset = 1.0f / yCount;
		for(int i = 0 ; i < yCount ; i++){
			for(int j = 0 ; j < xCount; j++){
				//设置每个顶点的uv坐标
				//这里我们也可以做一些小巧思，比如y轴翻转，或者x轴翻转，大家能想到怎么做吗
				uvs[i * xCount+ j] = new Vector2(j * xOffset , i * yOffset );
			}
		}
		//将uv设置给mesh
		mesh.uv = uvs;
		//这三个方法是相当于对mesh用新数据进行重置
		mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();
        //将mesh交给filter
        filter.mesh = mesh;
	}
}
