using System;
using System.Collections.Generic;
using ADV;

namespace AIProject
{
	// Token: 0x02000FE7 RID: 4071
	internal class ADVParames : SceneParameter
	{
		// Token: 0x060088E0 RID: 35040 RVA: 0x0038F5CB File Offset: 0x0038D9CB
		public ADVParames(IData data) : base(data)
		{
		}

		// Token: 0x060088E1 RID: 35041 RVA: 0x0038F5D4 File Offset: 0x0038D9D4
		public override void Init()
		{
			IPack pack = base.data.pack;
			bool? flag = (pack != null) ? new bool?(pack.isCommandListVisibleEnabled) : null;
			if (flag == null || flag.Value)
			{
				MapUIContainer.CommandList.Visibled = false;
			}
			IPack pack2 = base.data.pack;
			if (pack2 != null)
			{
				pack2.CommandListVisibleEnabledDefault();
			}
			IPack pack3 = base.data.pack;
			List<Program.Transfer> transferList = ((pack3 != null) ? pack3.Create() : null) ?? Program.Transfer.NewList(true, false);
			SceneParameter.advScene.Scenario.openData.Set(base.data.openData);
			SceneParameter.advScene.Scenario.transferList = transferList;
			SceneParameter.advScene.Scenario.SetPackage(base.data.pack);
			SceneParameter.advScene.Scenario.captions.CanvasGroupON();
		}

		// Token: 0x060088E2 RID: 35042 RVA: 0x0038F6D4 File Offset: 0x0038DAD4
		public override void Release()
		{
			IPack pack = base.data.pack;
			if (pack != null)
			{
				pack.Receive(SceneParameter.advScene.Scenario);
			}
			SceneParameter.advScene.Scenario.SetPackage(null);
			MapUIContainer.SetActiveChoiceUI(false);
			Singleton<MapUIContainer>.Instance.CloseADV();
		}
	}
}
