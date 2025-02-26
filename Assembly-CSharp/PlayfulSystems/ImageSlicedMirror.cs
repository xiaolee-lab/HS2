using System;
using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

namespace PlayfulSystems
{
	// Token: 0x0200064E RID: 1614
	public class ImageSlicedMirror : Image
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x000D99A0 File Offset: 0x000D7DA0
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			if (base.overrideSprite == null)
			{
				base.OnPopulateMesh(toFill);
				return;
			}
			if (base.hasBorder && base.type == Image.Type.Sliced)
			{
				this.GenerateSlicedFilledSprite(toFill);
				return;
			}
			base.OnPopulateMesh(toFill);
		}

		// Token: 0x06002648 RID: 9800 RVA: 0x000D99EC File Offset: 0x000D7DEC
		private void GenerateSlicedFilledSprite(VertexHelper toFill)
		{
			Vector4 outer;
			Vector4 inner;
			Vector4 vector;
			Vector4 vector2;
			if (base.overrideSprite != null)
			{
				outer = DataUtility.GetOuterUV(base.overrideSprite);
				inner = DataUtility.GetInnerUV(base.overrideSprite);
				vector = DataUtility.GetPadding(base.overrideSprite);
				vector2 = base.overrideSprite.border;
			}
			else
			{
				outer = Vector4.zero;
				inner = Vector4.zero;
				vector = Vector4.zero;
				vector2 = Vector4.zero;
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			vector2 = this.GetAdjustedBorders(vector2 / base.pixelsPerUnit, pixelAdjustedRect);
			vector /= base.pixelsPerUnit;
			this.SetSlicedVerts(pixelAdjustedRect, vector2, vector);
			this.SetSlicedUVs(outer, inner, vector2);
			toFill.Clear();
			for (int i = 0; i < 3; i++)
			{
				int num = i + 1;
				for (int j = 0; j < 3; j++)
				{
					if (base.fillCenter || i != 1 || j != 1)
					{
						int num2 = j + 1;
						ImageSlicedMirror.AddQuad(toFill, new Vector2(ImageSlicedMirror.s_VertScratch[i].x, ImageSlicedMirror.s_VertScratch[j].y), new Vector2(ImageSlicedMirror.s_VertScratch[num].x, ImageSlicedMirror.s_VertScratch[num2].y), this.color, new Vector2(ImageSlicedMirror.s_UVScratch[i].x, ImageSlicedMirror.s_UVScratch[j].y), new Vector2(ImageSlicedMirror.s_UVScratch[num].x, ImageSlicedMirror.s_UVScratch[num2].y));
					}
				}
			}
		}

		// Token: 0x06002649 RID: 9801 RVA: 0x000D9BA0 File Offset: 0x000D7FA0
		private void SetSlicedVerts(Rect rect, Vector4 border, Vector4 padding)
		{
			ImageSlicedMirror.s_VertScratch[0] = new Vector2(padding.x, padding.y);
			ImageSlicedMirror.s_VertScratch[3] = new Vector2(rect.width - padding.z, rect.height - padding.w);
			ImageSlicedMirror.s_VertScratch[1].x = border.x;
			ImageSlicedMirror.s_VertScratch[1].y = border.y;
			ImageSlicedMirror.s_VertScratch[2].x = rect.width - border.z;
			ImageSlicedMirror.s_VertScratch[2].y = rect.height - border.w;
			for (int i = 0; i < 4; i++)
			{
				Vector2[] array = ImageSlicedMirror.s_VertScratch;
				int num = i;
				array[num].x = array[num].x + rect.x;
				Vector2[] array2 = ImageSlicedMirror.s_VertScratch;
				int num2 = i;
				array2[num2].y = array2[num2].y + rect.y;
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x000D9CC0 File Offset: 0x000D80C0
		private void SetSlicedUVs(Vector4 outer, Vector4 inner, Vector4 border)
		{
			bool flag = border.x < base.overrideSprite.border.x || border.z < base.overrideSprite.border.z;
			bool flag2 = border.y < base.overrideSprite.border.y || border.w < base.overrideSprite.border.w;
			if (!flag && !flag2)
			{
				ImageSlicedMirror.s_UVScratch[0] = new Vector2(outer.x, outer.y);
				ImageSlicedMirror.s_UVScratch[1] = new Vector2(inner.x, inner.y);
				ImageSlicedMirror.s_UVScratch[2] = new Vector2(inner.z, inner.w);
				ImageSlicedMirror.s_UVScratch[3] = new Vector2(outer.z, outer.w);
				return;
			}
			ImageSlicedMirror.s_UVMultiplierScratch[0] = ((border.x == 0f || !flag) ? 1f : (border.x / base.overrideSprite.border.x));
			ImageSlicedMirror.s_UVMultiplierScratch[1] = ((border.y == 0f || !flag2) ? 1f : (border.y / base.overrideSprite.border.y));
			ImageSlicedMirror.s_UVMultiplierScratch[2] = ((border.z == 0f || !flag) ? 1f : (border.z / base.overrideSprite.border.z));
			ImageSlicedMirror.s_UVMultiplierScratch[3] = ((border.w == 0f || !flag2) ? 1f : (border.w / base.overrideSprite.border.w));
			ImageSlicedMirror.s_UVScratch[0] = new Vector2(outer.x, outer.y);
			ImageSlicedMirror.s_UVScratch[1] = new Vector2(inner.x * ImageSlicedMirror.s_UVMultiplierScratch[0], inner.y * ImageSlicedMirror.s_UVMultiplierScratch[1]);
			ImageSlicedMirror.s_UVScratch[2] = new Vector2(outer.z - (outer.z - inner.z) * ImageSlicedMirror.s_UVMultiplierScratch[2], outer.w - (outer.w - inner.w) * ImageSlicedMirror.s_UVMultiplierScratch[3]);
			ImageSlicedMirror.s_UVScratch[3] = new Vector2(outer.z, outer.w);
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x000D9FC4 File Offset: 0x000D83C4
		private static void AddQuad(VertexHelper vertexHelper, Vector2 posMin, Vector2 posMax, Color32 color, Vector2 uvMin, Vector2 uvMax)
		{
			int currentVertCount = vertexHelper.currentVertCount;
			vertexHelper.AddVert(new Vector3(posMin.x, posMin.y, 0f), color, new Vector2(uvMin.x, uvMin.y));
			vertexHelper.AddVert(new Vector3(posMin.x, posMax.y, 0f), color, new Vector2(uvMin.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMax.y, 0f), color, new Vector2(uvMax.x, uvMax.y));
			vertexHelper.AddVert(new Vector3(posMax.x, posMin.y, 0f), color, new Vector2(uvMax.x, uvMin.y));
			vertexHelper.AddTriangle(currentVertCount, currentVertCount + 1, currentVertCount + 2);
			vertexHelper.AddTriangle(currentVertCount + 2, currentVertCount + 3, currentVertCount);
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x000DA0BC File Offset: 0x000D84BC
		private Vector4 GetAdjustedBorders(Vector4 border, Rect rect)
		{
			for (int i = 0; i <= 1; i++)
			{
				float num = border[i] + border[i + 2];
				if (rect.size[i] < num && num != 0f)
				{
					float num2 = rect.size[i] / num;
					ref Vector4 ptr = ref border;
					int index;
					border[index = i] = ptr[index] * num2;
					ptr = ref border;
					int index2;
					border[index2 = i + 2] = ptr[index2] * num2;
				}
			}
			return border;
		}

		// Token: 0x040025FC RID: 9724
		private static readonly Vector2[] s_VertScratch = new Vector2[4];

		// Token: 0x040025FD RID: 9725
		private static readonly Vector2[] s_UVScratch = new Vector2[4];

		// Token: 0x040025FE RID: 9726
		private static readonly float[] s_UVMultiplierScratch = new float[4];
	}
}
