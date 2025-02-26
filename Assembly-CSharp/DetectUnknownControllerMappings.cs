using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002E5 RID: 741
public class DetectUnknownControllerMappings : MonoBehaviour
{
	// Token: 0x06000C8F RID: 3215 RVA: 0x00032518 File Offset: 0x00030918
	private void Update()
	{
		if (Input.GetAxis("Axis 1") > 0f)
		{
			this.axis1Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 1") < 0f)
		{
			this.axis1Value.text = "negative";
		}
		else
		{
			this.axis1Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 2") > 0f)
		{
			this.axis2Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 2") < 0f)
		{
			this.axis2Value.text = "negative";
		}
		else
		{
			this.axis2Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 3") > 0f)
		{
			this.axis3Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 3") < 0f)
		{
			this.axis3Value.text = "negative";
		}
		else
		{
			this.axis3Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 4") > 0f)
		{
			this.axis4Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 4") < 0f)
		{
			this.axis4Value.text = "negative";
		}
		else
		{
			this.axis4Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 5") > 0f)
		{
			this.axis5Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 5") < 0f)
		{
			this.axis5Value.text = "negative";
		}
		else
		{
			this.axis5Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 6") > 0f)
		{
			this.axis6Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 6") < 0f)
		{
			this.axis6Value.text = "negative";
		}
		else
		{
			this.axis6Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 7") > 0f)
		{
			this.axis7Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 7") < 0f)
		{
			this.axis7Value.text = "negative";
		}
		else
		{
			this.axis7Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 8") > 0f)
		{
			this.axis8Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 8") < 0f)
		{
			this.axis8Value.text = "negative";
		}
		else
		{
			this.axis8Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 9") > 0f)
		{
			this.axis9Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 9") < 0f)
		{
			this.axis9Value.text = "negative";
		}
		else
		{
			this.axis9Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 10") > 0f)
		{
			this.axis10Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 10") < 0f)
		{
			this.axis10Value.text = "negative";
		}
		else
		{
			this.axis10Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 11") > 0f)
		{
			this.axis11Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 11") < 0f)
		{
			this.axis11Value.text = "negative";
		}
		else
		{
			this.axis11Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 12") > 0f)
		{
			this.axis12Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 12") < 0f)
		{
			this.axis12Value.text = "negative";
		}
		else
		{
			this.axis12Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 13") > 0f)
		{
			this.axis13Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 13") < 0f)
		{
			this.axis13Value.text = "negative";
		}
		else
		{
			this.axis13Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 14") > 0f)
		{
			this.axis14Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 14") < 0f)
		{
			this.axis14Value.text = "negative";
		}
		else
		{
			this.axis14Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 15") > 0f)
		{
			this.axis15Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 15") < 0f)
		{
			this.axis15Value.text = "negative";
		}
		else
		{
			this.axis15Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 16") > 0f)
		{
			this.axis16Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 16") < 0f)
		{
			this.axis16Value.text = "negative";
		}
		else
		{
			this.axis16Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 17") > 0f)
		{
			this.axis17Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 17") < 0f)
		{
			this.axis17Value.text = "negative";
		}
		else
		{
			this.axis17Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 18") > 0f)
		{
			this.axis18Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 18") < 0f)
		{
			this.axis18Value.text = "negative";
		}
		else
		{
			this.axis18Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 19") > 0f)
		{
			this.axis19Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 19") < 0f)
		{
			this.axis19Value.text = "negative";
		}
		else
		{
			this.axis19Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 20") > 0f)
		{
			this.axis20Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 20") < 0f)
		{
			this.axis20Value.text = "negative";
		}
		else
		{
			this.axis20Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 21") > 0f)
		{
			this.axis21Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 21") < 0f)
		{
			this.axis21Value.text = "negative";
		}
		else
		{
			this.axis21Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 22") > 0f)
		{
			this.axis22Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 22") < 0f)
		{
			this.axis22Value.text = "negative";
		}
		else
		{
			this.axis22Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 23") > 0f)
		{
			this.axis23Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 23") < 0f)
		{
			this.axis23Value.text = "negative";
		}
		else
		{
			this.axis23Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 24") > 0f)
		{
			this.axis24Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 24") < 0f)
		{
			this.axis24Value.text = "negative";
		}
		else
		{
			this.axis24Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 25") > 0f)
		{
			this.axis25Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 25") < 0f)
		{
			this.axis25Value.text = "negative";
		}
		else
		{
			this.axis25Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 26") > 0f)
		{
			this.axis26Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 26") < 0f)
		{
			this.axis26Value.text = "negative";
		}
		else
		{
			this.axis26Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 27") > 0f)
		{
			this.axis27Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 27") < 0f)
		{
			this.axis27Value.text = "negative";
		}
		else
		{
			this.axis27Value.text = string.Empty;
		}
		if (Input.GetAxis("Axis 28") > 0f)
		{
			this.axis28Value.text = "positive";
		}
		else if (Input.GetAxis("Axis 28") < 0f)
		{
			this.axis28Value.text = "negative";
		}
		else
		{
			this.axis28Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton0))
		{
			this.button0Value.text = "pressed";
		}
		else
		{
			this.button0Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton1))
		{
			this.button1Value.text = "pressed";
		}
		else
		{
			this.button1Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton2))
		{
			this.button2Value.text = "pressed";
		}
		else
		{
			this.button2Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton3))
		{
			this.button3Value.text = "pressed";
		}
		else
		{
			this.button3Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton4))
		{
			this.button4Value.text = "pressed";
		}
		else
		{
			this.button4Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton5))
		{
			this.button5Value.text = "pressed";
		}
		else
		{
			this.button5Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton6))
		{
			this.button6Value.text = "pressed";
		}
		else
		{
			this.button6Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton7))
		{
			this.button7Value.text = "pressed";
		}
		else
		{
			this.button7Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton8))
		{
			this.button8Value.text = "pressed";
		}
		else
		{
			this.button8Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton9))
		{
			this.button9Value.text = "pressed";
		}
		else
		{
			this.button9Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton10))
		{
			this.button10Value.text = "pressed";
		}
		else
		{
			this.button10Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton11))
		{
			this.button11Value.text = "pressed";
		}
		else
		{
			this.button11Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton12))
		{
			this.button12Value.text = "pressed";
		}
		else
		{
			this.button12Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton13))
		{
			this.button13Value.text = "pressed";
		}
		else
		{
			this.button13Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton14))
		{
			this.button14Value.text = "pressed";
		}
		else
		{
			this.button14Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton15))
		{
			this.button15Value.text = "pressed";
		}
		else
		{
			this.button15Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton16))
		{
			this.button16Value.text = "pressed";
		}
		else
		{
			this.button16Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton17))
		{
			this.button17Value.text = "pressed";
		}
		else
		{
			this.button17Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton18))
		{
			this.button18Value.text = "pressed";
		}
		else
		{
			this.button18Value.text = string.Empty;
		}
		if (Input.GetKey(KeyCode.JoystickButton19))
		{
			this.button19Value.text = "pressed";
		}
		else
		{
			this.button19Value.text = string.Empty;
		}
	}

	// Token: 0x04000B6E RID: 2926
	public Text axis1Value;

	// Token: 0x04000B6F RID: 2927
	public Text axis2Value;

	// Token: 0x04000B70 RID: 2928
	public Text axis3Value;

	// Token: 0x04000B71 RID: 2929
	public Text axis4Value;

	// Token: 0x04000B72 RID: 2930
	public Text axis5Value;

	// Token: 0x04000B73 RID: 2931
	public Text axis6Value;

	// Token: 0x04000B74 RID: 2932
	public Text axis7Value;

	// Token: 0x04000B75 RID: 2933
	public Text axis8Value;

	// Token: 0x04000B76 RID: 2934
	public Text axis9Value;

	// Token: 0x04000B77 RID: 2935
	public Text axis10Value;

	// Token: 0x04000B78 RID: 2936
	public Text axis11Value;

	// Token: 0x04000B79 RID: 2937
	public Text axis12Value;

	// Token: 0x04000B7A RID: 2938
	public Text axis13Value;

	// Token: 0x04000B7B RID: 2939
	public Text axis14Value;

	// Token: 0x04000B7C RID: 2940
	public Text axis15Value;

	// Token: 0x04000B7D RID: 2941
	public Text axis16Value;

	// Token: 0x04000B7E RID: 2942
	public Text axis17Value;

	// Token: 0x04000B7F RID: 2943
	public Text axis18Value;

	// Token: 0x04000B80 RID: 2944
	public Text axis19Value;

	// Token: 0x04000B81 RID: 2945
	public Text axis20Value;

	// Token: 0x04000B82 RID: 2946
	public Text axis21Value;

	// Token: 0x04000B83 RID: 2947
	public Text axis22Value;

	// Token: 0x04000B84 RID: 2948
	public Text axis23Value;

	// Token: 0x04000B85 RID: 2949
	public Text axis24Value;

	// Token: 0x04000B86 RID: 2950
	public Text axis25Value;

	// Token: 0x04000B87 RID: 2951
	public Text axis26Value;

	// Token: 0x04000B88 RID: 2952
	public Text axis27Value;

	// Token: 0x04000B89 RID: 2953
	public Text axis28Value;

	// Token: 0x04000B8A RID: 2954
	public Text button0Value;

	// Token: 0x04000B8B RID: 2955
	public Text button1Value;

	// Token: 0x04000B8C RID: 2956
	public Text button2Value;

	// Token: 0x04000B8D RID: 2957
	public Text button3Value;

	// Token: 0x04000B8E RID: 2958
	public Text button4Value;

	// Token: 0x04000B8F RID: 2959
	public Text button5Value;

	// Token: 0x04000B90 RID: 2960
	public Text button6Value;

	// Token: 0x04000B91 RID: 2961
	public Text button7Value;

	// Token: 0x04000B92 RID: 2962
	public Text button8Value;

	// Token: 0x04000B93 RID: 2963
	public Text button9Value;

	// Token: 0x04000B94 RID: 2964
	public Text button10Value;

	// Token: 0x04000B95 RID: 2965
	public Text button11Value;

	// Token: 0x04000B96 RID: 2966
	public Text button12Value;

	// Token: 0x04000B97 RID: 2967
	public Text button13Value;

	// Token: 0x04000B98 RID: 2968
	public Text button14Value;

	// Token: 0x04000B99 RID: 2969
	public Text button15Value;

	// Token: 0x04000B9A RID: 2970
	public Text button16Value;

	// Token: 0x04000B9B RID: 2971
	public Text button17Value;

	// Token: 0x04000B9C RID: 2972
	public Text button18Value;

	// Token: 0x04000B9D RID: 2973
	public Text button19Value;
}
