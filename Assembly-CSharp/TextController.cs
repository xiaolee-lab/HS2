using System;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000798 RID: 1944
public class TextController : MonoBehaviour
{
	// Token: 0x170007DC RID: 2012
	// (get) Token: 0x06002DFE RID: 11774 RVA: 0x001047F9 File Offset: 0x00102BF9
	// (set) Token: 0x06002DFF RID: 11775 RVA: 0x00104801 File Offset: 0x00102C01
	public bool initialized { get; private set; }

	// Token: 0x170007DD RID: 2013
	// (get) Token: 0x06002E00 RID: 11776 RVA: 0x0010480A File Offset: 0x00102C0A
	public bool IsCompleteDisplayText
	{
		get
		{
			return this.isMovie ? (this.movieProgress >= 1f) : (!this.TA.isPlaying);
		}
	}

	// Token: 0x170007DE RID: 2014
	// (get) Token: 0x06002E01 RID: 11777 RVA: 0x0010483A File Offset: 0x00102C3A
	// (set) Token: 0x06002E02 RID: 11778 RVA: 0x00104850 File Offset: 0x00102C50
	public bool NameMessageVisible
	{
		get
		{
			return this.nameVisible && this.messageVisible;
		}
		set
		{
			this.nameVisible = value;
			this.messageVisible = value;
		}
	}

	// Token: 0x170007DF RID: 2015
	// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00104860 File Offset: 0x00102C60
	// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00104884 File Offset: 0x00102C84
	public bool nameVisible
	{
		get
		{
			return !(this.nameWindow == null) && this.nameWindow.enabled;
		}
		set
		{
			this.nameWindow.SafeProc(delegate(Text p)
			{
				p.enabled = value;
			});
		}
	}

	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x06002E05 RID: 11781 RVA: 0x001048B6 File Offset: 0x00102CB6
	// (set) Token: 0x06002E06 RID: 11782 RVA: 0x001048DC File Offset: 0x00102CDC
	public bool messageVisible
	{
		get
		{
			return !(this.messageWindow == null) && this.messageWindow.enabled;
		}
		set
		{
			this.messageWindow.SafeProc(delegate(Text p)
			{
				p.enabled = value;
			});
		}
	}

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x06002E07 RID: 11783 RVA: 0x0010490E File Offset: 0x00102D0E
	public Text NameWindow
	{
		get
		{
			return this.nameWindow;
		}
	}

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x06002E08 RID: 11784 RVA: 0x00104916 File Offset: 0x00102D16
	public Text MessageWindow
	{
		get
		{
			return this.messageWindow;
		}
	}

	// Token: 0x170007E3 RID: 2019
	// (get) Token: 0x06002E09 RID: 11785 RVA: 0x0010491E File Offset: 0x00102D1E
	// (set) Token: 0x06002E0A RID: 11786 RVA: 0x0010492B File Offset: 0x00102D2B
	public int FontSpeed
	{
		get
		{
			return this._fontSpeed.Value;
		}
		set
		{
			this._fontSpeed.Value = Mathf.Clamp(value, 1, 100);
		}
	}

	// Token: 0x170007E4 RID: 2020
	// (get) Token: 0x06002E0B RID: 11787 RVA: 0x00104941 File Offset: 0x00102D41
	// (set) Token: 0x06002E0C RID: 11788 RVA: 0x0010494E File Offset: 0x00102D4E
	public Color FontColor
	{
		get
		{
			return this._fontColor.Value;
		}
		set
		{
			this._fontColor.Value = value;
		}
	}

	// Token: 0x06002E0D RID: 11789 RVA: 0x0010495C File Offset: 0x00102D5C
	public void Change(Text nameWindow, Text messageWindow)
	{
		this.Clear();
		this.nameWindow = nameWindow;
		this.messageWindow = messageWindow;
		UnityEngine.Object.Destroy(this.hypJpn);
		this.Initialize();
	}

