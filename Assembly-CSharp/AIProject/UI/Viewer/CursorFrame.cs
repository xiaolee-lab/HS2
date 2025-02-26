using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace AIProject.UI.Viewer
{
	// Token: 0x02000EC4 RID: 3780
	public static class CursorFrame
	{
		// Token: 0x1700186D RID: 6253
		// (get) Token: 0x06007BD4 RID: 31700 RVA: 0x00344A7A File Offset: 0x00342E7A
		private static RectTransform.Axis[] axes
		{
			[CompilerGenerated]
			get
			{
				RectTransform.Axis[] result;
				if ((result = CursorFrame._axes) == null)
				{
					result = (CursorFrame._axes = Enum.GetValues(typeof(RectTransform.Axis)).Cast<RectTransform.Axis>().ToArray<RectTransform.Axis>());
				}
				return result;
			}
		}

		// Token: 0x06007BD5 RID: 31701 RVA: 0x00344AA7 File Offset: 0x00342EA7
		public static void Set(RectTransform cursor, RectTransform target, RectTransform size = null)
		{
			cursor.position = target.position;
			if (size == null)
			{
				size = target;
			}
			CursorFrame.SetSize(cursor, size);
		}

		// Token: 0x06007BD6 RID: 31702 RVA: 0x00344ACC File Offset: 0x00342ECC
		public static void SetSize(RectTransform cursor, RectTransform size)
		{
			for (int i = 0; i < CursorFrame.axes.Length; i++)
			{
				cursor.SetSizeWithCurrentAnchors(CursorFrame.axes[i], size.rect.size[i]);
			}
		}

		// Token: 0x06007BD7 RID: 31703 RVA: 0x00344B18 File Offset: 0x00342F18
		public static void Set(RectTransform cursor, RectTransform target, ref Vector3 velocity, float? smoothMoveTime = null, float? smoothSizeTime = null)
		{
			if (smoothMoveTime == null)
			{
				cursor.position = target.position;
			}
			else
			{
				cursor.position = Smooth.Damp(cursor.position, target.position, ref velocity, smoothMoveTime.Value);
			}
			if (smoothSizeTime != null)
			{
				Vector3 vector = velocity;
				CursorFrame.SetSize(cursor, target, ref vector, smoothSizeTime.Value);
				if (smoothMoveTime == null)
				{
					velocity = vector;
				}
			}
		}

		// Token: 0x06007BD8 RID: 31704 RVA: 0x00344B98 File Offset: 0x00342F98
		public static void SetSize(RectTransform cursor, RectTransform target, ref Vector3 velocity, float smoothTime)
		{
			for (int i = 0; i < CursorFrame.axes.Length; i++)
			{
				float value = velocity[i];
				float size = Smooth.Damp(cursor.rect.size[i], target.rect.size[i], ref value, smoothTime);
				velocity[i] = value;
				cursor.SetSizeWithCurrentAnchors(CursorFrame.axes[i], size);
			}
		}

		// Token: 0x04006367 RID: 25447
		private static RectTransform.Axis[] _axes;
	}
}
