using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Illusion.Elements.Reference
{
	// Token: 0x02001071 RID: 4209
	public class AutoIndexer<T>
	{
		// Token: 0x06008D16 RID: 36118 RVA: 0x000F3520 File Offset: 0x000F1920
		public AutoIndexer()
		{
			this.initializeValue = default(T);
		}

		// Token: 0x06008D17 RID: 36119 RVA: 0x000F354D File Offset: 0x000F194D
		public AutoIndexer(T initializeValue)
		{
			this.initializeValue = initializeValue;
		}

		// Token: 0x06008D18 RID: 36120 RVA: 0x000F3567 File Offset: 0x000F1967
		public AutoIndexer(Func<T> initializeValueFunc)
		{
			this.initializeValueFunc = initializeValueFunc;
		}

		// Token: 0x17001ED4 RID: 7892
		public T this[int index]
		{
			get
			{
				return this[index.ToString()];
			}
			set
			{
				this[index.ToString()] = value;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		public virtual T this[string key]
		{
			get
			{
				T result;
				if (!this.dic.TryGetValue(key, out result))
				{
					if (this.initializeValueFunc == null)
					{
						T t = this.initializeValue;
						this.dic[key] = t;
						result = t;
					}
					else
					{
						T t = this.initializeValueFunc();
						this.dic[key] = t;
						result = t;
					}
				}
				return result;
			}
			set
			{
				this.dic[key] = value;
			}
		}

		// Token: 0x06008D1D RID: 36125 RVA: 0x000F361D File Offset: 0x000F1A1D
		public void Clear()
		{
			this.dic.Clear();
		}

		// Token: 0x17001ED6 RID: 7894
		// (get) Token: 0x06008D1E RID: 36126 RVA: 0x000F362A File Offset: 0x000F1A2A
		public Dictionary<string, T> Source
		{
			[CompilerGenerated]
			get
			{
				return this.dic;
			}
		}

		// Token: 0x06008D1F RID: 36127 RVA: 0x000F3634 File Offset: 0x000F1A34
		public Dictionary<string, T> ToStringDictionary()
		{
			return this.dic.ToDictionary((KeyValuePair<string, T> v) => v.Key, (KeyValuePair<string, T> v) => v.Value);
		}

		// Token: 0x06008D20 RID: 36128 RVA: 0x000F3688 File Offset: 0x000F1A88
		public Dictionary<int, T> ToIntDictionary()
		{
			return this.dic.ToDictionary((KeyValuePair<string, T> v) => int.Parse(v.Key), (KeyValuePair<string, T> v) => v.Value);
		}

		// Token: 0x040072BA RID: 29370
		protected T initializeValue;

		// Token: 0x040072BB RID: 29371
		protected Func<T> initializeValueFunc;

		// Token: 0x040072BC RID: 29372
		protected Dictionary<string, T> dic = new Dictionary<string, T>();
	}
}
