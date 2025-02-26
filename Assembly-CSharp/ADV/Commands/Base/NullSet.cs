using System;
using Illusion;
using Illusion.Extensions;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x020006EB RID: 1771
	public class NullSet : CommandBase
	{
		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06002A42 RID: 10818 RVA: 0x000F60CE File Offset: 0x000F44CE
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Name",
					"Type"
				};
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06002A43 RID: 10819 RVA: 0x000F60E8 File Offset: 0x000F44E8
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					NullSet.Type.Base.ToString()
				};
			}
		}

		// Token: 0x06002A44 RID: 10820 RVA: 0x000F6118 File Offset: 0x000F4518
		public override void Do()
		{
			base.Do();
			int num = 0;
			string name = this.args[num++];
			string self = this.args[num++];
			switch (self.Check(true, Illusion.Utils.Enum<NullSet.Type>.Names))
			{
			case 0:
				this.Set(base.scenario.commandController.BaseRoot, name);
				break;
			case 1:
			{
				Transform cameraRoot = base.scenario.commandController.CameraRoot;
				Vector3 position = cameraRoot.position;
				Quaternion rotation = cameraRoot.rotation;
				cameraRoot.position = Vector3.zero;
				cameraRoot.rotation = Quaternion.identity;
				this.Set(base.scenario.AdvCamera.transform, name);
				cameraRoot.SetPositionAndRotation(position, rotation);
				break;
			}
			case 2:
				this.Set(base.scenario.commandController.CharaRoot, name);
				break;
			}
		}

		// Token: 0x06002A45 RID: 10821 RVA: 0x000F6210 File Offset: 0x000F4610
		private void Set(Transform transform, string name)
		{
			Transform transform2;
			if (base.scenario.commandController.NullDic.TryGetValue(name, out transform2))
			{
				transform.SetPositionAndRotation(transform2.position, transform2.rotation);
			}
		}

		// Token: 0x020006EC RID: 1772
		private enum Type
		{
			// Token: 0x04002AD5 RID: 10965
			Base,
			// Token: 0x04002AD6 RID: 10966
			Camera,
			// Token: 0x04002AD7 RID: 10967
			Chara
		}
	}
}
