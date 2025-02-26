using System;

// Token: 0x02000A85 RID: 2693
public static class EnumExtensions
{
	// Token: 0x06004F88 RID: 20360 RVA: 0x001E98D0 File Offset: 0x001E7CD0
	public static bool HasFlag(this Enum self, Enum flag)
	{
		if (self.GetType() != flag.GetType())
		{
			throw new ArgumentException("flag の型が、現在のインスタンスの型と異なっています。");
		}
		ulong num = Convert.ToUInt64(self);
		ulong num2 = Convert.ToUInt64(flag);
		return (num & num2) == num2;
	}
}
