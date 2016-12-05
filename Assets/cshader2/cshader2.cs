using UnityEngine;
using System.Collections;

public class cshader2 : MonoBehaviour {
	public Texture2D img;

	private RenderTexture tex;

	public Material mat;
	public ComputeShader cshader;
	private int kernel;

	void Start () {
		//img.Apply ();

		tex = new RenderTexture (1024,512,24);
		tex.enableRandomWrite = true;
		tex.Create ();
		CreatBuffer ();

		mat.mainTexture = tex;

		kernel = cshader.FindKernel ("CSMain");
		cshader.Dispatch (kernel,1024/8,512/8,1);
	}

	private void CreatBuffer(){
		cshader.SetTexture(kernel,"texbuffer",tex);
		cshader.SetTexture(kernel,"imgbuffer",img);
	}

	private void OnRenderObject(){

    }
		
}
