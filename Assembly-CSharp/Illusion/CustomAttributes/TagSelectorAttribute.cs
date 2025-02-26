using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200106F RID: 4207
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public class TagSelectorAttribute : PropertyAttribute
	{
		// Token: 0x040072B7 RID: 29367
		public bool AllowUntagged;
	}
}
