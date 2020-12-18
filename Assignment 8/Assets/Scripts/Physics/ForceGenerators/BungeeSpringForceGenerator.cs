using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungeeSpringForceGenerator : ForceGenerator2D
{
	Particle2D mObj;
	Vector2 mFixedPoint;
	float mSpringConstant;
	float mRestLength;
	public void BungeeSpringForceGeneratorIntializer(Particle2D obj, Vector2 fixedPoint, float springConstant, float restLength)
	{
		base.ForceGenerator2DIntializer(false);
		mObj = obj;
		mFixedPoint = fixedPoint;
		mSpringConstant = springConstant;
		mRestLength = restLength;
	}
	public override void updateForce(Particle2D obj, double dt)
	{
		if (mObj == null)//the Bungeed object no longer exists
			return;

		Vector2 pos = mObj.transform.position;


		Vector2 diff = pos - mFixedPoint;
		float dist = diff.magnitude;

		float magnitude = dist - mRestLength;
		//if (magnitude < 0.0f)
		//magnitude = -magnitude;
		magnitude *= mSpringConstant;

		diff.Normalize();
		diff *= -magnitude;

		mObj.mAccumulatedForces += diff;
	}
}
