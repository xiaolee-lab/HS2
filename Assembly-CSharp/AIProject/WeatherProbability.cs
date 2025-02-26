using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000F91 RID: 3985
	public class WeatherProbability : ScriptableObject
	{
		// Token: 0x17001D1A RID: 7450
		// (get) Token: 0x060084FF RID: 34047 RVA: 0x00373F98 File Offset: 0x00372398
		private Dictionary<Weather, float> Table
		{
			[CompilerGenerated]
			get
			{
				Dictionary<Weather, float> dictionary = new Dictionary<Weather, float>();
				dictionary[Weather.Clear] = this._clear;
				dictionary[Weather.Cloud1] = this._cloud1;
				dictionary[Weather.Cloud2] = this._cloud2;
				dictionary[Weather.Cloud3] = this._cloud3;
				dictionary[Weather.Cloud4] = this._cloud4;
				dictionary[Weather.Rain] = this._rain;
				dictionary[Weather.Storm] = this._storm;
				dictionary[Weather.Fog] = this._fog;
				return dictionary;
			}
		}

		// Token: 0x06008500 RID: 34048 RVA: 0x00374014 File Offset: 0x00372414
		public Weather Lottery()
		{
			float max = this._clear + this._rain + this._storm + this._fog + this._cloud1 + this._cloud2 + this._cloud3 + this._cloud4;
			float num = UnityEngine.Random.Range(0f, max);
			Weather result = (Weather)(-1);
			Dictionary<Weather, float> table = this.Table;
			foreach (KeyValuePair<Weather, float> keyValuePair in table)
			{
				if (num <= keyValuePair.Value)
				{
					result = keyValuePair.Key;
					break;
				}
				num -= keyValuePair.Value;
			}
			return result;
		}

		// Token: 0x06008501 RID: 34049 RVA: 0x003740D8 File Offset: 0x003724D8
		public Weather Lottery(List<Weather> list)
		{
			Dictionary<Weather, float> table = this.Table;
			List<KeyValuePair<Weather, float>> list2 = ListPool<KeyValuePair<Weather, float>>.Get();
			foreach (KeyValuePair<Weather, float> item in table)
			{
				foreach (Weather weather in list)
				{
					if (item.Key == weather)
					{
						list2.Add(item);
					}
				}
			}
			float num = 0f;
			foreach (KeyValuePair<Weather, float> keyValuePair in list2)
			{
				num += keyValuePair.Value;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			Weather result = (Weather)(-1);
			foreach (KeyValuePair<Weather, float> keyValuePair2 in list2)
			{
				if (num2 <= keyValuePair2.Value)
				{
					result = keyValuePair2.Key;
					break;
				}
				num2 -= keyValuePair2.Value;
			}
			ListPool<KeyValuePair<Weather, float>>.Release(list2);
			return result;
		}

		// Token: 0x04006B97 RID: 27543
		[SerializeField]
		private float _clear;

		// Token: 0x04006B98 RID: 27544
		[SerializeField]
		private float _rain;

		// Token: 0x04006B99 RID: 27545
		[SerializeField]
		private float _storm;

		// Token: 0x04006B9A RID: 27546
		[SerializeField]
		private float _fog;

		// Token: 0x04006B9B RID: 27547
		[SerializeField]
		private float _cloud1;

		// Token: 0x04006B9C RID: 27548
		[SerializeField]
		private float _cloud2;

		// Token: 0x04006B9D RID: 27549
		[SerializeField]
		private float _cloud3;

		// Token: 0x04006B9E RID: 27550
		[SerializeField]
		private float _cloud4;

		// Token: 0x04006B9F RID: 27551
		[SerializeField]
		[HideInInspector]
		private int _clearNum = 50;

		// Token: 0x04006BA0 RID: 27552
		[SerializeField]
		[HideInInspector]
		private int _rainNum = 50;

		// Token: 0x04006BA1 RID: 27553
		[SerializeField]
		[HideInInspector]
		private int _stormNum = 50;

		// Token: 0x04006BA2 RID: 27554
		[SerializeField]
		[HideInInspector]
		private int _fogNum = 50;

		// Token: 0x04006BA3 RID: 27555
		[SerializeField]
		[HideInInspector]
		private int _cloud1Num = 50;

		// Token: 0x04006BA4 RID: 27556
		[SerializeField]
		[HideInInspector]
		private int _cloud2Num = 50;

		// Token: 0x04006BA5 RID: 27557
		[SerializeField]
		[HideInInspector]
		private int _cloud3Num = 50;

		// Token: 0x04006BA6 RID: 27558
		[SerializeField]
		[HideInInspector]
		private int _cloud4Num = 50;
	}
}
