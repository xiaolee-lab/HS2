using System;
using UnityEngine;

namespace GUITree
{
	// Token: 0x02001242 RID: 4674
	internal static class SetPropertyUtility
	{
		// Token: 0x060099A6 RID: 39334 RVA: 0x003F3FB0 File Offset: 0x003F23B0
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060099A7 RID: 39335 RVA: 0x003F400F File Offset: 0x003F240F
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x060099A8 RID: 39336 RVA: 0x003F4034 File Offset: 0x003F2434
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
