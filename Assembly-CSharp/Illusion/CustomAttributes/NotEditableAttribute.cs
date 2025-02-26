using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001069 RID: 4201
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public sealed class NotEditableAttribute : PropertyAttribute
	{
	}
}
