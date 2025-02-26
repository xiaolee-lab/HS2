using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000432 RID: 1074
[ExecuteInEditMode]
public class ME_ParticleCollisionDecal : MonoBehaviour
{
	// Token: 0x06001391 RID: 5009 RVA: 0x00078E20 File Offset: 0x00077220
	private void OnEnable()
	{
		this.collisionEvents.Clear();
		this.collidedGameObjects.Clear();
		this.initiatorPS = base.GetComponent<ParticleSystem>();
		this.particles = new ParticleSystem.Particle[this.DecalParticles.main.maxParticles];
		if (this.InstantiateWhenZeroSpeed)
		{
			base.InvokeRepeating("CollisionDetect", 0f, 0.1f);
		}
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00078E8D File Offset: 0x0007728D
	private void OnDisable()
	{
		if (this.InstantiateWhenZeroSpeed)
		{
			base.CancelInvoke("CollisionDetect");
		}
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x00078EA8 File Offset: 0x000772A8
	private void CollisionDetect()
	{
		int aliveParticles = 0;
		if (this.InstantiateWhenZeroSpeed)
		{
			aliveParticles = this.DecalParticles.GetParticles(this.particles);
		}
		foreach (GameObject other in this.collidedGameObjects)
		{
			this.OnParticleCollisionManual(other, aliveParticles);
		}
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00078F24 File Offset: 0x00077324
	private void OnParticleCollisionManual(GameObject other, int aliveParticles = -1)
	{
		this.collisionEvents.Clear();
		int num = this.initiatorPS.GetCollisionEvents(other, this.collisionEvents);
		for (int i = 0; i < num; i++)
		{
			float num2 = Vector3.Angle(this.collisionEvents[i].normal, Vector3.up);
			if (num2 <= this.MaxGroundAngleDeviation)
			{
				if (this.InstantiateWhenZeroSpeed)
				{
					if (this.collisionEvents[i].velocity.sqrMagnitude > 0.1f)
					{
						goto IL_18D;
					}
					bool flag = false;
					for (int j = 0; j < aliveParticles; j++)
					{
						float num3 = Vector3.Distance(this.collisionEvents[i].intersection, this.particles[j].position);
						if (num3 < this.MinDistanceBetweenDecals)
						{
							flag = true;
						}
					}
					if (flag)
					{
						goto IL_18D;
					}
				}
				ParticleSystem.EmitParams emitParams = default(ParticleSystem.EmitParams);
				emitParams.position = this.collisionEvents[i].intersection + this.collisionEvents[i].normal * this.MinDistanceBetweenSurface;
				Vector3 eulerAngles = Quaternion.LookRotation(-this.collisionEvents[i].normal).eulerAngles;
				eulerAngles.z = (float)UnityEngine.Random.Range(0, 360);
				emitParams.rotation3D = eulerAngles;
				this.DecalParticles.Emit(emitParams, 1);
			}
			IL_18D:;
		}
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x000790C9 File Offset: 0x000774C9
	private void OnParticleCollision(GameObject other)
	{
		if (this.InstantiateWhenZeroSpeed)
		{
			if (!this.collidedGameObjects.Contains(other))
			{
				this.collidedGameObjects.Add(other);
			}
		}
		else
		{
			this.OnParticleCollisionManual(other, -1);
		}
	}

	// Token: 0x040015F0 RID: 5616
	public ParticleSystem DecalParticles;

	// Token: 0x040015F1 RID: 5617
	public bool IsBilboard;

	// Token: 0x040015F2 RID: 5618
	public bool InstantiateWhenZeroSpeed;

	// Token: 0x040015F3 RID: 5619
	public float MaxGroundAngleDeviation = 45f;

	// Token: 0x040015F4 RID: 5620
	public float MinDistanceBetweenDecals = 0.1f;

	// Token: 0x040015F5 RID: 5621
	public float MinDistanceBetweenSurface = 0.03f;

	// Token: 0x040015F6 RID: 5622
	private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

	// Token: 0x040015F7 RID: 5623
	private ParticleSystem.Particle[] particles;

	// Token: 0x040015F8 RID: 5624
	private ParticleSystem initiatorPS;

	// Token: 0x040015F9 RID: 5625
	private List<GameObject> collidedGameObjects = new List<GameObject>();
}
