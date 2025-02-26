using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ReMotion.Extensions
{
	// Token: 0x020004FC RID: 1276
	public static class TweenExtensions
	{
		// Token: 0x060017FE RID: 6142 RVA: 0x000954EC File Offset: 0x000938EC
		public static Tween<Transform, Vector3> TweenPosition(this Transform transform, Vector3 to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Transform, Vector3> tween = settings.UseVector3Tween(transform, (Transform x) => x.position, delegate(Transform target, ref Vector3 value)
			{
				target.position = value;
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x00095571 File Offset: 0x00093971
		public static IObservable<Unit> TweenPositionAsync(this Transform transform, Vector3 to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return transform.TweenPosition(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x00095588 File Offset: 0x00093988
		public static Tween<Transform, Vector2> TweenPositionXY(this Transform transform, Vector2 to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Transform, Vector2> tween = settings.UseVector2Tween(transform, (Transform x) => x.position, delegate(Transform target, ref Vector2 value)
			{
				Vector3 position = target.position;
				target.position = new Vector3
				{
					x = value.x,
					y = value.y,
					z = position.z
				};
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0009560D File Offset: 0x00093A0D
		public static IObservable<Unit> TweenPositionXYAsync(this Transform transform, Vector2 to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return transform.TweenPositionXY(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00095624 File Offset: 0x00093A24
		public static Tween<Transform, float> TweenPositionX(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Transform, float> tween = settings.UseFloatTween(transform, (Transform x) => x.position.x, delegate(Transform target, ref float value)
			{
				Vector3 position = target.position;
				target.position = new Vector3
				{
					x = value,
					y = position.y,
					z = position.z
				};
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000956A9 File Offset: 0x00093AA9
		public static IObservable<Unit> TweenPositionXAsync(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return transform.TweenPositionX(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000956C0 File Offset: 0x00093AC0
		public static Tween<Transform, float> TweenPositionY(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Transform, float> tween = settings.UseFloatTween(transform, (Transform x) => x.position.y, delegate(Transform target, ref float value)
			{
				Vector3 position = target.position;
				target.position = new Vector3
				{
					x = position.x,
					y = value,
					z = position.z
				};
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00095745 File Offset: 0x00093B45
		public static IObservable<Unit> TweenPositionYAsync(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return transform.TweenPositionY(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0009575C File Offset: 0x00093B5C
		public static Tween<Transform, float> TweenPositionZ(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Transform, float> tween = settings.UseFloatTween(transform, (Transform x) => x.position.z, delegate(Transform target, ref float value)
			{
				Vector3 position = target.position;
				target.position = new Vector3
				{
					x = position.x,
					y = position.y,
					z = value
				};
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x000957E1 File Offset: 0x00093BE1
		public static IObservable<Unit> TweenPositionZAsync(this Transform transform, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return transform.TweenPositionZ(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x000957F8 File Offset: 0x00093BF8
		public static Tween<Graphic, float> TweenAlpha(this Graphic graphic, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Graphic, float> tween = settings.UseFloatTween(graphic, (Graphic x) => x.color.a, delegate(Graphic target, ref float value)
			{
				Color color = target.color;
				target.color = new Color
				{
					r = color.r,
					g = color.g,
					b = color.b,
					a = value
				};
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0009587D File Offset: 0x00093C7D
		public static IObservable<Unit> TweenAlphaAsync(this Graphic graphic, float to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return graphic.TweenAlpha(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00095894 File Offset: 0x00093C94
		public static Tween<Graphic, Color> TweenColor(this Graphic graphic, Color to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false, bool autoStart = true)
		{
			settings = (settings ?? TweenSettings.Default);
			easing = (easing ?? settings.DefaultEasing);
			Tween<Graphic, Color> tween = settings.UseColorTween(graphic, (Graphic x) => x.color, delegate(Graphic target, ref Color value)
			{
				target.color = value;
			}, easing, duration, to, isRelativeTo);
			if (autoStart)
			{
				tween.Start();
			}
			return tween;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00095919 File Offset: 0x00093D19
		public static IObservable<Unit> TweenColorAsync(this Graphic graphic, Color to, float duration, EasingFunction easing = null, TweenSettings settings = null, bool isRelativeTo = false)
		{
			return graphic.TweenColor(to, duration, easing, settings, isRelativeTo, false).ToObservable(true);
		}
	}
}
