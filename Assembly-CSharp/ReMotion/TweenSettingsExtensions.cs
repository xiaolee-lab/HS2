using System;
using UnityEngine;

namespace ReMotion
{
	// Token: 0x020004ED RID: 1261
	public static class TweenSettingsExtensions
	{
		// Token: 0x060017AA RID: 6058 RVA: 0x00094493 File Offset: 0x00092893
		public static Tween<TObject, float> UseFloatTween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, float> getter, TweenSetter<TObject, float> setter, EasingFunction easingFunction, float duration, float to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.FloatTween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017AB RID: 6059 RVA: 0x000944A6 File Offset: 0x000928A6
		public static Tween<TObject, Vector3> UseVector3Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, Vector3> getter, TweenSetter<TObject, Vector3> setter, EasingFunction easingFunction, float duration, Vector3 to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.Vector3Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017AC RID: 6060 RVA: 0x000944B9 File Offset: 0x000928B9
		public static Tween<TObject, Vector2> UseVector2Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, Vector2> getter, TweenSetter<TObject, Vector2> setter, EasingFunction easingFunction, float duration, Vector2 to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.Vector2Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017AD RID: 6061 RVA: 0x000944CC File Offset: 0x000928CC
		public static Tween<TObject, Vector4> UseVector4Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, Vector4> getter, TweenSetter<TObject, Vector4> setter, EasingFunction easingFunction, float duration, Vector4 to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.Vector4Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x000944DF File Offset: 0x000928DF
		public static Tween<TObject, double> UseDoubleTween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, double> getter, TweenSetter<TObject, double> setter, EasingFunction easingFunction, float duration, double to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.DoubleTween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x000944F2 File Offset: 0x000928F2
		public static Tween<TObject, int> UseInt32Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, int> getter, TweenSetter<TObject, int> setter, EasingFunction easingFunction, float duration, int to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.Int32Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017B0 RID: 6064 RVA: 0x00094505 File Offset: 0x00092905
		public static Tween<TObject, long> UseInt64Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, long> getter, TweenSetter<TObject, long> setter, EasingFunction easingFunction, float duration, long to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.Int64Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017B1 RID: 6065 RVA: 0x00094518 File Offset: 0x00092918
		public static Tween<TObject, uint> UseUInt32Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, uint> getter, TweenSetter<TObject, uint> setter, EasingFunction easingFunction, float duration, uint to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.UInt32Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x0009452B File Offset: 0x0009292B
		public static Tween<TObject, ulong> UseUInt64Tween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, ulong> getter, TweenSetter<TObject, ulong> setter, EasingFunction easingFunction, float duration, ulong to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.UInt64Tween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x0009453E File Offset: 0x0009293E
		public static Tween<TObject, Color> UseColorTween<TObject>(this TweenSettings settings, TObject target, TweenGetter<TObject, Color> getter, TweenSetter<TObject, Color> setter, EasingFunction easingFunction, float duration, Color to, bool isRelative) where TObject : class
		{
			return new TweenSettingsExtensions.ColorTween<TObject>(settings, target, getter, setter, easingFunction, duration, to, isRelative);
		}

		// Token: 0x020004EE RID: 1262
		private class FloatTween<TObject> : Tween<TObject, float> where TObject : class
		{
			// Token: 0x060017B4 RID: 6068 RVA: 0x00094C14 File Offset: 0x00093014
			public FloatTween(TweenSettings settings, TObject target, TweenGetter<TObject, float> getter, TweenSetter<TObject, float> setter, EasingFunction easingFunction, float duration, float to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017B5 RID: 6069 RVA: 0x00094C34 File Offset: 0x00093034
			protected override float AddOperator(float left, float right)
			{
				return left + right;
			}

			// Token: 0x060017B6 RID: 6070 RVA: 0x00094C39 File Offset: 0x00093039
			protected override float GetDifference(float from, float to)
			{
				return to - from;
			}

			// Token: 0x060017B7 RID: 6071 RVA: 0x00094C3E File Offset: 0x0009303E
			protected override void CreateValue(ref float from, ref float difference, ref float ratio, out float value)
			{
				value = from + difference * ratio;
			}
		}

		// Token: 0x020004EF RID: 1263
		private class Vector3Tween<TObject> : Tween<TObject, Vector3> where TObject : class
		{
			// Token: 0x060017B8 RID: 6072 RVA: 0x00094C4C File Offset: 0x0009304C
			public Vector3Tween(TweenSettings settings, TObject target, TweenGetter<TObject, Vector3> getter, TweenSetter<TObject, Vector3> setter, EasingFunction easingFunction, float duration, Vector3 to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017B9 RID: 6073 RVA: 0x00094C6C File Offset: 0x0009306C
			protected override Vector3 AddOperator(Vector3 left, Vector3 right)
			{
				return left + right;
			}

			// Token: 0x060017BA RID: 6074 RVA: 0x00094C75 File Offset: 0x00093075
			protected override Vector3 GetDifference(Vector3 from, Vector3 to)
			{
				return new Vector3(to.x - from.x, to.y - from.y, to.z - from.z);
			}

			// Token: 0x060017BB RID: 6075 RVA: 0x00094CA9 File Offset: 0x000930A9
			protected override void CreateValue(ref Vector3 from, ref Vector3 difference, ref float ratio, out Vector3 value)
			{
				value = new Vector3(from.x + difference.x * ratio, from.y + difference.y * ratio, from.z + difference.z * ratio);
			}
		}

