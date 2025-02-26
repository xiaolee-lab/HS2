using System;
using System.Collections;
using PlayfulSystems;
using PlayfulSystems.ProgressBar;
using UnityEngine;

// Token: 0x0200064B RID: 1611
[ExecuteInEditMode]
public class ProgressBarPro : MonoBehaviour
{
	// Token: 0x06002633 RID: 9779 RVA: 0x000D957F File Offset: 0x000D797F
	public void Start()
	{
		if (this.views == null || this.views.Length == 0)
		{
			this.views = base.GetComponentsInChildren<ProgressBarProView>();
		}
	}

	// Token: 0x06002634 RID: 9780 RVA: 0x000D95A5 File Offset: 0x000D79A5
	private void OnEnable()
	{
		this.SetDisplayValue(this.m_value, true);
	}

	// Token: 0x1700058A RID: 1418
	// (get) Token: 0x06002635 RID: 9781 RVA: 0x000D95B4 File Offset: 0x000D79B4
	// (set) Token: 0x06002636 RID: 9782 RVA: 0x000D95BC File Offset: 0x000D79BC
	public float Value
	{
		get
		{
			return this.m_value;
		}
		set
		{
			if (value == this.m_value)
			{
				return;
			}
			this.SetValue(value, false);
		}
	}

	// Token: 0x06002637 RID: 9783 RVA: 0x000D95D3 File Offset: 0x000D79D3
	public void SetValue(float value, float maxValue)
	{
		if (maxValue != 0f)
		{
			this.SetValue(value / maxValue, false);
		}
		else
		{
			this.SetValue(0f, false);
		}
	}

	// Token: 0x06002638 RID: 9784 RVA: 0x000D95FB File Offset: 0x000D79FB
	public void SetValue(int value, int maxValue)
	{
		if (maxValue != 0)
		{
			this.SetValue((float)value / (float)maxValue, false);
		}
		else
		{
			this.SetValue(0f, false);
		}
	}

	// Token: 0x06002639 RID: 9785 RVA: 0x000D9620 File Offset: 0x000D7A20
	public void SetValue(float percentage, bool forceUpdate = false)
	{
		if (!forceUpdate && Mathf.Approximately(this.m_value, percentage))
		{
			return;
		}
		this.m_value = Mathf.Clamp01(percentage);
		for (int i = 0; i < this.views.Length; i++)
		{
			this.views[i].NewChangeStarted(this.displayValue, this.m_value);
		}
		if (this.animateBar && Application.isPlaying && base.gameObject.activeInHierarchy)
		{
			this.StartSizeAnim(percentage);
		}
		else
		{
			this.SetDisplayValue(percentage, false);
		}
	}

	// Token: 0x0600263A RID: 9786 RVA: 0x000D96BC File Offset: 0x000D7ABC
	public bool IsAnimating()
	{
		return this.animateBar && !Mathf.Approximately(this.displayValue, this.m_value);
	}

	// Token: 0x0600263B RID: 9787 RVA: 0x000D96E0 File Offset: 0x000D7AE0
	public void SetBarColor(Color color)
	{
		for (int i = 0; i < this.views.Length; i++)
		{
			this.views[i].SetBarColor(color);
		}
	}

	// Token: 0x0600263C RID: 9788 RVA: 0x000D9714 File Offset: 0x000D7B14
	private void StartSizeAnim(float percentage)
	{
		if (this.sizeAnim != null)
		{
			base.StopCoroutine(this.sizeAnim);
		}
		this.sizeAnim = base.StartCoroutine(this.DoBarSizeAnim());
	}

	// Token: 0x0600263D RID: 9789 RVA: 0x000D9740 File Offset: 0x000D7B40
	private IEnumerator DoBarSizeAnim()
	{
		float startValue = this.displayValue;
		float time = 0f;
		float change = this.m_value - this.displayValue;
		float duration = (this.animationType != ProgressBarPro.AnimationType.FixedTimeForChange) ? (Mathf.Abs(change) / this.animTime) : this.animTime;
		while (time < duration)
		{
			time += Time.deltaTime;
			this.SetDisplayValue(PlayfulSystems.Utils.EaseSinInOut(time / duration, startValue, change), false);
			yield return null;
		}
		this.SetDisplayValue(this.m_value, true);
		this.sizeAnim = null;
		yield break;
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x000D975C File Offset: 0x000D7B5C
	private void SetDisplayValue(float value, bool forceUpdate = false)
	{
		if (!forceUpdate && this.displayValue >= 0f && Mathf.Approximately(this.displayValue, value))
		{
			return;
		}
		this.displayValue = value;
		this.UpdateBarViews(this.displayValue, this.m_value, forceUpdate);
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x000D97AC File Offset: 0x000D7BAC
	private void UpdateBarViews(float currentValue, float targetValue, bool forceUpdate = false)
	{
		if (this.views != null)
		{
			for (int i = 0; i < this.views.Length; i++)
			{
				if (this.views[i] != null && (forceUpdate || this.views[i].CanUpdateView(currentValue, targetValue)))
				{
					this.views[i].UpdateView(currentValue, targetValue);
				}
			}
		}
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x000D9819 File Offset: 0x000D7C19
	private void OnDidApplyAnimationProperties()
	{
		this.SetValue(this.m_value, true);
	}

	// Token: 0x040025F2 RID: 9714
	[SerializeField]
	[Range(0f, 1f)]
	private float m_value = 1f;

	// Token: 0x040025F3 RID: 9715
	private float displayValue = -1f;

	// Token: 0x040025F4 RID: 9716
	[Space(10f)]
	[Tooltip("Smoothes out the animation of the bar.")]
	[SerializeField]
	private bool animateBar = true;

	// Token: 0x040025F5 RID: 9717
	[SerializeField]
	private ProgressBarPro.AnimationType animationType;

	// Token: 0x040025F6 RID: 9718
	[SerializeField]
	private float animTime = 0.25f;

	// Token: 0x040025F7 RID: 9719
	[Space(10f)]
	[SerializeField]
	private ProgressBarProView[] views;

	// Token: 0x040025F8 RID: 9720
	private Coroutine sizeAnim;

	// Token: 0x0200064C RID: 1612
	public enum AnimationType
	{
		// Token: 0x040025FA RID: 9722
		FixedTimeForChange,
		// Token: 0x040025FB RID: 9723
		ChangeSpeed
	}
}
