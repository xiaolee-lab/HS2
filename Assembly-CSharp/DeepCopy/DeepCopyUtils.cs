using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCopy
{
	// Token: 0x0200114E RID: 4430
	internal static class DeepCopyUtils
	{
		// Token: 0x060092A9 RID: 37545 RVA: 0x003CD1C0 File Offset: 0x003CB5C0
		public static object DeepCopy(this object target)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			object result;
			try
			{
				binaryFormatter.Serialize(memoryStream, target);
				memoryStream.Position = 0L;
				result = binaryFormatter.Deserialize(memoryStream);
			}
			finally
			{
				memoryStream.Close();
			}
			return result;
		}
	}
}
