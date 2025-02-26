using System;
using System.Collections.Generic;
using AIProject.SaveData;
using Manager;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BE3 RID: 3043
	public class SearchPointParticle : MonoBehaviour
	{
		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x06005D0C RID: 23820 RVA: 0x00276215 File Offset: 0x00274615
		// (set) Token: 0x06005D0D RID: 23821 RVA: 0x0027621D File Offset: 0x0027461D
		public int ID
		{
			get
			{
				return this._id;
			}
			set
			{
				this._id = value;
			}
		}

		// Token: 0x06005D0E RID: 23822 RVA: 0x00276228 File Offset: 0x00274628
		private void Update()
		{
			if (!Singleton<Game>.IsInstance())
			{
				return;
			}
			AIProject.SaveData.Environment environment = Singleton<Game>.Instance.Environment;
			Dictionary<int, AIProject.SaveData.Environment.SearchActionInfo> dictionary = (environment != null) ? environment.SearchActionLockTable : null;
			AIProject.SaveData.Environment.SearchActionInfo searchActionInfo;
			if (!dictionary.TryGetValue(this._id, out searchActionInfo))
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

		// Token: 0x0400537E RID: 21374
		[SerializeField]
		private int _id;

		// Token: 0x0400537F RID: 21375
		[SerializeField]
		private ParticleSystem[] _particleSystems;
	}
}
