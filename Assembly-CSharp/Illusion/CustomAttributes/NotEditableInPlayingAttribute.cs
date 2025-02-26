using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200106A RID: 4202
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public sealed class NotEditableInPlayingAttribute : PropertyAttribute
	{
	}
}
