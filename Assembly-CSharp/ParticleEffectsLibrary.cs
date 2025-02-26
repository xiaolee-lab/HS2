using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000414 RID: 1044
public class ParticleEffectsLibrary : MonoBehaviour
{
	// Token: 0x06001309 RID: 4873 RVA: 0x00075314 File Offset: 0x00073714
	private void Awake()
	{
		ParticleEffectsLibrary.GlobalAccess = this;
		this.currentActivePEList = new List<Transform>();
		this.TotalEffects = this.ParticleEffectPrefabs.Length;
		this.CurrentParticleEffectNum = 1;
		if (this.ParticleEffectSpawnOffsets.Length != this.TotalEffects)
		{
		}
		if (this.ParticleEffectPrefabs.Length != this.TotalEffects)
		{
		}
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x0600130A RID: 4874 RVA: 0x000753D3 File Offset: 0x000737D3
	private void Start()
	{
	}

	// Token: 0x0600130B RID: 4875 RVA: 0x000753D8 File Offset: 0x000737D8
	public string GetCurrentPENameString()
	{
		return string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x0600130C RID: 4876 RVA: 0x00075448 File Offset: 0x00073848
	public void PreviousParticleEffect()
	{
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f && this.currentActivePEList.Count > 0)
		{
			for (int i = 0; i < this.currentActivePEList.Count; i++)
			{
				if (this.currentActivePEList[i] != null)
				{
					UnityEngine.Object.Destroy(this.currentActivePEList[i].gameObject);
				}
			}
			this.currentActivePEList.Clear();
		}
		if (this.CurrentParticleEffectIndex > 0)
		{
			this.CurrentParticleEffectIndex--;
		}
		else
		{
			this.CurrentParticleEffectIndex = this.TotalEffects - 1;
		}
		this.CurrentParticleEffectNum = this.CurrentParticleEffectIndex + 1;
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x0600130D RID: 4877 RVA: 0x00075574 File Offset: 0x00073974
	public void NextParticleEffect()
	{
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f && this.currentActivePEList.Count > 0)
		{
			for (int i = 0; i < this.currentActivePEList.Count; i++)
			{
				if (this.currentActivePEList[i] != null)
				{
					UnityEngine.Object.Destroy(this.currentActivePEList[i].gameObject);
				}
			}
			this.currentActivePEList.Clear();
		}
		if (this.CurrentParticleEffectIndex < this.TotalEffects - 1)
		{
			this.CurrentParticleEffectIndex++;
		}
		else
		{
			this.CurrentParticleEffectIndex = 0;
		}
		this.CurrentParticleEffectNum = this.CurrentParticleEffectIndex + 1;
		this.effectNameString = string.Concat(new string[]
		{
			this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].name,
			" (",
			this.CurrentParticleEffectNum.ToString(),
			" of ",
			this.TotalEffects.ToString(),
			")"
		});
	}

	// Token: 0x0600130E RID: 4878 RVA: 0x000756A0 File Offset: 0x00073AA0
	public void SpawnParticleEffect(Vector3 positionInWorldToSpawn)
	{
		this.spawnPosition = positionInWorldToSpawn + this.ParticleEffectSpawnOffsets[this.CurrentParticleEffectIndex];
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex], this.spawnPosition, this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex].transform.rotation);
		gameObject.name = "PE_" + this.ParticleEffectPrefabs[this.CurrentParticleEffectIndex];
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] == 0f)
		{
			this.currentActivePEList.Add(gameObject.transform);
		}
		this.currentActivePEList.Add(gameObject.transform);
		if (this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex] != 0f)
		{
			UnityEngine.Object.Destroy(gameObject, this.ParticleEffectLifetimes[this.CurrentParticleEffectIndex]);
		}
	}

	// Token: 0x04001536 RID: 5430
	public static ParticleEffectsLibrary GlobalAccess;

	// Token: 0x04001537 RID: 5431
	public int TotalEffects;

	// Token: 0x04001538 RID: 5432
	public int CurrentParticleEffectIndex;

	// Token: 0x04001539 RID: 5433
	public int CurrentParticleEffectNum;

	// Token: 0x0400153A RID: 5434
	public Vector3[] ParticleEffectSpawnOffsets;

	// Token: 0x0400153B RID: 5435
	public float[] ParticleEffectLifetimes;

	// Token: 0x0400153C RID: 5436
	public GameObject[] ParticleEffectPrefabs;

	// Token: 0x0400153D RID: 5437
	private string effectNameString = string.Empty;

	// Token: 0x0400153E RID: 5438
	private List<Transform> currentActivePEList;

	// Token: 0x0400153F RID: 5439
	private Vector3 spawnPosition = Vector3.zero;
}
