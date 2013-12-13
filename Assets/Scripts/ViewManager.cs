using UnityEngine;
using System.Collections;

public class ViewManager : MonoBehaviour {
	
	public GUIManager guiManager;
	
	public Transform guiCamera;
	public Transform currentView;

	
	public Transform journalView;
	public Transform journalAddEventView;
	public Transform journalViewEventView;

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

	
	public void ToMoreView()
	{
		ChangeView(moreView);
	}
}
