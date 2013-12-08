using UnityEngine;

/// <summary>
/// Derived from NGUI's UICenterOnChild script, with kind permission from the original's author.
/// </summary>

public class UICenterOnChildManual : MonoBehaviour
{
	public float springStrength = 8f;
	public SpringPanel.OnFinished onFinished;
	
	UIScrollView mDrag;
	GameObject mCenteredObject;
	SpringPanel _springPanel;
	
	void Start ()
	{
		mDrag = gameObject.GetComponent ( typeof ( UIScrollView ) ) as UIScrollView;
	}
	
	/// <summary>
	/// Recenter the draggable panel on targetTrans. 
	/// </summary>

	public void CenterOnChild( Transform targetTrans )
	{
		if (mDrag.panel == null) return;

		// Calculate the panel's center in world coordinates
		Vector4 clip = mDrag.panel.clipRange;
		Transform dt = mDrag.panel.cachedTransform;
		Vector3 center = dt.localPosition;
		center.x += clip.x;
		center.y += clip.y;
		center = dt.parent.TransformPoint(center);

		// Offset this value by the momentum
		mDrag.currentMomentum = Vector3.zero;

		// Figure out the difference between the chosen child and the panel's center in local coordinates
		Vector3 cp = dt.InverseTransformPoint(targetTrans.position);
		Vector3 cc = dt.InverseTransformPoint(center);
		Vector3 offset = cp - cc;

		// Offset shouldn't occur if blocked by a zeroed-out scale
		if (mDrag.scale.x == 0f) offset.x = 0f;
		if (mDrag.scale.y == 0f) offset.y = 0f;
		if (mDrag.scale.z == 0f) offset.z = 0f;

		// Spring the panel to this calculated position
		_springPanel = SpringPanel.Begin ( gameObject, dt.localPosition - offset, springStrength );
		_springPanel.onFinished = onFinished;
	}
	
	public void Abort ()
	{
		if ( _springPanel != null )
		{
			_springPanel.onFinished = null;
			_springPanel.enabled = false;
			_springPanel = null;
		}
		
	}
}