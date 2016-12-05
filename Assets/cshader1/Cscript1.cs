using UnityEngine;
using System.Collections;

public class Cscript1 : MonoBehaviour {
	public Material mat;
	public ComputeShader cshader;
	private ComputeBuffer buffer;

	//private int number;
	private int kernel;

	[Range(1,100)]
	public int x=10,y=10,z=10;

	void Start () {
		kernel = cshader.FindKernel ("CSMain");
		//number = 1 * 1 * 1 * 1000;
		//为floa3
		buffer = new ComputeBuffer (1000, 12);
	}

	private void creatBuffer(){
		cshader.SetBuffer (kernel,"buffer",buffer);
		//mat.SetBuffer ("buffer",buffer);
	}

	private void OnRenderObject(){
		cshader.Dispatch (kernel,1,1,1);

		creatBuffer ();
		/*
		cshader.SetInt ("x",x);
		cshader.SetInt ("y",y);
		cshader.SetInt ("z",z);	
		*/
		mat.SetPass (0);
		mat.SetBuffer ("buffer",buffer);

		Graphics.DrawProcedural (MeshTopology.Points,1000);
	}


	private void OnDestroy(){
		buffer.Release ();
	}


}
