using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000434 RID: 1076
public class ME_ParticleTrails : MonoBehaviour
{
	// Token: 0x0600139A RID: 5018 RVA: 0x000792C8 File Offset: 0x000776C8
	private void Start()
	{
		this.ps = base.GetComponent<ParticleSystem>();
		this.particles = new ParticleSystem.Particle[this.ps.main.maxParticles];
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x000792FF File Offset: 0x000776FF
	private void OnEnable()
	{
		base.InvokeRepeating("ClearEmptyHashes", 1f, 1f);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00079316 File Offset: 0x00077716
	private void OnDisable()
	{
		this.Clear();
		base.CancelInvoke("ClearEmptyHashes");
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x0007932C File Offset: 0x0007772C
	public void Clear()
	{
		foreach (GameObject obj in this.currentGO)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.currentGO.Clear();
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00079394 File Offset: 0x00077794
	private void Update()
	{
		this.UpdateTrail();
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x0007939C File Offset: 0x0007779C
	private void UpdateTrail()
	{
		this.newHashTrails.Clear();
		int num = this.ps.GetParticles(this.particles);
		for (int i = 0; i < num; i++)
		{
			if (!this.hashTrails.ContainsKey(this.particles[i].randomSeed))
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.TrailPrefab, base.transform.position, default(Quaternion));
				gameObject.transform.parent = base.transform;
				this.currentGO.Add(gameObject);
				this.newHashTrails.Add(this.particles[i].randomSeed, gameObject);
				LineRenderer component = gameObject.GetComponent<LineRenderer>();
				component.widthMultiplier *= this.particles[i].startSize;
			}
			else
			{
				GameObject gameObject2 = this.hashTrails[this.particles[i].randomSeed];
				if (gameObject2 != null)
				{
					LineRenderer component2 = gameObject2.GetComponent<LineRenderer>();
					component2.startColor *= this.particles[i].GetCurrentColor(this.ps);
					component2.endColor *= this.particles[i].GetCurrentColor(this.ps);
					if (this.ps.main.simulationSpace == ParticleSystemSimulationSpace.World)
					{
						gameObject2.transform.position = this.particles[i].position;
					}
					if (this.ps.main.simulationSpace == ParticleSystemSimulationSpace.Local)
					{
						gameObject2.transform.position = this.ps.transform.TransformPoint(this.particles[i].position);
					}
					this.newHashTrails.Add(this.particles[i].randomSeed, gameObject2);
				}
				this.hashTrails.Remove(this.particles[i].randomSeed);
			}
		}
		foreach (KeyValuePair<uint, GameObject> keyValuePair in this.hashTrails)
		{
			if (keyValuePair.Value != null)
			{
				keyValuePair.Value.GetComponent<ME_TrailRendererNoise>().IsActive = false;
			}
		}
		this.AddRange<uint, GameObject>(this.hashTrails, this.newHashTrails);
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00079648 File Offset: 0x00077A48
	public void AddRange<T, S>(Dictionary<T, S> source, Dictionary<T, S> collection)
	{
		if (collection == null)
		{
			return;
		}
		foreach (KeyValuePair<T, S> keyValuePair in collection)
		{
			if (!source.ContainsKey(keyValuePair.Key))
			{
				source.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x000796C8 File Offset: 0x00077AC8
	private void ClearEmptyHashes()
	{
		this.hashTrails = (from h in this.hashTrails
		where h.Value != null
		select h).ToDictionary((KeyValuePair<uint, GameObject> h) => h.Key, (KeyValuePair<uint, GameObject> h) => h.Value);
	}

	// Token: 0x04001601 RID: 5633
	public GameObject TrailPrefab;

	// Token: 0x04001602 RID: 5634
	private ParticleSystem ps;

	// Token: 0x04001603 RID: 5635
	private ParticleSystem.Particle[] particles;

	// Token: 0x04001604 RID: 5636
	private Dictionary<uint, GameObject> hashTrails = new Dictionary<uint, GameObject>();

	// Token: 0x04001605 RID: 5637
	private Dictionary<uint, GameObject> newHashTrails = new Dictionary<uint, GameObject>();

	// Token: 0x04001606 RID: 5638
	private List<GameObject> currentGO = new List<GameObject>();
}
