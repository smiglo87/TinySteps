using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Vectrosity;

public class UIGraph : MonoBehaviour {


	public enum Span { all, year, month, week };

	public UIRoot uiRoot;
	public Transform bottomLeft;
	public UISprite canvas;
	public Camera guiCamera;
	public VectorLine line;
	public Material lineMaterial;
	public Material axisMaterial;

	void Awake()
	{
		//VectorLine.SetCamera(guiCamera, CameraClearFlags.SolidColor, true);

		//DrawLine(new Vector2[]{new Vector2(0,0), new Vector2(200f,200f)});

	}

	void Start()
	{
		SetCanvas();
	}


	/// <summary>
	/// Setting up both axes and labels
	/// </summary>
	void SetCanvas()
	{
		Vector3 originPosition = canvas.transform.position;
	
		//setup x axis

		Vector3 xMax = canvas.transform.position + new Vector3(canvas.width * uiRoot.transform.localScale.x, 0,0);

		Vector3[] xAxisPoints = new Vector3[2];
		xAxisPoints[0] = originPosition;
		xAxisPoints[1] = xMax;

		VectorLine xAxis = new VectorLine("xAxis", xAxisPoints, axisMaterial, 2f);
		xAxis.Draw3D();




	}



	void DrawLine(Vector2[] points)
	{
		line = new VectorLine("line", points, lineMaterial, 2f);

		line.Draw();
	}

}
