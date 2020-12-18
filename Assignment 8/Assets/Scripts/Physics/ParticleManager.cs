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
	public Sprite targetSprite;

	private void Start()
	{
		this.gameObject.AddComponent<BouyancyForceGenerator>();
		BouyancyForceGenerator buoyancyGenerator = this.gameObject.GetComponent<BouyancyForceGenerator>();
		buoyancyGenerator = new BouyancyForceGenerator();
		buoyancyGenerator.BouyancyForceGeneratorIntializer(1.0f, 30.0f, 0f);
		ForceManager.addForceGenerator(buoyancyGenerator);

		Particle2D target = createParticle(1.5f, Vector2.zero, Vector2.zero, Vector2.zero, new Vector2(0, -5), 0.99f);
		target.gameObject.GetComponent<SpriteRenderer>().sprite = targetSprite;
		target.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
		target.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
		target.gameObject.name = "target";
		target.gameObject.tag = "Target";
		target.gameObject.AddComponent<TargetColision>();
		target.gameObject.AddComponent<TargetRespawn>();
		target.gameObject.GetComponent<TargetRespawn>().min = new Vector2(-6, -.8f);
		target.gameObject.GetComponent<TargetRespawn>().min = new Vector2(8, 4);
	}

	Particle2D createParticle(float mass, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 grav, float dampingConst, Int32 id = -1)
	{
		if(mParticleMap == null)
		{
			mParticleMap.Clear();
		}
		Particle2D particle = null;
		if (maxNumParticles > mParticleMap.Count)
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
			mParticleMap[theID] = particle;

			//assign id and increment nextID counter
			particle.mID = theID;
		}
		return particle;
	}
	public Particle2D createProjectile(ProjectileType type, Vector2 pos, Vector2 fireDirection)
	{
		float mass = 1.0f;
		float speed = 0.0f;
		Vector2 gravity = Vector2.zero;
		Vector2 velocity = Vector2.zero;
		Sprite sprite;

		//add additional projectile types here
		switch (type)
		{
			case ProjectileType.BOLA:
			{
				if (maxNumParticles - mParticleMap.Count > 1)
				{
					float mass1 = 2.0f;
					float speed1 = 2.5f;
					Vector2 gravity1 = new Vector2(0.0f, -.1f);
					velocity = fireDirection* speed1;
					Particle2D partcle1 = createParticle(mass1, pos, velocity, new Vector2(0.0f, 0.0f), gravity1, 0.99f);

					mass = 4.0f;
					speed = 5.0f;
					gravity = new Vector2(0.0f, -1.0f);
					velocity = fireDirection* speed;
					Particle2D partcle2 = createParticle(mass, pos, velocity, new Vector2(0.0f, 0.0f), gravity, 0.99f);



					partcle2.gameObject.AddComponent<SpringForceGenerator>();
					SpringForceGenerator springGenerator = partcle2.GetComponent<SpringForceGenerator>();
					springGenerator = new SpringForceGenerator();
					springGenerator.SpringForceGeneratorIntializer(partcle1, partcle2, 5.0f, 1.0f);
					ForceManager.addForceGenerator(springGenerator);
				}
				return null;
				break;
			}
			case ProjectileType.ROD:
			{
				if (maxNumParticles - mParticleMap.Count > 1)
				{
					float mass1 = 1.1f;
					float speed1 = 1.0f;
					Vector2 gravity1 = new Vector2(0.0f, -1.0f);

					velocity = fireDirection* speed1;
					Particle2D particle1 = createParticle(mass1, pos, velocity, new Vector2(0.0f, 0.0f), gravity1, 0.99f);

					Vector2 pos2 = pos + (fireDirection);

					mass = 1.5f;
					speed = 10.0f;
					gravity = new Vector2(0.0f, -2.02f);
					velocity = fireDirection* speed;
					Particle2D particle2 = createParticle(mass, pos2, velocity, new Vector2(0.0f, 0.0f), gravity, 0.99f);

					particle1.gameObject.AddComponent<ParticleRod>();
					ParticleRod pLink = particle1.gameObject.GetComponent<ParticleRod>();
					pLink.mObj1 = particle1;
					pLink.mObj2 = particle2;
					pLink.mLength = 1;
					ContactResolver.mLinks.Add(pLink);

					

				}
				return null;
				break;
			}
		};
		float damping = 0.99f;
		velocity = fireDirection* speed;

		return createParticle(mass, pos, velocity, new Vector2(0.0f, 0.0f), gravity, damping);
	}


	private void Update()
	{
		foreach(KeyValuePair<int,Particle2D> partilePair in mParticleMap)
		{
			Integrator.integrate(partilePair.Value, Time.deltaTime);
		}

		ForceManager.updateAllForces(Time.deltaTime);
		ContactResolver.resolveContacts(Time.deltaTime);
	}

}






