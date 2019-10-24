using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class TestIOS : MonoBehaviour {

    public Text Txt_Result;

    public Button BtnUnity_IOS;

    public Button BtnIOS_Unity;

    static TestIOS Instance;

    private void Awake() {
        Instance = this;
        BtnUnity_IOS.onClick.AddListener( UnityCallIOS );
        BtnIOS_Unity.onClick.AddListener( IOSCallUnity );
    }

    private void Start() {
        ShowResult( "测试" );
    }

    void ShowResult( string msg ) {
        Txt_Result.text += msg + "\n";
    }


    //unity调用ios
    //ios中有 outputAppendString (char *str1, char *str2) 方法
    //引入声明
    [DllImport( "__Internal" )]
    static extern void outputAppendString( string str1, string str2 );

    void UnityCallIOS() {
#if UNITY_IPHONE
        //调用ios中的方法
        outputAppendString("Hello", "World");
#endif
    }


    //ios调用unity
    //UnitySendMessage("Game", "callback", resultStr.UTF8String);
    void callback( string resultStr ) {
        Debug.LogFormat( "result string = {0}", resultStr );
        ShowResult( "result string = " + resultStr );
    }

    //非托管方法
    [UnmanagedFunctionPointer( CallingConvention.Cdecl )]
    public delegate void ResultHandler( string resultString );

    [MonoPInvokeCallback( typeof( ResultHandler ) )]
    static void resultHandler( string resultStr ) {
        Debug.LogFormat( "result string = {0}", resultStr );
        Instance.ShowResult( "result string = " + resultStr );
    }

    //引入outputAppendString2
    [DllImport( "__Internal" )]
    static extern void outputAppendString2( string str1, string str2, IntPtr resultHandler );

    void IOSCallUnity() {
        ResultHandler handler = new ResultHandler( resultHandler );
        IntPtr fp = Marshal.GetFunctionPointerForDelegate( handler );
        outputAppendString2( "Hello", "World", fp );
    }

    private void OnDestroy() {
        Instance = null;
    }
}
