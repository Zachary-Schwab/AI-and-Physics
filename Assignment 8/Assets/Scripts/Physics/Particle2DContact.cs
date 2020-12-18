using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DContact : MonoBehaviour
{
	public Particle2D mObj1 = null;
	public Particle2D mObj2 = null;
	public float mRestitutionCoefficient = 0.0f;
	public Vector2 mContactNormal;
	public float mPenetration = 0.0f;
	public Vector2 mMove1;
	public Vector2 mMove2;
	
	public Particle2DContact()
	{

	}
	public void ContactInitialization(Particle2D obj1, Particle2D obj2, float restitution, Vector2 normal, float penetration, Vector2 move1, Vector2 move2)
	{
		mObj1 = obj1;
		mObj2 = obj2;
		mRestitutionCoefficient = restitution;
		mContactNormal = normal;
		mPenetration = penetration;
		mMove1 = move1;
		mMove2 = move2;
	}
	public float dotProduct(Vector2 a, Vector2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	public void resolve(double dt)
	{
		resolveVelocity(dt);
		resolveInterpenetration(dt);
	}

	public float calculateSeparatingVelocity()
	{
		Vector2 relativeVel = mObj1.mVel;
		if (mObj2)
		{
			relativeVel -= mObj2.mVel;
		}
		return dotProduct(relativeVel,mContactNormal);
	}

	void resolveVelocity(double dt)
	{
		float separatingVel = calculateSeparatingVelocity();
		if (separatingVel > 0.0f)//already separating so need to resolve
			return;

		float newSepVel = -separatingVel * mRestitutionCoefficient;

		Vector2 velFromAcc = mObj1.mAcc;
		if (mObj2)
			velFromAcc -= mObj2.mAcc;
		float accCausedSepVelocity = dotProduct(velFromAcc,mContactNormal) * (float)dt;

		if (accCausedSepVelocity < 0.0f)
		{
			newSepVel += mRestitutionCoefficient * accCausedSepVelocity;
			if (newSepVel < 0.0f)
				newSepVel = 0.0f;
		}

		float deltaVel = newSepVel - separatingVel;

		float totalInverseMass = mObj1.getInverseMass();
		if (mObj2)
			totalInverseMass += mObj2.getInverseMass();

		if (totalInverseMass <= 0)//all infinite massed objects
			return;

		float impulse = deltaVel / totalInverseMass;
		Vector2 impulsePerIMass = mContactNormal * impulse;

		Vector2 newVelocity = mObj1.mVel + impulsePerIMass * mObj1.getInverseMass();
		mObj1.mVel = newVelocity;
		if (mObj2)
		{
			newVelocity = mObj2.mVel + impulsePerIMass * -mObj2.getInverseMass();
			mObj2.mVel = newVelocity;
		}
	}

	void resolveInterpenetration(double dt)
	{
		if (mObj1 != null && mObj2 != null)
		{
			if (mPenetration <= 0.0f)
				return;

			float totalInverseMass = mObj1.getInverseMass();
			if (mObj2)
				totalInverseMass += mObj2.getInverseMass();

			if (totalInverseMass <= 0)//all infinite massed objects
				return;

			Vector2 movePerIMass = mContactNormal * (mPenetration / totalInverseMass);

			mMove1 = movePerIMass * mObj1.getInverseMass();
			if (mObj2)
				mMove2 = movePerIMass * -mObj2.getInverseMass();
			else
				mMove2 = Vector2.zero;

			Vector2 newPosition = new Vector2(mObj1.transform.position.x, mObj1.transform.position.y) + mMove1;
			mObj1.transform.position = newPosition;
			if (mObj2)
			{
				newPosition = new Vector2(mObj2.transform.position.x, mObj2.transform.position.y) + mMove2;
				mObj2.transform.position = newPosition;
			}
		}
	}
}




