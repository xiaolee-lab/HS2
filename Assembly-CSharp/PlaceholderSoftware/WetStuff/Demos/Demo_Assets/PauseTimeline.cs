using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace PlaceholderSoftware.WetStuff.Demos.Demo_Assets
{
	// Token: 0x020004CC RID: 1228
	public class PauseTimeline : MonoBehaviour
	{
		// Token: 0x060016AF RID: 5807 RVA: 0x0008B65B File Offset: 0x00089A5B
		private void Update()
		{
			if (this._isCameraFree)
			{
				this.FreeLook();
			}
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x0008B670 File Offset: 0x00089A70
		public void OnGUI()
		{
			if (this.ShowPauseTimeline)
			{
				PlayableDirector component = base.GetComponent<PlayableDirector>();
				Rect position = new Rect(20f, 50f, 130f, 30f);
				if (!this._isCameraFree)
				{
					if (GUI.Button(position, "Free Camera"))
					{
						this._isCameraFree = true;
						component.Pause();
					}
				}
				else if (GUI.Button(position, (!component.isActiveAndEnabled) ? "Lock Camera" : "Resume Timeline"))
				{
					this._isCameraFree = false;
					component.Resume();
				}
			}
			Rect position2 = new Rect(20f, 95f, 130f, 30f);
			if (GUI.Button(position2, "Back To Menu"))
			{
				SceneManager.LoadScene("0. Demo Menu");
			}
		}

		// Token: 0x060016B1 RID: 5809 RVA: 0x0008B740 File Offset: 0x00089B40
		private void FreeLook()
		{
			if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
			{
				base.transform.position = base.transform.position + base.transform.forward * 3f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
			{
				base.transform.position = base.transform.position + -base.transform.right * 3f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
			{
				base.transform.position = base.transform.position + -base.transform.forward * 3f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
			{
				base.transform.position = base.transform.position + base.transform.right * 3f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.E))
			{
				base.transform.position = base.transform.position + base.transform.up * 3f * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.Q))
			{
				base.transform.position = base.transform.position + -base.transform.up * 3f * Time.deltaTime;
			}
			if (this._isLooking)
			{
				float y = base.transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 3f;
				float x = base.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * 3f;
				base.transform.localEulerAngles = new Vector3(x, y, 0f);
			}
			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				this._isLooking = true;
			}
			else if (Input.GetKeyUp(KeyCode.Mouse1))
			{
				this._isLooking = false;
			}
		}

		// Token: 0x04001973 RID: 6515
		private const float MovementSpeed = 3f;

		// Token: 0x04001974 RID: 6516
		private const float FreeLookSensitivity = 3f;

		// Token: 0x04001975 RID: 6517
		private bool _isCameraFree;

		// Token: 0x04001976 RID: 6518
		private bool _isLooking;

		// Token: 0x04001977 RID: 6519
		public bool ShowPauseTimeline = true;
	}
}
