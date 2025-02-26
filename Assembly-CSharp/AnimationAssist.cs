using System;
using System.Collections;
using UnityEngine;

// Token: 0x020010AB RID: 4267
public class AnimationAssist
{
	// Token: 0x06008E63 RID: 36451 RVA: 0x003B41D0 File Offset: 0x003B25D0
	public AnimationAssist(Animation _animation)
	{
		this.animation = _animation;
		int num = 0;
		this.data = new AnimationAssist.ANMCTRLST(_animation.GetClipCount());
		IEnumerator enumerator = this.animation.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				AnimationState state = (AnimationState)obj;
				this.data.info[num++] = new AnimationAssist.ANMCTRLINFOST(state);
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x17001EE9 RID: 7913
	// (get) Token: 0x06008E64 RID: 36452 RVA: 0x003B4268 File Offset: 0x003B2668
	public Animation NowAnimation
	{
		get
		{
			return this.animation;
		}
	}

	// Token: 0x17001EEA RID: 7914
	// (get) Token: 0x06008E65 RID: 36453 RVA: 0x003B4270 File Offset: 0x003B2670
	public AnimationState NowAnimationState
	{
		get
		{
			return this.animation[this.GetID(this.data.NowPtn)];
		}
	}

	// Token: 0x06008E66 RID: 36454 RVA: 0x003B428E File Offset: 0x003B268E
	public string GetID(int id)
	{
		if ((ulong)id >= (ulong)((long)this.data.info.Length))
		{
			return string.Empty;
		}
		return this.data.info[id].Name;
	}

	// Token: 0x17001EEB RID: 7915
	// (get) Token: 0x06008E67 RID: 36455 RVA: 0x003B42BD File Offset: 0x003B26BD
	public AnimationAssist.ANMCTRLST Data
	{
		get
		{
			return this.data;
		}
	}

	// Token: 0x06008E68 RID: 36456 RVA: 0x003B42C8 File Offset: 0x003B26C8
	public bool IsAnimeEnd()
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		bool flag = nowAnimationState.wrapMode == WrapMode.ClampForever;
		flag |= (nowAnimationState.wrapMode == WrapMode.Loop);
		flag |= (nowAnimationState.wrapMode == WrapMode.PingPong);
		if (flag)
		{
			if (this.GetInfo(-1).isReverse)
			{
				if (nowAnimationState.time <= 0f)
				{
					return true;
				}
			}
			else if (nowAnimationState.time >= nowAnimationState.length)
			{
				return true;
			}
		}
		return !this.animation.isPlaying;
	}

