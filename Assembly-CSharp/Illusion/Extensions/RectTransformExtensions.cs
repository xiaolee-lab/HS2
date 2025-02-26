using System;
using UnityEngine;
using UnityEngine.UI;

namespace Illusion.Extensions
{
	// Token: 0x02001080 RID: 4224
	public static class RectTransformExtensions
	{
		// Token: 0x06008D72 RID: 36210 RVA: 0x003B13C8 File Offset: 0x003AF7C8
		public static void SetPosition(this RectTransform self, Transform target3D, Vector3 setPos, Camera targetCamera = null)
		{
			if (targetCamera == null)
			{
				targetCamera = Camera.main;
			}
			Vector2 vector = RectTransformUtility.WorldToScreenPoint(targetCamera, target3D.position);
			self.position = new Vector2(vector[0] + setPos[0], vector[1] + setPos[1]);
		}

		// Token: 0x06008D73 RID: 36211 RVA: 0x003B1427 File Offset: 0x003AF827
		public static void AdjustSize(this RectTransform self, Text text)
		{
			self.sizeDelta = new Vector2(text.preferredWidth, text.preferredHeight);
		}

		// Token: 0x06008D74 RID: 36212 RVA: 0x003B1440 File Offset: 0x003AF840
		public static void AdjustHeight(this RectTransform self, Text text)
		{
			self.sizeDelta = new Vector2(self.sizeDelta.x, text.preferredHeight);
		}

		// Token: 0x06008D75 RID: 36213 RVA: 0x003B146C File Offset: 0x003AF86C
		public static bool IsHeightOver(this RectTransform self, Text text)
		{
			return text.preferredHeight > self.rect.height;
		}

		// Token: 0x06008D76 RID: 36214 RVA: 0x003B1490 File Offset: 0x003AF890
		public static void Left(this RectTransform self, float v)
		{
			self.offsetMin = new Vector2(v, self.offsetMin.y);
		}

		// Token: 0x06008D77 RID: 36215 RVA: 0x003B14B8 File Offset: 0x003AF8B8
		public static void Top(this RectTransform self, float v)
		{
			self.offsetMin = new Vector2(self.offsetMin.x, v);
		}

		// Token: 0x06008D78 RID: 36216 RVA: 0x003B14E0 File Offset: 0x003AF8E0
		public static void Right(this RectTransform self, float v)
		{
			self.offsetMax = new Vector2(-v, self.offsetMax.y);
		}

		// Token: 0x06008D79 RID: 36217 RVA: 0x003B1508 File Offset: 0x003AF908
		public static void Bottom(this RectTransform self, float v)
		{
			self.offsetMax = new Vector2(self.offsetMax.x, -v);
		}

		// Token: 0x06008D7A RID: 36218 RVA: 0x003B1530 File Offset: 0x003AF930
		public static void Width(this RectTransform self, Vector2 v)
		{
			self.Left(v.x);
			self.Right(v.y);
		}

		// Token: 0x06008D7B RID: 36219 RVA: 0x003B154C File Offset: 0x003AF94C
		public static void Height(this RectTransform self, Vector2 v)
		{
			self.Top(v.x);
			self.Bottom(v.y);
		}

		// Token: 0x06008D7C RID: 36220 RVA: 0x003B1568 File Offset: 0x003AF968
		public static float Left(this RectTransform self)
		{
			return self.offsetMin.x;
		}

		// Token: 0x06008D7D RID: 36221 RVA: 0x003B1584 File Offset: 0x003AF984
		public static float Top(this RectTransform self)
		{
			return self.offsetMin.y;
		}

		// Token: 0x06008D7E RID: 36222 RVA: 0x003B15A0 File Offset: 0x003AF9A0
		public static float Right(this RectTransform self)
		{
			return -self.offsetMax.x;
		}

		// Token: 0x06008D7F RID: 36223 RVA: 0x003B15BC File Offset: 0x003AF9BC
		public static float Bottom(this RectTransform self)
		{
			return -self.offsetMax.y;
		}

		// Token: 0x06008D80 RID: 36224 RVA: 0x003B15D8 File Offset: 0x003AF9D8
		public static Vector2 Width(this RectTransform self)
		{
			return new Vector2(self.Left(), self.Right());
		}

		// Token: 0x06008D81 RID: 36225 RVA: 0x003B15EB File Offset: 0x003AF9EB
		public static Vector2 Height(this RectTransform self)
		{
			return new Vector2(self.Top(), self.Bottom());
		}

		// Token: 0x06008D82 RID: 36226 RVA: 0x003B1600 File Offset: 0x003AFA00
		public static bool IsStretchWidth(this RectTransform self)
		{
			return self.anchorMin.x == 0f && self.anchorMax.x == 1f;
		}

		// Token: 0x06008D83 RID: 36227 RVA: 0x003B1640 File Offset: 0x003AFA40
		public static bool IsStretchHeight(this RectTransform self)
		{
			return self.anchorMin.y == 0f && self.anchorMax.y == 1f;
		}

		// Token: 0x06008D84 RID: 36228 RVA: 0x003B167D File Offset: 0x003AFA7D
		public static bool IsStretch(this RectTransform self)
		{
			return self.IsStretchWidth() || self.IsStretchHeight();
		}

		// Token: 0x06008D85 RID: 36229 RVA: 0x003B1693 File Offset: 0x003AFA93
		public static bool IsStretchWidthHeight(this RectTransform self)
		{
			return self.IsStretchWidth() && self.IsStretchHeight();
		}
	}
}
