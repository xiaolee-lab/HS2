using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000650 RID: 1616
[RequireComponent(typeof(Text))]
[AddComponentMenu("UI/Effects/TypefaceAnimator")]
public class TypefaceAnimator : BaseMeshEffect
{
	// Token: 0x1700058B RID: 1419
	// (get) Token: 0x06002650 RID: 9808 RVA: 0x000DA35B File Offset: 0x000D875B
	// (set) Token: 0x06002651 RID: 9809 RVA: 0x000DA363 File Offset: 0x000D8763
	public float progress
	{
		get
		{
			return this.m_progress;
		}
		set
		{
			this.m_progress = value;
			if (base.graphic != null)
			{
				base.graphic.SetVerticesDirty();
			}
		}
	}

	// Token: 0x1700058C RID: 1420
	// (get) Token: 0x06002652 RID: 9810 RVA: 0x000DA388 File Offset: 0x000D8788
	public bool isPlaying
	{
		get
		{
			return this.m_isPlaying;
		}
	}

	// Token: 0x06002653 RID: 9811 RVA: 0x000DA390 File Offset: 0x000D8790
	protected override void OnEnable()
	{
		if (this.playOnAwake)
		{
			this.Play();
		}
		base.OnEnable();
	}

	// Token: 0x06002654 RID: 9812 RVA: 0x000DA3A9 File Offset: 0x000D87A9
	protected override void OnDisable()
	{
		this.Stop();
		base.OnDisable();
	}

	// Token: 0x06002655 RID: 9813 RVA: 0x000DA3B8 File Offset: 0x000D87B8
	public void Play()
	{
		this.progress = 0f;
		TypefaceAnimator.TimeMode timeMode = this.timeMode;
		if (timeMode != TypefaceAnimator.TimeMode.Time)
		{
			if (timeMode == TypefaceAnimator.TimeMode.Speed)
			{
				this.animationTime = (float)this.characterNumber / 10f / this.speed;
			}
		}
		else
		{
			this.animationTime = this.duration;
		}
		TypefaceAnimator.Style style = this.style;
		if (style != TypefaceAnimator.Style.Once)
		{
			if (style != TypefaceAnimator.Style.Loop)
			{
				if (style == TypefaceAnimator.Style.PingPong)
				{
					this.playCoroutine = base.StartCoroutine(this.PlayPingPongCoroutine());
				}
			}
			else
			{
				this.playCoroutine = base.StartCoroutine(this.PlayLoopCoroutine());
			}
		}
		else
		{
			this.playCoroutine = base.StartCoroutine(this.PlayOnceCoroutine());
		}
	}

	// Token: 0x06002656 RID: 9814 RVA: 0x000DA47E File Offset: 0x000D887E
	public void Stop()
	{
		if (this.playCoroutine != null)
		{
			base.StopCoroutine(this.playCoroutine);
		}
		this.m_isPlaying = false;
		this.playCoroutine = null;
	}

