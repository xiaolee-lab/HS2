using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F7D RID: 3965
	public class SearchAreaProbabilities : ScriptableObject
	{
		// Token: 0x17001C46 RID: 7238
		// (get) Token: 0x060083FA RID: 33786 RVA: 0x00371DA8 File Offset: 0x003701A8
		private Dictionary<int, float> Table
		{
			[CompilerGenerated]
			get
			{
				Dictionary<int, float> dictionary = new Dictionary<int, float>();
				dictionary[0] = this._hand;
				dictionary[3] = this._shovel;
				dictionary[4] = this._pickel;
				dictionary[5] = this._net;
				dictionary[6] = this._fishing;
				return dictionary;
			}
		}

		// Token: 0x060083FB RID: 33787 RVA: 0x00371E00 File Offset: 0x00370200
		public int Lottery(Dictionary<int, bool> filter = null, List<SearchAreaProbabilities.AddProb> list = null)
		{
			Dictionary<int, float> table = this.Table;
			int[] array = table.Keys.ToArray<int>();
			float num = 0f;
			if (!list.IsNullOrEmpty<SearchAreaProbabilities.AddProb>())
			{
				foreach (SearchAreaProbabilities.AddProb addProb in list)
				{
					float num2 = addProb.add / (float)table.Count - 1f;
					foreach (int num3 in array)
					{
						if (num3 == addProb.id)
						{
							Dictionary<int, float> dictionary;
							int key;
							(dictionary = table)[key = num3] = dictionary[key] + addProb.add;
						}
						else
						{
							Dictionary<int, float> dictionary;
							int key2;
							(dictionary = table)[key2 = num3] = dictionary[key2] - num2;
						}
					}
				}
			}
			Dictionary<int, float> dictionary2;
			if (!filter.IsNullOrEmpty<int, bool>())
			{
				dictionary2 = new Dictionary<int, float>();
				foreach (KeyValuePair<int, bool> keyValuePair in filter)
				{
					if (keyValuePair.Value)
					{
						dictionary2[keyValuePair.Key] = table[keyValuePair.Key];
					}
				}
			}
			else
			{
				dictionary2 = table;
			}
			int[] array3 = dictionary2.Keys.ToArray<int>();
			foreach (int key3 in array3)
			{
				num += dictionary2[key3];
			}
			float num4 = UnityEngine.Random.Range(0f, num);
			int result = -1;
			foreach (KeyValuePair<int, float> keyValuePair2 in dictionary2)
			{
				if (num4 <= keyValuePair2.Value)
				{
					result = keyValuePair2.Key;
					break;
				}
				num4 -= keyValuePair2.Value;
			}
			return result;
		}

		// Token: 0x04006A82 RID: 27266
		[SerializeField]
		private float _hand;

		// Token: 0x04006A83 RID: 27267
		[SerializeField]
		private float _shovel;

		// Token: 0x04006A84 RID: 27268
		[SerializeField]
		private float _pickel;

		// Token: 0x04006A85 RID: 27269
		[SerializeField]
		private float _net;

		// Token: 0x04006A86 RID: 27270
		[SerializeField]
		private float _fishing;

		// Token: 0x04006A87 RID: 27271
		[SerializeField]
		[HideInInspector]
		private int _handNum = 50;

		// Token: 0x04006A88 RID: 27272
		[SerializeField]
		[HideInInspector]
		private int _shovelNum = 50;

		// Token: 0x04006A89 RID: 27273
		[SerializeField]
		[HideInInspector]
		private int _pickelNum = 50;

		// Token: 0x04006A8A RID: 27274
		[SerializeField]
		[HideInInspector]
		private int _netNum = 50;

		// Token: 0x04006A8B RID: 27275
		[SerializeField]
		[HideInInspector]
		private int _fishingNum = 50;

		// Token: 0x02000F7E RID: 3966
		public struct AddProb
		{
			// Token: 0x060083FC RID: 33788 RVA: 0x0037203C File Offset: 0x0037043C
			public AddProb(int id, float add)
			{
				this.id = id;
				this.add = add;
			}

			// Token: 0x04006A8C RID: 27276
			public int id;

			// Token: 0x04006A8D RID: 27277
			public float add;
		}
	}
}
