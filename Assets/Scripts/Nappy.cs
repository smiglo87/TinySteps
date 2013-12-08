using UnityEngine;
using System.Collections;
using System;

public class Nappy : MonoBehaviour {


	public enum NappyType { Wet, Stool, Both };
	public NappyType nappyType;
	public DateTime nappyTime = new DateTime(2000, 01, 01, 00, 00, 00);




}