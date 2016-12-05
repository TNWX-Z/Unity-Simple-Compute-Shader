using UnityEngine;
using System.Collections;

public class cshader3 : MonoBehaviour {

	public Mesh mesh;
	public ComputeShader cshader;
	public Material mat;

	private ComputeBuffer buffer,posbuffer;
	private int kernel,posKernel;
	private int number;

	void Start () {
		number = 10 * 10 * 10 * 4 * 4 * 4;
		kernel = cshader.FindKernel ("CSMain");
		posKernel = cshader.FindKernel ("PosKernel");
		//mat = new Material (Shader.Find("compute/cshader3"));
		//float3 
		buffer = new ComputeBuffer(number,12);
		posbuffer = new ComputeBuffer (4,12);
	}

	private void BufferSet(){
		cshader.SetBuffer (kernel,"buffer",buffer);
		cshader.SetBuffer (posKernel,"posbuffer",posbuffer);

		mat.SetBuffer ("buffer",buffer);
		mat.SetBuffer ("posbuffer",posbuffer);
	}

	private void OnRenderObject(){
		BufferSet ();

		//1
		cshader.Dispatch (kernel,10,10,10);
		cshader.Dispatch (posKernel,1,1,1);
		//2
		mat.SetPass (0);
		//3
		Graphics.DrawProcedural (MeshTopology.Points,number,4);

	}


	private void OnDestroy(){
		buffer.Release ();
		posbuffer.Release ();
	}
}
