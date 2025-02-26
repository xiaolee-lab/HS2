using System;
using System.Collections;
using Manager;
using UnityEngine;

namespace CreateThumbnailAnother
{
	// Token: 0x02000881 RID: 2177
	public class CreateThumbnail : BaseLoader
	{
		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x00149BBA File Offset: 0x00147FBA
		// (set) Token: 0x060037B8 RID: 14264 RVA: 0x00149BC2 File Offset: 0x00147FC2
		public bool isInit { get; private set; }

		// Token: 0x170009E0 RID: 2528
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x00149BCC File Offset: 0x00147FCC
		public int frame
		{
			set
			{
				for (int i = 0; i < this.imageInfos.Length; i++)
				{
					this.imageInfos[i].active = (i == value);
				}
			}
		}

		// Token: 0x060037BA RID: 14266 RVA: 0x00149C04 File Offset: 0x00148004
		private IEnumerator Start()
		{
			yield return new WaitWhile(() => !Singleton<Housing>.Instance.IsLoadList);
			this.frame = 0;
			if (CreateThumbnail.action != null)
			{
				CreateThumbnail.action();
			}
			this.isInit = true;
			yield break;
		}

		// Token: 0x0400384C RID: 14412
		public CameraControl camCtrl;

		// Token: 0x0400384D RID: 14413
		public Camera camMain;

		// Token: 0x0400384E RID: 14414
		public Camera camBack;

		// Token: 0x0400384F RID: 14415
		public Camera camFront;

		// Token: 0x04003850 RID: 14416
		public CreateThumbnail.ImageInfo[] imageInfos;

		// Token: 0x04003852 RID: 14418
		public static Action action;

		// Token: 0x02000882 RID: 2178
		[Serializable]
		public class ImageInfo
		{
			// Token: 0x170009E1 RID: 2529
			// (set) Token: 0x060037BD RID: 14269 RVA: 0x00149C2C File Offset: 0x0014802C
			public bool active
			{
				set
				{
					this.objFront.SafeProc(delegate(GameObject _o)
					{
						_o.SetActive(value);
					});
					this.objBack.SafeProc(delegate(GameObject _o)
					{
						_o.SetActive(value);
					});
				}
			}

			// Token: 0x04003853 RID: 14419
			public GameObject objFront;

			// Token: 0x04003854 RID: 14420
			public GameObject objBack;
		}
	}
}
