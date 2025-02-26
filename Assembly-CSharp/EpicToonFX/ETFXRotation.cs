using System;
using UnityEngine;

namespace EpicToonFX
{
	// Token: 0x02000421 RID: 1057
	public class ETFXRotation : MonoBehaviour
	{
		// Token: 0x0600134C RID: 4940 RVA: 0x00076656 File Offset: 0x00074A56
		private void Start()
		{
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00076658 File Offset: 0x00074A58
		private void Update()
		{
			if (this.rotateSpace == ETFXRotation.spaceEnum.Local)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime);
			}
			if (this.rotateSpace == ETFXRotation.spaceEnum.World)
			{
				base.transform.Rotate(this.rotateVector * Time.deltaTime, Space.World);
			}
		}

		// Token: 0x0400157A RID: 5498
		[Header("Rotate axises by degrees per second")]
		public Vector3 rotateVector = Vector3.zero;

		// Token: 0x0400157B RID: 5499
		public ETFXRotation.spaceEnum rotateSpace;

		// Token: 0x02000422 RID: 1058
		public enum spaceEnum
		{
			// Token: 0x0400157D RID: 5501
			Local,
			// Token: 0x0400157E RID: 5502
			World
		}
	}
}
