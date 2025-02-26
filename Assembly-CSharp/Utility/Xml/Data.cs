using System;
using System.Xml;

namespace Utility.Xml
{
	// Token: 0x020011AF RID: 4527
	public class Data
	{
		// Token: 0x060094B4 RID: 38068 RVA: 0x003D5238 File Offset: 0x003D3638
		public Data(string elementName)
		{
			this.elementName = elementName;
		}

		// Token: 0x060094B5 RID: 38069 RVA: 0x003D5247 File Offset: 0x003D3647
		public virtual void Init()
		{
		}

		// Token: 0x060094B6 RID: 38070 RVA: 0x003D5249 File Offset: 0x003D3649
		public virtual void Read(string rootName, XmlDocument xml)
		{
		}

		// Token: 0x060094B7 RID: 38071 RVA: 0x003D524B File Offset: 0x003D364B
		public virtual void Write(XmlWriter writer)
		{
		}

		// Token: 0x04007798 RID: 30616
		protected readonly string elementName;
	}
}
