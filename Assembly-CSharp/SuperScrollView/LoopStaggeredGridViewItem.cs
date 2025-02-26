using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x0200060B RID: 1547
	public class LoopStaggeredGridViewItem : MonoBehaviour
	{
		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060024A3 RID: 9379 RVA: 0x000CE1F3 File Offset: 0x000CC5F3
		// (set) Token: 0x060024A4 RID: 9380 RVA: 0x000CE1FB File Offset: 0x000CC5FB
		public object UserObjectData
		{
			get
			{
				return this.mUserObjectData;
			}
			set
			{
				this.mUserObjectData = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060024A5 RID: 9381 RVA: 0x000CE204 File Offset: 0x000CC604
		// (set) Token: 0x060024A6 RID: 9382 RVA: 0x000CE20C File Offset: 0x000CC60C
		public int UserIntData1
		{
			get
			{
				return this.mUserIntData1;
			}
			set
			{
				this.mUserIntData1 = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x060024A7 RID: 9383 RVA: 0x000CE215 File Offset: 0x000CC615
		// (set) Token: 0x060024A8 RID: 9384 RVA: 0x000CE21D File Offset: 0x000CC61D
		public int UserIntData2
		{
			get
			{
				return this.mUserIntData2;
			}
			set
			{
				this.mUserIntData2 = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x060024A9 RID: 9385 RVA: 0x000CE226 File Offset: 0x000CC626
		// (set) Token: 0x060024AA RID: 9386 RVA: 0x000CE22E File Offset: 0x000CC62E
		public string UserStringData1
		{
			get
			{
				return this.mUserStringData1;
			}
			set
			{
				this.mUserStringData1 = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x060024AB RID: 9387 RVA: 0x000CE237 File Offset: 0x000CC637
		// (set) Token: 0x060024AC RID: 9388 RVA: 0x000CE23F File Offset: 0x000CC63F
		public string UserStringData2
		{
			get
			{
				return this.mUserStringData2;
			}
			set
			{
				this.mUserStringData2 = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x060024AD RID: 9389 RVA: 0x000CE248 File Offset: 0x000CC648
		// (set) Token: 0x060024AE RID: 9390 RVA: 0x000CE250 File Offset: 0x000CC650
		public float DistanceWithViewPortSnapCenter
		{
			get
			{
				return this.mDistanceWithViewPortSnapCenter;
			}
			set
			{
				this.mDistanceWithViewPortSnapCenter = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x060024AF RID: 9391 RVA: 0x000CE259 File Offset: 0x000CC659
		// (set) Token: 0x060024B0 RID: 9392 RVA: 0x000CE261 File Offset: 0x000CC661
		public float StartPosOffset
		{
			get
			{
				return this.mStartPosOffset;
			}
			set
			{
				this.mStartPosOffset = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x060024B1 RID: 9393 RVA: 0x000CE26A File Offset: 0x000CC66A
		// (set) Token: 0x060024B2 RID: 9394 RVA: 0x000CE272 File Offset: 0x000CC672
		public int ItemCreatedCheckFrameCount
		{
			get
			{
				return this.mItemCreatedCheckFrameCount;
			}
			set
			{
				this.mItemCreatedCheckFrameCount = value;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060024B3 RID: 9395 RVA: 0x000CE27B File Offset: 0x000CC67B
		// (set) Token: 0x060024B4 RID: 9396 RVA: 0x000CE283 File Offset: 0x000CC683
		public float Padding
		{
			get
			{
				return this.mPadding;
			}
			set
			{
				this.mPadding = value;
			}
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x060024B5 RID: 9397 RVA: 0x000CE28C File Offset: 0x000CC68C
		// (set) Token: 0x060024B6 RID: 9398 RVA: 0x000CE294 File Offset: 0x000CC694
		public float ExtraPadding
		{
			get
			{
				return this.mExtraPadding;
			}
			set
			{
				this.mExtraPadding = value;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x060024B7 RID: 9399 RVA: 0x000CE29D File Offset: 0x000CC69D
		public RectTransform CachedRectTransform
		{
			get
			{
				if (this.mCachedRectTransform == null)
				{
					this.mCachedRectTransform = base.gameObject.GetComponent<RectTransform>();
				}
				return this.mCachedRectTransform;
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x060024B8 RID: 9400 RVA: 0x000CE2C7 File Offset: 0x000CC6C7
		// (set) Token: 0x060024B9 RID: 9401 RVA: 0x000CE2CF File Offset: 0x000CC6CF
		public string ItemPrefabName
		{
			get
			{
				return this.mItemPrefabName;
			}
			set
			{
				this.mItemPrefabName = value;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x060024BA RID: 9402 RVA: 0x000CE2D8 File Offset: 0x000CC6D8
		// (set) Token: 0x060024BB RID: 9403 RVA: 0x000CE2E0 File Offset: 0x000CC6E0
		public int ItemIndexInGroup
		{
			get
			{
				return this.mItemIndexInGroup;
			}
			set
			{
				this.mItemIndexInGroup = value;
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060024BC RID: 9404 RVA: 0x000CE2E9 File Offset: 0x000CC6E9
		// (set) Token: 0x060024BD RID: 9405 RVA: 0x000CE2F1 File Offset: 0x000CC6F1
		public int ItemIndex
		{
			get
			{
				return this.mItemIndex;
			}
			set
			{
				this.mItemIndex = value;
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060024BE RID: 9406 RVA: 0x000CE2FA File Offset: 0x000CC6FA
		// (set) Token: 0x060024BF RID: 9407 RVA: 0x000CE302 File Offset: 0x000CC702
		public int ItemId
		{
			get
			{
				return this.mItemId;
			}
			set
			{
				this.mItemId = value;
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x000CE30B File Offset: 0x000CC70B
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x000CE313 File Offset: 0x000CC713
		public bool IsInitHandlerCalled
		{
			get
			{
				return this.mIsInitHandlerCalled;
			}
			set
			{
				this.mIsInitHandlerCalled = value;
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x000CE31C File Offset: 0x000CC71C
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x000CE324 File Offset: 0x000CC724
		public LoopStaggeredGridView ParentListView
		{
			get
			{
				return this.mParentListView;
			}
			set
			{
				this.mParentListView = value;
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x000CE330 File Offset: 0x000CC730
		public float TopY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y + this.CachedRectTransform.rect.height;
				}
				return 0f;
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x000CE398 File Offset: 0x000CC798
		public float BottomY
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.TopToBottom)
				{
					return this.CachedRectTransform.anchoredPosition3D.y - this.CachedRectTransform.rect.height;
				}
				if (arrangeType == ListItemArrangeType.BottomToTop)
				{
					return this.CachedRectTransform.anchoredPosition3D.y;
				}
				return 0f;
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x000CE400 File Offset: 0x000CC800
		public float LeftX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x - this.CachedRectTransform.rect.width;
				}
				return 0f;
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x000CE468 File Offset: 0x000CC868
		public float RightX
		{
			get
			{
				ListItemArrangeType arrangeType = this.ParentListView.ArrangeType;
				if (arrangeType == ListItemArrangeType.LeftToRight)
				{
					return this.CachedRectTransform.anchoredPosition3D.x + this.CachedRectTransform.rect.width;
				}
				if (arrangeType == ListItemArrangeType.RightToLeft)
				{
					return this.CachedRectTransform.anchoredPosition3D.x;
				}
				return 0f;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x000CE4D0 File Offset: 0x000CC8D0
		public float ItemSize
		{
			get
			{
				if (this.ParentListView.IsVertList)
				{
					return this.CachedRectTransform.rect.height;
				}
				return this.CachedRectTransform.rect.width;
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000CE514 File Offset: 0x000CC914
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x040023D4 RID: 9172
		private int mItemIndex = -1;

		// Token: 0x040023D5 RID: 9173
		private int mItemIndexInGroup = -1;

		// Token: 0x040023D6 RID: 9174
		private int mItemId = -1;

		// Token: 0x040023D7 RID: 9175
		private float mPadding;

		// Token: 0x040023D8 RID: 9176
		private float mExtraPadding;

		// Token: 0x040023D9 RID: 9177
		private bool mIsInitHandlerCalled;

		// Token: 0x040023DA RID: 9178
		private string mItemPrefabName;

		// Token: 0x040023DB RID: 9179
		private RectTransform mCachedRectTransform;

		// Token: 0x040023DC RID: 9180
		private LoopStaggeredGridView mParentListView;

		// Token: 0x040023DD RID: 9181
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x040023DE RID: 9182
		private int mItemCreatedCheckFrameCount;

		// Token: 0x040023DF RID: 9183
		private float mStartPosOffset;

		// Token: 0x040023E0 RID: 9184
		private object mUserObjectData;

		// Token: 0x040023E1 RID: 9185
		private int mUserIntData1;

		// Token: 0x040023E2 RID: 9186
		private int mUserIntData2;

		// Token: 0x040023E3 RID: 9187
		private string mUserStringData1;

		// Token: 0x040023E4 RID: 9188
		private string mUserStringData2;
	}
}
