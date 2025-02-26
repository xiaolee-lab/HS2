using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000621 RID: 1569
public class bl_SelectableText : MonoBehaviour
{
	// Token: 0x06002577 RID: 9591 RVA: 0x000D69A8 File Offset: 0x000D4DA8
	private void Awake()
	{
		if (base.GetComponent<Text>() != null)
		{
			this.m_Text = base.GetComponent<Text>();
			this.defaultColor = this.m_Text.color;
		}
		if (base.GetComponent<Button>() != null)
		{
			this.m_Button = base.GetComponent<Button>();
			this.defaultColorBlock = this.m_Button.colors;
			this.OnSelectColorBlock = this.defaultColorBlock;
			this.OnSelectColorBlock.normalColor = this.OnEnterColor;
		}
	}

	// Token: 0x06002578 RID: 9592 RVA: 0x000D6A30 File Offset: 0x000D4E30
	public void OnEnter()
	{
		if (this.m_Text != null)
		{
			this.m_Text.CrossFadeColor(this.OnEnterColor, this.Duration, true, true);
		}
		if (this.m_Button != null)
		{
			this.m_Button.colors = this.OnSelectColorBlock;
		}
	}

	// Token: 0x06002579 RID: 9593 RVA: 0x000D6A8C File Offset: 0x000D4E8C
	public void OnExit()
	{
		if (this.m_Text != null)
		{
			this.m_Text.CrossFadeColor(this.defaultColor, this.Duration, true, true);
		}
		if (this.m_Button != null)
		{
			this.m_Button.colors = this.defaultColorBlock;
		}
	}

	// Token: 0x04002534 RID: 9524
	[SerializeField]
	private Color OnEnterColor = new Color(1f, 1f, 1f, 1f);

	// Token: 0x04002535 RID: 9525
	[SerializeField]
	[Range(0.1f, 3f)]
	private float Duration = 1f;

	// Token: 0x04002536 RID: 9526
	private Text m_Text;

	// Token: 0x04002537 RID: 9527
	private Button m_Button;

	// Token: 0x04002538 RID: 9528
	private Color defaultColor;

	// Token: 0x04002539 RID: 9529
	private ColorBlock defaultColorBlock;

	// Token: 0x0400253A RID: 9530
	private ColorBlock OnSelectColorBlock;
}
