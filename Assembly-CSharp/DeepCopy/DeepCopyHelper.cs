using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DeepCopy
{
	// Token: 0x0200114F RID: 4431
	public static class DeepCopyHelper
	{
		// Token: 0x060092AA RID: 37546 RVA: 0x003CD210 File Offset: 0x003CB610
		public static T DeepCopy<T>(T target)
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			MemoryStream memoryStream = new MemoryStream();
			T result;
			try
			{
				binaryFormatter.Serialize(memoryStream, target);
				memoryStream.Position = 0L;
				result = (T)((object)binaryFormatter.Deserialize(memoryStream));
			}
			finally
			{
				memoryStream.Close();
			}
			return result;
		}
	}
}
