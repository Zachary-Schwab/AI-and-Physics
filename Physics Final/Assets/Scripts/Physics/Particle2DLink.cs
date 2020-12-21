using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2DLink : MonoBehaviour
{
	public Particle2D mObj1;
	public Particle2D mObj2;

	public Particle2DLink(Particle2D obj1, Particle2D obj2)
	{
		mObj1 = obj1;
		mObj2 = obj2;
	}
	public  double getCurrentLength()
	{
		double distance = Vector2.Distance(mObj1.transform.position, mObj2.transform.position);
		return distance;
	}
}
class ParticleCable : Particle2DLink
{

	double mMaxLength;
	double mRestitution;

	public ParticleCable(Particle2D obj1, Particle2D obj2, double maxLength, double restitution) : base(obj1, obj2)
	{
		mMaxLength = maxLength;
		mRestitution = restitution;
	}

	void createContacts(List<Particle2DContact> contacts)
	{
		double length =  base.getCurrentLength();
		if (length<mMaxLength)
			return;

		Vector2 normal = base.mObj2.transform.position - base.mObj1.transform.position;
		normal.Normalize();
		double penetration = length - mMaxLength;

		Particle2DContact contact = new Particle2DContact(mObj1, mObj2, mRestitution, normal, penetration, Vector2.zero, Vector2.zero);

		contacts.Add(contact);
	}
}

class ParticleRod : Particle2DLink
{
	public double mLength;
	public ParticleRod(Particle2D obj1, Particle2D obj2, double length) : base(obj1,obj2)
	{
		mLength = length;
	}

	void createContacts(List<Particle2DContact> contacts)
	{
		double length = getCurrentLength();
		if (length == mLength)
			return;

		Vector2 normal = base.mObj2.transform.position - base.mObj1.transform.position;
		normal.Normalize();
		double penetration = length - mLength;
		if (length > mLength)
		{
			normal *= -1.0f;
			penetration = mLength - length;
		}
		Particle2DContact contact = new Particle2DContact(mObj1, mObj2, 0, normal, penetration, Vector2.zero, Vector2.zero);

		contacts.Add(contact);
	}
}