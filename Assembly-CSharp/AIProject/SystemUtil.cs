using System;
using System.Threading.Tasks;

namespace AIProject
{
	// Token: 0x02000969 RID: 2409
	public static class SystemUtil
	{
		// Token: 0x060042CE RID: 17102 RVA: 0x001A4F70 File Offset: 0x001A3370
		public static async Task TryProcAsync(Task task)
		{
			try
			{
				await task;
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x001A4FA8 File Offset: 0x001A33A8
		public static async Task<T> TryProcAsync<T>(Task<T> task)
		{
			try
			{
				return await task;
			}
			catch (Exception ex)
			{
			}
			return default(T);
		}

		// Token: 0x060042D0 RID: 17104 RVA: 0x001A4FDD File Offset: 0x001A33DD
		public static bool TryParse(string input, out int result)
		{
			return int.TryParse(input, out result);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x001A4FEE File Offset: 0x001A33EE
		public static bool TryParse(string input, out float result)
		{
			return float.TryParse(input, out result);
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x001A4FFF File Offset: 0x001A33FF
		public static bool TryParse<TEnum>(string input, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(input, out result);
		}

		// Token: 0x060042D3 RID: 17107 RVA: 0x001A5010 File Offset: 0x001A3410
		public static bool SetSafeStruct<T>(ref T destination, T newValue) where T : struct
		{
			if (destination.Equals(newValue))
			{
				return false;
			}
			destination = newValue;
			return true;
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x001A5034 File Offset: 0x001A3434
		public static bool SetSafeClass<T>(ref T destination, T newValue) where T : class
		{
			if ((destination == null && newValue == null) || (destination != null && destination.Equals(newValue)))
			{
				return false;
			}
			destination = newValue;
			return true;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x001A5090 File Offset: 0x001A3490
		public static string Replace(this string source, string newValue, params string[] oldValues)
		{
			if (source.IsNullOrEmpty())
			{
				return source;
			}
			string text = source;
			foreach (string oldValue in oldValues)
			{
				text = text.Replace(oldValue, newValue);
			}
			return text;
		}
	}
}
