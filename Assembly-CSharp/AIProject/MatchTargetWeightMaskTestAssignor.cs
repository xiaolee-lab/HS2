using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AIChara;
using Manager;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject
{
	// Token: 0x020011C2 RID: 4546
	public class MatchTargetWeightMaskTestAssignor : MonoBehaviour
	{
		// Token: 0x06009513 RID: 38163 RVA: 0x003D7AA4 File Offset: 0x003D5EA4
		private void Start()
		{
			this._charaRoot = new GameObject("root_Chara").transform;
			foreach (TestChara testChara in this._charaList)
			{
				if (testChara != null)
				{
					ChaControl chaControl;
					if (testChara.Sex == 0)
					{
						chaControl = Singleton<Character>.Instance.CreateChara(0, this._charaRoot.gameObject, 0, null);
					}
					else
					{
						chaControl = Singleton<Character>.Instance.CreateChara(1, this._charaRoot.gameObject, 0, null);
					}
					chaControl.Load(false);
					chaControl.ChangeLookEyesPtn(3);
					chaControl.ChangeLookNeckPtn(3, 1f);
					MatchTargetWeightMaskTester matchTargetWeightMaskTester = chaControl.animBody.gameObject.AddComponent<MatchTargetWeightMaskTester>();
					matchTargetWeightMaskTester.Animator = chaControl.animBody;
					matchTargetWeightMaskTester.Animator.runtimeAnimatorController = testChara.Rac;
					matchTargetWeightMaskTester.StateName = this._stateName.Value;
					matchTargetWeightMaskTester.Targets = new MatchTargetWeightMaskTester.TargetParameter[this._targets.Length];
					for (int i = 0; i < this._targets.Length; i++)
					{
						MatchTargetWeightMaskTester.TargetParameter targetParameter = matchTargetWeightMaskTester.Targets[i];
						if (targetParameter == null)
						{
							targetParameter = (matchTargetWeightMaskTester.Targets[i] = new MatchTargetWeightMaskTester.TargetParameter());
						}
						targetParameter.Start = this._targets[i].Start;
						targetParameter.End = this._targets[i].End;
						targetParameter.Target = this._targets[i].Target;
					}
					matchTargetWeightMaskTester.PositionWeight = this._positionWeight.Value;
					matchTargetWeightMaskTester.RotationWeight = this._rotationWeight.Value;
					this._chaCtrlList.Add(chaControl);
					this._testScripts.Add(matchTargetWeightMaskTester);
				}
			}
			this._stateName.Subscribe(delegate(string x)
			{
				if (this._stateNameInput != null)
				{
					this._stateNameInput.text = x;
				}
			});
			this._positionWeight.Subscribe(delegate(Vector3 x)
			{
				foreach (MatchTargetWeightMaskTester matchTargetWeightMaskTester2 in this._testScripts)
				{
					if (matchTargetWeightMaskTester2 != null)
					{
						matchTargetWeightMaskTester2.PositionWeight = x;
					}
				}
			});
			this._rotationWeight.Subscribe(delegate(float x)
			{
				foreach (MatchTargetWeightMaskTester matchTargetWeightMaskTester2 in this._testScripts)
				{
					if (matchTargetWeightMaskTester2 != null)
					{
						matchTargetWeightMaskTester2.RotationWeight = x;
					}
				}
			});
			if (this._stateNameInput != null)
			{
				this._stateNameInput.text = this._stateName.Value;
				this._stateNameInput.onValueChanged.AddListener(delegate(string x)
				{
					this._stateName.Value = x;
					foreach (MatchTargetWeightMaskTester matchTargetWeightMaskTester2 in this._testScripts)
					{
						if (matchTargetWeightMaskTester2 != null)
						{
							matchTargetWeightMaskTester2.StateName = x;
						}
					}
				});
			}
			if (this._button != null)
			{
				this._button.onClick.AddListener(delegate()
				{
					foreach (MatchTargetWeightMaskTester matchTargetWeightMaskTester2 in this._testScripts)
					{
						if (matchTargetWeightMaskTester2 != null)
						{
							matchTargetWeightMaskTester2.StateName = this._stateName.Value;
							if (matchTargetWeightMaskTester2.Targets.Length != this._targets.Length)
							{
								matchTargetWeightMaskTester2.Targets = new MatchTargetWeightMaskTester.TargetParameter[this._targets.Length];
							}
							for (int j = 0; j < this._targets.Length; j++)
							{
								MatchTargetWeightMaskTester.TargetParameter targetParameter2 = matchTargetWeightMaskTester2.Targets[j];
								if (targetParameter2 == null)
								{
									targetParameter2 = (matchTargetWeightMaskTester2.Targets[j] = new MatchTargetWeightMaskTester.TargetParameter());
								}
								targetParameter2.Start = this._targets[j].Start;
								targetParameter2.End = this._targets[j].End;
								targetParameter2.Target = this._targets[j].Target;
							}
							matchTargetWeightMaskTester2.PlayAnim();
						}
					}
				});
			}
			if (this._addButton != null)
			{
				this._addButton.onClick.AddListener(delegate()
				{
					TestChara testChara2 = TestChara.CreateFemale(null, 0);
					testChara2.Load();
					this._charaList.Add(testChara2);
					ChaControl chaControl2 = Singleton<Character>.Instance.CreateChara(1, this._charaRoot.gameObject, 0, null);
					chaControl2.Load(false);
					chaControl2.ChangeLookEyesPtn(3);
					chaControl2.ChangeLookNeckPtn(3, 1f);
					MatchTargetWeightMaskTester matchTargetWeightMaskTester2 = chaControl2.animBody.gameObject.AddComponent<MatchTargetWeightMaskTester>();
					matchTargetWeightMaskTester2.Animator = chaControl2.animBody;
					matchTargetWeightMaskTester2.Animator.runtimeAnimatorController = this._charaList[0].Rac;
					matchTargetWeightMaskTester2.StateName = this._stateName.Value;
					matchTargetWeightMaskTester2.Targets = new MatchTargetWeightMaskTester.TargetParameter[this._targets.Length];
					for (int j = 0; j < this._targets.Length; j++)
					{
						MatchTargetWeightMaskTestAssignor.TargetParameter targetParameter2 = this._targets[j];
						MatchTargetWeightMaskTester.TargetParameter targetParameter3 = matchTargetWeightMaskTester2.Targets[j];
						if (targetParameter3 == null)
						{
							targetParameter3 = (matchTargetWeightMaskTester2.Targets[j] = new MatchTargetWeightMaskTester.TargetParameter());
						}
						targetParameter3.Start = targetParameter2.Start;
						targetParameter3.End = targetParameter2.End;
						targetParameter3.Target = targetParameter2.Target;
					}
					matchTargetWeightMaskTester2.PositionWeight = this._positionWeight.Value;
					matchTargetWeightMaskTester2.RotationWeight = this._rotationWeight.Value;
					this._chaCtrlList.Add(chaControl2);
					this._testScripts.Add(matchTargetWeightMaskTester2);
					chaControl2.animBody.playbackTime = this._chaCtrlList[0].animBody.playbackTime;
				});
			}
			if (this._subButton != null)
			{
				this._subButton.onClick.AddListener(delegate()
				{
					if (this._charaList.Count < 2)
					{
						return;
					}
					TestChara testChara2 = this._charaList[this._charaList.Count - 1];
					ChaControl chaControl2 = this._chaCtrlList[this._chaCtrlList.Count - 1];
					MatchTargetWeightMaskTester item = this._testScripts[this._testScripts.Count - 1];
					this._charaList.Remove(testChara2);
					this._chaCtrlList.Remove(chaControl2);
					this._testScripts.Remove(item);
					UnityEngine.Object.Destroy(testChara2.gameObject);
					Singleton<Character>.Instance.DeleteChara(chaControl2, false);
				});
			}
		}

		// Token: 0x06009514 RID: 38164 RVA: 0x003D7DAC File Offset: 0x003D61AC
		private void Update()
		{
			if (this._isMatchingTargetOutputs != null)
			{
				for (int i = 0; i < this._testScripts.Count; i++)
				{
					MatchTargetWeightMaskTester matchTargetWeightMaskTester = this._testScripts[i];
					if (matchTargetWeightMaskTester != null)
					{
						Text text = this._isMatchingTargetOutputs[i];
						text.text = string.Format("{0:00}: isMatchingTarget = {1}", i, matchTargetWeightMaskTester.Animator.isMatchingTarget);
						if (matchTargetWeightMaskTester.Targets.Length != this._targets.Length)
						{
							matchTargetWeightMaskTester.Targets = new MatchTargetWeightMaskTester.TargetParameter[this._targets.Length];
						}
						for (int j = 0; j < this._targets.Length; j++)
						{
							MatchTargetWeightMaskTestAssignor.TargetParameter targetParameter = this._targets[j];
							MatchTargetWeightMaskTester.TargetParameter targetParameter2 = matchTargetWeightMaskTester.Targets[j];
							if (targetParameter != null && targetParameter2 != null)
							{
								targetParameter2.Start = targetParameter.Start;
								targetParameter2.End = targetParameter.End;
								targetParameter2.Target = targetParameter.Target;
							}
						}
					}
				}
			}
		}

		// Token: 0x040077CA RID: 30666
		[Header("UI")]
		[SerializeField]
		private InputField _stateNameInput;

		// Token: 0x040077CB RID: 30667
		[SerializeField]
		private Button _button;

		// Token: 0x040077CC RID: 30668
		[SerializeField]
		private Button _addButton;

		// Token: 0x040077CD RID: 30669
		[SerializeField]
		private Button _subButton;

		// Token: 0x040077CE RID: 30670
		[SerializeField]
		private Text[] _isMatchingTargetOutputs;

		// Token: 0x040077CF RID: 30671
		[Header("Matching Target Objects")]
		[SerializeField]
		private List<TestChara> _charaList;

		// Token: 0x040077D0 RID: 30672
		private List<ChaControl> _chaCtrlList = new List<ChaControl>();

		// Token: 0x040077D1 RID: 30673
		private List<MatchTargetWeightMaskTester> _testScripts = new List<MatchTargetWeightMaskTester>();

		// Token: 0x040077D2 RID: 30674
		[SerializeField]
		private StringReactiveProperty _stateName = new StringReactiveProperty(string.Empty);

		// Token: 0x040077D3 RID: 30675
		[SerializeField]
		private MatchTargetWeightMaskTestAssignor.TargetParameter[] _targets;

		// Token: 0x040077D4 RID: 30676
		[SerializeField]
		private Vector3ReactiveProperty _positionWeight = new Vector3ReactiveProperty(Vector3.one);

		// Token: 0x040077D5 RID: 30677
		[SerializeField]
		private FloatReactiveProperty _rotationWeight = new FloatReactiveProperty(0f);

		// Token: 0x040077D6 RID: 30678
		private Transform _charaRoot;

		// Token: 0x020011C3 RID: 4547
		[Serializable]
		private class TargetParameter
		{
			// Token: 0x17001F92 RID: 8082
			// (get) Token: 0x0600951D RID: 38173 RVA: 0x003D837E File Offset: 0x003D677E
			public float Start
			{
				[CompilerGenerated]
				get
				{
					return this._start;
				}
			}

			// Token: 0x17001F93 RID: 8083
			// (get) Token: 0x0600951E RID: 38174 RVA: 0x003D8386 File Offset: 0x003D6786
			public float End
			{
				[CompilerGenerated]
				get
				{
					return this._end;
				}
			}

			// Token: 0x17001F94 RID: 8084
			// (get) Token: 0x0600951F RID: 38175 RVA: 0x003D838E File Offset: 0x003D678E
			// (set) Token: 0x06009520 RID: 38176 RVA: 0x003D8396 File Offset: 0x003D6796
			public Transform Target
			{
				get
				{
					return this._target;
				}
				set
				{
					this._target = value;
				}
			}

			// Token: 0x040077D7 RID: 30679
			[SerializeField]
			private float _start;

			// Token: 0x040077D8 RID: 30680
			[SerializeField]
			private float _end = 1f;

			// Token: 0x040077D9 RID: 30681
			[SerializeField]
			private Transform _target;
		}
	}
}
