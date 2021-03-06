﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepulsiveForceGenerator : ForceGenerator2D
{
	Vector2 mPoint;
	float mRange;
	float mMagnitude;
	bool mActive;
	public void RepulsiveForceGeneratorIntializer(Vector2 point, float magnitude, float range, bool active)
	{
		base.ForceGenerator2DIntializer(true);
		mPoint = point;
		mRange = range;
		mMagnitude = magnitude;
		mActive = active;
	}

	public override void updateForce(Particle2D obj, double dt)
	{
		if (mActive)
		{
			if (obj)
			{
				//find distance and direction
				Vector2 diff = new Vector2(obj.transform.position.x, obj.transform.position.y) - mPoint;
				float rangeSQ = mRange * mRange;
				float distSQ = Mathf.Pow(diff.magnitude, 2);
				if (distSQ < rangeSQ)
				{
					float dist = diff.magnitude;
					float proportionAway = dist / mRange;
					proportionAway = 1 - proportionAway;
					diff.Normalize();

					obj.mAccumulatedForces += (diff * (mMagnitude * proportionAway));
					//std::cout << diff * mMagnitude << std::endl;
				}
			}
		}
	}
}
