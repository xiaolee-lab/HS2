using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ReMotion
{
	// Token: 0x020004EA RID: 1258
	public static class EasingFunctions
	{
		// Token: 0x06001758 RID: 5976 RVA: 0x00093260 File Offset: 0x00091660
		public static EasingFunction EaseInBack(float overshoot = 1.70158f)
		{
			return (overshoot != 1.70158f) ? ((float time, float duration) => EasingFunctions.EaseInBack_(time, duration, overshoot)) : EasingFunctions.defaultEaseInBack;
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x000932A0 File Offset: 0x000916A0
		public static EasingFunction EaseOutBack(float overshoot = 1.70158f)
		{
			return (overshoot != 1.70158f) ? ((float time, float duration) => EasingFunctions.EaseOutBack_(time, duration, overshoot)) : EasingFunctions.defaultEaseOutBack;
		}

		// Token: 0x0600175A RID: 5978 RVA: 0x000932E0 File Offset: 0x000916E0
		public static EasingFunction EaseInOutBack(float overshoot = 1.70158f)
		{
			return (overshoot != 1.70158f) ? ((float time, float duration) => EasingFunctions.EaseInOutBack_(time, duration, overshoot)) : EasingFunctions.defaultEaseInOutBack;
		}

		// Token: 0x0600175B RID: 5979 RVA: 0x00093320 File Offset: 0x00091720
		public static EasingFunction EaseInElastic(float amplitude = 1.70158f, float period = 0f)
		{
			return (amplitude != 1.70158f || period != 0f) ? ((float time, float duration) => EasingFunctions.EaseInElastic_(time, duration, amplitude, period)) : EasingFunctions.defaultEaseInElastic;
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00093378 File Offset: 0x00091778
		public static EasingFunction EaseOutElastic(float amplitude = 1.70158f, float period = 0f)
		{
			return (amplitude != 1.70158f || period != 0f) ? ((float time, float duration) => EasingFunctions.EaseOutElastic_(time, duration, amplitude, period)) : EasingFunctions.defaultEaseOutElastic;
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x000933D0 File Offset: 0x000917D0
		public static EasingFunction EaseInOutElastic(float amplitude = 1.70158f, float period = 0f)
		{
			return (amplitude != 1.70158f || period != 0f) ? ((float time, float duration) => EasingFunctions.EaseInOutElastic_(time, duration, amplitude, period)) : EasingFunctions.defaultEaseInOutElastic;
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00093428 File Offset: 0x00091828
		public static EasingFunction Shake(float amplitude = 1f)
		{
			return (float time, float duration) => UnityEngine.Random.Range(0f, amplitude);
		}

		// Token: 0x0600175F RID: 5983 RVA: 0x00093450 File Offset: 0x00091850
		public static EasingFunction AnimationCurve(AnimationCurve animationCurve)
		{
			if (animationCurve.keys.Length == 0)
			{
				return EasingFunctions.Linear;
			}
			float curveDuration = animationCurve.keys[animationCurve.keys.Length - 1].time;
			return delegate(float time, float duration)
			{
				float time2 = time * curveDuration / duration;
				return animationCurve.Evaluate(time2);
			};
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x000934B8 File Offset: 0x000918B8
		private static float Linear_(float time, float duration)
		{
			return time / duration;
		}

		// Token: 0x06001761 RID: 5985 RVA: 0x000934BD File Offset: 0x000918BD
		private static float EaseInSine_(float time, float duration)
		{
			return -1f * (float)Math.Cos((double)(time / duration * 1.5707964f)) + 1f;
		}

		// Token: 0x06001762 RID: 5986 RVA: 0x000934DB File Offset: 0x000918DB
		private static float EaseOutSine_(float time, float duration)
		{
			return (float)Math.Sin((double)(time / duration * 1.5707964f));
		}

		// Token: 0x06001763 RID: 5987 RVA: 0x000934ED File Offset: 0x000918ED
		private static float EaseInOutSine_(float time, float duration)
		{
			return -0.5f * ((float)Math.Cos((double)(3.1415927f * time / duration)) - 1f);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0009350B File Offset: 0x0009190B
		private static float EaseInQuad_(float time, float duration)
		{
			time /= duration;
			return time * time;
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x00093515 File Offset: 0x00091915
		private static float EaseOutQuad_(float time, float duration)
		{
			time /= duration;
			return -time * (time - 2f);
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x00093526 File Offset: 0x00091926
		private static float EaseInOutQuad_(float time, float duration)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * time * time;
			}
			time -= 1f;
			return -0.5f * (time * (time - 2f) - 1f);
		}

		// Token: 0x06001767 RID: 5991 RVA: 0x00093566 File Offset: 0x00091966
		private static float EaseInCubic_(float time, float duration)
		{
			time /= duration;
			return time * time * time;
		}

		// Token: 0x06001768 RID: 5992 RVA: 0x00093572 File Offset: 0x00091972
		private static float EaseOutCubic_(float time, float duration)
		{
			time = time / duration - 1f;
			return time * time * time + 1f;
		}

		// Token: 0x06001769 RID: 5993 RVA: 0x0009358A File Offset: 0x0009198A
		private static float EaseInOutCubic_(float time, float duration)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * time * time * time;
			}
			time -= 2f;
			return 0.5f * (time * time * time + 2f);
		}

		// Token: 0x0600176A RID: 5994 RVA: 0x000935C8 File Offset: 0x000919C8
		private static float EaseInQuart_(float time, float duration)
		{
			time /= duration;
			return time * time * time * time;
		}

		// Token: 0x0600176B RID: 5995 RVA: 0x000935D6 File Offset: 0x000919D6
		private static float EaseOutQuart_(float time, float duration)
		{
			time = time / duration - 1f;
			return -(time * time * time * time - 1f);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x000935F4 File Offset: 0x000919F4
		private static float EaseInOutQuart_(float time, float duration)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * time * time * time * time;
			}
			time -= 2f;
			return -0.5f * (time * time * time * time - 2f);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00093641 File Offset: 0x00091A41
		private static float EaseInQuint_(float time, float duration)
		{
			time /= duration;
			return time * time * time * time * time;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x00093651 File Offset: 0x00091A51
		private static float EaseOutQuint_(float time, float duration)
		{
			time = time / duration - 1f;
			return time * time * time * time * time + 1f;
		}

		// Token: 0x0600176F RID: 5999 RVA: 0x00093670 File Offset: 0x00091A70
		private static float EaseInOutQuint_(float time, float duration)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * time * time * time * time * time;
			}
			time -= 2f;
			return 0.5f * (time * time * time * time * time + 2f);
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x000936C1 File Offset: 0x00091AC1
		private static float EaseInExpo_(float time, float duration)
		{
			return (time != 0f) ? ((float)Math.Pow(2.0, (double)(10f * (time / duration - 1f)))) : 0f;
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x000936F7 File Offset: 0x00091AF7
		private static float EaseOutExpo_(float time, float duration)
		{
			if (time == duration)
			{
				return 1f;
			}
			return -(float)Math.Pow(2.0, (double)(-10f * time / duration)) + 1f;
		}

		// Token: 0x06001772 RID: 6002 RVA: 0x00093728 File Offset: 0x00091B28
		private static float EaseInOutExpo_(float time, float duration)
		{
			if (time == 0f)
			{
				return 0f;
			}
			if (time == duration)
			{
				return 1f;
			}
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * (float)Math.Pow(2.0, (double)(10f * (time - 1f)));
			}
			return 0.5f * (-(float)Math.Pow(2.0, (double)(-10f * (time -= 1f))) + 2f);
		}

		// Token: 0x06001773 RID: 6003 RVA: 0x000937BA File Offset: 0x00091BBA
		private static float EaseInCirc_(float time, float duration)
		{
			time /= duration;
			return -((float)Math.Sqrt((double)(1f - time * time)) - 1f);
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x000937D8 File Offset: 0x00091BD8
		private static float EaseOutCirc_(float time, float duration)
		{
			time = time / duration - 1f;
			return (float)Math.Sqrt((double)(1f - time * time));
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x000937F8 File Offset: 0x00091BF8
		private static float EaseInOutCirc_(float time, float duration)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return -0.5f * ((float)Math.Sqrt((double)(1f - time * time)) - 1f);
			}
			time -= 2f;
			return 0.5f * ((float)Math.Sqrt((double)(1f - time * time)) + 1f);
		}

		// Token: 0x06001776 RID: 6006 RVA: 0x0009385D File Offset: 0x00091C5D
		private static float EaseInBack_(float time, float duration, float overshoot)
		{
			time /= duration;
			return time * time * ((overshoot + 1f) * time - overshoot);
		}

		// Token: 0x06001777 RID: 6007 RVA: 0x00093873 File Offset: 0x00091C73
		private static float EaseOutBack_(float time, float duration, float overshoot)
		{
			time = time / duration - 1f;
			return time * time * ((overshoot + 1f) * time + overshoot) + 1f;
		}

		// Token: 0x06001778 RID: 6008 RVA: 0x00093898 File Offset: 0x00091C98
		private static float EaseInOutBack_(float time, float duration, float overshoot)
		{
			time /= duration * 0.5f;
			if (time < 1f)
			{
				return 0.5f * (time * time * (((overshoot *= 1.525f) + 1f) * time - overshoot));
			}
			time -= 2f;
			return 0.5f * (time * time * (((overshoot *= 1.525f) + 1f) * time + overshoot) + 2f);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x00093908 File Offset: 0x00091D08
		private static float EaseInElastic_(float time, float duration, float amplitude, float period)
		{
			if (time == 0f)
			{
				return 0f;
			}
			time /= duration;
			if (time == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			float num;
			if (amplitude < 1f)
			{
				amplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = period / 6.2831855f * (float)Math.Asin((double)(1f / amplitude));
			}
			time -= 1f;
			return -(amplitude * (float)Math.Pow(2.0, (double)(10f * time)) * (float)Math.Sin((double)((time * duration - num) * 6.2831855f / period)));
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x000939C0 File Offset: 0x00091DC0
		private static float EaseOutElastic_(float time, float duration, float amplitude, float period)
		{
			if (time == 0f)
			{
				return 0f;
			}
			time /= duration;
			if (time == 1f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.3f;
			}
			float num;
			if (amplitude < 1f)
			{
				amplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = period / 6.2831855f * (float)Math.Asin((double)(1f / amplitude));
			}
			return amplitude * (float)Math.Pow(2.0, (double)(-10f * time)) * (float)Math.Sin((double)((time * duration - num) * 6.2831855f / period)) + 1f;
		}

		// Token: 0x0600177B RID: 6011 RVA: 0x00093A74 File Offset: 0x00091E74
		private static float EaseInOutElastic_(float time, float duration, float amplitude, float period)
		{
			if (time == 0f)
			{
				return 0f;
			}
			time /= duration * 0.5f;
			if (time == 2f)
			{
				return 1f;
			}
			if (period == 0f)
			{
				period = duration * 0.45000002f;
			}
			float num;
			if (amplitude < 1f)
			{
				amplitude = 1f;
				num = period / 4f;
			}
			else
			{
				num = period / 6.2831855f * (float)Math.Asin((double)(1f / amplitude));
			}
			if (time < 1f)
			{
				time -= 1f;
				return -0.5f * (amplitude * (float)Math.Pow(2.0, (double)(10f * time)) * (float)Math.Sin((double)((time * duration - num) * 6.2831855f / period)));
			}
			time -= 1f;
			return amplitude * (float)Math.Pow(2.0, (double)(-10f * time)) * (float)Math.Sin((double)((time * duration - num) * 6.2831855f / period)) * 0.5f + 1f;
		}

		// Token: 0x0600177C RID: 6012 RVA: 0x00093B84 File Offset: 0x00091F84
		private static float EaseInBounce_(float time, float duration)
		{
			return 1f - EasingFunctions.EaseOutBounce_(duration - time, duration);
		}

		// Token: 0x0600177D RID: 6013 RVA: 0x00093B98 File Offset: 0x00091F98
		private static float EaseOutBounce_(float time, float duration)
		{
			time /= duration;
			if (time < 0.36363637f)
			{
				return 7.5625f * time * time;
			}
			if (time < 0.72727275f)
			{
				time -= 0.54545456f;
				return 7.5625f * time * time + 0.75f;
			}
			if (time < 0.90909094f)
			{
				time -= 0.8181818f;
				return 7.5625f * time * time + 0.9375f;
			}
			time -= 0.95454544f;
			return 7.5625f * time * time + 0.984375f;
		}

		// Token: 0x0600177E RID: 6014 RVA: 0x00093C1F File Offset: 0x0009201F
		private static float EaseInOutBounce_(float time, float duration)
		{
			if (time < duration * 0.5f)
			{
				return EasingFunctions.EaseInBounce_(time * 2f, duration) * 0.5f;
			}
			return EasingFunctions.EaseOutBounce_(time * 2f - duration, duration) * 0.5f + 0.5f;
		}

		// Token: 0x0600177F RID: 6015 RVA: 0x00093C60 File Offset: 0x00092060
		// Note: this type is marked as 'beforefieldinit'.
		static EasingFunctions()
		{
			if (EasingFunctions.<>f__mg$cache0 == null)
			{
				EasingFunctions.<>f__mg$cache0 = new EasingFunction(EasingFunctions.Linear_);
			}
			EasingFunctions.Linear = EasingFunctions.<>f__mg$cache0;
			if (EasingFunctions.<>f__mg$cache1 == null)
			{
				EasingFunctions.<>f__mg$cache1 = new EasingFunction(EasingFunctions.EaseInSine_);
			}
			EasingFunctions.EaseInSine = EasingFunctions.<>f__mg$cache1;
			if (EasingFunctions.<>f__mg$cache2 == null)
			{
				EasingFunctions.<>f__mg$cache2 = new EasingFunction(EasingFunctions.EaseOutSine_);
			}
			EasingFunctions.EaseOutSine = EasingFunctions.<>f__mg$cache2;
			if (EasingFunctions.<>f__mg$cache3 == null)
			{
				EasingFunctions.<>f__mg$cache3 = new EasingFunction(EasingFunctions.EaseInOutSine_);
			}
			EasingFunctions.EaseInOutSine = EasingFunctions.<>f__mg$cache3;
			if (EasingFunctions.<>f__mg$cache4 == null)
			{
				EasingFunctions.<>f__mg$cache4 = new EasingFunction(EasingFunctions.EaseInQuad_);
			}
			EasingFunctions.EaseInQuad = EasingFunctions.<>f__mg$cache4;
			if (EasingFunctions.<>f__mg$cache5 == null)
			{
				EasingFunctions.<>f__mg$cache5 = new EasingFunction(EasingFunctions.EaseOutQuad_);
			}
			EasingFunctions.EaseOutQuad = EasingFunctions.<>f__mg$cache5;
			if (EasingFunctions.<>f__mg$cache6 == null)
			{
				EasingFunctions.<>f__mg$cache6 = new EasingFunction(EasingFunctions.EaseInOutQuad_);
			}
			EasingFunctions.EaseInOutQuad = EasingFunctions.<>f__mg$cache6;
			if (EasingFunctions.<>f__mg$cache7 == null)
			{
				EasingFunctions.<>f__mg$cache7 = new EasingFunction(EasingFunctions.EaseInCubic_);
			}
			EasingFunctions.EaseInCubic = EasingFunctions.<>f__mg$cache7;
			if (EasingFunctions.<>f__mg$cache8 == null)
			{
				EasingFunctions.<>f__mg$cache8 = new EasingFunction(EasingFunctions.EaseOutCubic_);
			}
			EasingFunctions.EaseOutCubic = EasingFunctions.<>f__mg$cache8;
			if (EasingFunctions.<>f__mg$cache9 == null)
			{
				EasingFunctions.<>f__mg$cache9 = new EasingFunction(EasingFunctions.EaseInOutCubic_);
			}
			EasingFunctions.EaseInOutCubic = EasingFunctions.<>f__mg$cache9;
			if (EasingFunctions.<>f__mg$cacheA == null)
			{
				EasingFunctions.<>f__mg$cacheA = new EasingFunction(EasingFunctions.EaseInQuart_);
			}
			EasingFunctions.EaseInQuart = EasingFunctions.<>f__mg$cacheA;
			if (EasingFunctions.<>f__mg$cacheB == null)
			{
				EasingFunctions.<>f__mg$cacheB = new EasingFunction(EasingFunctions.EaseOutQuart_);
			}
			EasingFunctions.EaseOutQuart = EasingFunctions.<>f__mg$cacheB;
			if (EasingFunctions.<>f__mg$cacheC == null)
			{
				EasingFunctions.<>f__mg$cacheC = new EasingFunction(EasingFunctions.EaseInOutQuart_);
			}
			EasingFunctions.EaseInOutQuart = EasingFunctions.<>f__mg$cacheC;
			if (EasingFunctions.<>f__mg$cacheD == null)
			{
				EasingFunctions.<>f__mg$cacheD = new EasingFunction(EasingFunctions.EaseInQuint_);
			}
			EasingFunctions.EaseInQuint = EasingFunctions.<>f__mg$cacheD;
			if (EasingFunctions.<>f__mg$cacheE == null)
			{
				EasingFunctions.<>f__mg$cacheE = new EasingFunction(EasingFunctions.EaseOutQuint_);
			}
			EasingFunctions.EaseOutQuint = EasingFunctions.<>f__mg$cacheE;
			if (EasingFunctions.<>f__mg$cacheF == null)
			{
				EasingFunctions.<>f__mg$cacheF = new EasingFunction(EasingFunctions.EaseInOutQuint_);
			}
			EasingFunctions.EaseInOutQuint = EasingFunctions.<>f__mg$cacheF;
			if (EasingFunctions.<>f__mg$cache10 == null)
			{
				EasingFunctions.<>f__mg$cache10 = new EasingFunction(EasingFunctions.EaseInExpo_);
			}
			EasingFunctions.EaseInExpo = EasingFunctions.<>f__mg$cache10;
			if (EasingFunctions.<>f__mg$cache11 == null)
			{
				EasingFunctions.<>f__mg$cache11 = new EasingFunction(EasingFunctions.EaseOutExpo_);
			}
			EasingFunctions.EaseOutExpo = EasingFunctions.<>f__mg$cache11;
			if (EasingFunctions.<>f__mg$cache12 == null)
			{
				EasingFunctions.<>f__mg$cache12 = new EasingFunction(EasingFunctions.EaseInOutExpo_);
			}
			EasingFunctions.EaseInOutExpo = EasingFunctions.<>f__mg$cache12;
			if (EasingFunctions.<>f__mg$cache13 == null)
			{
				EasingFunctions.<>f__mg$cache13 = new EasingFunction(EasingFunctions.EaseInCirc_);
			}
			EasingFunctions.EaseInCirc = EasingFunctions.<>f__mg$cache13;
			if (EasingFunctions.<>f__mg$cache14 == null)
			{
				EasingFunctions.<>f__mg$cache14 = new EasingFunction(EasingFunctions.EaseOutCirc_);
			}
			EasingFunctions.EaseOutCirc = EasingFunctions.<>f__mg$cache14;
			if (EasingFunctions.<>f__mg$cache15 == null)
			{
				EasingFunctions.<>f__mg$cache15 = new EasingFunction(EasingFunctions.EaseInOutCirc_);
			}
			EasingFunctions.EaseInOutCirc = EasingFunctions.<>f__mg$cache15;
			if (EasingFunctions.<>f__mg$cache16 == null)
			{
				EasingFunctions.<>f__mg$cache16 = new EasingFunction(EasingFunctions.EaseInBounce_);
			}
			EasingFunctions.EaseInBounce = EasingFunctions.<>f__mg$cache16;
			if (EasingFunctions.<>f__mg$cache17 == null)
			{
				EasingFunctions.<>f__mg$cache17 = new EasingFunction(EasingFunctions.EaseOutBounce_);
			}
			EasingFunctions.EaseOutBounce = EasingFunctions.<>f__mg$cache17;
			if (EasingFunctions.<>f__mg$cache18 == null)
			{
				EasingFunctions.<>f__mg$cache18 = new EasingFunction(EasingFunctions.EaseInOutBounce_);
			}
			EasingFunctions.EaseInOutBounce = EasingFunctions.<>f__mg$cache18;
			EasingFunctions.defaultEaseInBack = ((float time, float duration) => EasingFunctions.EaseInBack_(time, duration, 1.70158f));
			EasingFunctions.defaultEaseOutBack = ((float time, float duration) => EasingFunctions.EaseOutBack_(time, duration, 1.70158f));
			EasingFunctions.defaultEaseInOutBack = ((float time, float duration) => EasingFunctions.EaseInOutBack_(time, duration, 1.70158f));
			EasingFunctions.defaultEaseInElastic = ((float time, float duration) => EasingFunctions.EaseInElastic_(time, duration, 1.70158f, 0f));
			EasingFunctions.defaultEaseOutElastic = ((float time, float duration) => EasingFunctions.EaseOutElastic_(time, duration, 1.70158f, 0f));
			EasingFunctions.defaultEaseInOutElastic = ((float time, float duration) => EasingFunctions.EaseInOutElastic_(time, duration, 1.70158f, 0f));
		}

		// Token: 0x04001ABB RID: 6843
		private const float DefaultOvershoot = 1.70158f;

		// Token: 0x04001ABC RID: 6844
		private const float DefaultAmplitude = 1.70158f;

		// Token: 0x04001ABD RID: 6845
		private const float DefaultPeriod = 0f;

		// Token: 0x04001ABE RID: 6846
		private const float PiDivide2 = 1.5707964f;

		// Token: 0x04001ABF RID: 6847
		private const float PiMultiply2 = 6.2831855f;

		// Token: 0x04001AC0 RID: 6848
		public static readonly EasingFunction Linear;

		// Token: 0x04001AC1 RID: 6849
		public static readonly EasingFunction EaseInSine;

		// Token: 0x04001AC2 RID: 6850
		public static readonly EasingFunction EaseOutSine;

		// Token: 0x04001AC3 RID: 6851
		public static readonly EasingFunction EaseInOutSine;

		// Token: 0x04001AC4 RID: 6852
		public static readonly EasingFunction EaseInQuad;

		// Token: 0x04001AC5 RID: 6853
		public static readonly EasingFunction EaseOutQuad;

		// Token: 0x04001AC6 RID: 6854
		public static readonly EasingFunction EaseInOutQuad;

		// Token: 0x04001AC7 RID: 6855
		public static readonly EasingFunction EaseInCubic;

		// Token: 0x04001AC8 RID: 6856
		public static readonly EasingFunction EaseOutCubic;

		// Token: 0x04001AC9 RID: 6857
		public static readonly EasingFunction EaseInOutCubic;

		// Token: 0x04001ACA RID: 6858
		public static readonly EasingFunction EaseInQuart;

		// Token: 0x04001ACB RID: 6859
		public static readonly EasingFunction EaseOutQuart;

		// Token: 0x04001ACC RID: 6860
		public static readonly EasingFunction EaseInOutQuart;

		// Token: 0x04001ACD RID: 6861
		public static readonly EasingFunction EaseInQuint;

		// Token: 0x04001ACE RID: 6862
		public static readonly EasingFunction EaseOutQuint;

		// Token: 0x04001ACF RID: 6863
		public static readonly EasingFunction EaseInOutQuint;

		// Token: 0x04001AD0 RID: 6864
		public static readonly EasingFunction EaseInExpo;

		// Token: 0x04001AD1 RID: 6865
		public static readonly EasingFunction EaseOutExpo;

		// Token: 0x04001AD2 RID: 6866
		public static readonly EasingFunction EaseInOutExpo;

		// Token: 0x04001AD3 RID: 6867
		public static readonly EasingFunction EaseInCirc;

		// Token: 0x04001AD4 RID: 6868
		public static readonly EasingFunction EaseOutCirc;

		// Token: 0x04001AD5 RID: 6869
		public static readonly EasingFunction EaseInOutCirc;

		// Token: 0x04001AD6 RID: 6870
		public static readonly EasingFunction EaseInBounce;

		// Token: 0x04001AD7 RID: 6871
		public static readonly EasingFunction EaseOutBounce;

		// Token: 0x04001AD8 RID: 6872
		public static readonly EasingFunction EaseInOutBounce;

		// Token: 0x04001AD9 RID: 6873
		private static readonly EasingFunction defaultEaseInBack;

		// Token: 0x04001ADA RID: 6874
		private static readonly EasingFunction defaultEaseOutBack;

		// Token: 0x04001ADB RID: 6875
		private static readonly EasingFunction defaultEaseInOutBack;

		// Token: 0x04001ADC RID: 6876
		private static readonly EasingFunction defaultEaseInElastic;

		// Token: 0x04001ADD RID: 6877
		private static readonly EasingFunction defaultEaseOutElastic;

		// Token: 0x04001ADE RID: 6878
		private static readonly EasingFunction defaultEaseInOutElastic;

		// Token: 0x04001ADF RID: 6879
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache0;

		// Token: 0x04001AE0 RID: 6880
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache1;

		// Token: 0x04001AE1 RID: 6881
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache2;

		// Token: 0x04001AE2 RID: 6882
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache3;

		// Token: 0x04001AE3 RID: 6883
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache4;

		// Token: 0x04001AE4 RID: 6884
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache5;

		// Token: 0x04001AE5 RID: 6885
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache6;

		// Token: 0x04001AE6 RID: 6886
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache7;

		// Token: 0x04001AE7 RID: 6887
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache8;

		// Token: 0x04001AE8 RID: 6888
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache9;

		// Token: 0x04001AE9 RID: 6889
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheA;

		// Token: 0x04001AEA RID: 6890
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheB;

		// Token: 0x04001AEB RID: 6891
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheC;

		// Token: 0x04001AEC RID: 6892
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheD;

		// Token: 0x04001AED RID: 6893
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheE;

		// Token: 0x04001AEE RID: 6894
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cacheF;

		// Token: 0x04001AEF RID: 6895
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache10;

		// Token: 0x04001AF0 RID: 6896
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache11;

		// Token: 0x04001AF1 RID: 6897
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache12;

		// Token: 0x04001AF2 RID: 6898
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache13;

		// Token: 0x04001AF3 RID: 6899
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache14;

		// Token: 0x04001AF4 RID: 6900
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache15;

		// Token: 0x04001AF5 RID: 6901
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache16;

		// Token: 0x04001AF6 RID: 6902
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache17;

		// Token: 0x04001AF7 RID: 6903
		[CompilerGenerated]
		private static EasingFunction <>f__mg$cache18;
	}
}
