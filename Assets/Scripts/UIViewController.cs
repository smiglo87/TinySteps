using UnityEngine;
using System.Collections;


/// <summary>
/// Manages all views in the project.
/// </summary>
public class UIViewController : MonoBehaviour {

	public Transform guiCamera;
	public UIView currentView;

	public ViewWelcome viewWelcome;
	public ViewBabyRegistration viewBabyRegistration;
	public ViewBabyRegistration2 viewBabyRegistration2;
	public ViewDashboard viewDashboard;

	public ViewTracker viewTracker;

	public ViewFeedingList viewFeedingList;
	public ViewAddFeeding viewAddFeeding;
	public ViewNappyList viewNappyList;
	public ViewAddNappy viewAddNappy;
	public ViewSleepingList viewSleepingList;
	public ViewAddSleeping viewAddSleeping;



	public void ChangeView(UIView target)
	{
		if(currentView != null) currentView.Hide();
		guiCamera.localPosition = target.transform.localPosition;
		currentView = target;
		currentView.Show();
	}


	public void ToWelcomeView()
	{
		ChangeView(viewWelcome);
	}
	
	public void ToBabyRegistrationView()
	{
		ChangeView(viewBabyRegistration);
	}
	
	public void ToBabyRegistrationView2()
	{
		ChangeView(viewBabyRegistration2);
	}
	
	public void ToDashboardMainView()
	{
		ChangeView(viewDashboard);
	}



	public void ToViewTracker()
	{
		ChangeView(viewTracker);
	}



	public void ToViewFeedingList()
	{
		ChangeView(viewFeedingList);
	}

	public void ToViewAddFeeding()
	{
		ChangeView(viewAddFeeding);
	}


	public void ToViewNappyList()
	{
		ChangeView(viewNappyList);
	}
	
	public void ToViewAddNappy()
	{
		ChangeView(viewAddNappy);
	}



	public void ToViewSleepingList()
	{
		ChangeView(viewSleepingList);
	}
	
	public void ToViewAddSleeping()
	{
		ChangeView(viewAddSleeping);
	}
}
