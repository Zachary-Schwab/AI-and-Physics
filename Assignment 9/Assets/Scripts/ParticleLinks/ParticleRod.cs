using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ParticleRod : Particle2DLink
{
	public float mLength;

	public ParticleRod(Particle2D obj1, Particle2D obj2, float length) : base(obj1, obj2)
	{
		mLength = length;
	}

	void FixedUpdate()
	{
		createContacts();
	}
	public void createContacts()
	{
		if (mObj1 != null && mObj2 != null)
		{
			float length = getCurrentLength();
			if (length == mLength)
				return;

			Vector2 normal = base.mObj2.transform.position - base.mObj1.transform.position;
			float penetration = length - mLength;
			if (penetration < 0.0f)
			{
				penetration = mLength - length;
				normal *= -1.0f;
			}
			normal.Normalize();

			Particle2DContact contact = new Particle2DContact();
			contact.ContactInitialization(mObj1, mObj2, 0, normal, penetration, Vector2.zero, Vector2.zero);

			ContactResolver.mContacts.Add(contact);
		}
	}
}