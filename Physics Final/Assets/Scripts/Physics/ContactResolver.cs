using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ContactResolver
{
	static double mIterations;

	public static float dotProduct(Vector2 a, Vector2 b)
	{
		return a.x * b.x + a.y * b.y;
	}

	static void resolveContacts(List<Particle2DContact> contacts, double dt)
	{
		int mIterationsUsed = 0;
		while (mIterationsUsed < mIterations)
		{
			double max = double.MaxValue;
			Int32 numContacts = contacts.Count;
			Int32 maxIndex = numContacts;
			for (Int32 i = 0; i < numContacts; i++)
			{
				double sepVel = contacts[i].calculateSeparatingVelocity();
				if (sepVel < max && (sepVel < 0.0f || contacts[i].mPenetration > 0.0f))
				{
					max = sepVel;
					maxIndex = i;
				}
			}
			if (maxIndex == numContacts)
				break;

			contacts[maxIndex].resolve(dt);

			for (Int32 i = 0; i < numContacts; i++)
			{
				if (contacts[i].mObj1 == contacts[maxIndex].mObj1)
				{
					contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove1,contacts[i].mContactNormal);
				}
				else if (contacts[i].mObj1 == contacts[maxIndex].mObj2)
				{
					contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove2,contacts[i].mContactNormal);
				}

				if (contacts[i].mObj2)
				{
					if (contacts[i].mObj2 == contacts[maxIndex].mObj1)
					{
						contacts[i].mPenetration += dotProduct(contacts[maxIndex].mMove1,contacts[i].mContactNormal);
					}
					else if (contacts[i].mObj2 == contacts[maxIndex].mObj2)
					{
						contacts[i].mPenetration -= dotProduct(contacts[maxIndex].mMove2,contacts[i].mContactNormal);
					}
				}
			}
			mIterationsUsed++;
		}
	}
}

