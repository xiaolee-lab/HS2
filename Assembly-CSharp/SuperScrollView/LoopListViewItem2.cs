using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x02000605 RID: 1541
	public class LoopListViewItem2 : MonoBehaviour
	{
		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06002443 RID: 9283 RVA: 0x000CC892 File Offset: 0x000CAC92
		// (set) Token: 0x06002444 RID: 9284 RVA: 0x000CC89A File Offset: 0x000CAC9A
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

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06002445 RID: 9285 RVA: 0x000CC8A3 File Offset: 0x000CACA3
		// (set) Token: 0x06002446 RID: 9286 RVA: 0x000CC8AB File Offset: 0x000CACAB
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

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x06002447 RID: 9287 RVA: 0x000CC8B4 File Offset: 0x000CACB4
		// (set) Token: 0x06002448 RID: 9288 RVA: 0x000CC8BC File Offset: 0x000CACBC
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

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x000CC8C5 File Offset: 0x000CACC5
		// (set) Token: 0x0600244A RID: 9290 RVA: 0x000CC8CD File Offset: 0x000CACCD
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

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x000CC8D6 File Offset: 0x000CACD6
		// (set) Token: 0x0600244C RID: 9292 RVA: 0x000CC8DE File Offset: 0x000CACDE
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

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x0600244D RID: 9293 RVA: 0x000CC8E7 File Offset: 0x000CACE7
		// (set) Token: 0x0600244E RID: 9294 RVA: 0x000CC8EF File Offset: 0x000CACEF
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

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x0600244F RID: 9295 RVA: 0x000CC8F8 File Offset: 0x000CACF8
		// (set) Token: 0x06002450 RID: 9296 RVA: 0x000CC900 File Offset: 0x000CAD00
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

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06002451 RID: 9297 RVA: 0x000CC909 File Offset: 0x000CAD09
		// (set) Token: 0x06002452 RID: 9298 RVA: 0x000CC911 File Offset: 0x000CAD11
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

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06002453 RID: 9299 RVA: 0x000CC91A File Offset: 0x000CAD1A
		// (set) Token: 0x06002454 RID: 9300 RVA: 0x000CC922 File Offset: 0x000CAD22
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

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06002455 RID: 9301 RVA: 0x000CC92B File Offset: 0x000CAD2B
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

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06002456 RID: 9302 RVA: 0x000CC955 File Offset: 0x000CAD55
		// (set) Token: 0x06002457 RID: 9303 RVA: 0x000CC95D File Offset: 0x000CAD5D
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

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x000CC966 File Offset: 0x000CAD66
		// (set) Token: 0x06002459 RID: 9305 RVA: 0x000CC96E File Offset: 0x000CAD6E
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

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x000CC977 File Offset: 0x000CAD77
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x000CC97F File Offset: 0x000CAD7F
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

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000CC988 File Offset: 0x000CAD88
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x000CC990 File Offset: 0x000CAD90
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

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600245E RID: 9310 RVA: 0x000CC999 File Offset: 0x000CAD99
		// (set) Token: 0x0600245F RID: 9311 RVA: 0x000CC9A1 File Offset: 0x000CADA1
		public LoopListView2 ParentListView
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06002460 RID: 9312 RVA: 0x000CC9AC File Offset: 0x000CADAC
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

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002461 RID: 9313 RVA: 0x000CCA14 File Offset: 0x000CAE14
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

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002462 RID: 9314 RVA: 0x000CCA7C File Offset: 0x000CAE7C
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

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002463 RID: 9315 RVA: 0x000CCAE4 File Offset: 0x000CAEE4
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

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x06002464 RID: 9316 RVA: 0x000CCB4C File Offset: 0x000CAF4C
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

		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06002465 RID: 9317 RVA: 0x000CCB90 File Offset: 0x000CAF90
		public float ItemSizeWithPadding
		{
			get
			{
				return this.ItemSize + this.mPadding;
			}
		}

		// Token: 0x04002398 RID: 9112
		private int mItemIndex = -1;

		// Token: 0x04002399 RID: 9113
		private int mItemId = -1;

		// Token: 0x0400239A RID: 9114
		private LoopListView2 mParentListView;

		// Token: 0x0400239B RID: 9115
		private bool mIsInitHandlerCalled;

		// Token: 0x0400239C RID: 9116
		private string mItemPrefabName;

		// Token: 0x0400239D RID: 9117
		private RectTransform mCachedRectTransform;

		// Token: 0x0400239E RID: 9118
		private float mPadding;

		// Token: 0x0400239F RID: 9119
		private float mDistanceWithViewPortSnapCenter;

		// Token: 0x040023A0 RID: 9120
		private int mItemCreatedCheckFrameCount;

		// Token: 0x040023A1 RID: 9121
		private float mStartPosOffset;

		// Token: 0x040023A2 RID: 9122
		private object mUserObjectData;

		// Token: 0x040023A3 RID: 9123
		private int mUserIntData1;

		// Token: 0x040023A4 RID: 9124
		private int mUserIntData2;

		// Token: 0x040023A5 RID: 9125
		private string mUserStringData1;

		// Token: 0x040023A6 RID: 9126
		private string mUserStringData2;
	}
}
