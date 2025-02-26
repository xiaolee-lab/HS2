using System;
using UnityEngine;

namespace Exploder
{
	// Token: 0x02000393 RID: 915
	public class Example : MonoBehaviour
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0005AAAC File Offset: 0x00058EAC
		public void ExplodeObject(GameObject obj)
		{
			this.Exploder.ExplodeObject(obj, new ExploderObject.OnExplosion(this.OnExplosion));
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0005AAC6 File Offset: 0x00058EC6
		private void OnExplosion(float time, ExploderObject.ExplosionState state)
		{
			if (state == ExploderObject.ExplosionState.ExplosionFinished)
			{
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0005AACF File Offset: 0x00058ECF
		private void CrackAndExplodeObject(GameObject obj)
		{
			this.Exploder.CrackObject(obj, new ExploderObject.OnExplosion(this.OnCracked));
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0005AAE9 File Offset: 0x00058EE9
		private void OnCracked(float time, ExploderObject.ExplosionState state)
		{
			this.Exploder.ExplodeCracked(new ExploderObject.OnExplosion(this.OnExplosion));
		}

		// Token: 0x040011EE RID: 4590
		public ExploderObject Exploder;
	}
}
