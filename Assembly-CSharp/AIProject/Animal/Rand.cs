using System;
using System.Collections.Generic;

namespace AIProject.Animal
{
	// Token: 0x02000B7A RID: 2938
	public static class Rand
	{
		// Token: 0x06005723 RID: 22307 RVA: 0x0025AFA6 File Offset: 0x002593A6
		public static T Get<T>(List<T> source)
		{
			return source.Rand<T>();
		}

		// Token: 0x06005724 RID: 22308 RVA: 0x0025AFAE File Offset: 0x002593AE
		public static KeyValuePair<T1, T2> Get<T1, T2>(Dictionary<T1, T2> source)
		{
			return source.Rand<T1, T2>();
		}

		// Token: 0x06005725 RID: 22309 RVA: 0x0025AFB6 File Offset: 0x002593B6
		public static T1 GetKey<T1, T2>(Dictionary<T1, T2> source)
		{
			return source.RandKey<T1, T2>();
		}

		// Token: 0x06005726 RID: 22310 RVA: 0x0025AFBE File Offset: 0x002593BE
		public static T2 GetValue<T1, T2>(Dictionary<T1, T2> source)
		{
			return source.RandValue<T1, T2>();
		}
	}
}
