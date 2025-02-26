using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ReMotion;

namespace AIProject.Definitions
{
	// Token: 0x02000948 RID: 2376
	public static class Tween
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x001A29AA File Offset: 0x001A0DAA
		public static ReadOnlyDictionary<MotionType, EasingFunction> MotionFunctionTable { get; }

		// Token: 0x0600428A RID: 17034 RVA: 0x001A29B4 File Offset: 0x001A0DB4
		// Note: this type is marked as 'beforefieldinit'.
		static Tween()
		{
			Dictionary<MotionType, EasingFunction> dictionary = new Dictionary<MotionType, EasingFunction>();
			dictionary[MotionType.Linear] = EasingFunctions.Linear;
			dictionary[MotionType.EaseInSine] = EasingFunctions.EaseInSine;
			dictionary[MotionType.EaseOutSine] = EasingFunctions.EaseOutSine;
			dictionary[MotionType.EaseInOutSine] = EasingFunctions.EaseInOutSine;
			dictionary[MotionType.EaseInQuad] = EasingFunctions.EaseInQuad;
			dictionary[MotionType.EaseOutQuad] = EasingFunctions.EaseOutQuad;
			dictionary[MotionType.EaseInOutQuad] = EasingFunctions.EaseInOutQuad;
			dictionary[MotionType.EaseInCubic] = EasingFunctions.EaseInCubic;
			dictionary[MotionType.EaseOutCubic] = EasingFunctions.EaseOutCubic;
			dictionary[MotionType.EaseInOutCubic] = EasingFunctions.EaseInOutCubic;
			dictionary[MotionType.EaseInQuart] = EasingFunctions.EaseInQuart;
			dictionary[MotionType.EaseOutQuart] = EasingFunctions.EaseOutQuart;
			dictionary[MotionType.EaseInOutQuart] = EasingFunctions.EaseInOutQuart;
			dictionary[MotionType.EaseInQuint] = EasingFunctions.EaseInQuint;
			dictionary[MotionType.EaseOutQuint] = EasingFunctions.EaseOutQuint;
			dictionary[MotionType.EaseInOutQuint] = EasingFunctions.EaseInOutQuint;
			dictionary[MotionType.EaseInExpo] = EasingFunctions.EaseInExpo;
			dictionary[MotionType.EaseOutExpo] = EasingFunctions.EaseOutExpo;
			dictionary[MotionType.EaseInOutExpo] = EasingFunctions.EaseInOutExpo;
			dictionary[MotionType.EaseInCirc] = EasingFunctions.EaseInCirc;
			dictionary[MotionType.EaseOutCirc] = EasingFunctions.EaseOutCirc;
			dictionary[MotionType.EaseInOutCirc] = EasingFunctions.EaseInOutCirc;
			dictionary[MotionType.EaseInBack] = EasingFunctions.EaseInBack(1.70158f);
			dictionary[MotionType.EaseOutBack] = EasingFunctions.EaseOutBack(1.70158f);
			dictionary[MotionType.EaseInOutBack] = EasingFunctions.EaseInOutBack(1.70158f);
			dictionary[MotionType.EaseInElastic] = EasingFunctions.EaseInElastic(1.70158f, 0f);
			dictionary[MotionType.EaseOutElastic] = EasingFunctions.EaseOutElastic(1.70158f, 0f);
			dictionary[MotionType.EaseInOutElastic] = EasingFunctions.EaseInOutElastic(1.70158f, 0f);
			dictionary[MotionType.EaseInBounce] = EasingFunctions.EaseInBounce;
			dictionary[MotionType.EaseOutBounce] = EasingFunctions.EaseOutBounce;
			dictionary[MotionType.EaseInOutBounce] = EasingFunctions.EaseInOutBounce;
			Tween.MotionFunctionTable = new ReadOnlyDictionary<MotionType, EasingFunction>(dictionary);
		}
	}
}
