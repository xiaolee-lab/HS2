using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C2B RID: 3115
	public class SpaceTypeChangeVolume : MonoBehaviour
	{
		// Token: 0x170012F9 RID: 4857
		// (get) Token: 0x0600606E RID: 24686 RVA: 0x0028920C File Offset: 0x0028760C
		public SpaceTypeChangeVolume.SpaceType Type
		{
			[CompilerGenerated]
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x00289214 File Offset: 0x00287614
		public bool Check(Vector3 point)
		{
			bool result = false;
			foreach (Collider collider in this._colliders)
			{
				Vector3 b = collider.ClosestPoint(point);
				float a = Vector3.Distance(point, b);
				if (Mathf.Approximately(a, 0.0001f))
				{
					result = true;
					break;
				}
			}
			return result;
		}

		// Token: 0x0400559F RID: 21919
		[SerializeField]
		private SpaceTypeChangeVolume.SpaceType _type;

		// Token: 0x040055A0 RID: 21920
		[SerializeField]
		private Collider[] _colliders;

		// Token: 0x02000C2C RID: 3116
		public enum SpaceType
		{
			// Token: 0x040055A2 RID: 21922
			Outdoor,
			// Token: 0x040055A3 RID: 21923
			Indoor,
			// Token: 0x040055A4 RID: 21924
			Roof
		}
	}
}
