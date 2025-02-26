using System;
using System.Collections.Generic;
using AIChara;
using Manager;
using UnityEngine;
using UnityEx;

// Token: 0x02000A96 RID: 2710
public class HAutoCtrl : MonoBehaviour
{
	// Token: 0x06004FD6 RID: 20438 RVA: 0x001EBD84 File Offset: 0x001EA184
	public void Load(string _strAssetPath, int _personal, int _attribute = 0)
	{
		this.lerp = new Vector2(-1f, -1f);
		if (!this.LoadAuto())
		{
			return;
		}
		this.autoLeave.time.minmax = new Vector2(20f, 20f);
		this.autoLeave.time.Reset();
		this.autoLeave.rate = 50;
		if (!this.LoadAutoLeaveItToYou())
		{
			return;
		}
		if (!this.LoadAutoLeaveItToYouPersonality(_personal))
		{
			return;
		}
		if (!this.LoadAutoLeaveItToYouAttribute(_attribute))
		{
			return;
		}
	}

	// Token: 0x06004FD7 RID: 20439 RVA: 0x001EBE14 File Offset: 0x001EA214
	private bool LoadAuto()
	{
		this.info = Singleton<Manager.Resources>.Instance.HSceneTable.HAutoInfo;
		return this.info != null;
	}

	// Token: 0x06004FD8 RID: 20440 RVA: 0x001EBE37 File Offset: 0x001EA237
	private bool LoadAutoLeaveItToYou()
	{
		this.autoLeave = Singleton<Manager.Resources>.Instance.HSceneTable.HAutoLeaveItToYou;
		return this.autoLeave != null;
	}

