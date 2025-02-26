using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Illusion;

namespace AIProject
{
	// Token: 0x02000F12 RID: 3858
	public class HarvestDataInfo
	{
		// Token: 0x06007E44 RID: 32324 RVA: 0x0035BFAC File Offset: 0x0035A3AC
		public HarvestDataInfo(HarvestData.Param data)
		{
			this._data = data;
			IEnumerable<IGrouping<int, HarvestData.Data>> source = data.data.ToLookup((HarvestData.Data v) => v.group, (HarvestData.Data v) => v);
			Func<IGrouping<int, HarvestData.Data>, int> keySelector = (IGrouping<int, HarvestData.Data> v) => v.Key;
			if (HarvestDataInfo.<>f__mg$cache0 == null)
			{
				HarvestDataInfo.<>f__mg$cache0 = new Func<IGrouping<int, HarvestData.Data>, List<HarvestData.Data>>(Enumerable.ToList<HarvestData.Data>);
			}
			this.table = source.ToDictionary(keySelector, HarvestDataInfo.<>f__mg$cache0);
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x0035C050 File Offset: 0x0035A450
		public IReadOnlyCollection<HarvestData.Data> Get()
		{
			List<HarvestData.Data> list = new List<HarvestData.Data>();
			foreach (KeyValuePair<int, List<HarvestData.Data>> keyValuePair in this.table)
			{
				Dictionary<HarvestData.Data, int> dictionary = keyValuePair.Value.ToDictionary((HarvestData.Data p) => p, (HarvestData.Data p) => p.percent);
				if (dictionary.Any<KeyValuePair<HarvestData.Data, int>>())
				{
					HarvestData.Data data = Illusion.Utils.ProbabilityCalclator.DetermineFromDict<HarvestData.Data>(dictionary);
					if (data.nameHash != -1)
					{
						list.Add(data);
					}
				}
			}
			return list;
		}

		// Token: 0x170018FB RID: 6395
		// (get) Token: 0x06007E46 RID: 32326 RVA: 0x0035C128 File Offset: 0x0035A528
		private HarvestData.Param _data { get; }

		// Token: 0x170018FC RID: 6396
		// (get) Token: 0x06007E47 RID: 32327 RVA: 0x0035C130 File Offset: 0x0035A530
		public Dictionary<int, List<HarvestData.Data>> table { get; }

		// Token: 0x170018FD RID: 6397
		// (get) Token: 0x06007E48 RID: 32328 RVA: 0x0035C138 File Offset: 0x0035A538
		public int Time
		{
			[CompilerGenerated]
			get
			{
				return this._data.time;
			}
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x06007E49 RID: 32329 RVA: 0x0035C145 File Offset: 0x0035A545
		public int nameHash
		{
			[CompilerGenerated]
			get
			{
				return this._data.nameHash;
			}
		}

		// Token: 0x040065E4 RID: 26084
		[CompilerGenerated]
		private static Func<IGrouping<int, HarvestData.Data>, List<HarvestData.Data>> <>f__mg$cache0;
	}
}
