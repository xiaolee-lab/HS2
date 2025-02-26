using System;
using Exploder.Utils;
using UnityEngine;

namespace Exploder
{
	// Token: 0x020003C9 RID: 969
	public class ExploderSlowMotion : MonoBehaviour
	{
		// Token: 0x06001131 RID: 4401 RVA: 0x00064F0A File Offset: 0x0006330A
		private void Start()
		{
			this.Exploder = ExploderSingleton.Instance;
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x00064F18 File Offset: 0x00063318
		public void EnableSlowMotion(bool status)
		{
			this.slowmo = status;
			if (this.slowmo)
			{
				this.slowMotionSpeed = 0.05f;
				if (this.Exploder)
				{
					this.Exploder.FragmentOptions.MeshColliders = true;
				}
			}
			else
			{
				this.slowMotionSpeed = 1f;
				if (this.Exploder)
				{
					this.Exploder.FragmentOptions.MeshColliders = false;
				}
			}
			this.slowMotionTime = this.slowMotionSpeed;
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x00064FA0 File Offset: 0x000633A0
		public void Update()
		{
			this.slowMotionSpeed = this.slowMotionTime;
			Time.timeScale = this.slowMotionSpeed;
			Time.fixedDeltaTime = this.slowMotionSpeed * 0.02f;
			if (Input.GetKeyDown(KeyCode.T))
			{
				this.slowmo = !this.slowmo;
				this.EnableSlowMotion(this.slowmo);
			}
		}

		// Token: 0x0400131A RID: 4890
		public float slowMotionTime = 1f;

		// Token: 0x0400131B RID: 4891
		private ExploderObject Exploder;

		// Token: 0x0400131C RID: 4892
		private float slowMotionSpeed = 1f;

		// Token: 0x0400131D RID: 4893
		private bool slowmo;
	}
}
