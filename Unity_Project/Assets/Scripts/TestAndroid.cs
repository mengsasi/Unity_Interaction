using UnityEngine;
using UnityEngine.UI;

public class TestAndroid : MonoBehaviour {

    public Text Txt_Result;

    public Button BtnUnity_Android;

    public Button BtnAndroid_Unity;

    private void Awake() {
        BtnUnity_Android.onClick.AddListener( UnityCallAndroid );
        BtnAndroid_Unity.onClick.AddListener( AndroidCallUnity );
    }

    private void Start() {
        ShowResult( "测试" );
    }

    void ShowResult( string msg ) {
        Txt_Result.text += msg + "\n";
    }

    void UnityCallAndroid() {
        AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" );
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" );
        string message = "this is my title";
        string body = "this is my content";
        jo.Call( "ShareText", message, body );
    }

    void AndroidCallUnity() {
        AndroidJavaClass jc = new AndroidJavaClass( "com.unity3d.player.UnityPlayer" );
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>( "currentActivity" );
        string message = "unity-android-unity";
        jo.Call( "ShowMsg", message );
    }

    public void ShowMessage( string msg ) {
        ShowResult( msg );
    }
}
