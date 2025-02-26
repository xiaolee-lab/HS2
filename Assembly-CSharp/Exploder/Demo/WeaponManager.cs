using System;
using UnityEngine;

namespace Exploder.Demo
{
	// Token: 0x0200038D RID: 909
	public class WeaponManager : MonoBehaviour
	{
		// Token: 0x0600100B RID: 4107 RVA: 0x0005A328 File Offset: 0x00058728
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				ExploderUtils.SetActiveRecursively(this.RPG, false);
				ExploderUtils.SetActiveRecursively(this.Shotgun, true);
				this.Shotgun.GetComponent<ShotgunController>().OnActivate();
			}
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				ExploderUtils.SetActiveRecursively(this.RPG, true);
				ExploderUtils.SetActiveRecursively(this.Shotgun, false);
				this.RPG.GetComponent<RPGController>().OnActivate();
			}
		}

		// Token: 0x040011DD RID: 4573
		public GameObject Shotgun;

		// Token: 0x040011DE RID: 4574
		public GameObject RPG;
	}
}
