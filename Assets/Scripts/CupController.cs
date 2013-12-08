using UnityEngine;
using System.Collections;

public class CupController : MonoBehaviour {


	public UserManager userManager;
	
	
	public Camera guiCamera;
	public UISprite food;
	public Transform foodFullPoint;
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
				Vector3 cupBottomScreenPosition = guiCamera.WorldToScreenPoint(food.transform.position);
				Vector3 cupTopScreenPosition = guiCamera.WorldToScreenPoint(foodFullPoint.position);
				
				float foodSize = Vector3.Distance(cupBottomScreenPosition, cupTopScreenPosition);
				float distance = Vector3.Distance(cupBottomScreenPosition, new Vector3(cupBottomScreenPosition.x, gesture.Position.y, cupTopScreenPosition.z));
				
				float rootHeight = (float)uiRoot.activeHeight;
				float screenHeight = (float)Screen.height;
				
				
				if (gesture.Position.y > cupBottomScreenPosition.y) 
				{
					food.fillAmount = distance.Remap(0, foodSize, 0, 1);
					
					if (userManager.userUnit == UserManager.Unit.metric)
					{
						currentAmount = Mathf.Floor((300 * food.fillAmount) / 5 ) * 5;
						currentAmountLabel.text = currentAmount.ToString() + " g";
					}
					else if (userManager.userUnit == UserManager.Unit.imperial)
					{
						currentAmount = Mathf.Floor((10.5585196f * food.fillAmount) / 0.5f ) * 0.5f;
						currentAmountLabel.text = currentAmount.ToString() + " oz";
					}
					
					currentAmountLabel.alpha = 1f;
					//label position
					
					if (gesture.Position.y <= cupTopScreenPosition.y)
					{
						//currentAmountLabel.transform.position = new Vector3(currentAmountLabel.transform.position.x, guiCamera.ScreenToWorldPoint(gesture.Position).y + 0.2f);;
					}
					
				}
				else if (gesture.Position.y <= cupBottomScreenPosition.y)
				{
					currentAmountLabel.alpha = 0;
					food.fillAmount = 0;
				}
				
				
				
			}
		}
		else if(gesture.Phase == ContinuousGesturePhase.Ended)
		{
			dragging = false;
		}
	}




}
