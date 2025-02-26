using System;
using UnityEngine;

namespace ADV
{
	// Token: 0x02000784 RID: 1924
	public class BackupPosRot
	{
		// Token: 0x06002CFD RID: 11517 RVA: 0x00101431 File Offset: 0x000FF831
		public BackupPosRot(Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			this.position = transform.localPosition;
			this.rotation = transform.localRotation;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x0010145E File Offset: 0x000FF85E
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x00101466 File Offset: 0x000FF866
		public Vector3 position { get; private set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0010146F File Offset: 0x000FF86F
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x00101477 File Offset: 0x000FF877
		public Quaternion rotation { get; private set; }

		// Token: 0x06002D02 RID: 11522 RVA: 0x00101480 File Offset: 0x000FF880
		public void Set(Transform transform)
		{
			if (transform == null)
			{
				return;
			}
			transform.localPosition = this.position;
			transform.localRotation = this.rotation;
		}
	}
}
