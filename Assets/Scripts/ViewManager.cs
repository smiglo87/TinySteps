using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour {
	
	public GUIManager guiManager;
	
	public Transform guiCamera;
	public Transform currentView;

	
	public Transform journalView;
	public Transform journalAddEventView;
	public Transform journalViewEventView;
	
	
	public Transform growthView;
	public Transform growthWeightListView;
	public Transform growthAddWeightView;
	public Transform growthLengthListView;
	public Transform growthAddLengthView;
	
	public Transform moreView;
	

	
	
	public void ChangeView(Transform targetTransform)
	{
		guiCamera.localPosition = targetTransform.localPosition;
		currentView = targetTransform;
	}
	

	
	
	public void ToJournalView()
	{
		ChangeView(journalView);
	}
	
	public void ToJournalAddEventView()
	{
		ChangeView(journalAddEventView);
	}
	
	public void ToJournalViewEventView()
	{
		ChangeView(journalViewEventView);
	}


	
	
	public void ToGrowthView()
	{
		ChangeView(growthView);
		guiManager.WeightListRefresh();
		guiManager.LengthListRefresh();
	}
	
	public void ToGrowthWeightListView()
	{
		guiManager.WeightListRefresh();
		ChangeView(growthWeightListView);
	}
	
	
	public void ToGrowthAddWeightView()
	{
		guiManager.UpdateTimeInputsWeight();
		//guiManager.LabelWeightUnitChange();
		guiManager.LabelWeightClearing();
		ChangeView(growthAddWeightView);
	}
	
	public void ToGrowthLengthListView()
	{
		guiManager.LengthListRefresh();
		ChangeView(growthLengthListView);
	}
	
	
	public void ToGrowthAddLengthView()
	{
		guiManager.UpdateTimeInputsLength();
		guiManager.LabelLengthClearing();
		//guiManager.LabelLengthUnitChange();
		ChangeView(growthAddLengthView);
	}
	
	
	
	
	
	public void ToMoreView()
	{
		ChangeView(moreView);
	}
}
