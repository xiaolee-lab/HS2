using System;
using System.Collections.Generic;
using System.IO;
using MessagePack;

namespace AIChara
{
	// Token: 0x0200080E RID: 2062
	[MessagePackObject(true)]
	public class ChaListData
	{
		// Token: 0x06003453 RID: 13395 RVA: 0x001345C0 File Offset: 0x001329C0
		public ChaListData()
		{
			this.mark = string.Empty;
			this.categoryNo = 0;
			this.distributionNo = 0;
			this.filePath = string.Empty;
			this.lstKey = new List<string>();
			this.dictList = new Dictionary<int, List<string>>();
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003454 RID: 13396 RVA: 0x0013460D File Offset: 0x00132A0D
		// (set) Token: 0x06003455 RID: 13397 RVA: 0x00134615 File Offset: 0x00132A15
		public string mark { get; set; }

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003456 RID: 13398 RVA: 0x0013461E File Offset: 0x00132A1E
		// (set) Token: 0x06003457 RID: 13399 RVA: 0x00134626 File Offset: 0x00132A26
		public int categoryNo { get; set; }

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003458 RID: 13400 RVA: 0x0013462F File Offset: 0x00132A2F
		// (set) Token: 0x06003459 RID: 13401 RVA: 0x00134637 File Offset: 0x00132A37
		public int distributionNo { get; set; }

		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x0600345A RID: 13402 RVA: 0x00134640 File Offset: 0x00132A40
		// (set) Token: 0x0600345B RID: 13403 RVA: 0x00134648 File Offset: 0x00132A48
		public string filePath { get; set; }

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600345C RID: 13404 RVA: 0x00134651 File Offset: 0x00132A51
		// (set) Token: 0x0600345D RID: 13405 RVA: 0x00134659 File Offset: 0x00132A59
		public List<string> lstKey { get; set; }

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600345E RID: 13406 RVA: 0x00134662 File Offset: 0x00132A62
		// (set) Token: 0x0600345F RID: 13407 RVA: 0x0013466A File Offset: 0x00132A6A
		public Dictionary<int, List<string>> dictList { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003460 RID: 13408 RVA: 0x00134673 File Offset: 0x00132A73
		[IgnoreMember]
		public string fileName
		{
			get
			{
				return Path.GetFileName(this.filePath);
			}
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00134680 File Offset: 0x00132A80
		public Dictionary<string, string> GetInfoAll(int id)
		{
			List<string> list = null;
			if (!this.dictList.TryGetValue(id, out list))
			{
				return null;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			int count = this.lstKey.Count;
			if (list.Count != count)
			{
				return null;
			}
			for (int i = 0; i < count; i++)
			{
				dictionary[this.lstKey[i]] = list[i];
			}
			return dictionary;
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x001346F0 File Offset: 0x00132AF0
		public string GetInfo(int id, string key)
		{
			List<string> list = null;
			if (!this.dictList.TryGetValue(id, out list))
			{
				return string.Empty;
			}
			int num = this.lstKey.IndexOf(key);
			if (num == -1)
			{
				return string.Empty;
			}
			int count = this.lstKey.Count;
			if (list.Count != count)
			{
				return null;
			}
			return list[num];
		}

		// Token: 0x04003434 RID: 13364
		[IgnoreMember]
		public static readonly string ChaListDataMark = "【ChaListData】";
	}
}
