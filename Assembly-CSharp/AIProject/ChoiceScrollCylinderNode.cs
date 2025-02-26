using System;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x02000E4B RID: 3659
	public class ChoiceScrollCylinderNode : ScrollCylinderNode
	{
		// Token: 0x06007333 RID: 29491 RVA: 0x00311854 File Offset: 0x0030FC54
		protected override void Update()
		{
			this.tmpColor = new Color(0f, 0f, 0f, this._alpha);
			float num = 0f;
			float deltaTime = Time.deltaTime;
			this.tmpColor.a = (this._alpha = Mathf.SmoothDamp(this.tmpColor.a, this.endA, ref num, this.smoothTime, float.PositiveInfinity, deltaTime));
			for (int i = 0; i < 3; i++)
			{
				this.tmpColor[i] = this._alphaBG.color[i];
			}
			this._alphaBG.color = this.tmpColor;
			if (this.text != null)
			{
				for (int j = 0; j < 3; j++)
				{
					this.tmpColor[j] = this.text.color[j];
				}
				this.text.color = this.tmpColor;
			}
			this.tmpScl = this._localScale;
			Vector3 zero = Vector3.zero;
			if ((this.prephaseScale == 0 && this.phaseScale == 1) || (this.prephaseScale == 1 && this.phaseScale == 0))
			{
				this.tmpScl = (this._localScale = Vector3.SmoothDamp(this.tmpScl, this.endScl, ref zero, this.smoothTime, float.PositiveInfinity, deltaTime));
			}
			else
			{
				this.tmpScl = (this._localScale = Vector3.SmoothDamp(this.tmpScl, this.endScl, ref zero, this.smoothTimeV2, float.PositiveInfinity, deltaTime));
			}
			this._scaleBG.transform.localScale = this.tmpScl;
			if (this.text != null)
			{
				this.text.transform.localScale = this.tmpScl;
			}
		}

		// Token: 0x04005E36 RID: 24118
		[SerializeField]
		private Image _alphaBG;

		// Token: 0x04005E37 RID: 24119
		[SerializeField]
		private Image _scaleBG;

		// Token: 0x04005E38 RID: 24120
		private float _alpha;

		// Token: 0x04005E39 RID: 24121
		private Vector3 _localScale = Vector3.zero;
	}
}
