using System;
using Illusion.Extensions;
using SceneAssist;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02001035 RID: 4149
	public class TitleSaveItemInfo : MonoBehaviour
	{
		// Token: 0x06008AFC RID: 35580 RVA: 0x003A7B46 File Offset: 0x003A5F46
		private void Awake()
		{
			if (this.txtInitialize)
			{
				this.txtInitialize.text = this.initializeComment;
			}
			if (this.objSelect)
			{
				this.objSelect.SetActiveIfDifferent(false);
			}
		}

		// Token: 0x04007175 RID: 29045
		public Button btnEntry;

		// Token: 0x04007176 RID: 29046
		public GameObject objSelect;

		// Token: 0x04007177 RID: 29047
		public PointerEnterExitAction action;

		// Token: 0x04007178 RID: 29048
		public SelectUI selectUI;

		// Token: 0x04007179 RID: 29049
		[Header("セーブデータあり")]
		public GameObject objSave;

		// Token: 0x0400717A RID: 29050
		public Text txtTitle;

		// Token: 0x0400717B RID: 29051
		public Text txtDay;

		// Token: 0x0400717C RID: 29052
		public Text txtTime;

		// Token: 0x0400717D RID: 29053
		[Header("セーブデータなし")]
		public GameObject objInitialize;

		// Token: 0x0400717E RID: 29054
		public Text txtInitialize;

		// Token: 0x0400717F RID: 29055
		[Header("初期値")]
		public string initializeComment = string.Empty;

		// Token: 0x04007180 RID: 29056
		[Header("セーブファイル情報")]
		public string path = string.Empty;

		// Token: 0x04007181 RID: 29057
		public int num;

		// Token: 0x04007182 RID: 29058
		public bool isData;
	}
}
