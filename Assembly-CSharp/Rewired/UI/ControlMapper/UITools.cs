using System;
using UnityEngine;
using UnityEngine.UI;

namespace Rewired.UI.ControlMapper
{
	// Token: 0x0200056A RID: 1386
	public static class UITools
	{
		// Token: 0x06001D24 RID: 7460 RVA: 0x000AB944 File Offset: 0x000A9D44
		public static GameObject InstantiateGUIObject<T>(GameObject prefab, Transform parent, string name) where T : Component
		{
			GameObject gameObject = UITools.InstantiateGUIObject_Pre<T>(prefab, parent, name);
			if (gameObject == null)
			{
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				UnityEngine.Debug.LogError(name + " prefab is missing RectTransform component!");
			}
			else
			{
				component.localScale = Vector3.one;
			}
			return gameObject;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000AB99C File Offset: 0x000A9D9C
		public static GameObject InstantiateGUIObject<T>(GameObject prefab, Transform parent, string name, Vector2 pivot, Vector2 anchorMin, Vector2 anchorMax, Vector2 anchoredPosition) where T : Component
		{
			GameObject gameObject = UITools.InstantiateGUIObject_Pre<T>(prefab, parent, name);
			if (gameObject == null)
			{
				return null;
			}
			RectTransform component = gameObject.GetComponent<RectTransform>();
			if (component == null)
			{
				UnityEngine.Debug.LogError(name + " prefab is missing RectTransform component!");
			}
			else
			{
				component.localScale = Vector3.one;
				component.pivot = pivot;
				component.anchorMin = anchorMin;
				component.anchorMax = anchorMax;
				component.anchoredPosition = anchoredPosition;
			}
			return gameObject;
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000ABA14 File Offset: 0x000A9E14
		private static GameObject InstantiateGUIObject_Pre<T>(GameObject prefab, Transform parent, string name) where T : Component
		{
			if (prefab == null)
			{
				UnityEngine.Debug.LogError(name + " prefab is null!");
				return null;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(prefab);
			if (!string.IsNullOrEmpty(name))
			{
				gameObject.name = name;
			}
			T component = gameObject.GetComponent<T>();
			if (component == null)
			{
				UnityEngine.Debug.LogError(name + " prefab is missing the " + component.GetType().ToString() + " component!");
				return null;
			}
			if (parent != null)
			{
				gameObject.transform.SetParent(parent, false);
			}
			return gameObject;
		}

		// Token: 0x06001D27 RID: 7463 RVA: 0x000ABAB4 File Offset: 0x000A9EB4
		public static Vector3 GetPointOnRectEdge(RectTransform rectTransform, Vector2 dir)
		{
			if (rectTransform == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			Rect rect = rectTransform.rect;
			dir = rect.center + Vector2.Scale(rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x06001D28 RID: 7464 RVA: 0x000ABB3C File Offset: 0x000A9F3C
		public static Rect GetWorldSpaceRect(RectTransform rt)
		{
			if (rt == null)
			{
				return default(Rect);
			}
			Rect rect = rt.rect;
			Vector2 vector = rt.TransformPoint(new Vector2(rect.xMin, rect.yMin));
			Vector2 vector2 = rt.TransformPoint(new Vector2(rect.xMin, rect.yMax));
			Vector2 vector3 = rt.TransformPoint(new Vector2(rect.xMax, rect.yMin));
			return new Rect(vector.x, vector.y, vector3.x - vector.x, vector2.y - vector.y);
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000ABC04 File Offset: 0x000AA004
		public static Rect TransformRectTo(Transform from, Transform to, Rect rect)
		{
			Vector3 position;
			Vector3 position2;
			Vector3 position3;
			if (from != null)
			{
				position = from.TransformPoint(new Vector2(rect.xMin, rect.yMin));
				position2 = from.TransformPoint(new Vector2(rect.xMin, rect.yMax));
				position3 = from.TransformPoint(new Vector2(rect.xMax, rect.yMin));
			}
			else
			{
				position = new Vector2(rect.xMin, rect.yMin);
				position2 = new Vector2(rect.xMin, rect.yMax);
				position3 = new Vector2(rect.xMax, rect.yMin);
			}
			if (to != null)
			{
				position = to.InverseTransformPoint(position);
				position2 = to.InverseTransformPoint(position2);
				position3 = to.InverseTransformPoint(position3);
			}
			return new Rect(position.x, position.y, position3.x - position.x, position.y - position2.y);
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x000ABD1F File Offset: 0x000AA11F
		public static Rect InvertY(Rect rect)
		{
			return new Rect(rect.xMin, rect.yMin, rect.width, -rect.height);
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x000ABD44 File Offset: 0x000AA144
		public static void SetInteractable(Selectable selectable, bool state, bool playTransition)
		{
			if (selectable == null)
			{
				return;
			}
			if (!playTransition)
			{
				if (selectable.transition == Selectable.Transition.ColorTint)
				{
					ColorBlock colors = selectable.colors;
					float fadeDuration = colors.fadeDuration;
					colors.fadeDuration = 0f;
					selectable.colors = colors;
					selectable.interactable = state;
					colors.fadeDuration = fadeDuration;
					selectable.colors = colors;
				}
				else
				{
					selectable.interactable = state;
				}
			}
			else
			{
				selectable.interactable = state;
			}
		}
	}
}
