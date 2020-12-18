using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouyancyForceGenerator : ForceGenerator2D
{
	float mVolume;
	float mDensity;
	float mWaterLineHeight;


	public void BouyancyForceGeneratorIntializer(float volume, float density, float waterLineHeight)
	{
		base.ForceGenerator2DIntializer(true);
		mVolume = volume;
		mDensity = density;
		mWaterLineHeight = waterLineHeight;
	}

	public override void updateForce(Particle2D obj, double dt)
	{
		if (obj == null)//either object no longer exists
			return;

		Vector2 pos = obj.transform.position;

		float currentY = obj.transform.position.y;

		Vector2 calculatedBuoyantForce = new Vector2(0, 1);

		if (currentY < mWaterLineHeight)
		{
			calculatedBuoyantForce *= mDensity * mVolume;
		}
		else if (currentY > mWaterLineHeight)
		{
			calculatedBuoyantForce = Vector2.zero;
		}
		else
		{
			calculatedBuoyantForce.y = (mDensity * mVolume);
		}

		obj.mAccumulatedForces += calculatedBuoyantForce;
	}
}
