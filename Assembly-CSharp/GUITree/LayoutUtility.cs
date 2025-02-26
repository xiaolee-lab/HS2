using System;
using UnityEngine;
using UnityEngine.UI;

namespace GUITree
{
	// Token: 0x0200123F RID: 4671
	public static class LayoutUtility
	{
		// Token: 0x06009992 RID: 39314 RVA: 0x003F3D63 File Offset: 0x003F2163
		public static float GetMinSize(ILayoutElement _element, int _axis)
		{
			return (_axis != 0) ? LayoutUtility.GetMinHeight(_element) : LayoutUtility.GetMinWidth(_element);
		}

		// Token: 0x06009993 RID: 39315 RVA: 0x003F3D7C File Offset: 0x003F217C
		public static float GetPreferredSize(ILayoutElement _element, int _axis)
		{
			return (_axis != 0) ? LayoutUtility.GetPreferredHeight(_element) : LayoutUtility.GetPreferredWidth(_element);
		}

		// Token: 0x06009994 RID: 39316 RVA: 0x003F3D95 File Offset: 0x003F2195
		public static float GetFlexibleSize(ILayoutElement _element, int _axis)
		{
			return (_axis != 0) ? LayoutUtility.GetFlexibleHeight(_element) : LayoutUtility.GetFlexibleWidth(_element);
		}

		// Token: 0x06009995 RID: 39317 RVA: 0x003F3DAE File Offset: 0x003F21AE
		public static float GetMinWidth(ILayoutElement _element)
		{
			return (_element != null) ? _element.minWidth : 0f;
		}

		// Token: 0x06009996 RID: 39318 RVA: 0x003F3DC6 File Offset: 0x003F21C6
		public static float GetPreferredWidth(ILayoutElement _element)
		{
			return (_element != null) ? Mathf.Max(Mathf.Max(_element.minWidth, _element.preferredWidth), 0f) : 0f;
		}

		// Token: 0x06009997 RID: 39319 RVA: 0x003F3DF3 File Offset: 0x003F21F3
		public static float GetFlexibleWidth(ILayoutElement _element)
		{
			return (_element != null) ? _element.flexibleWidth : 0f;
		}

		// Token: 0x06009998 RID: 39320 RVA: 0x003F3E0B File Offset: 0x003F220B
		public static float GetMinHeight(ILayoutElement _element)
		{
			return (_element != null) ? _element.minHeight : 0f;
		}

		// Token: 0x06009999 RID: 39321 RVA: 0x003F3E23 File Offset: 0x003F2223
		public static float GetPreferredHeight(ILayoutElement _element)
		{
			return (_element != null) ? Mathf.Max(Mathf.Max(_element.minHeight, _element.preferredHeight), 0f) : 0f;
		}

		// Token: 0x0600999A RID: 39322 RVA: 0x003F3E50 File Offset: 0x003F2250
		public static float GetFlexibleHeight(ILayoutElement _element)
		{
			return (_element != null) ? _element.flexibleHeight : 0f;
		}
	}
}
