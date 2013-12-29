using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Vectrosity;

public class UIGraph : MonoBehaviour {


	public UserManager userManager;

	public enum Span { all, year, month, week };

	public UIRoot uiRoot;
	public Transform bottomLeft;
	public UISprite canvas;
	public Camera guiCamera;
	public VectorLine line;
	public Material lineMaterial;
	public Material axisMaterial;
	public GameObject axisLabelPrefab;

	void Awake()
	{
		//VectorLine.SetCamera(guiCamera, CameraClearFlags.SolidColor, true);

		//DrawLine(new Vector2[]{new Vector2(0,0), new Vector2(200f,200f)});

	}

	void Start()
	{
		SetCanvas(Span.all, DateTime.Now);
	}


	/// <summary>
	/// Setting up both axes and labels
	/// </summary>
	/// <param name="span">Span of the graph</param>
	/// <param name="dateMax">Date max is simply max time on x axis</param>
	void SetCanvas(Span span, DateTime dateMax)
	{
		Vector3 originPosition = canvas.transform.position;
	
		//setup x axis

		Vector3 xMax = canvas.transform.position + new Vector3(canvas.width * uiRoot.transform.localScale.x, 0,0);

		Vector3[] xAxisPoints = new Vector3[2];
		xAxisPoints[0] = originPosition;
		xAxisPoints[1] = xMax;

		VectorLine xAxis = new VectorLine("xAxis", xAxisPoints, axisMaterial, 2f);
		xAxis.Draw3D();

		//spawn X Labels
		if (span == Span.all)
		{
			for (int i=0; i<2; i++)
			{
				GameObject labelGO = (GameObject)Instantiate(axisLabelPrefab);
				labelGO.transform.parent = transform;
				labelGO.transform.localScale = Vector3.one;
				UILabel label = labelGO.GetComponent<UILabel>();

				//first (origin) label
				if (i==0)
				{
					labelGO.transform.localPosition = Vector3.zero;
					label.text = userManager.babies[userManager.currentBaby].dateOfBirth.ToString("MMM\nyyyy");
				}
				//end label
				else if (i==1)
				{
					labelGO.transform.localPosition = new Vector3(canvas.width, 0, 0);
					label.text = dateMax.ToString("MMM\nyyyy");
				}

			}

		}




	}



	void DrawLine(Vector2[] points)
	{
		line = new VectorLine("line", points, lineMaterial, 2f);
		line.Draw();
	}

}
