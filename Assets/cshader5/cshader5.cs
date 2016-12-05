using UnityEngine;
using System.Collections;

public class cshader5 : MonoBehaviour {

	public Mesh meshdata;
	public Mesh meshpos;

	public Material mat;

	private Vector3[] num4data2vert;
	private int[] num4data2tri;
	private Vector3[] newdatavert;

	private Vector3[] num4pos2vert;

	private ComputeBuffer data4vert2buffer;
	private ComputeBuffer pos4vert2buffer;

	int num4data2length;
	int num4pos2length;

	private void meshInfo(){
		//data mesh
		num4data2vert = meshdata.vertices;
		num4data2tri = meshdata.triangles;
		num4data2length = num4data2tri.Length;
		newdatavert = new Vector3[num4data2length];
		for(int i = 0;i<num4data2length;++i){
			newdatavert[i] = num4data2vert[num4data2tri[i]];
		}
		//pos mesh
		num4pos2vert = meshpos.vertices;
		num4pos2length = num4pos2vert.Length;
		//buffer
		data4vert2buffer = new ComputeBuffer(num4data2length,12);
		pos4vert2buffer = new ComputeBuffer (num4pos2length,12);
		//set data for buffer
		data4vert2buffer.SetData(newdatavert);
		pos4vert2buffer.SetData (num4pos2vert);
	}

	private void infoDebug(){
        print(num4pos2length);
        print(num4data2length);
	}

	void Start () {
		meshInfo ();
		infoDebug ();
	}

	private void SetBuffer(){
		mat.SetBuffer ("databuffer",data4vert2buffer);
		mat.SetBuffer ("posbuffer",pos4vert2buffer);
	}

	private void OnRenderObject(){
		//1
		SetBuffer();
		//2
		mat.SetPass (0);
		//3
		Graphics.DrawProcedural(MeshTopology.Points,num4data2length,num4pos2length);
	}
	
	private void OnDestroy(){
		data4vert2buffer.Release ();
		pos4vert2buffer.Release ();
	}
}