	// Token: 0x06002657 RID: 9815 RVA: 0x000DA4A8 File Offset: 0x000D88A8
	private IEnumerator PlayOnceCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.m_isPlaying)
		{
			yield break;
		}
		this.m_isPlaying = true;
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		while (this.progress < 1f)
		{
			this.progress += Time.deltaTime / this.animationTime;
			yield return null;
		}
		this.m_isPlaying = false;
		this.progress = 1f;
		if (this.onComplete != null)
		{
			this.onComplete.Invoke();
		}
		yield break;
	}

	// Token: 0x06002658 RID: 9816 RVA: 0x000DA4C4 File Offset: 0x000D88C4
	private IEnumerator PlayLoopCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.m_isPlaying)
		{
			yield break;
		}
		this.m_isPlaying = true;
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		for (;;)
		{
			this.progress += Time.deltaTime / this.animationTime;
			if (this.progress > 1f)
			{
				this.progress -= 1f;
			}
			yield return null;
		}
		yield break;
	}

	// Token: 0x06002659 RID: 9817 RVA: 0x000DA4E0 File Offset: 0x000D88E0
	private IEnumerator PlayPingPongCoroutine()
	{
		if (this.delay > 0f)
		{
			yield return new WaitForSeconds(this.delay);
		}
		if (this.m_isPlaying)
		{
			yield break;
		}
		this.m_isPlaying = true;
		if (this.onStart != null)
		{
			this.onStart.Invoke();
		}
		bool isPositive = true;
		for (;;)
		{
			float t = Time.deltaTime / this.animationTime;
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

	// Token: 0x0600265A RID: 9818 RVA: 0x000DA4FC File Offset: 0x000D88FC
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

	// Token: 0x0600265B RID: 9819 RVA: 0x000DA61D File Offset: 0x000D8A1D
	public void ModifyVertices(List<UIVertex> verts)
	{
		if (!this.IsActive())
		{
			return;
		}
		this.Modify(verts);
	}

	// Token: 0x0600265C RID: 9820 RVA: 0x000DA634 File Offset: 0x000D8A34
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
					float d = this.positionAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.positionSeparation));
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
						float num = this.scaleAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
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
						float num2 = this.scaleAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
						float d3 = (this.scaleTo - this.scaleFrom) * num2 + this.scaleFrom;
						float x2 = (value2.position.x - value4.position.x) * this.scalePivot.x + value4.position.x;
						float y2 = (value2.position.y - value4.position.y) * this.scalePivot.y + value4.position.y;
						Vector3 b3 = new Vector3(x2, y2, 0f);
						value.position = new Vector3(((value.position - b3) * d3 + b3).x, value.position.y, value.position.z);
						value2.position = new Vector3(((value2.position - b3) * d3 + b3).x, value2.position.y, value2.position.z);
						value3.position = new Vector3(((value3.position - b3) * d3 + b3).x, value3.position.y, value3.position.z);
						value4.position = new Vector3(((value4.position - b3) * d3 + b3).x, value4.position.y, value4.position.z);
						num2 = this.scaleAnimationCurveY.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.scaleSeparation));
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
					float num3 = this.rotationAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.rotationSeparation));
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
					float b5 = this.colorAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.colorSeparation));
					c = (this.colorTo - this.colorFrom) * b5 + this.colorFrom;
					value.color = (value2.color = (value3.color = (value4.color = c)));
				}
				if (this.useAlpha)
				{
					float num4 = this.alphaAnimationCurve.Evaluate(TypefaceAnimator.SeparationRate(this.progress, currentCharacterNumber, this.characterNumber, this.alphaSeparation));
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

	// Token: 0x0600265D RID: 9821 RVA: 0x000DAF25 File Offset: 0x000D9325
	private static float SeparationRate(float progress, int currentCharacterNumber, int characterNumber, float separation)
	{
		return Mathf.Clamp01((progress - (float)currentCharacterNumber * separation / (float)characterNumber) / (separation / (float)characterNumber + 1f - separation));
	}

	// Token: 0x040025FF RID: 9727
	public TypefaceAnimator.TimeMode timeMode;

	// Token: 0x04002600 RID: 9728
	public float duration = 1f;

	// Token: 0x04002601 RID: 9729
	public float speed = 5f;

	// Token: 0x04002602 RID: 9730
	public float delay;

	// Token: 0x04002603 RID: 9731
	public TypefaceAnimator.Style style;

	// Token: 0x04002604 RID: 9732
	public bool playOnAwake = true;

	// Token: 0x04002605 RID: 9733
	[SerializeField]
	private float m_progress = 1f;

	// Token: 0x04002606 RID: 9734
	public bool usePosition;

	// Token: 0x04002607 RID: 9735
	public bool useRotation;

	// Token: 0x04002608 RID: 9736
	public bool useScale;

	// Token: 0x04002609 RID: 9737
	public bool useAlpha;

	// Token: 0x0400260A RID: 9738
	public bool useColor;

	// Token: 0x0400260B RID: 9739
	public UnityEvent onStart;

	// Token: 0x0400260C RID: 9740
	public UnityEvent onComplete;

	// Token: 0x0400260D RID: 9741
	[SerializeField]
	private int characterNumber;

	// Token: 0x0400260E RID: 9742
	private float animationTime;

	// Token: 0x0400260F RID: 9743
	private Coroutine playCoroutine;

	// Token: 0x04002610 RID: 9744
	private bool m_isPlaying;

	// Token: 0x04002611 RID: 9745
	public Vector3 positionFrom = Vector3.zero;

	// Token: 0x04002612 RID: 9746
	public Vector3 positionTo = Vector3.zero;

	// Token: 0x04002613 RID: 9747
	public AnimationCurve positionAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002614 RID: 9748
	public float positionSeparation = 0.5f;

	// Token: 0x04002615 RID: 9749
	public float rotationFrom;

	// Token: 0x04002616 RID: 9750
	public float rotationTo;

	// Token: 0x04002617 RID: 9751
	public Vector2 rotationPivot = new Vector2(0.5f, 0.5f);

	// Token: 0x04002618 RID: 9752
	public AnimationCurve rotationAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002619 RID: 9753
	public float rotationSeparation = 0.5f;

	// Token: 0x0400261A RID: 9754
	public bool scaleSyncXY = true;

	// Token: 0x0400261B RID: 9755
	public float scaleFrom;

	// Token: 0x0400261C RID: 9756
	public float scaleTo = 1f;

	// Token: 0x0400261D RID: 9757
	public Vector2 scalePivot = new Vector2(0.5f, 0.5f);

	// Token: 0x0400261E RID: 9758
	public AnimationCurve scaleAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400261F RID: 9759
	public float scaleFromY;

	// Token: 0x04002620 RID: 9760
	public float scaleToY = 1f;

	// Token: 0x04002621 RID: 9761
	public Vector2 scalePivotY = new Vector2(0.5f, 0.5f);

	// Token: 0x04002622 RID: 9762
	public AnimationCurve scaleAnimationCurveY = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002623 RID: 9763
	public float scaleSeparation = 0.5f;

	// Token: 0x04002624 RID: 9764
	public float alphaFrom;

	// Token: 0x04002625 RID: 9765
	public float alphaTo = 1f;

	// Token: 0x04002626 RID: 9766
	public AnimationCurve alphaAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x04002627 RID: 9767
	public float alphaSeparation = 0.5f;

	// Token: 0x04002628 RID: 9768
	public Color colorFrom = Color.white;

	// Token: 0x04002629 RID: 9769
	public Color colorTo = Color.white;

	// Token: 0x0400262A RID: 9770
	public AnimationCurve colorAnimationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);

	// Token: 0x0400262B RID: 9771
	public float colorSeparation = 0.5f;

	// Token: 0x02000651 RID: 1617
	public enum TimeMode
	{
		// Token: 0x0400262D RID: 9773
		Time,
		// Token: 0x0400262E RID: 9774
		Speed
	}

	// Token: 0x02000652 RID: 1618
	public enum Style
	{
		// Token: 0x04002630 RID: 9776
		Once,
		// Token: 0x04002631 RID: 9777
		Loop,
		// Token: 0x04002632 RID: 9778
		PingPong
	}
}
