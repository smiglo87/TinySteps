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

	}

	void Start()
	{
		ShowWeightGraph(userManager.babies[userManager.currentBaby].weights);
	}



	public void ShowWeightGraph(List<Weight> weights )
	{
		List<Hashtable> htList = new List<Hashtable>();

		foreach (Weight weight in weights)
		{
			Hashtable ht = new Hashtable();
			ht.Add("date", weight.weightDate);
			float val = weight.weightUnits + (weight.weightDecimals/1000f);
			ht.Add("value", val);
			htList.Add(ht);
		}

		DrawGraph(htList.ToArray());

	}





	/// <summary>
	/// Setting up both axes and labels
	/// </summary>
	/// <param name="span">Span of the graph</param>
	/// <param name="dateMax">Date max is simply max time on x axis</param>
	void DrawGraph(Hashtable[] data)
	{
		Vector3 originPosition = canvas.transform.position;
	
		//setup x axis

		Vector3 xMax = canvas.transform.position + new Vector3(canvas.width * uiRoot.transform.localScale.x, 0,0);

		Vector3[] xAxisPoints = new Vector3[2];
		xAxisPoints[0] = originPosition;
		xAxisPoints[1] = xMax;

		VectorLine xAxis = new VectorLine("xAxis", xAxisPoints, axisMaterial, 2f);
		xAxis.Draw3D();


		//setup y axis
		
		Vector3 yMax = canvas.transform.position + new Vector3(0, canvas.height * uiRoot.transform.localScale.y, 0);
		
		Vector3[] yAxisPoints = new Vector3[2];
		yAxisPoints[0] = originPosition;
		yAxisPoints[1] = yMax;
		
		VectorLine yAxis = new VectorLine("yAxis", yAxisPoints, axisMaterial, 2f);
		yAxis.Draw3D();


		float minValue = 0;
		float maxValue = 0;

		DateTime minDate = DateTime.MinValue;
		DateTime maxDate = DateTime.MinValue;

		for (int i=0; i<4; i++)
		{
			GameObject labelGO = (GameObject)Instantiate(axisLabelPrefab);
			labelGO.transform.parent = transform;
			labelGO.transform.localScale = Vector3.one;
			UILabel label = labelGO.GetComponent<UILabel>();

			//first (origin) label
			if (i==0)
			{
				label.pivot = UIWidget.Pivot.TopLeft;
				labelGO.transform.localPosition = Vector3.zero;
				Hashtable firstEntry = data[0];
				DateTime firstDate = (DateTime)firstEntry["date"];
				minDate = firstDate;
				label.text = firstDate.ToString("MMM\nyyyy");
			}
			//end label
			else if (i==1)
			{
				labelGO.transform.localPosition = new Vector3(canvas.width, 0, 0);
				Hashtable lastEntry = data[data.Length-1];
				DateTime lastDate = (DateTime)lastEntry["date"];
				maxDate = lastDate;
				label.text = lastDate.ToString("MMM\nyyyy");
			}
			//y axis
			else if (i==2)
			{
				label.pivot = UIWidget.Pivot.BottomRight;
				Hashtable firstEntry = data[0];
				float firstValue = (float)firstEntry["value"];
				minValue = firstValue;
				label.text = firstValue.ToString() + "  " ;
				labelGO.transform.localPosition = Vector3.zero;
			}
			else if (i==3)
			{
				label.pivot = UIWidget.Pivot.TopRight;
				Hashtable lastEntry = data[data.Length-1];
				float lastValue = (float)lastEntry["value"];
				maxValue = lastValue;
				label.text = lastValue.ToString() + "  " ;
				labelGO.transform.localPosition = new Vector3(0, canvas.height, 0);
			}

		}

		//Drawing line

		Vector3[] linePoints = new Vector3[data.Length];


		for (int i=0; i<data.Length; i++)
		{
			//if first point give origin
			if (i==0) linePoints[i] = canvas.transform.position;
			else
			{
				DateTime xDate = (DateTime)data[i]["date"];
				TimeSpan totalSpan = maxDate - minDate;
				TimeSpan thisSpan = minDate - xDate ;

				float xfactor = (float)(thisSpan.TotalDays / totalSpan.TotalDays);
				float xPos = canvas.transform.position.x + canvas.width * uiRoot.transform.localScale.x * -xfactor;

				float yVal = (float)data[i]["value"];

				float yfactor = ((yVal-minValue) / (maxValue-minValue));
				float yPos = canvas.transform.position.y + canvas.height * uiRoot.transform.localScale.y * yfactor;
				linePoints[i] = new Vector3(xPos, yPos, 0);

				//additional labels
				GameObject labelGO = (GameObject)Instantiate(axisLabelPrefab);
				labelGO.transform.parent = transform;
				labelGO.transform.localScale = Vector3.one;
				UILabel label = labelGO.GetComponent<UILabel>();
				label.pivot = UIWidget.Pivot.Right;
				label.text = data[i]["value"].ToString() + "  ";

				labelGO.transform.position = new Vector3(0, yPos, 0);
			}

		}

		line = new VectorLine("line", linePoints, lineMaterial, 3f, LineType.Continuous, Joins.Weld );

		line.Draw3D();





	}



	void DrawLine(Vector2[] points)
	{
		line = new VectorLine("line", points, lineMaterial, 2f);
		line.Draw();
	}

}
