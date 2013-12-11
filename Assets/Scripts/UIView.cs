using UnityEngine;
using System.Collections;

/// <summary>
/// Manages specific view.
/// </summary>
public class UIView : MonoBehaviour {


	public bool isVisible = false;


	/// <summary>
	/// Showing view by calling other function with alpha.
	/// </summary>
	public virtual void Show()
	{
		UpdatePanelVisibility(true);
	}


	/// <summary>
	/// Hiding view by calling other function with alpha.
	/// </summary>
	public virtual void Hide()
	{
		UpdatePanelVisibility(false);
	}


	/// <summary>
	/// Updating alpha values on root panel and its children if any.
	/// </summary>
	/// <param name="show">If set to <c>true</c> is setting target panels alpha to 1.0</param>
	void UpdatePanelVisibility(bool show)
	{
		float alpha;
		if (show == true) alpha = 1f;
		else alpha = 0;

		//check if UIPanel component exist on this view and update alpha
		if (GetComponent<UIPanel>() != null) GetComponent<UIPanel>().alpha = alpha;

		//find UIPanel components in children and update alpha on each if any
		//UIPanel[] panels = (UIPanel[])GetComponentsInChildren(typeof(UIPanel));
		//foreach (UIPanel panel in panels) panel.alpha = alpha;

		//update public variable iVisible
		isVisible = show;
	}


}
