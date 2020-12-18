using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
	public static void integrate(Particle2D obj, double dt)
	{
		obj.mPos += (obj.mVel * (float)dt);
		//only accumulate forces if ignoreForces is false
		obj.mAcc = obj.mGrav;
		if (!obj.mShouldIgnoreForces)//accumulate forces here
		{
			obj.mAcc += obj.mAccumulatedForces * obj.getInverseMass();
		}
		obj.mVel += (obj.mAcc * (float)dt);
		float damping = Mathf.Pow(obj.mDampingConstant, (float)dt);
		obj.mVel *= damping;
		obj.mAccumulatedForces = Vector2.zero;
	}
}
