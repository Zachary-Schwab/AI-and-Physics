using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactResolver
{
	public static List<Particle2DLink> mLinks = new List<Particle2DLink>();
	public static List<Particle2DContact> mContacts = new List<Particle2DContact>();

	static float mIterations = 5;
	public static float dotProduct(Vector2 a, Vector2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	public static void resolveContacts(double dt)
	{
		int mIterationsUsed = 0;
		while (mIterationsUsed < mIterations)
		{
			float max = float.MaxValue;
			Int32 nummContacts = mContacts.Count;
			Int32 maxIndex = nummContacts;
			for (Int32 i = 0; i < nummContacts; i++)
			{
				float sepVel = mContacts[i].calculateSeparatingVelocity();
				if (sepVel < max && (sepVel < 0.0f || mContacts[i].mPenetration > 0.0f))
				{
					max = sepVel;
					maxIndex = i;
				}
			}
			if (maxIndex == nummContacts)
				break;

			mContacts[maxIndex].resolve(dt);

			for (Int32 i = 0; i < nummContacts; i++)
			{
				if (mContacts[i].mObj1 == mContacts[maxIndex].mObj1)
				{
					mContacts[i].mPenetration -= dotProduct(mContacts[maxIndex].mMove1,mContacts[i].mContactNormal);
				}
				else if (mContacts[i].mObj1 == mContacts[maxIndex].mObj2)
				{
					mContacts[i].mPenetration -= dotProduct(mContacts[maxIndex].mMove2,mContacts[i].mContactNormal);
				}

				if (mContacts[i].mObj2)
				{
					if (mContacts[i].mObj2 == mContacts[maxIndex].mObj1)
					{
						mContacts[i].mPenetration += dotProduct(mContacts[maxIndex].mMove1,mContacts[i].mContactNormal);
					}
					else if (mContacts[i].mObj2 == mContacts[maxIndex].mObj2)
					{
						mContacts[i].mPenetration -= dotProduct(mContacts[maxIndex].mMove2,mContacts[i].mContactNormal);
					}
				}
			}
			mIterationsUsed++;
		}
	}
}

