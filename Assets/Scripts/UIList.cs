using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIList : MonoBehaviour {
	
	
	public GameObject cellPrefab;
	public float cellHeight;
	public ArrayList info = new ArrayList();
	public ArrayList currentInfo = new ArrayList();
	public int lazyIncement = 10;
	public bool lazyLoading;
	public UIScrollBar scrollBar;
	public int loadedMin;
	public int loadedMax;

	
	public UIScrollView draggablePanel;
	
	void Start()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);	
		}	

		
		if (GetComponent<UIScrollView>() != null)
		{
			draggablePanel = GetComponent<UIScrollView>();
		}
		else Debug.LogError("Cant access draggable panel component, Make sure is added to list gameobject");
	}
	
	
	
	//call BuildList only on info Refresh
	public void BuildList (ArrayList list) 
	{	
		info = new ArrayList();
		info.AddRange(list);
		loadedMax = 0;
		
		Rebuild(info);
	}
	
	
	
	public void Rebuild(ArrayList list)
	{

		draggablePanel.ResetPosition();
		Clear();
		
		loadedMax = 0;
		currentInfo = new ArrayList();
		currentInfo.AddRange(list);

		AddCells(list.Count, list);

 		draggablePanel.ResetPosition();
		draggablePanel.panel.Refresh();
		
	}
	
	
	
	
	void AddCells(int numberToAdd, ArrayList list)
	{
		for (int i=loadedMax; i<loadedMax+numberToAdd; i++)
		{
			GameObject cellCopy = Instantiate(cellPrefab) as GameObject;
			cellCopy.transform.parent = transform;
			cellCopy.transform.localScale = Vector3.one;
			cellCopy.transform.localPosition = new Vector3(0, -(cellHeight * i), 0);
			
			cellCopy.SendMessage("Refresh", list[i]);	
		}
		loadedMax = loadedMax+numberToAdd;
	}
	
	
	
	
	public void Clear()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}

	}
	
	
	void Update()
	{
		if (lazyLoading)
		{
			if(scrollBar.scrollValue > 0.9f)
			{
				AddCells(20, currentInfo);	
			}
			else if(scrollBar.scrollValue < 0.1f)
			{
				//RemoveCells();
			}
		}
	}
	
	
	
	
}
