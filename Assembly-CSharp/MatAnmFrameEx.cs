using System;
using System.Linq;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x0200110B RID: 4363
public class MatAnmFrameEx : MonoBehaviour
{
	// Token: 0x060090CF RID: 37071 RVA: 0x003C476F File Offset: 0x003C2B6F
	private void Awake()
	{
		this._Color = Shader.PropertyToID("_Color");
	}

	// Token: 0x060090D0 RID: 37072 RVA: 0x003C4781 File Offset: 0x003C2B81
	private void Start()
	{
		this.rendererData = base.GetComponent<Renderer>();
		if (null == this.rendererData)
		{
			base.enabled = false;
		}
	}

	// Token: 0x060090D1 RID: 37073 RVA: 0x003C47A8 File Offset: 0x003C2BA8
	private void Update()
	{
		if (!this.Usage)
		{
			return;
		}
		AnimationClip playingClip = this.Anm.GetPlayingClip();
		int num = -1;
		foreach (var <>__AnonType in this.PtnInfo.Select((MatAnmPtnInfoEx value, int index) => new
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
		MatAnmPtnInfoEx matAnmPtnInfoEx = this.PtnInfo[num];
		AnimationState animationState = this.Anm[playingClip.name];
		float num2 = animationState.time;
		while (playingClip.length < num2)
		{
			num2 -= playingClip.length;
		}
		float time = Mathf.InverseLerp(0f, playingClip.length, num2);
		Color value2;
		value2.r = matAnmPtnInfoEx.Value.Evaluate(time).r;
		value2.g = matAnmPtnInfoEx.Value.Evaluate(time).g;
		value2.b = matAnmPtnInfoEx.Value.Evaluate(time).b;
		value2.a = matAnmPtnInfoEx.Value.Evaluate(time).a;
		this.rendererData.material.SetColor(this._Color, value2);
	}

	// Token: 0x0400755A RID: 30042
	public bool Usage = true;

	// Token: 0x0400755B RID: 30043
	public Animation Anm;

	// Token: 0x0400755C RID: 30044
	public MatAnmPtnInfoEx[] PtnInfo;

	// Token: 0x0400755D RID: 30045
	private Renderer rendererData;

	// Token: 0x0400755E RID: 30046
	private int _Color;
}