	// Token: 0x06004FD9 RID: 20441 RVA: 0x001EBE5C File Offset: 0x001EA25C
	private bool LoadAutoLeaveItToYouPersonality(int _personal)
	{
		float num;
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.autoLeavePersonalityRate.TryGetValue(_personal, out num))
		{
			num = 1f;
		}
		this.autoLeave.rate = Mathf.CeilToInt((float)this.autoLeave.rate * num);
		return true;
	}

	// Token: 0x06004FDA RID: 20442 RVA: 0x001EBEAC File Offset: 0x001EA2AC
	private bool LoadAutoLeaveItToYouAttribute(int _attribute)
	{
		float num;
		if (!Singleton<Manager.Resources>.Instance.HSceneTable.autoLeaveAttributeRate.TryGetValue(_attribute, out num))
		{
			num = 1f;
		}
		this.autoLeave.rate = Mathf.CeilToInt((float)this.autoLeave.rate * num);
		return true;
	}

	// Token: 0x06004FDB RID: 20443 RVA: 0x001EBEFA File Offset: 0x001EA2FA
	public void Reset()
	{
		this.StartInit();
		this.ReStartInit();
		this.SpeedInit();
		this.LoopMotionInit();
		this.MotionChangeInit();
		this.PullInit();
	}

	// Token: 0x06004FDC RID: 20444 RVA: 0x001EBF20 File Offset: 0x001EA320
	public void StartInit()
	{
		this.info.start.Reset();
	}

	// Token: 0x06004FDD RID: 20445 RVA: 0x001EBF32 File Offset: 0x001EA332
	public bool IsStart()
	{
		return this.info.start.IsTime();
	}

	// Token: 0x06004FDE RID: 20446 RVA: 0x001EBF44 File Offset: 0x001EA344
	public void ReStartInit()
	{
		this.info.reStart.Reset();
	}

	// Token: 0x06004FDF RID: 20447 RVA: 0x001EBF56 File Offset: 0x001EA356
	public bool IsReStart()
	{
		return this.info.reStart.IsTime();
	}

	// Token: 0x06004FE0 RID: 20448 RVA: 0x001EBF68 File Offset: 0x001EA368
	public void SpeedInit()
	{
		this.info.speed.Reset();
		this.centerSpeed = 0f;
	}

	// Token: 0x06004FE1 RID: 20449 RVA: 0x001EBF88 File Offset: 0x001EA388
	public bool AddSpeed(float _wheel, int _loop)
	{
		if (this.lerp.x >= 0f && this.lerp.y >= 0f)
		{
			return false;
		}
		bool result = false;
		this.centerSpeed += _wheel;
		if (_loop == 0)
		{
			if (this.centerSpeed > 1f)
			{
				result = true;
			}
		}
		else if (_loop == 1 && this.centerSpeed < 1f)
		{
			result = true;
		}
		if (_wheel != 0f)
		{
			this.info.speed.Reset();
		}
		if (_loop != 2)
		{
			this.centerSpeed = Mathf.Clamp(this.centerSpeed, 0f, 2f);
		}
		else
		{
			this.centerSpeed = Mathf.Clamp(this.centerSpeed, 0f, 1f);
		}
		return result;
	}

	// Token: 0x06004FE2 RID: 20450 RVA: 0x001EC065 File Offset: 0x001EA465
	public void LoopMotionInit()
	{
		this.info.loopChange.Reset();
	}

	// Token: 0x06004FE3 RID: 20451 RVA: 0x001EC078 File Offset: 0x001EA478
	public bool ChangeLoopMotion(bool _loop)
	{
		if (this.lerp.x >= 0f && this.lerp.y >= 0f)
		{
			return false;
		}
		if (!this.info.loopChange.IsTime())
		{
			return false;
		}
		this.info.loopChange.Reset();
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		shuffleRand.Init(100);
		return this.IsChangeLoop(_loop, this.info.rateWeakLoop < shuffleRand.Get());
	}

	// Token: 0x06004FE4 RID: 20452 RVA: 0x001EC101 File Offset: 0x001EA501
	private bool IsChangeLoop(bool _loop, bool _changeLoop)
	{
		if (_loop == _changeLoop)
		{
			return false;
		}
		this.centerSpeed = 1f;
		return true;
	}

	// Token: 0x06004FE5 RID: 20453 RVA: 0x001EC118 File Offset: 0x001EA518
	public bool ChangeSpeed(bool _loop, Vector2 _hit)
	{
		if (this.lerp.x >= 0f && this.lerp.y >= 0f)
		{
			return false;
		}
		if (!this.info.speed.IsTime())
		{
			return false;
		}
		this.info.speed.Reset();
		this.timeLerp = 0f;
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		if (_hit.x >= 0f)
		{
			shuffleRand.Init(100);
			if (this.info.rateHit > shuffleRand.Get())
			{
				this.lerp.x = (_loop ? (this.centerSpeed - 1f) : this.centerSpeed);
				this.lerp.y = UnityEngine.Random.Range(_hit.x, _hit.y);
				return false;
			}
		}
		shuffleRand.Init(100);
		int num;
		do
		{
			num = shuffleRand.Get();
		}
		while (GlobalMethod.RangeOn<int>(num, (int)(_hit.x * 100f), (int)(_hit.y * 100f)));
		this.lerp.x = (_loop ? (this.centerSpeed - 1f) : this.centerSpeed);
		this.lerp.y = (float)num * 0.01f;
		return false;
	}

	// Token: 0x06004FE6 RID: 20454 RVA: 0x001EC284 File Offset: 0x001EA684
	public float GetSpeed(bool _loop)
	{
		if (this.lerp.x < 0f && this.lerp.y < 0f)
		{
			return _loop ? (this.centerSpeed - 1f) : this.centerSpeed;
		}
		this.timeLerp = Mathf.Clamp(this.timeLerp + Time.deltaTime, 0f, this.info.lerpTimeSpeed);
		this.centerSpeed = Mathf.Lerp(this.lerp.x, this.lerp.y, this.lerpCurve.Evaluate(Mathf.InverseLerp(0f, this.info.lerpTimeSpeed, this.timeLerp)));
		if (_loop)
		{
			this.centerSpeed += 1f;
		}
		if (this.timeLerp >= this.info.lerpTimeSpeed)
		{
			this.lerp = new Vector2(-1f, -1f);
			this.timeLerp = 0f;
		}
		return _loop ? (this.centerSpeed - 1f) : this.centerSpeed;
	}

	// Token: 0x06004FE7 RID: 20455 RVA: 0x001EC3B2 File Offset: 0x001EA7B2
	public void SetSpeed(float _speed)
	{
		this.centerSpeed = _speed;
	}

	// Token: 0x06004FE8 RID: 20456 RVA: 0x001EC3BB File Offset: 0x001EA7BB
	public void MotionChangeInit()
	{
		this.info.motionChange.Reset();
	}

	// Token: 0x06004FE9 RID: 20457 RVA: 0x001EC3D0 File Offset: 0x001EA7D0
	public bool IsChangeActionAtLoop()
	{
		if (this.lerp.x >= 0f && this.lerp.y >= 0f)
		{
			return false;
		}
		if (!this.info.motionChange.IsTime())
		{
			return false;
		}
		this.info.motionChange.Reset();
		return true;
	}

	// Token: 0x06004FEA RID: 20458 RVA: 0x001EC434 File Offset: 0x001EA834
	public bool IsChangeActionAtRestart()
	{
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		shuffleRand.Init(100);
		return shuffleRand.Get() < this.info.rateRestartMotionChange;
	}

	// Token: 0x06004FEB RID: 20459 RVA: 0x001EC464 File Offset: 0x001EA864
	public HScene.StartMotion GetAnimation(List<HScene.AnimationListInfo>[] _listAnim, int _initiative, bool _isFirst = false)
	{
		bool flag = true;
		HAutoCtrl.AutoRandom autoRandom = new HAutoCtrl.AutoRandom();
		for (int i = 0; i < _listAnim.Length; i++)
		{
			for (int j = 0; j < _listAnim[i].Count; j++)
			{
				if (_listAnim[i][j].nPositons.Contains(Singleton<HSceneFlagCtrl>.Instance.nPlace))
				{
					if (Singleton<HSceneManager>.Instance.Player.ChaControl.sex == 0 || (Singleton<HSceneManager>.Instance.Player.ChaControl.sex == 1 && Singleton<HSceneManager>.Instance.bFutanari))
					{
						if (i == 4)
						{
							goto IL_1AB;
						}
						ChaControl[] females = Singleton<HSceneManager>.Instance.HSceneSet.GetComponentInChildren<HScene>().GetFemales();
						if (females[1] == null && i == 5)
						{
							goto IL_1AB;
						}
					}
					else if (i != 4)
					{
						goto IL_1AB;
					}
					if (_initiative == 1)
					{
						if (_listAnim[i][j].nInitiativeFemale != 1 && (!flag || _listAnim[i][j].nInitiativeFemale != 2))
						{
							goto IL_1AB;
						}
					}
					else
					{
						if (_initiative != 2)
						{
							goto IL_1AB;
						}
						if (_listAnim[i][j].nInitiativeFemale != 2)
						{
							goto IL_1AB;
						}
					}
					autoRandom.Add(new HAutoCtrl.AutoRandom.AutoRandomDate
					{
						mode = i,
						id = _listAnim[i][j].id
					}, 10f + ((!Singleton<HSceneManager>.Instance.HSkil.ContainsKey(_listAnim[i][j].nFemaleProclivity)) ? 0f : this.info.rateAddMotionChange));
					if (_isFirst)
					{
						break;
					}
				}
				IL_1AB:;
			}
		}
		HAutoCtrl.AutoRandom.AutoRandomDate autoRandomDate = autoRandom.Random();
		return new HScene.StartMotion(autoRandomDate.mode, autoRandomDate.id);
	}

	// Token: 0x06004FEC RID: 20460 RVA: 0x001EC656 File Offset: 0x001EAA56
	public void PullInit()
	{
		this.info.pull.Reset();
		this.isPulljudge = false;
	}

	// Token: 0x06004FED RID: 20461 RVA: 0x001EC670 File Offset: 0x001EAA70
	public bool IsPull(bool _isInsert)
	{
		if (this.isPulljudge)
		{
			return false;
		}
		if (!this.info.pull.IsTime())
		{
			return false;
		}
		this.info.pull.Reset();
		this.isPulljudge = true;
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		shuffleRand.Init(100);
		int num = shuffleRand.Get();
		return (!_isInsert) ? ((float)num < this.info.rateNotInsertPull) : ((float)num < this.info.rateInsertPull);
	}

	// Token: 0x06004FEE RID: 20462 RVA: 0x001EC6F8 File Offset: 0x001EAAF8
	public bool IsAutoAutoLeaveItToYou(ChaControl _female, HScene.AnimationListInfo _ali, bool _isAutoLeaveItToYouButton, bool _isInitiative)
	{
		if (_female == null)
		{
			return false;
		}
		if (_ali == null)
		{
			return false;
		}
		AnimatorStateInfo animatorStateInfo = _female.getAnimatorStateInfo(0);
		if (_isInitiative)
		{
			return false;
		}
		if (!_isAutoLeaveItToYouButton)
		{
			return false;
		}
		if (!animatorStateInfo.IsName("Idle"))
		{
			return false;
		}
		if (_ali.nAnimListInfoID == 3)
		{
			return false;
		}
		if (!this.autoLeave.time.IsTime())
		{
			return false;
		}
		ShuffleRand shuffleRand = new ShuffleRand(-1);
		shuffleRand.Init(100);
		bool flag = shuffleRand.Get() < this.autoLeave.rate;
		if (!flag)
		{
			this.autoLeave.time.minmax.x = Mathf.Max(this.autoLeave.time.minmax.x - 5f, 0f);
			this.autoLeave.time.minmax.y = Mathf.Max(this.autoLeave.time.minmax.y - 5f, 0f);
		}
		else
		{
			this.autoLeave.time.minmax = this.autoLeave.baseTime;
		}
		this.autoLeave.time.Reset();
		return flag;
	}

	// Token: 0x06004FEF RID: 20463 RVA: 0x001EC83A File Offset: 0x001EAC3A
	public void AutoAutoLeaveItToYouInit()
	{
		this.autoLeave.time.minmax = this.autoLeave.baseTime;
		this.autoLeave.time.Reset();
	}

	// Token: 0x040048DC RID: 18652
	private HAutoCtrl.HAutoInfo info;

	// Token: 0x040048DD RID: 18653
	[DisabledGroup("今のスピード")]
	public float centerSpeed;

	// Token: 0x040048DE RID: 18654
	[DisabledGroup("リープ時間")]
	public float timeLerp;

	// Token: 0x040048DF RID: 18655
	[DisabledGroup("リープ先")]
	public Vector2 lerp = new Vector2(-1f, -1f);

	// Token: 0x040048E0 RID: 18656
	[Label("リープアニメーション")]
	public AnimationCurve lerpCurve;

	// Token: 0x040048E1 RID: 18657
	[DisabledGroup("抜く確率判定")]
	public bool isPulljudge;

	// Token: 0x040048E2 RID: 18658
	public HAutoCtrl.AutoLeaveItToYou autoLeave;

	// Token: 0x02000A97 RID: 2711
	public class AutoRandom
	{
		// Token: 0x06004FF0 RID: 20464 RVA: 0x001EC867 File Offset: 0x001EAC67
		public AutoRandom()
		{
			this.allVal = 0f;
		}

		// Token: 0x06004FF1 RID: 20465 RVA: 0x001EC8A8 File Offset: 0x001EACA8
		public bool Add(HAutoCtrl.AutoRandom.AutoRandomDate _date, float _rate)
		{
			if (_rate == 0f)
			{
				GlobalMethod.DebugLog("ランダム 追加登録個数が0", 0);
				return false;
			}
			if (this.checks.Exists((HAutoCtrl.AutoRandom.CCheck i) => i.date.mode == _date.mode && i.date.id == _date.id))
			{
				GlobalMethod.DebugLog("ランダム 重複登録", 0);
				return false;
			}
			HAutoCtrl.AutoRandom.CCheck ccheck = new HAutoCtrl.AutoRandom.CCheck();
			ccheck.date = _date;
			ccheck.rate = _rate;
			this.backup.Add(ccheck);
			this.RandomSort();
			this.allVal = 0f;
			foreach (HAutoCtrl.AutoRandom.CCheck ccheck2 in this.checks)
			{
				ccheck2.minVal = this.allVal;
				ccheck2.maxVal = this.allVal + ccheck2.rate;
				this.allVal += ccheck2.rate;
			}
			return true;
		}

		// Token: 0x06004FF2 RID: 20466 RVA: 0x001EC9B4 File Offset: 0x001EADB4
		private void RandomSort()
		{
			this.tmpTuple.Clear();
			for (int i = 0; i < this.backup.Count; i++)
			{
				this.tmpTuple.Add(new UnityEx.ValueTuple<Guid, int>(Guid.NewGuid(), i));
			}
			this.tmpTuple.Sort(this.backupSortCompare);
			this.checks.Clear();
			for (int j = 0; j < this.tmpTuple.Count; j++)
			{
				this.checks.Add(this.backup[this.tmpTuple[j].Item2]);
			}
		}

		// Token: 0x06004FF3 RID: 20467 RVA: 0x001ECA60 File Offset: 0x001EAE60
		public HAutoCtrl.AutoRandom.AutoRandomDate Random()
		{
			if (this.IsEmpty())
			{
				return new HAutoCtrl.AutoRandom.AutoRandomDate();
			}
			float randVal = UnityEngine.Random.Range(0f, this.allVal);
			HAutoCtrl.AutoRandom.CCheck ccheck = this.checks.Find((HAutoCtrl.AutoRandom.CCheck x) => randVal >= x.minVal && randVal <= x.maxVal);
			if (ccheck == null)
			{
				return new HAutoCtrl.AutoRandom.AutoRandomDate();
			}
			return ccheck.date;
		}

		// Token: 0x06004FF4 RID: 20468 RVA: 0x001ECAC4 File Offset: 0x001EAEC4
		public bool IsEmpty()
		{
			return this.backup.Count == 0;
		}

		// Token: 0x06004FF5 RID: 20469 RVA: 0x001ECAD4 File Offset: 0x001EAED4
		public void Clear()
		{
			this.allVal = 0f;
			this.checks.Clear();
			this.backup.Clear();
		}

		// Token: 0x040048E3 RID: 18659
		private float allVal;

		// Token: 0x040048E4 RID: 18660
		private List<HAutoCtrl.AutoRandom.CCheck> backup = new List<HAutoCtrl.AutoRandom.CCheck>();

		// Token: 0x040048E5 RID: 18661
		private List<HAutoCtrl.AutoRandom.CCheck> checks = new List<HAutoCtrl.AutoRandom.CCheck>();

		// Token: 0x040048E6 RID: 18662
		private List<UnityEx.ValueTuple<Guid, int>> tmpTuple = new List<UnityEx.ValueTuple<Guid, int>>();

		// Token: 0x040048E7 RID: 18663
		private HAutoCtrl.AutoRandom.ListComparer backupSortCompare = new HAutoCtrl.AutoRandom.ListComparer();

		// Token: 0x02000A98 RID: 2712
		public class AutoRandomDate
		{
			// Token: 0x040048E8 RID: 18664
			public int mode = -1;

			// Token: 0x040048E9 RID: 18665
			public int id = -1;
		}

		// Token: 0x02000A99 RID: 2713
		protected class CCheck
		{
			// Token: 0x040048EA RID: 18666
			public HAutoCtrl.AutoRandom.AutoRandomDate date;

			// Token: 0x040048EB RID: 18667
			public float rate;

			// Token: 0x040048EC RID: 18668
			public float minVal;

			// Token: 0x040048ED RID: 18669
			public float maxVal;
		}

		// Token: 0x02000A9A RID: 2714
		private class ListComparer : IComparer<UnityEx.ValueTuple<Guid, int>>
		{
			// Token: 0x06004FF9 RID: 20473 RVA: 0x001ECB1D File Offset: 0x001EAF1D
			public int Compare(UnityEx.ValueTuple<Guid, int> a, UnityEx.ValueTuple<Guid, int> b)
			{
				return this.SortCompare<Guid>(a.Item1, b.Item1);
			}

			// Token: 0x06004FFA RID: 20474 RVA: 0x001ECB33 File Offset: 0x001EAF33
			private int SortCompare<T>(T a, T b) where T : IComparable
			{
				return a.CompareTo(b);
			}
		}
	}

	// Token: 0x02000A9B RID: 2715
	[Serializable]
	public class AutoTime
	{
		// Token: 0x06004FFC RID: 20476 RVA: 0x001ECBBF File Offset: 0x001EAFBF
		public void Reset()
		{
			this.time = UnityEngine.Random.Range(this.minmax.x, this.minmax.y);
			this.timeDelta = 0f;
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x001ECBED File Offset: 0x001EAFED
		public bool IsTime()
		{
			this.timeDelta = Mathf.Clamp(this.timeDelta + Time.deltaTime, 0f, this.time);
			return this.timeDelta >= this.time;
		}

		// Token: 0x040048EE RID: 18670
		[DisabledGroup("最小最大")]
		public Vector2 minmax;

		// Token: 0x040048EF RID: 18671
		[DisabledGroup("時間まで")]
		public float time;

		// Token: 0x040048F0 RID: 18672
		[DisabledGroup("経過時間")]
		public float timeDelta;
	}

	// Token: 0x02000A9C RID: 2716
	[Serializable]
	public class AutoLeaveItToYou
	{
		// Token: 0x040048F1 RID: 18673
		public HAutoCtrl.AutoTime time = new HAutoCtrl.AutoTime();

		// Token: 0x040048F2 RID: 18674
		[Label("元の変更時間")]
		public Vector2 baseTime = Vector2.zero;

		// Token: 0x040048F3 RID: 18675
		[Label("おまかせにいく確率")]
		public int rate = 50;
	}

	// Token: 0x02000A9D RID: 2717
	[Serializable]
	public class HAutoInfo
	{
		// Token: 0x040048F4 RID: 18676
		public HAutoCtrl.AutoTime start = new HAutoCtrl.AutoTime();

		// Token: 0x040048F5 RID: 18677
		public HAutoCtrl.AutoTime reStart = new HAutoCtrl.AutoTime();

		// Token: 0x040048F6 RID: 18678
		public HAutoCtrl.AutoTime speed = new HAutoCtrl.AutoTime();

		// Token: 0x040048F7 RID: 18679
		public HAutoCtrl.AutoTime loopChange = new HAutoCtrl.AutoTime();

		// Token: 0x040048F8 RID: 18680
		public HAutoCtrl.AutoTime motionChange = new HAutoCtrl.AutoTime();

		// Token: 0x040048F9 RID: 18681
		public HAutoCtrl.AutoTime pull = new HAutoCtrl.AutoTime();

		// Token: 0x040048FA RID: 18682
		[DisabledGroup("スピード変更のリープ時間")]
		public float lerpTimeSpeed;

		// Token: 0x040048FB RID: 18683
		[DisabledGroup("弱ループ率")]
		public int rateWeakLoop;

		// Token: 0x040048FC RID: 18684
		[DisabledGroup("当たりに向かう率")]
		public int rateHit;

		// Token: 0x040048FD RID: 18685
		[DisabledGroup("体位変更性癖加算")]
		public float rateAddMotionChange;

		// Token: 0x040048FE RID: 18686
		[DisabledGroup("リスタート時の体位変更率")]
		public int rateRestartMotionChange;

		// Token: 0x040048FF RID: 18687
		[DisabledGroup("中出しした時に抜く確率")]
		public float rateInsertPull;

		// Token: 0x04004900 RID: 18688
		[DisabledGroup("中出しされてない時に抜く確率")]
		public float rateNotInsertPull;
	}
}
