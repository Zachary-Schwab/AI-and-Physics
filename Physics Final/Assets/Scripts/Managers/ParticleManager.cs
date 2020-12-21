using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Experimental.GraphView;
using UnityEngine;



public class ParticleManager : MonoBehaviour
{
	public List<Sprite> particleSprites;
	public int maxNumParticles = 100;
	public Dictionary<Int32, Particle2D> mParticleMap = new Dictionary<Int32, Particle2D> ();
	List<Int32> mDeadUnits;
	Int32 msNextUnitID = 0;
	public GameObject baseParticle;
	public int iterationsPerFrame;
	public Particle2D createParticle(double mass, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 grav, float dampingConst, float scale, Int32 id = -1)
	{
		if(mParticleMap == null)
		{
			mParticleMap.Clear();
		}
		Particle2D particle = null;
		if (maxNumParticles > mParticleMap.Count)
		{
			GameObject particleObj = Instantiate(baseParticle,pos,Quaternion.identity);
			particleObj.transform.localScale = new Vector3(scale,scale,scale);
			particleObj.AddComponent<Particle2D>();
			particle = particleObj.GetComponent<Particle2D>();
			particle.InstatiateParticle2D(mass, pos, vel, acc, grav, dampingConst, id);
			Int32 theID = id;
			if (theID == -1)
			{
				theID = msNextUnitID;
				msNextUnitID++;
			}

			//place in map
			mParticleMap[theID] = particle;

			//assign id and increment nextID counter
			particle.mID = theID;
		}
		return particle;
	}


	private void Update()
	{
		for(int i = 0; i < iterationsPerFrame; i++)
		{
			updateStuff();
		}
	}
	private void updateStuff()
	{
		foreach (KeyValuePair<int, Particle2D> particlePair in mParticleMap)
		{
			Integrator.integrate(particlePair.Value, Time.deltaTime);
		}

		ForceManager.updateAllForces(Time.deltaTime);
	}
}






