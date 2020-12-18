using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceGenerator2D : MonoBehaviour
{
	public bool mShouldEffectAll;
	public void ForceGenerator2DIntializer(bool shouldEffectAll)
	{
		mShouldEffectAll = shouldEffectAll;
	}
	public virtual void updateForce(Particle2D obj, double dt) { }
}