	// Token: 0x06002E0E RID: 11790 RVA: 0x00104984 File Offset: 0x00102D84
	public void Clear()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
		this.nameWindow.SafeProc(delegate(Text p)
		{
			p.text = string.Empty;
		});
		this.messageWindow.text = string.Empty;
		if (this.hypJpn != null)
		{
			this.hypJpn.SetText(string.Empty);
		}
		this.TA.Stop();
		this.TA.progress = 0f;
		this.movieProgress = 0f;
	}

	// Token: 0x06002E0F RID: 11791 RVA: 0x00104A24 File Offset: 0x00102E24
	public void Set(string nameText, string messageText)
	{
		this.nameWindow.SafeProc(delegate(Text p)
		{
			p.text = nameText;
		});
		this.messageWindow.text = messageText;
		if (this.hypJpn != null)
		{
			this.hypJpn.SetText(this.messageWindow.text);
		}
		this.TA.Play();
		this.movieProgress = 0f;
	}

	// Token: 0x06002E10 RID: 11792 RVA: 0x00104A9F File Offset: 0x00102E9F
	public void ForceCompleteDisplayText()
	{
		this.TA.progress = 1f;
	}

	// Token: 0x06002E11 RID: 11793 RVA: 0x00104AB4 File Offset: 0x00102EB4
	public void Initialize()
	{
		this.hypJpn = this.messageWindow.GetComponent<HyphenationJpn>();
		bool flag = false;
		if (flag)
		{
			UnityEngine.Object.Destroy(this.hypJpn);
			this.hypJpn = null;
		}
		else if (this.hypJpn == null)
		{
			this.hypJpn = this.messageWindow.gameObject.AddComponent<HyphenationJpn>();
		}
		if (this.hypJpn != null)
		{
			this.hypJpn.SetText(this.messageWindow);
		}
		this.TA = this.messageWindow.GetComponent<TypefaceAnimatorEx>();
		this.initialized = true;
	}

	// Token: 0x06002E12 RID: 11794 RVA: 0x00104B52 File Offset: 0x00102F52
	private void Awake()
	{
		if (!this.initialized)
		{
			this.Initialize();
		}
	}

	// Token: 0x06002E13 RID: 11795 RVA: 0x00104B68 File Offset: 0x00102F68
	private void Start()
	{
		this._fontSpeed.TakeUntilDestroy(this).Subscribe(delegate(int value)
		{
			this.TA.isNoWait = (value == 100);
			if (!this.TA.isNoWait)
			{
				this.TA.timeMode = TypefaceAnimatorEx.TimeMode.Speed;
				this.TA.speed = (float)value;
			}
		});
		this._fontColor.TakeUntilDestroy(this).Subscribe(delegate(Color color)
		{
			this.nameWindow.SafeProc(delegate(Text p)
			{
				p.color = color;
			});
			this.messageWindow.SafeProc(delegate(Text p)
			{
				p.color = color;
			});
		});
		(from _ in this.UpdateAsObservable()
		where base.enabled
		where this.isMovie
		select _).Subscribe(delegate(Unit _)
		{
			this.movieProgress = Mathf.Min(this.movieProgress + Time.deltaTime / this.movieFontSpeed, 1f);
		});
	}

	// Token: 0x04002D00 RID: 11520
	[SerializeField]
	private Text nameWindow;

	// Token: 0x04002D01 RID: 11521
	[SerializeField]
	private Text messageWindow;

	// Token: 0x04002D02 RID: 11522
	private const int MAX_FONT_SPEED = 100;

	// Token: 0x04002D03 RID: 11523
	[SerializeField]
	[RangeReactiveProperty(1f, 100f)]
	private IntReactiveProperty _fontSpeed = new IntReactiveProperty(100);

	// Token: 0x04002D04 RID: 11524
	private ColorReactiveProperty _fontColor = new ColorReactiveProperty(Color.white);

	// Token: 0x04002D05 RID: 11525
	private TypefaceAnimatorEx TA;

	// Token: 0x04002D06 RID: 11526
	private HyphenationJpn hypJpn;

	// Token: 0x04002D07 RID: 11527
	public bool isMovie;

	// Token: 0x04002D08 RID: 11528
	[SerializeField]
	private float movieFontSpeed = 1f;

	// Token: 0x04002D09 RID: 11529
	[SerializeField]
	[Range(0f, 1f)]
	private float movieProgress;
}
