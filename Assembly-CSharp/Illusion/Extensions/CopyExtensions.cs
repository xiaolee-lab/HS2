using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Illusion.Extensions
{
	// Token: 0x02001078 RID: 4216
	public static class CopyExtensions
	{
		// Token: 0x06008D4B RID: 36171 RVA: 0x003B0EC8 File Offset: 0x003AF2C8
		public static T DeepCopy<T>(this T self)
		{
			if (self == null)
			{
				return default(T);
			}
			MemoryStream memoryStream = new MemoryStream();
			T result;
			try
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, self);
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
