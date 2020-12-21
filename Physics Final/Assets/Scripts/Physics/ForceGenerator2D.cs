using System;
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

public class PointForceGenerator : ForceGenerator2D
{
	Vector2 mPoint;
	float mRange;
	float mMagnitude;
	bool mActive;
	public void PointForceGeneratorIntializer(Vector2 point, float magnitude, float range, bool active)
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
				Vector2 diff = mPoint - new Vector2(obj.transform.position.x, obj.transform.position.y);
				float rangeSQ = mRange * mRange;
				float distSQ = Mathf.Pow(diff.magnitude,2);
				if (distSQ < rangeSQ)
				{
					float dist = diff.magnitude;
					float proportionAway = dist / mRange;
					proportionAway = 1 - proportionAway;
					diff.Normalize();

					obj.mAccumulatedForces += (diff * (mMagnitude * proportionAway));
				}
			}
		}
	}
}

public class AttractiveForceGenerator : ForceGenerator2D
{
	Vector2 mPoint;
	float mRange;
	float mMagnitude;
	bool mActive;
	public void AttractiveForceGeneratorIntializer(Vector2 point, float magnitude, float range, bool active)
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
				Vector2 diff = mPoint - new Vector2(obj.transform.position.x, obj.transform.position.y);
				float rangeSQ = mRange * mRange;
				float distSQ = Mathf.Pow(diff.magnitude, 2);
				if (distSQ < rangeSQ)
				{
					float dist = diff.magnitude;
					float proportionAway = dist / mRange;
					proportionAway = 1 - proportionAway;
					diff.Normalize();

					obj.mAccumulatedForces += (diff * (mMagnitude * proportionAway));
				}
			}
		}
	}
}

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

public class BouyancyForceGenerator : ForceGenerator2D
{
	Particle2D mObj;
	float mVolume;
	float mDensity;
	float mWaterLineHeight;
	public void BouyancyForceGeneratorIntializer(Particle2D obj, float volume, float density, float waterLineHeight)
	{
		base.ForceGenerator2DIntializer(false);
		mObj = obj;
		mVolume = volume;
		mDensity = density;
		mWaterLineHeight = waterLineHeight;
	}

	public override void updateForce(Particle2D obj, double dt)
	{
		if (mObj == null)//either object no longer exists
			return;

		Vector2 pos = mObj.transform.position;

		float currentY = mObj.transform.position.y;

		Vector2 calculatedBuoyantForce = new Vector2(0, -1);

		if (currentY > mWaterLineHeight)
		{
			calculatedBuoyantForce *= mDensity * mVolume;
		}
		else if (currentY < mWaterLineHeight)
		{
			calculatedBuoyantForce = Vector2.zero;
		}
		else
		{
			//calculatedBuoyantForce .setY(mDensity * mVolume);
		}

		mObj.mAccumulatedForces += calculatedBuoyantForce;
	}
}

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

public class GravityForceGenerator : ForceGenerator2D
{
	Particle2D mObj;
	const float mGravitationalConstant = 1.56f;
	//const float mGravitationalConstant = 0.00000000006672f;
	public void GravityForceGeneratorIntializer(Particle2D obj)
	{
		mObj = obj;
		base.ForceGenerator2DIntializer(false);
	}
	public override void updateForce(Particle2D obj, double dt)
	{
		if (mObj == null)//the Bungeed object no longer exists
			return;
		Dictionary<Int32, Particle2D> particleMap = GameObject.Find("GameManager").GetComponent<ParticleManager>().mParticleMap;

		Vector2 forceDirection = new Vector2(0,0);

		foreach (KeyValuePair<int, Particle2D> particlePair in particleMap)
		{
			float dist = Mathf.Sqrt(Mathf.Pow((mObj.transform.position.x - particlePair.Value.transform.position.x), 2) + Mathf.Pow((mObj.transform.position.y - particlePair.Value.transform.position.y), 2));
			if (dist != 0)
			{
				double gravity = (mGravitationalConstant * mObj.getMass() * particlePair.Value.getMass())/(dist*dist);
				Vector2 direction = particlePair.Value.transform.position - mObj.transform.position;
				direction.Normalize();
				direction *= (float)gravity;
				forceDirection += direction;
			}
		}
		forceDirection.Normalize();
		mObj.mAccumulatedForces += forceDirection;
	}
}