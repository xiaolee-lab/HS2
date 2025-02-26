using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x0200105F RID: 4191
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public sealed class ButtonAttribute : PropertyAttribute
	{
		// Token: 0x06008CF3 RID: 36083 RVA: 0x003B060B File Offset: 0x003AEA0B
		public ButtonAttribute(string function, string name, params object[] parameters)
		{
			this.Function = function;
			this.Name = name;
			this.Parameters = parameters;
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x06008CF4 RID: 36084 RVA: 0x003B0628 File Offset: 0x003AEA28
		// (set) Token: 0x06008CF5 RID: 36085 RVA: 0x003B0630 File Offset: 0x003AEA30
		public string Function { get; private set; }

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x06008CF6 RID: 36086 RVA: 0x003B0639 File Offset: 0x003AEA39
		// (set) Token: 0x06008CF7 RID: 36087 RVA: 0x003B0641 File Offset: 0x003AEA41
		public string Name { get; private set; }

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x06008CF8 RID: 36088 RVA: 0x003B064A File Offset: 0x003AEA4A
		// (set) Token: 0x06008CF9 RID: 36089 RVA: 0x003B0652 File Offset: 0x003AEA52
		public object[] Parameters { get; private set; }
	}
}
