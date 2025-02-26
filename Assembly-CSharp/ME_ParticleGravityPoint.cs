using System;
using UnityEngine;

// Token: 0x02000433 RID: 1075
[ExecuteInEditMode]
public class ME_ParticleGravityPoint : MonoBehaviour
{
	// Token: 0x06001397 RID: 5015 RVA: 0x00079113 File Offset: 0x00077513
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		this.mainModule = this.ps.main;
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x00079134 File Offset: 0x00077534
	private void LateUpdate()
	{
		int maxParticles = this.mainModule.maxParticles;
		if (this.particles == null || this.particles.Length < maxParticles)
		{
			this.particles = new ParticleSystem.Particle[maxParticles];
		}
		int num = this.ps.GetParticles(this.particles);
		Vector3 vector = Vector3.zero;
		if (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.Local)
		{
			vector = base.transform.InverseTransformPoint(this.target.position);
		}
		if (this.mainModule.simulationSpace == ParticleSystemSimulationSpace.World)
		{
			vector = this.target.position;
		}
		float num2 = Time.deltaTime * this.Force;
		if (this.DistanceRelative)
		{
			num2 *= Mathf.Abs((this.prevPos - vector).magnitude);
		}
		for (int i = 0; i < num; i++)
		{
			Vector3 value = vector - this.particles[i].position;
			Vector3 a = Vector3.Normalize(value);
			if (this.DistanceRelative)
			{
				a = Vector3.Normalize(vector - this.prevPos);
			}
			Vector3 b = a * num2;
			ParticleSystem.Particle[] array = this.particles;
			int num3 = i;
			array[num3].velocity = array[num3].velocity + b;
		}
		this.ps.SetParticles(this.particles, num);
		this.prevPos = vector;
	}

	// Token: 0x040015FA RID: 5626
	public Transform target;

	// Token: 0x040015FB RID: 5627
	public float Force = 1f;

	// Token: 0x040015FC RID: 5628
	public bool DistanceRelative;

	// Token: 0x040015FD RID: 5629
	private ParticleSystem ps;

	// Token: 0x040015FE RID: 5630
	private ParticleSystem.Particle[] particles;

	// Token: 0x040015FF RID: 5631
	private ParticleSystem.MainModule mainModule;

	// Token: 0x04001600 RID: 5632
	private Vector3 prevPos;
}
