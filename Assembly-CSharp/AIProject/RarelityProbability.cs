using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F7C RID: 3964
	public class RarelityProbability : ScriptableObject
	{
		// Token: 0x17001C3A RID: 7226
		// (get) Token: 0x060083E8 RID: 33768 RVA: 0x0037194F File Offset: 0x0036FD4F
		public float None
		{
			[CompilerGenerated]
			get
			{
				return this._none;
			}
		}

		// Token: 0x17001C3B RID: 7227
		// (get) Token: 0x060083E9 RID: 33769 RVA: 0x00371957 File Offset: 0x0036FD57
		public float Normal
		{
			[CompilerGenerated]
			get
			{
				return this._normal;
			}
		}

		// Token: 0x17001C3C RID: 7228
		// (get) Token: 0x060083EA RID: 33770 RVA: 0x0037195F File Offset: 0x0036FD5F
		public float Rare
		{
			[CompilerGenerated]
			get
			{
				return this._rare;
			}
		}

		// Token: 0x17001C3D RID: 7229
		// (get) Token: 0x060083EB RID: 33771 RVA: 0x00371967 File Offset: 0x0036FD67
		public float SuperRare
		{
			[CompilerGenerated]
			get
			{
				return this._superRare;
			}
		}

		// Token: 0x17001C3E RID: 7230
		// (get) Token: 0x060083EC RID: 33772 RVA: 0x0037196F File Offset: 0x0036FD6F
		public float HighRare
		{
			[CompilerGenerated]
			get
			{
				return this._highRare;
			}
		}

		// Token: 0x17001C3F RID: 7231
		// (get) Token: 0x060083ED RID: 33773 RVA: 0x00371977 File Offset: 0x0036FD77
		public float UltraRare
		{
			[CompilerGenerated]
			get
			{
				return this._ultraRare;
			}
		}

		// Token: 0x17001C40 RID: 7232
		// (get) Token: 0x060083EE RID: 33774 RVA: 0x0037197F File Offset: 0x0036FD7F
		public float Failure
		{
			[CompilerGenerated]
			get
			{
				return this._failure;
			}
		}

		// Token: 0x17001C41 RID: 7233
		// (get) Token: 0x060083EF RID: 33775 RVA: 0x00371987 File Offset: 0x0036FD87
		public float Success
		{
			[CompilerGenerated]
			get
			{
				return this._success;
			}
		}

		// Token: 0x17001C42 RID: 7234
		// (get) Token: 0x060083F0 RID: 33776 RVA: 0x0037198F File Offset: 0x0036FD8F
		public float Triumph
		{
			[CompilerGenerated]
			get
			{
				return this._triumph;
			}
		}

		// Token: 0x17001C43 RID: 7235
		// (get) Token: 0x060083F1 RID: 33777 RVA: 0x00371997 File Offset: 0x0036FD97
		public float FailureRate
		{
			[CompilerGenerated]
			get
			{
				return this._failureRate;
			}
		}

		// Token: 0x17001C44 RID: 7236
		// (get) Token: 0x060083F2 RID: 33778 RVA: 0x0037199F File Offset: 0x0036FD9F
		public float SuccessRate
		{
			[CompilerGenerated]
			get
			{
				return this._successRate;
			}
		}

		// Token: 0x17001C45 RID: 7237
		// (get) Token: 0x060083F3 RID: 33779 RVA: 0x003719A7 File Offset: 0x0036FDA7
		public float TriumphRate
		{
			[CompilerGenerated]
			get
			{
				return this._triumphRate;
			}
		}

		// Token: 0x060083F4 RID: 33780 RVA: 0x003719B0 File Offset: 0x0036FDB0
		private UnityEx.ValueTuple<ResultType, float>[] GetResultProbabilities()
		{
			this._resultProbabilities[0] = new UnityEx.ValueTuple<ResultType, float>(ResultType.Failure, this._failure);
			this._resultProbabilities[1] = new UnityEx.ValueTuple<ResultType, float>(ResultType.Success, this._success);
			this._resultProbabilities[2] = new UnityEx.ValueTuple<ResultType, float>(ResultType.Triumph, this._triumph);
			return this._resultProbabilities;
		}

		// Token: 0x060083F5 RID: 33781 RVA: 0x00371A1C File Offset: 0x0036FE1C
		public ResultType LotteryResult()
		{
			float max = this.Failure + this.Success + this.Triumph;
			float num = UnityEngine.Random.Range(0f, max);
			ResultType result = (ResultType)(-1);
			UnityEx.ValueTuple<ResultType, float>[] resultProbabilities = this.GetResultProbabilities();
			foreach (UnityEx.ValueTuple<ResultType, float> valueTuple in resultProbabilities)
			{
				if (num <= valueTuple.Item2)
				{
					result = valueTuple.Item1;
					break;
				}
				num -= valueTuple.Item2;
			}
			return result;
		}

		// Token: 0x060083F6 RID: 33782 RVA: 0x00371AA4 File Offset: 0x0036FEA4
		private UnityEx.ValueTuple<Rarelity, float>[] GetProbabilities()
		{
			this._probabilities[0] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.None, this._none);
			this._probabilities[1] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.N, this._normal);
			this._probabilities[2] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.R, this._rare);
			this._probabilities[3] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.SR, this._superRare);
			this._probabilities[4] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.SSR, this._highRare);
			this._probabilities[5] = new UnityEx.ValueTuple<Rarelity, float>(Rarelity.UR, this._ultraRare);
			return this._probabilities;
		}

		// Token: 0x060083F7 RID: 33783 RVA: 0x00371B68 File Offset: 0x0036FF68
		public Rarelity Lottery(bool containsNone)
		{
			float num = this.Normal + this.Rare + this.SuperRare + this.HighRare + this.UltraRare;
			if (containsNone)
			{
				num += this._none;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			Rarelity result = (Rarelity)(-1);
			UnityEx.ValueTuple<Rarelity, float>[] probabilities = this.GetProbabilities();
			foreach (UnityEx.ValueTuple<Rarelity, float> valueTuple in probabilities)
			{
				if (num2 <= valueTuple.Item2)
				{
					result = valueTuple.Item1;
					break;
				}
				num2 -= valueTuple.Item2;
			}
			return result;
		}

		// Token: 0x060083F8 RID: 33784 RVA: 0x00371C10 File Offset: 0x00370010
		public Rarelity Lottery(bool containsNone, Rarelity[] table)
		{
			UnityEx.ValueTuple<Rarelity, float>[] probabilities = this.GetProbabilities();
			List<UnityEx.ValueTuple<Rarelity, float>> list = ListPool<UnityEx.ValueTuple<Rarelity, float>>.Get();
			foreach (UnityEx.ValueTuple<Rarelity, float> item in probabilities)
			{
				foreach (Rarelity rarelity in table)
				{
					if (containsNone || rarelity != Rarelity.None)
					{
						if (item.Item1 == rarelity)
						{
							list.Add(item);
						}
					}
				}
			}
			float num = 0f;
			foreach (UnityEx.ValueTuple<Rarelity, float> valueTuple in list)
			{
				num += valueTuple.Item2;
			}
			float num2 = UnityEngine.Random.Range(0f, num);
			Rarelity result = (Rarelity)(-1);
			foreach (UnityEx.ValueTuple<Rarelity, float> valueTuple2 in list)
			{
				if (num2 <= valueTuple2.Item2)
				{
					result = valueTuple2.Item1;
					break;
				}
				num2 -= valueTuple2.Item2;
			}
			ListPool<UnityEx.ValueTuple<Rarelity, float>>.Release(list);
			return result;
		}

		// Token: 0x04006A6B RID: 27243
		[SerializeField]
		private float _none;

		// Token: 0x04006A6C RID: 27244
		[SerializeField]
		private float _normal;

		// Token: 0x04006A6D RID: 27245
		[SerializeField]
		private float _rare;

		// Token: 0x04006A6E RID: 27246
		[SerializeField]
		private float _superRare;

		// Token: 0x04006A6F RID: 27247
		[SerializeField]
		private float _highRare;

		// Token: 0x04006A70 RID: 27248
		[SerializeField]
		private float _ultraRare;

		// Token: 0x04006A71 RID: 27249
		[SerializeField]
		[HideInInspector]
		private int _noneNum = 50;

		// Token: 0x04006A72 RID: 27250
		[SerializeField]
		[HideInInspector]
		private int _normalNum = 50;

		// Token: 0x04006A73 RID: 27251
		[SerializeField]
		[HideInInspector]
		private int _rareNum = 50;

		// Token: 0x04006A74 RID: 27252
		[SerializeField]
		[HideInInspector]
		private int _superRareNum = 50;

		// Token: 0x04006A75 RID: 27253
		[SerializeField]
		[HideInInspector]
		private int _highRareNum = 50;

		// Token: 0x04006A76 RID: 27254
		[SerializeField]
		[HideInInspector]
		private int _ultraRareNum = 50;

		// Token: 0x04006A77 RID: 27255
		[SerializeField]
		private float _failure;

		// Token: 0x04006A78 RID: 27256
		[SerializeField]
		private float _success;

		// Token: 0x04006A79 RID: 27257
		[SerializeField]
		private float _triumph;

		// Token: 0x04006A7A RID: 27258
		[SerializeField]
		private float _failureRate = 0.1f;

		// Token: 0x04006A7B RID: 27259
		[SerializeField]
		private float _successRate = 1f;

		// Token: 0x04006A7C RID: 27260
		[SerializeField]
		private float _triumphRate = 1.5f;

		// Token: 0x04006A7D RID: 27261
		[SerializeField]
		[HideInInspector]
		private int _failureNum = 5;

		// Token: 0x04006A7E RID: 27262
		[SerializeField]
		[HideInInspector]
		private int _successNum = 85;

		// Token: 0x04006A7F RID: 27263
		[SerializeField]
		[HideInInspector]
		private int _triumphNum = 10;

		// Token: 0x04006A80 RID: 27264
		private UnityEx.ValueTuple<ResultType, float>[] _resultProbabilities = new UnityEx.ValueTuple<ResultType, float>[3];

		// Token: 0x04006A81 RID: 27265
		private UnityEx.ValueTuple<Rarelity, float>[] _probabilities = new UnityEx.ValueTuple<Rarelity, float>[6];
	}
}
