using System;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005FF RID: 1535
	public class LoopGridViewItem : MonoBehaviour
	{
		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x000C6E79 File Offset: 0x000C5279
		// (set) Token: 0x060023C7 RID: 9159 RVA: 0x000C6E81 File Offset: 0x000C5281
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

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060023C8 RID: 9160 RVA: 0x000C6E8A File Offset: 0x000C528A
		// (set) Token: 0x060023C9 RID: 9161 RVA: 0x000C6E92 File Offset: 0x000C5292
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060023CA RID: 9162 RVA: 0x000C6E9B File Offset: 0x000C529B
		// (set) Token: 0x060023CB RID: 9163 RVA: 0x000C6EA3 File Offset: 0x000C52A3
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

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060023CC RID: 9164 RVA: 0x000C6EAC File Offset: 0x000C52AC
		// (set) Token: 0x060023CD RID: 9165 RVA: 0x000C6EB4 File Offset: 0x000C52B4
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

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x060023CE RID: 9166 RVA: 0x000C6EBD File Offset: 0x000C52BD
		// (set) Token: 0x060023CF RID: 9167 RVA: 0x000C6EC5 File Offset: 0x000C52C5
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

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060023D0 RID: 9168 RVA: 0x000C6ECE File Offset: 0x000C52CE
		// (set) Token: 0x060023D1 RID: 9169 RVA: 0x000C6ED6 File Offset: 0x000C52D6
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

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000C6EDF File Offset: 0x000C52DF
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

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060023D3 RID: 9171 RVA: 0x000C6F09 File Offset: 0x000C5309
		// (set) Token: 0x060023D4 RID: 9172 RVA: 0x000C6F11 File Offset: 0x000C5311
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x000C6F1A File Offset: 0x000C531A
		// (set) Token: 0x060023D6 RID: 9174 RVA: 0x000C6F22 File Offset: 0x000C5322
		public int Row
		{
			get
			{
				return this.mRow;
			}
			set
			{
				this.mRow = value;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x000C6F2B File Offset: 0x000C532B
		// (set) Token: 0x060023D8 RID: 9176 RVA: 0x000C6F33 File Offset: 0x000C5333
		public int Column
		{
			get
			{
				return this.mColumn;
			}
			set
			{
				this.mColumn = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x000C6F3C File Offset: 0x000C533C
		// (set) Token: 0x060023DA RID: 9178 RVA: 0x000C6F44 File Offset: 0x000C5344
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

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060023DB RID: 9179 RVA: 0x000C6F4D File Offset: 0x000C534D
		// (set) Token: 0x060023DC RID: 9180 RVA: 0x000C6F55 File Offset: 0x000C5355
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

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060023DD RID: 9181 RVA: 0x000C6F5E File Offset: 0x000C535E
		// (set) Token: 0x060023DE RID: 9182 RVA: 0x000C6F66 File Offset: 0x000C5366
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

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060023DF RID: 9183 RVA: 0x000C6F6F File Offset: 0x000C536F
		// (set) Token: 0x060023E0 RID: 9184 RVA: 0x000C6F77 File Offset: 0x000C5377
		public LoopGridView ParentGridView
		{
			get
			{
				return this.mParentGridView;
			}
			set
			{
				this.mParentGridView = value;
			}
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060023E1 RID: 9185 RVA: 0x000C6F80 File Offset: 0x000C5380
		// (set) Token: 0x060023E2 RID: 9186 RVA: 0x000C6F88 File Offset: 0x000C5388
		public LoopGridViewItem PrevItem
		{
			get
			{
				return this.mPrevItem;
			}
			set
			{
				this.mPrevItem = value;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x000C6F91 File Offset: 0x000C5391
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x000C6F99 File Offset: 0x000C5399
		public LoopGridViewItem NextItem
		{
			get
			{
				return this.mNextItem;
			}
			set
			{
				this.mNextItem = value;
			}
		}

		// Token: 0x0400233B RID: 9019
		private int mItemIndex = -1;

		// Token: 0x0400233C RID: 9020
		private int mRow = -1;

		// Token: 0x0400233D RID: 9021
		private int mColumn = -1;

		// Token: 0x0400233E RID: 9022
		private int mItemId = -1;

		// Token: 0x0400233F RID: 9023
		private LoopGridView mParentGridView;

		// Token: 0x04002340 RID: 9024
		private bool mIsInitHandlerCalled;

		// Token: 0x04002341 RID: 9025
		private string mItemPrefabName;

		// Token: 0x04002342 RID: 9026
		private RectTransform mCachedRectTransform;

		// Token: 0x04002343 RID: 9027
		private int mItemCreatedCheckFrameCount;

		// Token: 0x04002344 RID: 9028
		private object mUserObjectData;

		// Token: 0x04002345 RID: 9029
		private int mUserIntData1;

		// Token: 0x04002346 RID: 9030
		private int mUserIntData2;

		// Token: 0x04002347 RID: 9031
		private string mUserStringData1;

		// Token: 0x04002348 RID: 9032
		private string mUserStringData2;

		// Token: 0x04002349 RID: 9033
		private LoopGridViewItem mPrevItem;

		// Token: 0x0400234A RID: 9034
		private LoopGridViewItem mNextItem;
	}
}
