using System;
using System.IO;
using System.Xml.Serialization;

namespace Utility.Xml
{
	// Token: 0x020011AD RID: 4525
	public class Seializer
	{
		// Token: 0x060094AC RID: 38060 RVA: 0x003D4F48 File Offset: 0x003D3348
		public static T Seialize<T>(string filename, T data)
		{
			using (FileStream fileStream = new FileStream(filename, FileMode.Create))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				xmlSerializer.Serialize(fileStream, data);
			}
			return data;
		}

		// Token: 0x060094AD RID: 38061 RVA: 0x003D4FA0 File Offset: 0x003D33A0
		public static T Deserialize<T>(string filename)
		{
			T result;
			using (FileStream fileStream = new FileStream(filename, FileMode.Open))
			{
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
				result = (T)((object)xmlSerializer.Deserialize(fileStream));
			}
			return result;
		}
	}
}
