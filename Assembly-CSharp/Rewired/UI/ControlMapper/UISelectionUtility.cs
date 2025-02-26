using System;
using System.Collections.Generic;
using Rewired.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x02000568 RID: 1384
	public static class UISelectionUtility
	{
		// Token: 0x06001D1D RID: 7453 RVA: 0x000AB5A8 File Offset: 0x000A99A8
		public static Selectable FindNextSelectable(Selectable selectable, Transform transform, List<Selectable> allSelectables, Vector3 direction)
		{
			RectTransform rectTransform = transform as RectTransform;
			if (rectTransform == null)
			{
				return null;
			}
			direction.Normalize();
			Vector2 vector = direction;
			Vector2 vector2 = UITools.GetPointOnRectEdge(rectTransform, vector);
			bool flag = vector == Vector2.right * -1f || vector == Vector2.right;
			float num = float.PositiveInfinity;
			float num2 = float.PositiveInfinity;
			Selectable selectable2 = null;
			Selectable selectable3 = null;
			Vector2 point = vector2 + vector * 999999f;
			for (int i = 0; i < allSelectables.Count; i++)
			{
				Selectable selectable4 = allSelectables[i];
				if (!(selectable4 == selectable) && !(selectable4 == null))
				{
					if (selectable4.navigation.mode != Navigation.Mode.None)
					{
						if (selectable4.IsInteractable() || ReflectionTools.GetPrivateField<Selectable, bool>(selectable4, "m_GroupsAllowInteraction"))
						{
							RectTransform rectTransform2 = selectable4.transform as RectTransform;
							if (!(rectTransform2 == null))
							{
								Rect rect = UITools.InvertY(UITools.TransformRectTo(rectTransform2, transform, rectTransform2.rect));
								float num3;
								if (MathTools.LineIntersectsRect(vector2, point, rect, out num3))
								{
									if (flag)
									{
										num3 *= 0.25f;
									}
									if (num3 < num2)
									{
										num2 = num3;
										selectable3 = selectable4;
									}
								}
								Vector2 a = UnityTools.TransformPoint(rectTransform2, transform, rectTransform2.rect.center);
								Vector2 to = a - vector2;
								float num4 = Mathf.Abs(Vector2.Angle(vector, to));
								if (num4 <= 75f)
								{
									float sqrMagnitude = to.sqrMagnitude;
									if (sqrMagnitude < num)
									{
										num = sqrMagnitude;
										selectable2 = selectable4;
									}
								}
							}
						}
					}
				}
			}
			if (!(selectable3 != null) || !(selectable2 != null))
			{
				return selectable3 ?? selectable2;
			}
			if (num2 > num)
			{
				return selectable2;
			}
			return selectable3;
		}
	}
}