		// Token: 0x020004F0 RID: 1264
		private class Vector2Tween<TObject> : Tween<TObject, Vector2> where TObject : class
		{
			// Token: 0x060017BC RID: 6076 RVA: 0x00094CE4 File Offset: 0x000930E4
			public Vector2Tween(TweenSettings settings, TObject target, TweenGetter<TObject, Vector2> getter, TweenSetter<TObject, Vector2> setter, EasingFunction easingFunction, float duration, Vector2 to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017BD RID: 6077 RVA: 0x00094D04 File Offset: 0x00093104
			protected override Vector2 AddOperator(Vector2 left, Vector2 right)
			{
				return left + right;
			}

			// Token: 0x060017BE RID: 6078 RVA: 0x00094D0D File Offset: 0x0009310D
			protected override Vector2 GetDifference(Vector2 from, Vector2 to)
			{
				return new Vector2(to.x - from.x, to.y - from.y);
			}

			// Token: 0x060017BF RID: 6079 RVA: 0x00094D32 File Offset: 0x00093132
			protected override void CreateValue(ref Vector2 from, ref Vector2 difference, ref float ratio, out Vector2 value)
			{
				value = new Vector2(from.x + difference.x * ratio, from.y + difference.y * ratio);
			}
		}

		// Token: 0x020004F1 RID: 1265
		private class Vector4Tween<TObject> : Tween<TObject, Vector4> where TObject : class
		{
			// Token: 0x060017C0 RID: 6080 RVA: 0x00094D5C File Offset: 0x0009315C
			public Vector4Tween(TweenSettings settings, TObject target, TweenGetter<TObject, Vector4> getter, TweenSetter<TObject, Vector4> setter, EasingFunction easingFunction, float duration, Vector4 to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017C1 RID: 6081 RVA: 0x00094D7C File Offset: 0x0009317C
			protected override Vector4 AddOperator(Vector4 left, Vector4 right)
			{
				return left + right;
			}

			// Token: 0x060017C2 RID: 6082 RVA: 0x00094D88 File Offset: 0x00093188
			protected override Vector4 GetDifference(Vector4 from, Vector4 to)
			{
				return new Vector4(to.x - from.x, to.y - from.y, to.z - from.z, to.w = from.w);
			}

			// Token: 0x060017C3 RID: 6083 RVA: 0x00094DD8 File Offset: 0x000931D8
			protected override void CreateValue(ref Vector4 from, ref Vector4 difference, ref float ratio, out Vector4 value)
			{
				value = new Vector4(from.x + difference.x * ratio, from.y + difference.y * ratio, from.z + difference.z * ratio, from.w + difference.w * ratio);
			}
		}

		// Token: 0x020004F2 RID: 1266
		private class DoubleTween<TObject> : Tween<TObject, double> where TObject : class
		{
			// Token: 0x060017C4 RID: 6084 RVA: 0x00094E2C File Offset: 0x0009322C
			public DoubleTween(TweenSettings settings, TObject target, TweenGetter<TObject, double> getter, TweenSetter<TObject, double> setter, EasingFunction easingFunction, float duration, double to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017C5 RID: 6085 RVA: 0x00094E4C File Offset: 0x0009324C
			protected override double AddOperator(double left, double right)
			{
				return left + right;
			}

			// Token: 0x060017C6 RID: 6086 RVA: 0x00094E51 File Offset: 0x00093251
			protected override double GetDifference(double from, double to)
			{
				return to - from;
			}

			// Token: 0x060017C7 RID: 6087 RVA: 0x00094E56 File Offset: 0x00093256
			protected override void CreateValue(ref double from, ref double difference, ref float ratio, out double value)
			{
				value = from + difference * (double)ratio;
			}
		}

		// Token: 0x020004F3 RID: 1267
		private class Int32Tween<TObject> : Tween<TObject, int> where TObject : class
		{
			// Token: 0x060017C8 RID: 6088 RVA: 0x00094E64 File Offset: 0x00093264
			public Int32Tween(TweenSettings settings, TObject target, TweenGetter<TObject, int> getter, TweenSetter<TObject, int> setter, EasingFunction easingFunction, float duration, int to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017C9 RID: 6089 RVA: 0x00094E84 File Offset: 0x00093284
			protected override int AddOperator(int left, int right)
			{
				return left + right;
			}

			// Token: 0x060017CA RID: 6090 RVA: 0x00094E89 File Offset: 0x00093289
			protected override int GetDifference(int from, int to)
			{
				return to - from;
			}

			// Token: 0x060017CB RID: 6091 RVA: 0x00094E8E File Offset: 0x0009328E
			protected override void CreateValue(ref int from, ref int difference, ref float ratio, out int value)
			{
				value = (int)((float)from + (float)difference * ratio);
			}
		}

