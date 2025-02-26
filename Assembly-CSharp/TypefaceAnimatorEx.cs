using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000653 RID: 1619
[RequireComponent(typeof(Text))]
[AddComponentMenu("UI/Effects/TypefaceAnimatorEx")]
public class TypefaceAnimatorEx : BaseMeshEffect
{
	// Token: 0x1700058D RID: 1421
	// (get) Token: 0x0600265F RID: 9823 RVA: 0x000DB5D6 File Offset: 0x000D99D6
	// (set) Token: 0x06002660 RID: 9824 RVA: 0x000DB5DE File Offset: 0x000D99DE
	public float progress
	{
		get
		{
			return this.m_progress;
		}
		set
		{
			this.m_progress = value;
			if (this != null && base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x1700058E RID: 1422
	// (get) Token: 0x06002661 RID: 9825 RVA: 0x000DB60F File Offset: 0x000D9A0F
	public bool isPlaying
	{
		get
		{
			return this.m_isPlaying;
		}
	}

	// Token: 0x06002662 RID: 9826 RVA: 0x000DB617 File Offset: 0x000D9A17
	private Coroutine StartCoroutineEx(IEnumerator routine)
	{
		return (!Singleton<Game>.IsInstance()) ? base.StartCoroutine(routine) : Singleton<Game>.Instance.StartCoroutine(routine);
	}

	// Token: 0x06002663 RID: 9827 RVA: 0x000DB63A File Offset: 0x000D9A3A
	private void StopCoroutineEx(IEnumerator routine)
	{
		if (Singleton<Game>.IsInstance())
		{
			Singleton<Game>.Instance.StopCoroutine(routine);
		}
		else
		{
			base.StopCoroutine(routine);
		}
	}

	// Token: 0x06002664 RID: 9828 RVA: 0x000DB65D File Offset: 0x000D9A5D
	protected override void OnEnable()
	{
		if (this.playOnAwake)
		{
			this.Play();
		}
		else if (this.played != null)
		{
			this.StartCoroutineEx(this.played);
		}
		base.OnEnable();
	}

	// Token: 0x06002665 RID: 9829 RVA: 0x000DB693 File Offset: 0x000D9A93
	protected override void OnDisable()
	{
		this.Stop();
		base.OnDisable();
	}

	// Token: 0x06002666 RID: 9830 RVA: 0x000DB6A4 File Offset: 0x000D9AA4
	private IEnumerator WaitPlay()
	{
		this.progress = 0f;
		this.m_isPlaying = true;
		if (this.played != null)
		{
			this.StopCoroutineEx(this.played);
		}
		if (this.timeMode == TypefaceAnimatorEx.TimeMode.Speed)
		{
			yield return null;
		}
		this.animationTime = delegate()
		{
			TypefaceAnimatorEx.TimeMode timeMode = this.timeMode;
			if (timeMode == TypefaceAnimatorEx.TimeMode.Time)
			{
				return this.duration;
			}
			if (timeMode != TypefaceAnimatorEx.TimeMode.Speed)
			{
				return 1f;
			}
			return (float)this.characterNumber / this.speed;
		};
		TypefaceAnimatorEx.Style style = this.style;
		if (style != TypefaceAnimatorEx.Style.Once)
		{
			if (style != TypefaceAnimatorEx.Style.Loop)
			{
				if (style == TypefaceAnimatorEx.Style.PingPong)
				{
					this.played = this.PlayPingPongCoroutine();
				}
			}
			else
			{
				this.played = this.PlayLoopCoroutine();
			}
		}
		else
		{
			this.played = this.PlayOnceCoroutine();
		}
		this.StartCoroutineEx(this.played);
		if (this == null)
		{
			yield break;
		}
		if (!base.isActiveAndEnabled)
		{
			this.Stop();
		}
		yield break;
	}

	// Token: 0x06002667 RID: 9831 RVA: 0x000DB6BF File Offset: 0x000D9ABF
	public void Play()
	{
		this.StartCoroutineEx(this.WaitPlay());
	}

	// Token: 0x06002668 RID: 9832 RVA: 0x000DB6CE File Offset: 0x000D9ACE
	public void Stop()
	{
		if (this.played != null)
		{
			this.StopCoroutineEx(this.played);
		}
	}

	// Token: 0x06002669 RID: 9833 RVA: 0x000DB6E8 File Offset: 0x000D9AE8
	private IEnumerator PlayOnceCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		if (!this.isNoWait)
		{
			while (this.progress < 1f)
			{
				this.progress += ((this.timeScale != TypefaceAnimatorEx.TimeScale.DeltaTime) ? Time.unscaledDeltaTime : Time.deltaTime) / this.animationTime();
				yield return null;
			}
		}
		this.m_isPlaying = false;
		this.progress = 1f;
		if (this.onComplete != null)
		{
			this.onComplete.Invoke();
		}
		this.played = null;
		yield break;
	}

	// Token: 0x0600266A RID: 9834 RVA: 0x000DB704 File Offset: 0x000D9B04
	private IEnumerator PlayLoopCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		for (;;)
		{
			this.progress += ((this.timeScale != TypefaceAnimatorEx.TimeScale.DeltaTime) ? Time.unscaledDeltaTime : Time.deltaTime) / this.animationTime();
			if (!this.isNoWait)
			{
				if (this.progress > 1f)
				{
					this.progress %= 1f;
				}
			}
			else
			{
				this.progress = 1f;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600266B RID: 9835 RVA: 0x000DB720 File Offset: 0x000D9B20
	private IEnumerator PlayPingPongCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		bool isPositive = true;
		for (;;)
		{
			float t = ((this.timeScale != TypefaceAnimatorEx.TimeScale.DeltaTime) ? Time.unscaledDeltaTime : Time.deltaTime) / this.animationTime();
			if (isPositive)
			{
				this.progress += t;
				if (this.progress > 1f)
				{
					isPositive = false;
					this.progress -= t;
				}
			}
			else
			{
				this.progress -= t;
				if (this.progress < 0f)
				{
					isPositive = true;
					this.progress += t;
				}
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600266C RID: 9836 RVA: 0x000DB73C File Offset: 0x000D9B3C
	public override void ModifyMesh(VertexHelper vertexHelper)
	{
		if (!this.IsActive() || vertexHelper.currentVertCount == 0)
		{
			return;
		}
		List<UIVertex> list = new List<UIVertex>();
		vertexHelper.GetUIVertexStream(list);
		List<UIVertex> list2 = new List<UIVertex>();
		for (int i = 0; i < list.Count; i++)
		{
			int num = i % 6;
			if (num == 0 || num == 1 || num == 2 || num == 4)
			{
				list2.Add(list[i]);
			}
		}
		this.ModifyVertices(list2);
		List<UIVertex> list3 = new List<UIVertex>(list.Count);
		for (int j = 0; j < list.Count / 6; j++)
		{
			int num2 = j * 4;
			list3.Add(list2[num2]);
			list3.Add(list2[num2 + 1]);
			list3.Add(list2[num2 + 2]);
			list3.Add(list2[num2 + 2]);
			list3.Add(list2[num2 + 3]);
			list3.Add(list2[num2]);
		}
		vertexHelper.Clear();
		vertexHelper.AddUIVertexTriangleStream(list3);
	}

	// Token: 0x0600266D RID: 9837 RVA: 0x000DB85D File Offset: 0x000D9C5D
	public void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive())
		{
			return;
		}
		this.Modify(verts);
	}

	// Token: 0x0600266E RID: 9838 RVA: 0x000DB874 File Offset: 0x000D9C74
	private void Modify(List<UIVertex> verts)
	{
		this.characterNumber = verts.Count / 4;
		for (int i = 0; i < verts.Count; i++)
		{
			if (i % 4 == 0)
			{
				int currentCharacterNumber = i / 4;
				UIVertex value = verts[i];
				UIVertex value2 = verts[i + 1];
				UIVertex value3 = verts[i + 2];
				UIVertex value4 = verts[i + 3];
				if (this.usePosition)
				{
					float d = this.positionAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.positionSeparation));
					Vector3 b = (this.positionTo - this.positionFrom) * d + this.positionFrom;
					value.position += b;
					value2.position += b;
					value3.position += b;
					value4.position += b;
				}
				if (this.useScale)
				{
					if (this.scaleSyncXY)
					{
						float num = this.scaleAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
						float d2 = (this.scaleTo - this.scaleFrom) * num + this.scaleFrom;
						float x = (value2.position.x - value4.position.x) * this.scalePivot.x + value4.position.x;
						float y = (value2.position.y - value4.position.y) * this.scalePivot.y + value4.position.y;
						Vector3 b2 = new Vector3(x, y, 0f);
						value.position = (value.position - b2) * d2 + b2;
						value2.position = (value2.position - b2) * d2 + b2;
						value3.position = (value3.position - b2) * d2 + b2;
						value4.position = (value4.position - b2) * d2 + b2;
					}
					else
					{
						float num2 = this.scaleAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
						float d3 = (this.scaleTo - this.scaleFrom) * num2 + this.scaleFrom;
						float x2 = (value2.position.x - value4.position.x) * this.scalePivot.x + value4.position.x;
						float y2 = (value2.position.y - value4.position.y) * this.scalePivot.y + value4.position.y;
						Vector3 b3 = new Vector3(x2, y2, 0f);
						value.position = new Vector3(((value.position - b3) * d3 + b3).x, value.position.y, value.position.z);
						value2.position = new Vector3(((value2.position - b3) * d3 + b3).x, value2.position.y, value2.position.z);
						value3.position = new Vector3(((value3.position - b3) * d3 + b3).x, value3.position.y, value3.position.z);
						value4.position = new Vector3(((value4.position - b3) * d3 + b3).x, value4.position.y, value4.position.z);
						num2 = this.scaleAnimationCurveY.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
						d3 = (this.scaleToY - this.scaleFromY) * num2 + this.scaleFromY;
						x2 = (value2.position.x - value4.position.x) * this.scalePivotY.x + value4.position.x;
						y2 = (value2.position.y - value4.position.y) * this.scalePivotY.y + value4.position.y;
						b3 = new Vector3(x2, y2, 0f);
						value.position = new Vector3(value.position.x, ((value.position - b3) * d3 + b3).y, value.position.z);
						value2.position = new Vector3(value2.position.x, ((value2.position - b3) * d3 + b3).y, value2.position.z);
						value3.position = new Vector3(value3.position.x, ((value3.position - b3) * d3 + b3).y, value3.position.z);
						value4.position = new Vector3(value4.position.x, ((value4.position - b3) * d3 + b3).y, value4.position.z);
					}
				}
				if (this.useRotation)
				{
					float num3 = this.rotationAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.rotationSeparation));
					float angle = (this.rotationTo - this.rotationFrom) * num3 + this.rotationFrom;
					float x3 = (value2.position.x - value4.position.x) * this.rotationPivot.x + value4.position.x;
					float y3 = (value2.position.y - value4.position.y) * this.rotationPivot.y + value4.position.y;
					Vector3 b4 = new Vector3(x3, y3, 0f);
					value.position = Quaternion.AngleAxis(angle, Vector3.forward) * (value.position - b4) + b4;
					value2.position = Quaternion.AngleAxis(angle, Vector3.forward) * (value2.position - b4) + b4;
					value3.position = Quaternion.AngleAxis(angle, Vector3.forward) * (value3.position - b4) + b4;
					value4.position = Quaternion.AngleAxis(angle, Vector3.forward) * (value4.position - b4) + b4;
				}
				Color c = value.color;
				if (this.useColor)
				{
					float b5 = this.colorAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.colorSeparation));
					c = (this.colorTo - this.colorFrom) * b5 + this.colorFrom;
					value.color = (value2.color = (value3.color = (value4.color = c)));
				}
				if (this.useAlpha)
				{
					float num4 = this.alphaAnimationCurve.Evaluate(TypefaceAnimatorEx.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.alphaSeparation));
					float num5 = (this.alphaTo - this.alphaFrom) * num4 + this.alphaFrom;
					c = new Color(c.r, c.g, c.b, c.a * num5);
					value.color = (value2.color = (value3.color = (value4.color = c)));
				}
				verts[i] = value;
				verts[i + 1] = value2;
				verts[i + 2] = value3;
				verts[i + 3] = value4;
			}
		}
	}

	// Token: 0x0600266F RID: 9839 RVA: 0x000DC165 File Offset: 0x000DA565
	private static float SeparationRate(float progress, int currentCharacterNumber, int characterNumber, float separation)
	{
		return Mathf.Clamp01((progress - (float)currentCharacterNumber * separation / (float)characterNumber) / (separation / (float)characterNumber + 1f - separation));
	}

	// Token: 0x04002633 RID: 9779
	public bool isNoWait;

	// Token: 0x04002634 RID: 9780
	public TypefaceAnimatorEx.TimeMode timeMode;

	// Token: 0x04002635 RID: 9781
	public TypefaceAnimatorEx.TimeScale timeScale;

	// Token: 0x04002636 RID: 9782
	public float duration = 1f;

	// Token: 0x04002637 RID: 9783
	public float speed = 5f;

	// Token: 0x04002638 RID: 9784
	public float delay;

	// Token: 0x04002639 RID: 9785
	public TypefaceAnimatorEx.Style style;

	// Token: 0x0400263A RID: 9786
	public bool playOnAwake = true;

	// Token: 0x0400263B RID: 9787
	[SerializeField]
	private float m_progress = 1f;

	// Token: 0x0400263C RID: 9788
	public bool usePosition;

	// Token: 0x0400263D RID: 9789
	public bool useRotation;

	// Token: 0x0400263E RID: 9790
	public bool useScale;

	// Token: 0x0400263F RID: 9791
	public bool useAlpha;

	// Token: 0x04002640 RID: 9792
	public bool useColor;

	// Token: 0x04002641 RID: 9793
	public UnityEvent onStart;

	// Token: 0x04002642 RID: 9794
	public UnityEvent onComplete;

	// Token: 0x04002643 RID: 9795
	[SerializeField]
	private int characterNumber;

	// Token: 0x04002644 RID: 9796
	private Func<float> animationTime = () => 0f;

	// Token: 0x04002645 RID: 9797
	private bool m_isPlaying;

	// Token: 0x04002646 RID: 9798
	public Vector3 positionFrom = Vector3.zero;

	// Token: 0x04002647 RID: 9799
	public Vector3 positionTo = Vector3.zero;

	// Token: 0x04002648 RID: 9800
	public AnimationCurve positionAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002649 RID: 9801
	public float positionSeparation = 0.5f;

	// Token: 0x0400264A RID: 9802
	public float rotationFrom;

	// Token: 0x0400264B RID: 9803
	public float rotationTo;

	// Token: 0x0400264C RID: 9804
	public Vector2 rotationPivot = new Vector2(0.5f, 0.5f);

	// Token: 0x0400264D RID: 9805
	public AnimationCurve rotationAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400264E RID: 9806
	public float rotationSeparation = 0.5f;

	// Token: 0x0400264F RID: 9807
	public bool scaleSyncXY = true;

	// Token: 0x04002650 RID: 9808
	public float scaleFrom;

	// Token: 0x04002651 RID: 9809
	public float scaleTo = 1f;

	// Token: 0x04002652 RID: 9810
	public Vector2 scalePivot = new Vector2(0.5f, 0.5f);

	// Token: 0x04002653 RID: 9811
	public AnimationCurve scaleAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002654 RID: 9812
	public float scaleFromY;

	// Token: 0x04002655 RID: 9813
	public float scaleToY = 1f;

	// Token: 0x04002656 RID: 9814
	public Vector2 scalePivotY = new Vector2(0.5f, 0.5f);

	// Token: 0x04002657 RID: 9815
	public AnimationCurve scaleAnimationCurveY = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002658 RID: 9816
	public float scaleSeparation = 0.5f;

	// Token: 0x04002659 RID: 9817
	public float alphaFrom;

	// Token: 0x0400265A RID: 9818
	public float alphaTo = 1f;

	// Token: 0x0400265B RID: 9819
	public AnimationCurve alphaAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400265C RID: 9820
	public float alphaSeparation = 0.5f;

	// Token: 0x0400265D RID: 9821
	public Color colorFrom = Color.white;

	// Token: 0x0400265E RID: 9822
	public Color colorTo = Color.white;

	// Token: 0x0400265F RID: 9823
	public AnimationCurve colorAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002660 RID: 9824
	public float colorSeparation = 0.5f;

	// Token: 0x04002661 RID: 9825
	private IEnumerator played;

	// Token: 0x02000654 RID: 1620
	public enum TimeMode
	{
		// Token: 0x04002664 RID: 9828
		Time,
		// Token: 0x04002665 RID: 9829
		Speed
	}

	// Token: 0x02000655 RID: 1621
	public enum TimeScale
	{
		// Token: 0x04002667 RID: 9831
		DeltaTime,
		// Token: 0x04002668 RID: 9832
		RealTime
	}

	// Token: 0x02000656 RID: 1622
	public enum Style
	{
		// Token: 0x0400266A RID: 9834
		Once,
		// Token: 0x0400266B RID: 9835
		Loop,
		// Token: 0x0400266C RID: 9836
		PingPong
	}
}
