using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ParticleCable : Particle2DLink
{

	float mMaxLength;
	float mRestitution;

	public ParticleCable(Particle2D obj1, Particle2D obj2, float maxLength, float restitution) : base(obj1, obj2)
	{
		mMaxLength = maxLength;
		mRestitution = restitution;
	}

	void createContacts(List<Particle2DContact> contacts)
	{
		float length = base.getCurrentLength();
		if (length < mMaxLength)
			return;

		Vector2 normal = base.mObj2.transform.position - base.mObj1.transform.position;
		normal.Normalize();
		float penetration = length - mMaxLength;

		Particle2DContact contact = new Particle2DContact();
		contact.ContactInitialization(mObj1, mObj2, 0, normal, penetration, Vector2.zero, Vector2.zero);

		contacts.Add(contact);
	}
}
