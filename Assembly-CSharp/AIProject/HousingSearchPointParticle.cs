using System;
using System.Collections.Generic;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BE0 RID: 3040
	public class HousingSearchPointParticle : MonoBehaviour
	{
		// Token: 0x06005D01 RID: 23809 RVA: 0x00275D6C File Offset: 0x0027416C
		private void Start()
		{
			this._searchActionPoint = base.GetComponent<SearchActionPoint>();
		}

		// Token: 0x06005D02 RID: 23810 RVA: 0x00275D7C File Offset: 0x0027417C
		private void Update()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			if (this._searchActionPoint == null)
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> dictionary = (environment != null) ? environment.SearchActionLockTable : null;
			AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
			if (!dictionary.TryGetValue(this._searchActionPoint.RegisterID, out searchActionInfo))
			{
				return;
			}
			foreach (ParticleSystem particleSystem in this._particleSystems)
			{
				if (!(particleSystem == null))
				{
					ParticleSystem.EmissionModule emission = particleSystem.emission;
					bool enabled = emission.enabled;
					bool flag = searchActionInfo.Count <= 0;
					if (enabled != flag)
					{
						emission.enabled = flag;
					}
				}
			}
		}

		// Token: 0x04005377 RID: 21367
		[SerializeField]
		private ParticleSystem[] _particleSystems;

		// Token: 0x04005378 RID: 21368
		private SearchActionPoint _searchActionPoint;
	}
}
