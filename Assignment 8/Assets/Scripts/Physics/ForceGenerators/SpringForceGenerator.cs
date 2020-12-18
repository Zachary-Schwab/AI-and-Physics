using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringForceGenerator : ForceGenerator2D
{
	Particle2D mObj1;
	Particle2D mObj2;
	float mSpringConstant;
	float mRestLength;
	public void SpringForceGeneratorIntializer(Particle2D obj1, Particle2D obj2, float springConstant, float restLength)
	{
		base.ForceGenerator2DIntializer(false);
		mObj1 = obj1;
		mObj2 = obj2;
		mSpringConstant = springConstant;
		mRestLength = restLength;
	}

	public override void updateForce(Particle2D obj, double dt)
	{
		if (mObj1 == null || mObj2 == null)//either object no longer exists
			return;

		Vector2 pos1 = mObj1.transform.position;
		Vector2 pos2 = mObj2.transform.position;

		Vector2 diff = pos1 - pos2;
		float dist = diff.magnitude;

		float magnitude = dist - mRestLength;
		//if (magnitude < 0.0f)
		//magnitude = -magnitude;
		magnitude *= mSpringConstant;

		diff.Normalize();
		diff *= -magnitude;

		mObj1.mAccumulatedForces += diff;
		mObj2.mAccumulatedForces += new Vector2(-diff.x, -diff.y);
	}
}
