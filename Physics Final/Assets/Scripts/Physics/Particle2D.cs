using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle2D : MonoBehaviour
{
    // Start is called before the first frame update
    public double mMass;
    double mInverseMass;
    public Vector2 mPos;
    public Vector2 mGrav;
    public Vector2 mVel;
    public Vector2 mAcc;
    public Vector2 mAccumulatedForces;
    public float mDampingConstant;
    public bool mShouldIgnoreForces;
    public Int32 mID;

    public void InstatiateParticle2D(double mass, Vector2 pos, Vector2 vel, Vector2 acc, Vector2 grav, float dampingConstant, Int32 id)
    {
        mMass = mass;
        mInverseMass = 1/mass;
        mPos = pos;
        mVel = vel;
        mAcc = acc;
        mGrav = grav;
        mDampingConstant = dampingConstant;
        mID = id;
}
    public void setMass(float Mass)
    {
        mMass = Mass;
        mInverseMass = 1 / Mass;
    }
    public double getMass()
    {
        return mMass;
    }
    public double getInverseMass()
    {
        return mInverseMass;
    }
    public void Update()
    {
        this.transform.position = mPos;
    }
}
