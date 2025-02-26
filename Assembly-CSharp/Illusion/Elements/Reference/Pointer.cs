using System;

namespace Illusion.Elements.Reference
{
	// Token: 0x02001070 RID: 4208
	public class Pointer<T>
	{
		// Token: 0x06008D13 RID: 36115 RVA: 0x000F0BD2 File Offset: 0x000EEFD2
		public Pointer(Func<T> get, Action<T> set = null)
		{
			this.get = get;
			this.set = set;
		}

		// Token: 0x17001ED3 RID: 7891
		// (get) Token: 0x06008D14 RID: 36116 RVA: 0x000F0BE8 File Offset: 0x000EEFE8
		// (set) Token: 0x06008D15 RID: 36117 RVA: 0x000F0C09 File Offset: 0x000EF009
		public T value
		{
			get
			{
				return this.get.Call(default(T));
			}
			set
			{
				this.set.Call(value);
			}
		}

		// Token: 0x040072B8 RID: 29368
		private Func<T> get;

		// Token: 0x040072B9 RID: 29369
		private Action<T> set;
	}
}
