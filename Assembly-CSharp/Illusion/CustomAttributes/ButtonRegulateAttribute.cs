using System;
using UnityEngine;

namespace Illusion.CustomAttributes
{
	// Token: 0x02001060 RID: 4192
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public sealed class ButtonRegulateAttribute : PropertyAttribute
	{
		// Token: 0x06008CFA RID: 36090 RVA: 0x003B065B File Offset: 0x003AEA5B
		public ButtonRegulateAttribute(string function, string name, bool playingRegulate, params object[] parameters)
		{
			this.Function = function;
			this.Name = name;
			this.PlayingRegulate = playingRegulate;
			this.Parameters = parameters;
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x06008CFB RID: 36091 RVA: 0x003B0680 File Offset: 0x003AEA80
		// (set) Token: 0x06008CFC RID: 36092 RVA: 0x003B0688 File Offset: 0x003AEA88
		public string Function { get; private set; }

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x06008CFD RID: 36093 RVA: 0x003B0691 File Offset: 0x003AEA91
		// (set) Token: 0x06008CFE RID: 36094 RVA: 0x003B0699 File Offset: 0x003AEA99
		public string Name { get; private set; }

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x06008CFF RID: 36095 RVA: 0x003B06A2 File Offset: 0x003AEAA2
		// (set) Token: 0x06008D00 RID: 36096 RVA: 0x003B06AA File Offset: 0x003AEAAA
		public bool PlayingRegulate { get; private set; }

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x06008D01 RID: 36097 RVA: 0x003B06B3 File Offset: 0x003AEAB3
		// (set) Token: 0x06008D02 RID: 36098 RVA: 0x003B06BB File Offset: 0x003AEABB
		public object[] Parameters { get; private set; }
	}
}
