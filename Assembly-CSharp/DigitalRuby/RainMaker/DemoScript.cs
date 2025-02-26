using System;
using UnityEngine;
using UnityEngine.UI;

namespace DigitalRuby.RainMaker
{
	// Token: 0x020004DE RID: 1246
	public class DemoScript : MonoBehaviour
	{
		// Token: 0x06001715 RID: 5909 RVA: 0x00091700 File Offset: 0x0008FB00
		private void UpdateRain()
		{
			if (this.RainScript != null)
			{
				if (Input.GetKeyDown(KeyCode.Alpha1))
				{
					this.RainScript.RainIntensity = 0f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha2))
				{
					this.RainScript.RainIntensity = 0.2f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha3))
				{
					this.RainScript.RainIntensity = 0.5f;
				}
				else if (Input.GetKeyDown(KeyCode.Alpha4))
				{
					this.RainScript.RainIntensity = 0.8f;
				}
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x000917A0 File Offset: 0x0008FBA0
		private void UpdateMovement()
		{
			float num = 5f * Time.deltaTime;
			if (Input.GetKey(KeyCode.W))
			{
				Camera.main.transform.Translate(0f, 0f, num);
			}
			else if (Input.GetKey(KeyCode.S))
			{
				Camera.main.transform.Translate(0f, 0f, -num);
			}
			if (Input.GetKey(KeyCode.A))
			{
				Camera.main.transform.Translate(-num, 0f, 0f);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				Camera.main.transform.Translate(num, 0f, 0f);
			}
			if (Input.GetKeyDown(KeyCode.F))
			{
				this.FlashlightToggle.isOn = !this.FlashlightToggle.isOn;
			}
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x00091884 File Offset: 0x0008FC84
		private void UpdateMouseLook()
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.M))
			{
				this.MouseLookToggle.isOn = !this.MouseLookToggle.isOn;
			}
			if (!this.MouseLookToggle.isOn)
			{
				return;
			}
			if (this.axes == DemoScript.RotationAxes.MouseXAndY)
			{
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationX = DemoScript.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
				this.rotationY = DemoScript.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
				Quaternion rhs = Quaternion.AngleAxis(this.rotationX, Vector3.up);
				Quaternion rhs2 = Quaternion.AngleAxis(this.rotationY, -Vector3.right);
				base.transform.localRotation = this.originalRotation * rhs * rhs2;
			}
			else if (this.axes == DemoScript.RotationAxes.MouseX)
			{
				this.rotationX += Input.GetAxis("Mouse X") * this.sensitivityX;
				this.rotationX = DemoScript.ClampAngle(this.rotationX, this.minimumX, this.maximumX);
				Quaternion rhs3 = Quaternion.AngleAxis(this.rotationX, Vector3.up);
				base.transform.localRotation = this.originalRotation * rhs3;
			}
			else
			{
				this.rotationY += Input.GetAxis("Mouse Y") * this.sensitivityY;
				this.rotationY = DemoScript.ClampAngle(this.rotationY, this.minimumY, this.maximumY);
				Quaternion rhs4 = Quaternion.AngleAxis(-this.rotationY, Vector3.right);
				base.transform.localRotation = this.originalRotation * rhs4;
			}
		}

		// Token: 0x06001718 RID: 5912 RVA: 0x00091A75 File Offset: 0x0008FE75
		public void RainSliderChanged(float val)
		{
			this.RainScript.RainIntensity = val;
		}

		// Token: 0x06001719 RID: 5913 RVA: 0x00091A83 File Offset: 0x0008FE83
		public void MouseLookChanged(bool val)
		{
			this.MouseLookToggle.isOn = val;
		}

		// Token: 0x0600171A RID: 5914 RVA: 0x00091A91 File Offset: 0x0008FE91
		public void FlashlightChanged(bool val)
		{
			this.FlashlightToggle.isOn = val;
			this.Flashlight.enabled = val;
		}

		// Token: 0x0600171B RID: 5915 RVA: 0x00091AAB File Offset: 0x0008FEAB
		public void DawnDuskSliderChanged(float val)
		{
			this.Sun.transform.rotation = Quaternion.Euler(val, 0f, 0f);
		}

		// Token: 0x0600171C RID: 5916 RVA: 0x00091ACD File Offset: 0x0008FECD
		public void FollowCameraChanged(bool val)
		{
			this.RainScript.FollowCamera = val;
		}

		// Token: 0x0600171D RID: 5917 RVA: 0x00091ADC File Offset: 0x0008FEDC
		private void Start()
		{
			this.originalRotation = base.transform.localRotation;
			BaseRainScript rainScript = this.RainScript;
			float num = 0.5f;
			this.RainSlider.value = num;
			rainScript.RainIntensity = num;
			this.RainScript.EnableWind = true;
		}

		// Token: 0x0600171E RID: 5918 RVA: 0x00091B24 File Offset: 0x0008FF24
		private void Update()
		{
			this.UpdateRain();
			this.UpdateMovement();
			this.UpdateMouseLook();
		}

		// Token: 0x0600171F RID: 5919 RVA: 0x00091B38 File Offset: 0x0008FF38
		public static float ClampAngle(float angle, float min, float max)
		{
			if (angle < -360f)
			{
				angle += 360f;
			}
			if (angle > 360f)
			{
				angle -= 360f;
			}
			return Mathf.Clamp(angle, min, max);
		}

		// Token: 0x04001A6F RID: 6767
		public RainScript RainScript;

		// Token: 0x04001A70 RID: 6768
		public Toggle MouseLookToggle;

		// Token: 0x04001A71 RID: 6769
		public Toggle FlashlightToggle;

		// Token: 0x04001A72 RID: 6770
		public Slider RainSlider;

		// Token: 0x04001A73 RID: 6771
		public Light Flashlight;

		// Token: 0x04001A74 RID: 6772
		public GameObject Sun;

		// Token: 0x04001A75 RID: 6773
		private DemoScript.RotationAxes axes;

		// Token: 0x04001A76 RID: 6774
		private float sensitivityX = 15f;

		// Token: 0x04001A77 RID: 6775
		private float sensitivityY = 15f;

		// Token: 0x04001A78 RID: 6776
		private float minimumX = -360f;

		// Token: 0x04001A79 RID: 6777
		private float maximumX = 360f;

		// Token: 0x04001A7A RID: 6778
		private float minimumY = -60f;

		// Token: 0x04001A7B RID: 6779
		private float maximumY = 60f;

		// Token: 0x04001A7C RID: 6780
		private float rotationX;

		// Token: 0x04001A7D RID: 6781
		private float rotationY;

		// Token: 0x04001A7E RID: 6782
		private Quaternion originalRotation;

		// Token: 0x020004DF RID: 1247
		private enum RotationAxes
		{
			// Token: 0x04001A80 RID: 6784
			MouseXAndY,
			// Token: 0x04001A81 RID: 6785
			MouseX,
			// Token: 0x04001A82 RID: 6786
			MouseY
		}
	}
}
