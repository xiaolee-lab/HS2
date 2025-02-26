using System;
using System.Xml;

namespace Illusion.Elements.Xml
{
	// Token: 0x02001073 RID: 4211
	public abstract class Data
	{
		// Token: 0x06008D32 RID: 36146 RVA: 0x00140784 File Offset: 0x0013EB84
		public Data(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x06008D33 RID: 36147 RVA: 0x00140793 File Offset: 0x0013EB93
		public virtual void Init()
		{
		}

		// Token: 0x06008D34 RID: 36148 RVA: 0x00140795 File Offset: 0x0013EB95
		public virtual void Read(string rootName, XmlDocument xml)
		{
		}

		// Token: 0x06008D35 RID: 36149 RVA: 0x00140797 File Offset: 0x0013EB97
		public virtual void Write(XmlWriter writer)
		{
		}

		// Token: 0x040072C5 RID: 29381
		protected readonly string elementName;
	}
}
