using System;
using UnityEngine;

namespace AIProject.RootMotion
{
	// Token: 0x02000964 RID: 2404
	public class RigLine : MonoBehaviour
	{
		// Token: 0x060042AF RID: 17071 RVA: 0x001A3143 File Offset: 0x001A1543
		private void Start()
		{
			if (this._parent == null)
			{
				this._parent = base.transform;
			}
		}

		// Token: 0x04003F90 RID: 16272
		[SerializeField]
		private Transform _parent;
	}
}
