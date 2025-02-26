using System;
using System.Linq;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x02001109 RID: 4361
public class MatAnmFrame : MonoBehaviour
{
	// Token: 0x060090C9 RID: 37065 RVA: 0x003C443C File Offset: 0x003C283C
	private void Awake()
	{
		this._Color = Shader.PropertyToID("_Color");
	}

	// Token: 0x060090CA RID: 37066 RVA: 0x003C444E File Offset: 0x003C284E
	private void Start()
	{
		this.rendererData = base.GetComponent<Renderer>();
		if (null == this.rendererData)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060090CB RID: 37067 RVA: 0x003C4474 File Offset: 0x003C2874
	private void Update()
	{
		if (!this.Usage)
		{
			return;
		}
		AnimationClip playingClip = this.Anm.GetPlayingClip();
		int num = -1;
		foreach (var <>__AnonType in this.PtnInfo.Select((MatAnmPtnInfo value, int index) => new
		{
			value,
			index
		}))
		{
			if (!(<>__AnonType.value.PtnName != playingClip.name))
			{
				num = <>__AnonType.index;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		MatAnmPtnInfo matAnmPtnInfo = this.PtnInfo[num];
		AnimationState animationState = this.Anm[playingClip.name];
		float num2 = animationState.time;
		while (playingClip.length < num2)
		{
			num2 -= playingClip.length;
		}
		int num3 = (int)Mathf.Lerp(0f, playingClip.frameRate * playingClip.length, Mathf.InverseLerp(0f, playingClip.length, num2));
		bool flag = false;
		for (int i = 0; i < matAnmPtnInfo.Param.Length - 1; i++)
		{
			if (matAnmPtnInfo.Param[i].Frame <= num3 && matAnmPtnInfo.Param[i + 1].Frame >= num3)
			{
				float t = Mathf.InverseLerp((float)matAnmPtnInfo.Param[i].Frame, (float)matAnmPtnInfo.Param[i + 1].Frame, (float)num3);
				Color32 c = default(Color32);
				c.r = (byte)Mathf.Lerp((float)matAnmPtnInfo.Param[i].ColorVal.r, (float)matAnmPtnInfo.Param[i + 1].ColorVal.r, t);
				c.g = (byte)Mathf.Lerp((float)matAnmPtnInfo.Param[i].ColorVal.g, (float)matAnmPtnInfo.Param[i + 1].ColorVal.g, t);
				c.b = (byte)Mathf.Lerp((float)matAnmPtnInfo.Param[i].ColorVal.b, (float)matAnmPtnInfo.Param[i + 1].ColorVal.b, t);
				c.a = (byte)Mathf.Lerp((float)matAnmPtnInfo.Param[i].ColorVal.a, (float)matAnmPtnInfo.Param[i + 1].ColorVal.a, t);
				this.rendererData.material.SetColor(this._Color, c);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
		}
	}

	// Token: 0x04007552 RID: 30034
	public bool Usage = true;

	// Token: 0x04007553 RID: 30035
	public Animation Anm;

	// Token: 0x04007554 RID: 30036
	public MatAnmPtnInfo[] PtnInfo;

	// Token: 0x04007555 RID: 30037
	private Renderer rendererData;

	// Token: 0x04007556 RID: 30038
	private int _Color;
}
