using System;
using System.Collections.Generic;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001359 RID: 4953
	public class TextSlideEffect : TextEffect
	{
		// Token: 0x170022B6 RID: 8886
		// (get) Token: 0x0600A60C RID: 42508 RVA: 0x00439523 File Offset: 0x00437923
		// (set) Token: 0x0600A60D RID: 42509 RVA: 0x0043952B File Offset: 0x0043792B
		public float subPos
		{
			get
			{
				return this.m_SubPos;
			}
			set
			{
				this.m_SubPos = value;
				if (base.graphic != null)
				{
					base.graphic.SetVerticesDirty();
				}
			}
		}

		// Token: 0x0600A60E RID: 42510 RVA: 0x00439550 File Offset: 0x00437950
		protected override void Modify(ref List<UIVertex> _stream)
		{
			int i = 0;
			int count = _stream.Count;
			while (i < count)
			{
				for (int j = 0; j < 6; j++)
				{
					UIVertex value = _stream[i + j];
					Vector3 position = value.position;
					position.x -= this.subPos;
					value.position = position;
					_stream[i + j] = value;
				}
				i += 6;
			}
		}

		// Token: 0x04008279 RID: 33401
		private float m_SubPos;
	}
}