		// Token: 0x020004F4 RID: 1268
		private class Int64Tween<TObject> : Tween<TObject, long> where TObject : class
		{
			// Token: 0x060017CC RID: 6092 RVA: 0x00094EA0 File Offset: 0x000932A0
			public Int64Tween(TweenSettings settings, TObject target, TweenGetter<TObject, long> getter, TweenSetter<TObject, long> setter, EasingFunction easingFunction, float duration, long to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017CD RID: 6093 RVA: 0x00094EC0 File Offset: 0x000932C0
			protected override long AddOperator(long left, long right)
			{
				return left + right;
			}

			// Token: 0x060017CE RID: 6094 RVA: 0x00094EC5 File Offset: 0x000932C5
			protected override long GetDifference(long from, long to)
			{
				return to - from;
			}

			// Token: 0x060017CF RID: 6095 RVA: 0x00094ECA File Offset: 0x000932CA
			protected override void CreateValue(ref long from, ref long difference, ref float ratio, out long value)
			{
				value = (long)((float)from + (float)difference * ratio);
			}
		}

		// Token: 0x020004F5 RID: 1269
		private class UInt32Tween<TObject> : Tween<TObject, uint> where TObject : class
		{
			// Token: 0x060017D0 RID: 6096 RVA: 0x00094EDC File Offset: 0x000932DC
			public UInt32Tween(TweenSettings settings, TObject target, TweenGetter<TObject, uint> getter, TweenSetter<TObject, uint> setter, EasingFunction easingFunction, float duration, uint to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017D1 RID: 6097 RVA: 0x00094EFC File Offset: 0x000932FC
			protected override uint AddOperator(uint left, uint right)
			{
				return left + right;
			}

			// Token: 0x060017D2 RID: 6098 RVA: 0x00094F01 File Offset: 0x00093301
			protected override uint GetDifference(uint from, uint to)
			{
				return to - from;
			}

			// Token: 0x060017D3 RID: 6099 RVA: 0x00094F06 File Offset: 0x00093306
			protected override void CreateValue(ref uint from, ref uint difference, ref float ratio, out uint value)
			{
				value = (uint)(from + difference * ratio);
			}
		}

		// Token: 0x020004F6 RID: 1270
		private class UInt64Tween<TObject> : Tween<TObject, ulong> where TObject : class
		{
			// Token: 0x060017D4 RID: 6100 RVA: 0x00094F18 File Offset: 0x00093318
			public UInt64Tween(TweenSettings settings, TObject target, TweenGetter<TObject, ulong> getter, TweenSetter<TObject, ulong> setter, EasingFunction easingFunction, float duration, ulong to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017D5 RID: 6101 RVA: 0x00094F38 File Offset: 0x00093338
			protected override ulong AddOperator(ulong left, ulong right)
			{
				return left + right;
			}

			// Token: 0x060017D6 RID: 6102 RVA: 0x00094F3D File Offset: 0x0009333D
			protected override ulong GetDifference(ulong from, ulong to)
			{
				return to - from;
			}

			// Token: 0x060017D7 RID: 6103 RVA: 0x00094F42 File Offset: 0x00093342
			protected override void CreateValue(ref ulong from, ref ulong difference, ref float ratio, out ulong value)
			{
				value = (ulong)(from + difference * ratio);
			}
		}

		// Token: 0x020004F7 RID: 1271
		private class ColorTween<TObject> : Tween<TObject, Color> where TObject : class
		{
			// Token: 0x060017D8 RID: 6104 RVA: 0x00094F54 File Offset: 0x00093354
			public ColorTween(TweenSettings settings, TObject target, TweenGetter<TObject, Color> getter, TweenSetter<TObject, Color> setter, EasingFunction easingFunction, float duration, Color to, bool isRelative) : base(settings, target, getter, setter, easingFunction, duration, to, isRelative)
			{
			}

			// Token: 0x060017D9 RID: 6105 RVA: 0x00094F74 File Offset: 0x00093374
			protected override Color AddOperator(Color left, Color right)
			{
				return left + right;
			}

			// Token: 0x060017DA RID: 6106 RVA: 0x00094F80 File Offset: 0x00093380
			protected override Color GetDifference(Color from, Color to)
			{
				return new Color(to.a - from.a, to.r - from.r, to.g - from.g, to.b - from.b);
			}

			// Token: 0x060017DB RID: 6107 RVA: 0x00094FD0 File Offset: 0x000933D0
			protected override void CreateValue(ref Color from, ref Color difference, ref float ratio, out Color value)
			{
				value = new Color(from.a + difference.a * ratio, from.r + difference.r * ratio, from.g + difference.g * ratio, from.b + difference.b * ratio);
			}
		}
	}
}
