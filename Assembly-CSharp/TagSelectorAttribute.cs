using System;
using UnityEngine;

// Token: 0x02001199 RID: 4505
[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class TagSelectorAttribute : PropertyAttribute
{
	// Token: 0x04007772 RID: 30578
	public bool AllowUntagged;
}
