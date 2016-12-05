using UnityEngine;
using System.Collections;

public class cshader4 : MonoBehaviour {
	public ComputeShader cshader;
	public Material mat;

	private int kernel;
	private int num4pos;

	private ComputeBuffer posbuffer;

	public Mesh meshdata;
	private int num4vertex;
	private ComputeBuffer vertexbuffer;


	private void meshInfo(){
		Vector3[] vertics = meshdata.vertices;
		int[] triangles = meshdata.triangles;
		num4vertex = triangles.Length;

		Vector3[] newVertics = new Vector3[num4vertex];
		for(int i = 0;i<num4vertex;++i){
			newVertics[i] = vertics[triangles[i]];
		} 

		vertexbuffer = new ComputeBuffer (num4vertex,12);
		vertexbuffer.SetData (newVertics);
	}

	void Start(){
		kernel = cshader.FindKernel ("CSMain");
		num4pos = 4 * 4 * 4;
		//num4vertex = ;
		meshInfo();
		//float3 
		posbuffer = new ComputeBuffer (num4pos,12);
		
	}

	private void BufferSet(){
		cshader.SetBuffer (kernel,"posbuffer",posbuffer);
		mat.SetBuffer ("posbuffer",posbuffer);

		mat.SetBuffer ("vertexbuffer",vertexbuffer);
	}

	private void OnRenderObject(){
		BufferSet ();
		//1
		cshader.Dispatch(kernel,1,1,1);
		//2
		mat.SetPass(0);
		//3
		Graphics.DrawProcedural(MeshTopology.Points,num4vertex,num4pos);
	}

	private void OnDestroy(){
		posbuffer.Release ();
		vertexbuffer.Release ();
	}
}
