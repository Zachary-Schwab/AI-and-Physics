using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
	public Particle2D mObj1;
	public Particle2D mObj2;

	public Particle2DLink(Particle2D obj1, Particle2D obj2)
	{
		mObj1 = obj1;
		mObj2 = obj2;
	}
	public float getCurrentLength()
	{
		float distance = 0;
		if (mObj1 != null && mObj2 != null)
		{
			distance = Vector2.Distance(mObj1.transform.position, mObj2.transform.position);
		}
		return distance;
	}
}