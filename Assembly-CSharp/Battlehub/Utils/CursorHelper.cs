using System;
using System.Collections.Generic;
using UnityEngine;

namespace Battlehub.Utils
{
	// Token: 0x020000B1 RID: 177
	public class CursorHelper
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00018944 File Offset: 0x00016D44
		public void Map(KnownCursor cursorType, Texture2D texture)
		{
			this.m_knownCursorToTexture[cursorType] = texture;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00018953 File Offset: 0x00016D53
		public void Reset()
		{
			this.m_knownCursorToTexture.Clear();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00018960 File Offset: 0x00016D60
		public void SetCursor(object locker, KnownCursor cursorType)
		{
			this.SetCursor(locker, cursorType, new Vector2(0.5f, 0.5f), CursorMode.Auto);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001897C File Offset: 0x00016D7C
		public void SetCursor(object locker, KnownCursor cursorType, Vector2 hotspot, CursorMode mode)
		{
			Texture2D texture;
			if (!this.m_knownCursorToTexture.TryGetValue(cursorType, out texture))
			{
				texture = null;
			}
			this.SetCursor(locker, texture, hotspot, mode);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000189A9 File Offset: 0x00016DA9
		public void SetCursor(object locker, Texture2D texture)
		{
			this.SetCursor(locker, texture, new Vector2(0.5f, 0.5f), CursorMode.Auto);
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000189C4 File Offset: 0x00016DC4
		public void SetCursor(object locker, Texture2D texture, Vector2 hotspot, CursorMode mode)
		{
			if (this.m_locker != null && this.m_locker != locker)
			{
				return;
			}
			if (texture != null)
			{
				hotspot = new Vector2((float)texture.width * hotspot.x, (float)texture.height * hotspot.y);
			}
			this.m_locker = locker;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
			Cursor.SetCursor(texture, hotspot, mode);
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x00018A36 File Offset: 0x00016E36
		public void ResetCursor(object locker)
		{
			if (this.m_locker != locker)
			{
				return;
			}
			this.m_locker = null;
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}

		// Token: 0x04000348 RID: 840
		private object m_locker;

		// Token: 0x04000349 RID: 841
		private readonly Dictionary<KnownCursor, Texture2D> m_knownCursorToTexture = new Dictionary<KnownCursor, Texture2D>();
	}
}
