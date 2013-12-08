using UnityEngine;
using System.Collections;
using System;

public class Length : MonoBehaviour {


	public DateTime lengthDate = new DateTime(2000, 01, 01);
	
	public enum LengthUnit { metric, imperial };
	public LengthUnit lengthUnit;
	
	public float lengthUnits = 0.0f;
	public float lengthDecimals = 0.0f;


}
