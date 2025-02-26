using System;
using UnityEngine;

namespace ADV.Commands.Game
{
	// Token: 0x0200073E RID: 1854
	public class CameraLookAt : CommandBase
	{
		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x000FD521 File Offset: 0x000FB921
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"X",
					"Y",
					"Z"
				};
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06002BE3 RID: 11235 RVA: 0x000FD541 File Offset: 0x000FB941
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty
				};
			}
		}

		// Token: 0x06002BE4 RID: 11236 RVA: 0x000FD554 File Offset: 0x000FB954
		public override void Do()
		{
			base.Do();
			int num = 0;
			Vector3 worldPosition;
			if (!base.scenario.commandController.GetV3Dic(this.args[num], out worldPosition))
			{
				CommandBase.CountAddV3(this.args, ref num, ref worldPosition);
			}
			base.scenario.AdvCamera.transform.LookAt(worldPosition);
		}
	}
}
