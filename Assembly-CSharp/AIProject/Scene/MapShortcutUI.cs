using System;
using System.Collections.Generic;
using AIProject.UI;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace AIProject.Scene
{
	// Token: 0x02000E9B RID: 3739
	public class MapShortcutUI : MenuUIBehaviour
	{
		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x0600789D RID: 30877 RVA: 0x0032D2AA File Offset: 0x0032B6AA
		// (set) Token: 0x0600789E RID: 30878 RVA: 0x0032D2B1 File Offset: 0x0032B6B1
		public static int ImageIndex { get; set; }

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x0600789F RID: 30879 RVA: 0x0032D2B9 File Offset: 0x0032B6B9
		// (set) Token: 0x060078A0 RID: 30880 RVA: 0x0032D2C0 File Offset: 0x0032B6C0
		public static Action ClosedEvent { get; set; }

		// Token: 0x060078A1 RID: 30881 RVA: 0x0032D2C8 File Offset: 0x0032B6C8
		protected override void Awake()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.MapShortcutUI = this;
			}
			if (Singleton<Manager.Input>.IsInstance())
			{
				this._validType = Singleton<Manager.Input>.Instance.State;
				Singleton<Manager.Input>.Instance.ReserveState(Manager.Input.ValidType.UI);
				Singleton<Manager.Input>.Instance.SetupState();
			}
			Sprite sprite;
			this._imageTable.TryGetValue(MapShortcutUI.ImageIndex, out sprite);
			this._image.sprite = sprite;
		}

		// Token: 0x060078A2 RID: 30882 RVA: 0x0032D338 File Offset: 0x0032B738
		protected override void Start()
		{
			this.Open();
			KeyCodeDownCommand keyCodeDownCommand = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Escape
			};
			keyCodeDownCommand.TriggerEvent.AddListener(delegate()
			{
				if (UnityEngine.Input.GetKey(KeyCode.F2))
				{
					return;
				}
				GameUtil.GameEnd(true);
			});
			this._keyCommands.Add(keyCodeDownCommand);
			KeyCodeDownCommand keyCodeDownCommand2 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.Mouse1
			};
			keyCodeDownCommand2.TriggerEvent.AddListener(delegate()
			{
				this.Close(delegate
				{
					UnityEngine.Object.Destroy(base.gameObject);
					GC.Collect();
					UnityEngine.Resources.UnloadUnusedAssets();
				});
			});
			this._keyCommands.Add(keyCodeDownCommand2);
			KeyCodeDownCommand keyCodeDownCommand3 = new KeyCodeDownCommand
			{
				KeyCode = KeyCode.F2
			};
			keyCodeDownCommand3.TriggerEvent.AddListener(delegate()
			{
				this.Close(delegate
				{
					UnityEngine.Object.Destroy(base.gameObject);
					GC.Collect();
					UnityEngine.Resources.UnloadUnusedAssets();
				});
			});
			this._keyCommands.Add(keyCodeDownCommand3);
			base.Start();
		}

		// Token: 0x060078A3 RID: 30883 RVA: 0x0032D401 File Offset: 0x0032B801
		protected override void OnDisable()
		{
			if (Singleton<Game>.IsInstance())
			{
				Singleton<Game>.Instance.MapShortcutUI = null;
			}
		}

		// Token: 0x060078A4 RID: 30884 RVA: 0x0032D418 File Offset: 0x0032B818
		private void Open()
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = x.Value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				this._canvasGroup.blocksRaycasts = true;
				base.EnabledInput = true;
			});
		}

		// Token: 0x060078A5 RID: 30885 RVA: 0x0032D484 File Offset: 0x0032B884
		private void Close(Action onCompleted)
		{
			base.EnabledInput = false;
			this._canvasGroup.blocksRaycasts = false;
			if (Singleton<Manager.Input>.IsInstance())
			{
				Singleton<Manager.Input>.Instance.ReserveState(this._validType);
				Singleton<Manager.Input>.Instance.SetupState();
			}
			ObservableEasing.Linear(0.2f, true).FrameTimeInterval(true).Subscribe(delegate(TimeInterval<float> x)
			{
				this._canvasGroup.alpha = 1f - x.Value;
			}, delegate(Exception ex)
			{
			}, delegate()
			{
				Action onCompleted2 = onCompleted;
				if (onCompleted2 != null)
				{
					onCompleted2();
				}
				Action closedEvent = MapShortcutUI.ClosedEvent;
				if (closedEvent != null)
				{
					closedEvent();
				}
			});
		}

		// Token: 0x0400618C RID: 24972
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x0400618D RID: 24973
		[SerializeField]
		private Image _image;

		// Token: 0x0400618E RID: 24974
		[SerializeField]
		private Dictionary<int, Sprite> _imageTable = new Dictionary<int, Sprite>();

		// Token: 0x0400618F RID: 24975
		private Manager.Input.ValidType _validType;
	}
}