	// Token: 0x06008E69 RID: 36457 RVA: 0x003B4350 File Offset: 0x003B2750
	public void Update()
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(-1);
		if (this.data.msTime != -1f)
		{
			if (Time.timeScale == 0f)
			{
				nowAnimationState.time += this.data.msTime;
			}
			else
			{
				this.data.msTime = Time.deltaTime;
			}
		}
		if (nowAnimationState.wrapMode == WrapMode.Loop && info.LoopNum > 0)
		{
			if (this.IsAnimeEnd())
			{
				info.LoopCnt++;
				if (info.isReverse)
				{
					nowAnimationState.time = nowAnimationState.length;
				}
				else
				{
					nowAnimationState.time = 0f;
				}
			}
			if (info.LoopCnt > info.LoopNum)
			{
				nowAnimationState.wrapMode = WrapMode.Default;
			}
		}
		if (nowAnimationState.wrapMode == WrapMode.Default && this.IsAnimeEnd() && info.LinkNo != -1 && this.data.NowPtn != info.LinkNo)
		{
			this.Play(info.LinkNo, -1f, 0.3f, 0, WrapMode.Default);
		}
		if (this.IsAnimeEnd())
		{
			AnimationState animationState = this.animation[this.GetID(this.data.BeforePtn)];
			if (animationState)
			{
				animationState.wrapMode = this.GetInfo(this.data.BeforePtn).baseMode;
			}
		}
	}

	// Token: 0x06008E6A RID: 36458 RVA: 0x003B44CC File Offset: 0x003B28CC
	public void LoopSet(int ptn, int LoopNum)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(ptn);
		if (info != null)
		{
			info.LoopNum = LoopNum;
			info.LoopCnt = 0;
		}
		AnimationState animationState = this.animation[this.GetID(ptn)];
		if (animationState)
		{
			animationState.wrapMode = ((LoopNum != -1) ? WrapMode.Loop : WrapMode.Default);
		}
	}

	// Token: 0x06008E6B RID: 36459 RVA: 0x003B4528 File Offset: 0x003B2928
	public void SpeedSetAll(float speed)
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		if (nowAnimationState)
		{
			AnimationAssist.ANMCTRLST anmctrlst = this.data;
			nowAnimationState.speed = speed;
			anmctrlst.speed = speed;
		}
	}

	// Token: 0x06008E6C RID: 36460 RVA: 0x003B455C File Offset: 0x003B295C
	public void SpeedSet(float speed)
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		if (nowAnimationState)
		{
			nowAnimationState.speed = speed;
		}
	}

	// Token: 0x06008E6D RID: 36461 RVA: 0x003B4584 File Offset: 0x003B2984
	public void ReStart()
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(-1);
		if (info != null)
		{
			info.isStop = false;
		}
		AnimationState nowAnimationState = this.NowAnimationState;
		if (nowAnimationState)
		{
			nowAnimationState.speed = this.data.speed;
			this.Play(string.Empty, nowAnimationState.time, 0.3f, 0, WrapMode.Default);
		}
	}

	// Token: 0x06008E6E RID: 36462 RVA: 0x003B45E4 File Offset: 0x003B29E4
	public void Stop()
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(-1);
		if (info != null)
		{
			info.isStop = true;
		}
		AnimationState nowAnimationState = this.NowAnimationState;
		if (nowAnimationState)
		{
			nowAnimationState.speed = 0f;
			float time = nowAnimationState.time;
			this.animation.Stop();
			nowAnimationState.time = time;
		}
	}

	// Token: 0x06008E6F RID: 36463 RVA: 0x003B463C File Offset: 0x003B2A3C
	public int GetNowPtn()
	{
		return this.data.NowPtn;
	}

	// Token: 0x06008E70 RID: 36464 RVA: 0x003B4649 File Offset: 0x003B2A49
	public void Play(int id, float time = -1f, float fadeSpeed = 0.3f, int layer = 0, WrapMode mode = WrapMode.Default)
	{
		this.Play(this.GetID(id), time, fadeSpeed, layer, mode);
	}

	// Token: 0x06008E71 RID: 36465 RVA: 0x003B4660 File Offset: 0x003B2A60
	public void Play(string name = "", float time = -1f, float fadeSpeed = 0.3f, int layer = 0, WrapMode mode = WrapMode.Default)
	{
		if (name == string.Empty)
		{
			name = this.GetID(this.data.NowPtn);
		}
		AnimationState animationState = this.animation[name];
		if (animationState == null)
		{
			return;
		}
		AnimationState animationState2 = this.animation[this.GetID(this.data.BeforePtn)];
		if (animationState2)
		{
			animationState2.wrapMode = this.GetInfo(this.data.BeforePtn).baseMode;
		}
		for (int i = 0; i < this.data.info.Length; i++)
		{
			if (name == this.GetID(i))
			{
				this.data.BeforePtn = this.data.NowPtn;
				this.data.NowPtn = i;
				break;
			}
		}
		animationState.speed = this.data.speed;
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(-1);
		if (info.isStop)
		{
			animationState.speed = 0f;
			return;
		}
		if (info.isReverse)
		{
			if (animationState.speed > 0f)
			{
				animationState.speed *= -1f;
			}
			animationState.time = animationState.length - time;
			animationState.time = Mathf.Clamp(animationState.time, 0f, animationState.length);
		}
		else if (time >= 0f)
		{
			animationState.time = time;
		}
		animationState.layer = layer;
		if (mode != WrapMode.Default)
		{
			animationState.wrapMode = mode;
		}
		if (fadeSpeed == 0f)
		{
			this.animation.Play(name);
		}
		else
		{
			if (animationState.wrapMode == WrapMode.Default)
			{
				animationState.wrapMode = WrapMode.ClampForever;
			}
			this.animation.CrossFade(name, fadeSpeed);
		}
	}

	// Token: 0x06008E72 RID: 36466 RVA: 0x003B4836 File Offset: 0x003B2C36
	public void PlayOverride(int id, float time = -1f, float fadeSpeed = 0.3f, int layer = 1)
	{
		this.PlayOverride(this.GetID(id), time, fadeSpeed, layer);
	}

	// Token: 0x06008E73 RID: 36467 RVA: 0x003B4849 File Offset: 0x003B2C49
	public void PlayOverride(string name = "", float time = -1f, float fadeSpeed = 0.3f, int layer = 1)
	{
		this.Play(name, time, fadeSpeed, layer, WrapMode.Once);
	}

	// Token: 0x06008E74 RID: 36468 RVA: 0x003B4857 File Offset: 0x003B2C57
	public void Fusion(int id, float weight = 0.5f, int layer = 1)
	{
		this.Fusion(this.GetID(id), weight, layer);
	}

	// Token: 0x06008E75 RID: 36469 RVA: 0x003B4868 File Offset: 0x003B2C68
	public void Fusion(string name = "", float weight = 0.5f, int layer = 1)
	{
		if (name == string.Empty)
		{
			name = this.GetID(this.data.NowPtn);
		}
		AnimationState animationState = this.animation[name];
		if (animationState == null)
		{
			return;
		}
		this.Play(name, -1f, 0f, layer, WrapMode.Default);
		animationState.weight = weight;
	}

	// Token: 0x06008E76 RID: 36470 RVA: 0x003B48CC File Offset: 0x003B2CCC
	private void PlaySync(Animation anime, int num)
	{
		AnimationState animationState = anime[this.GetID(num)];
		AnimationState nowAnimationState = this.NowAnimationState;
		if (animationState && nowAnimationState)
		{
			nowAnimationState.time = animationState.time;
		}
	}

	// Token: 0x06008E77 RID: 36471 RVA: 0x003B4910 File Offset: 0x003B2D10
	public AnimationAssist.ANMCTRLINFOST GetInfo(int nPtn = -1)
	{
		if (nPtn == -1)
		{
			return this.data.info[this.data.NowPtn];
		}
		if ((ulong)nPtn >= (ulong)((long)this.data.info.Length))
		{
			return null;
		}
		return this.data.info[nPtn];
	}

	// Token: 0x06008E78 RID: 36472 RVA: 0x003B4960 File Offset: 0x003B2D60
	public bool SetNowFrame(float nowFrame)
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		if (!nowAnimationState)
		{
			return false;
		}
		nowAnimationState.time = nowFrame;
		return true;
	}

	// Token: 0x06008E79 RID: 36473 RVA: 0x003B498C File Offset: 0x003B2D8C
	public bool GetNowFrame(ref float nowFrame)
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		if (!nowAnimationState)
		{
			return false;
		}
		nowFrame = nowAnimationState.time;
		return true;
	}

	// Token: 0x06008E7A RID: 36474 RVA: 0x003B49B8 File Offset: 0x003B2DB8
	public bool GetNowEndFrame(ref float endFrame)
	{
		AnimationState nowAnimationState = this.NowAnimationState;
		if (!nowAnimationState)
		{
			return false;
		}
		endFrame = nowAnimationState.length;
		return true;
	}

	// Token: 0x06008E7B RID: 36475 RVA: 0x003B49E2 File Offset: 0x003B2DE2
	public bool GetPtnNow(ref int nowPtn)
	{
		nowPtn = this.data.NowPtn;
		return true;
	}

	// Token: 0x06008E7C RID: 36476 RVA: 0x003B49F2 File Offset: 0x003B2DF2
	public bool GetPtnBefore(ref int beforePtn)
	{
		beforePtn = this.data.BeforePtn;
		return true;
	}

	// Token: 0x06008E7D RID: 36477 RVA: 0x003B4A02 File Offset: 0x003B2E02
	public bool GetName(int nPtn, ref string name)
	{
		name = this.GetID(nPtn);
		return name != string.Empty;
	}

	// Token: 0x06008E7E RID: 36478 RVA: 0x003B4A1C File Offset: 0x003B2E1C
	public bool GetEndFrame(int nPtn, ref float endFrame)
	{
		AnimationState animationState = this.animation[this.GetID(nPtn)];
		if (!animationState)
		{
			return false;
		}
		endFrame = animationState.length;
		return true;
	}

	// Token: 0x06008E7F RID: 36479 RVA: 0x003B4A54 File Offset: 0x003B2E54
	public bool SetSpeed(int nPtn, float speed)
	{
		AnimationState animationState = this.animation[this.GetID(nPtn)];
		if (!animationState)
		{
			return false;
		}
		animationState.speed = speed;
		return true;
	}

	// Token: 0x06008E80 RID: 36480 RVA: 0x003B4A8C File Offset: 0x003B2E8C
	public bool GetSpeed(int nPtn, ref float speed)
	{
		AnimationState animationState = this.animation[this.GetID(nPtn)];
		if (!animationState)
		{
			return false;
		}
		speed = animationState.speed;
		return true;
	}

	// Token: 0x06008E81 RID: 36481 RVA: 0x003B4AC4 File Offset: 0x003B2EC4
	public bool SetWrapMode(int nPtn, WrapMode mode)
	{
		AnimationState animationState = this.animation[this.GetID(nPtn)];
		if (!animationState)
		{
			return false;
		}
		animationState.wrapMode = mode;
		return true;
	}

	// Token: 0x06008E82 RID: 36482 RVA: 0x003B4AFC File Offset: 0x003B2EFC
	public bool GetWrapMode(int nPtn, ref WrapMode mode)
	{
		AnimationState animationState = this.animation[this.GetID(nPtn)];
		if (!animationState)
		{
			return false;
		}
		mode = animationState.wrapMode;
		return true;
	}

	// Token: 0x06008E83 RID: 36483 RVA: 0x003B4B34 File Offset: 0x003B2F34
	public bool SetLoopCnt(int nPtn, int loopCnt)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		info.LoopCnt = loopCnt;
		return true;
	}

	// Token: 0x06008E84 RID: 36484 RVA: 0x003B4B5C File Offset: 0x003B2F5C
	public bool GetLoopCnt(int nPtn, ref int loopCnt)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		loopCnt = info.LoopCnt;
		return true;
	}

	// Token: 0x06008E85 RID: 36485 RVA: 0x003B4B84 File Offset: 0x003B2F84
	public bool SetLoopNum(int nPtn, int loopNum)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		info.LoopNum = loopNum;
		return true;
	}

	// Token: 0x06008E86 RID: 36486 RVA: 0x003B4BAC File Offset: 0x003B2FAC
	public bool GetLoopNum(int nPtn, ref int loopNum)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		loopNum = info.LoopNum;
		return true;
	}

	// Token: 0x06008E87 RID: 36487 RVA: 0x003B4BD4 File Offset: 0x003B2FD4
	public bool SetLinkNo(int nPtn, int LinkNo)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		info.LinkNo = LinkNo;
		return true;
	}

	// Token: 0x06008E88 RID: 36488 RVA: 0x003B4BFC File Offset: 0x003B2FFC
	public bool GetLinkNo(int nPtn, ref int LinkNo)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		LinkNo = info.LinkNo;
		return true;
	}

	// Token: 0x06008E89 RID: 36489 RVA: 0x003B4C24 File Offset: 0x003B3024
	public bool SetReverseFlag(int nPtn, bool isReverse)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		info.isReverse = isReverse;
		return true;
	}

	// Token: 0x06008E8A RID: 36490 RVA: 0x003B4C4C File Offset: 0x003B304C
	public bool GetReverseFlag(int nPtn, ref bool isReverse)
	{
		AnimationAssist.ANMCTRLINFOST info = this.GetInfo(nPtn);
		if (info == null)
		{
			return false;
		}
		isReverse = info.isReverse;
		return true;
	}

	// Token: 0x04007335 RID: 29493
	private Animation animation;

	// Token: 0x04007336 RID: 29494
	private AnimationAssist.ANMCTRLST data;

	// Token: 0x020010AC RID: 4268
	public class ANMCTRLINFOST
	{
		// Token: 0x06008E8B RID: 36491 RVA: 0x003B4C74 File Offset: 0x003B3074
		public ANMCTRLINFOST(AnimationState state)
		{
			this.name = state.name;
			this.LoopCnt = 0;
			this.LoopNum = 0;
			this.LinkNo = -1;
			this.isReverse = (state.speed < 0f);
			this.isStop = (state.speed == 0f);
			this.baseMode = state.wrapMode;
		}

		// Token: 0x17001EEC RID: 7916
		// (get) Token: 0x06008E8C RID: 36492 RVA: 0x003B4CDA File Offset: 0x003B30DA
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04007337 RID: 29495
		private string name;

		// Token: 0x04007338 RID: 29496
		public int LoopCnt;

		// Token: 0x04007339 RID: 29497
		public int LoopNum;

		// Token: 0x0400733A RID: 29498
		public int LinkNo;

		// Token: 0x0400733B RID: 29499
		public bool isReverse;

		// Token: 0x0400733C RID: 29500
		public bool isStop;

		// Token: 0x0400733D RID: 29501
		public WrapMode baseMode;
	}

	// Token: 0x020010AD RID: 4269
	public class ANMCTRLST
	{
		// Token: 0x06008E8D RID: 36493 RVA: 0x003B4CE2 File Offset: 0x003B30E2
		public ANMCTRLST(int num)
		{
			this.info = new AnimationAssist.ANMCTRLINFOST[num];
			this.fChange = false;
			this.BeforePtn = -1;
			this.NowPtn = 0;
			this.msTime = -1f;
			this.speed = 1f;
		}

		// Token: 0x0400733E RID: 29502
		public AnimationAssist.ANMCTRLINFOST[] info;

		// Token: 0x0400733F RID: 29503
		public bool fChange;

		// Token: 0x04007340 RID: 29504
		public int BeforePtn;

		// Token: 0x04007341 RID: 29505
		public int NowPtn;

		// Token: 0x04007342 RID: 29506
		public float msTime;

		// Token: 0x04007343 RID: 29507
		public float speed;
	}
}
