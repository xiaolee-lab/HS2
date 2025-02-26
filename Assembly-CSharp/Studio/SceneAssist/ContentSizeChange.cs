using System;
using UnityEngine;

namespace Studio.SceneAssist
{
	// Token: 0x02001294 RID: 4756
	public class ContentSizeChange : MonoBehaviour
	{
		// Token: 0x06009D51 RID: 40273 RVA: 0x004045DC File Offset: 0x004029DC
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

		// Token: 0x06009D52 RID: 40274 RVA: 0x00404678 File Offset: 0x00402A78
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

		// Token: 0x04007D31 RID: 32049
		[SerializeField]
		private RectTransform target;

		// Token: 0x04007D32 RID: 32050
		[SerializeField]
		private ContentSizeChange.SizeInfo width = new ContentSizeChange.SizeInfo();

		// Token: 0x04007D33 RID: 32051
		[SerializeField]
		private ContentSizeChange.SizeInfo height = new ContentSizeChange.SizeInfo();

		// Token: 0x02001295 RID: 4757
		[Serializable]
		public class SizeInfo
		{
			// Token: 0x04007D34 RID: 32052
			public bool use = true;

			// Token: 0x04007D35 RID: 32053
			public float enableSize;

			// Token: 0x04007D36 RID: 32054
			public float disableSize;
		}
	}
}
