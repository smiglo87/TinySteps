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

	public ViewGrowth viewGrowth;

	public ViewWeightList viewWeightList;
	public ViewAddWeight viewAddWeight;
	public ViewLengthList viewLengthList;
	public ViewAddLength viewAddLength;

	public ViewJournalList viewJournalList;
	public ViewAddJournalEvent viewAddJournalEvent;
	public ViewGraph viewGraph;



	public ViewMore viewMore;



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


	#region Tracker Views
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
	#endregion

	#region Growth Views
	public void ToViewGrowth()
	{
		ChangeView(viewGrowth);
	}


	public void ToViewWeightList()
	{
		ChangeView(viewWeightList);
	}
	
	public void ToViewAddWeight()
	{
		ChangeView(viewAddWeight);
	}


	public void ToViewLengthList()
	{
		ChangeView(viewLengthList);
	}
	
	public void ToViewAddLength()
	{
		ChangeView(viewAddLength);
	}
	#endregion
	
	#region Journal Views
	public void ToViewJournalList()
	{
		ChangeView(viewJournalList);
	}

	public void ToViewAddJournalEvent()
	{
		ChangeView(viewAddJournalEvent);
	}
	#endregion



	public void ToViewMore()
	{
		ChangeView(viewMore);
	}


	public void ToViewGraph()
	{
		ChangeView(viewGraph);
	}

}
