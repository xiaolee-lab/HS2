using System;
using System.Text;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Housing
{
	// Token: 0x020008A8 RID: 2216
	[Serializable]
	public class InfoUICtrl : UIDerived
	{
		// Token: 0x06003995 RID: 14741 RVA: 0x00152EB4 File Offset: 0x001512B4
		public override void Init(UICtrl _uiCtrl, bool _tutorial)
		{
			base.Init(_uiCtrl, _tutorial);
			StringBuilder stringBuilder = new StringBuilder();
			if (Singleton<Map>.IsInstance())
			{
				stringBuilder.AppendFormat("拠点{0}", Singleton<Map>.Instance.HousingID + 1);
			}
			if (Singleton<Game>.IsInstance() && Singleton<Game>.Instance.WorldData != null && Singleton<Map>.IsInstance())
			{
				CraftInfo craftInfo = null;
				if (Singleton<Game>.Instance.WorldData.HousingData.CraftInfos.TryGetValue(Singleton<Map>.Instance.HousingID, out craftInfo))
				{
					stringBuilder.AppendLine();
					stringBuilder.AppendFormat("範囲 {0:#} : {1:#} : {2:#}", craftInfo.LimitSize.x, craftInfo.LimitSize.y, craftInfo.LimitSize.z);
				}
			}
			this.textInfo.text = stringBuilder.ToString();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x00152FA3 File Offset: 0x001513A3
		public override void UpdateUI()
		{
		}

		// Token: 0x0400393A RID: 14650
		[SerializeField]
		private Text textInfo;
	}
}
