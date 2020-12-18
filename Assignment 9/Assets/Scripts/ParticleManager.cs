using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class ParticleManager : MonoBehaviour
{
	public int maxNumParticles = 100;
	public List<Particle2D> mParticleList = new List<Particle2D>();
	List<Particle2D> mDeadUnits = new List<Particle2D>();
	Int32 msNextUnitID = 0;
	public GameObject baseParticle;

	private void Start()
	{
		this.gameObject.AddComponent<BouyancyForceGenerator>();
		BouyancyForceGenerator buoyancyGenerator = this.gameObject.GetComponent<BouyancyForceGenerator>();
		buoyancyGenerator = new BouyancyForceGenerator();
		buoyancyGenerator.BouyancyForceGeneratorIntializer(1.0f, 15.0f, 0f);
		ForceManager.addForceGenerator(buoyancyGenerator);
	}

	public Particle2D createParticle(float mass, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 grav, float dampingConst, Int32 id = -1)
	{
		if(mParticleList == null)
		{
			mParticleList.Clear();
		}
		Particle2D particle = null;
		if (maxNumParticles > mParticleList.Count)
		{
			GameObject particleObj = Instantiate(baseParticle,pos,Quaternion.identity);

			particleObj.AddComponent<Particle2D>();
			particle = particleObj.GetComponent<Particle2D>();
			particle.InstatiateParticle2D(mass, pos, vel, acc, grav, dampingConst, id);
			particle.gameObject.tag = "particle";
			Int32 theID = id;
			if (theID == -1)
			{
				theID = msNextUnitID;
				msNextUnitID++;
			}

			//place in map
			mParticleList.Add(particle);

			//assign id and increment nextID counter
			particle.mID = theID;
		}
		return particle;
	}

	private void Update()
	{
		foreach(Particle2D partile in mParticleList)
		{
			Integrator.integrate(partile, Time.deltaTime);
		}

		ForceManager.updateAllForces(Time.deltaTime);
		ContactResolver.resolveContacts(Time.deltaTime);
		
		foreach (Particle2D particle in mParticleList)
		{
			foreach(Particle2D particle2 in mParticleList)
			{
				if (particle.mID != particle2.mID)
				{
					if(CollisionDetector.CollisionDetection(particle, particle2))
					{
						mDeadUnits.Add(particle);
						mDeadUnits.Add(particle2);
					}
				}
			}
		}

		removeDead();
	}

	private void removeDead()
	{
		for(int i = 0; i < mDeadUnits.Count; i++)
		{
			mParticleList.Remove(mDeadUnits[i]);
			Destroy(mDeadUnits[i].gameObject);
		}
		mDeadUnits.Clear();
	}
}






