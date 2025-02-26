using System;
using UnityEngine;

namespace SceneAssist
{
	// Token: 0x02001024 RID: 4132
	public class ContentSizeChange : MonoBehaviour
	{
		// Token: 0x06008A8D RID: 35469 RVA: 0x003A43C0 File Offset: 0x003A27C0
		private void OnEnable()
		{
			if (this.target == null)
			{
				return;
			}
			if (!this.width.use && !this.height.use)
			{
				return;
			}
			Vector2 sizeDelta = this.target.sizeDelta;
			if (this.width.use)
			{
				sizeDelta.x = this.width.enableSize;
			}
			if (this.height.use)
			{
				sizeDelta.y = this.height.enableSize;
			}
			this.target.sizeDelta = sizeDelta;
		}

		// Token: 0x06008A8E RID: 35470 RVA: 0x003A445C File Offset: 0x003A285C
		private void OnDisable()
		{
			if (this.target == null)
			{
				return;
			}
			if (!this.width.use && !this.height.use)
			{
				return;
			}
			Vector2 sizeDelta = this.target.sizeDelta;
			if (this.width.use)
			{
				sizeDelta.x = this.width.disableSize;
			}
			if (this.height.use)
			{
				sizeDelta.y = this.height.disableSize;
			}
			this.target.sizeDelta = sizeDelta;
		}

		// Token: 0x0400710B RID: 28939
		[SerializeField]
		private RectTransform target;

		// Token: 0x0400710C RID: 28940
		[SerializeField]
		private ContentSizeChange.SizeInfo width = new ContentSizeChange.SizeInfo();

		// Token: 0x0400710D RID: 28941
		[SerializeField]
		private ContentSizeChange.SizeInfo height = new ContentSizeChange.SizeInfo();

		// Token: 0x02001025 RID: 4133
		[Serializable]
		public class SizeInfo
		{
			// Token: 0x0400710E RID: 28942
			public bool use = true;

			// Token: 0x0400710F RID: 28943
			public float enableSize;

			// Token: 0x04007110 RID: 28944
			public float disableSize;
		}
	}
}
