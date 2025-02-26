using System;
using System.Collections.Generic;
using UnityEngine;

namespace Exploder
{
	// Token: 0x0200039E RID: 926
	internal class CrackManager
	{
		// Token: 0x06001061 RID: 4193 RVA: 0x0005BE04 File Offset: 0x0005A204
		public CrackManager(Core core)
		{
			this.crackedObjects = new Dictionary<GameObject, CrackedObject>();
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005BE18 File Offset: 0x0005A218
		public CrackedObject Create(GameObject originalObject, ExploderParams parameters)
		{
			CrackedObject crackedObject = new CrackedObject(originalObject, parameters);
			this.crackedObjects[originalObject] = crackedObject;
			return crackedObject;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005BE3C File Offset: 0x0005A23C
		public long Explode(GameObject gameObject)
		{
			if (this.crackedObjects.ContainsKey(gameObject))
			{
				long result = 0L;
				CrackedObject crackedObject;
				if (this.crackedObjects.TryGetValue(gameObject, out crackedObject))
				{
					result = crackedObject.Explode();
					this.crackedObjects.Remove(gameObject);
				}
				return result;
			}
			UnityEngine.Debug.LogErrorFormat("GameObject {0} not cracked, Call CrackObject first!", new object[]
			{
				gameObject.name
			});
			return 0L;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0005BEA4 File Offset: 0x0005A2A4
		public long ExplodePartial(GameObject gameObject, Vector3 shotDir, Vector3 hitPosition, float bulletSize)
		{
			if (this.crackedObjects.ContainsKey(gameObject))
			{
				long result = 0L;
				CrackedObject crackedObject;
				if (this.crackedObjects.TryGetValue(gameObject, out crackedObject))
				{
					result = crackedObject.ExplodePartial(gameObject, shotDir, hitPosition, bulletSize);
				}
				return result;
			}
			UnityEngine.Debug.LogErrorFormat("GameObject {0} not cracked, Call CrackObject first!", new object[]
			{
				gameObject.name
			});
			return 0L;
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005BF04 File Offset: 0x0005A304
		public long ExplodeAll()
		{
			long num = 0L;
			foreach (CrackedObject crackedObject in this.crackedObjects.Values)
			{
				num += crackedObject.Explode();
			}
			this.crackedObjects.Clear();
			return num;
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005BF78 File Offset: 0x0005A378
		public bool IsCracked(GameObject gameObject)
		{
			return this.crackedObjects.ContainsKey(gameObject);
		}

		// Token: 0x0400121D RID: 4637
		private readonly Dictionary<GameObject, CrackedObject> crackedObjects;
	}
}
