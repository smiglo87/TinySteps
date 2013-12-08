using UnityEngine;
using System.Collections;

public class BottleController: MonoBehaviour {
	
	public UserManager userManager;
	
	
	public Camera guiCamera;
	public UISprite milk;
	public Transform milkFullPoint;
	public UIRoot uiRoot;
	public float currentAmount = 0.0f;
	public UILabel currentAmountLabel;
	
	private bool dragging;
	
	
	
	
	public void FingerGesturesOnDrag(DragGesture gesture)
	{
		if(gesture.Phase == ContinuousGesturePhase.Started)
		{
			if(ObjectPicker.PickObject(gesture.StartPosition, guiCamera) == gameObject)
			{
				dragging = true;
			}
			else
			{
				dragging = false;
			}
		}
		else if(gesture.Phase == ContinuousGesturePhase.Updated)
		{
			if(dragging)
			{
				Vector3 bottleBottomScreenPosition = guiCamera.WorldToScreenPoint(milk.transform.position);
				Vector3 bottleTopScreenPosition = guiCamera.WorldToScreenPoint(milkFullPoint.position);
				
				float milkSize = Vector3.Distance(bottleBottomScreenPosition, bottleTopScreenPosition);
				float distance = Vector3.Distance(bottleBottomScreenPosition, new Vector3(bottleBottomScreenPosition.x, gesture.Position.y, bottleTopScreenPosition.z));
				
				float rootHeight = (float)uiRoot.activeHeight;
				float screenHeight = (float)Screen.height;
				
				
				if (gesture.Position.y > bottleBottomScreenPosition.y) 
				{
					milk.fillAmount = distance.Remap(0, milkSize, 0, 1);
					
					if (userManager.userUnit == UserManager.Unit.metric)
					{
						currentAmount = Mathf.Floor((300 * milk.fillAmount) / 5 ) * 5;
						currentAmountLabel.text = currentAmount.ToString() + " ml";
					}
					else if (userManager.userUnit == UserManager.Unit.imperial)
					{
						currentAmount = Mathf.Floor((10.5585196f * milk.fillAmount) / 0.5f ) * 0.5f;
						currentAmountLabel.text = currentAmount.ToString() + " oz";
					}
				
					currentAmountLabel.alpha = 1f;
					//label position
					
					if (gesture.Position.y <= bottleTopScreenPosition.y)
					{
						//currentAmountLabel.transform.position = new Vector3(currentAmountLabel.transform.position.x, guiCamera.ScreenToWorldPoint(gesture.Position).y + 0.2f);;
					}
					
				}
				else if (gesture.Position.y <= bottleBottomScreenPosition.y)
				{
					currentAmountLabel.alpha = 0;
					milk.fillAmount = 0;
				}
				
			
				
			}
		}
		else if(gesture.Phase == ContinuousGesturePhase.Ended)
		{
			dragging = false;
		}
	}
	
	
	
	
}
