using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200106B RID: 4203
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public sealed class NotEditableInStopingAttribute : PropertyAttribute
	{
	}
}
