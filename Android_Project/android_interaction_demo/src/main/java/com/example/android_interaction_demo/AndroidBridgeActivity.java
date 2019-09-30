package com.example.android_interaction_demo;

import android.content.Intent;
import android.os.Bundle;
import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerActivity;

public class AndroidBridgeActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
    }

    //分享
    public void ShareText(String message, String body) {
        Intent sharingIntent = new Intent(android.content.Intent.ACTION_SEND);
        sharingIntent.setType("text/plain");
        sharingIntent.putExtra(android.content.Intent.EXTRA_SUBJECT, message);
        sharingIntent.putExtra(android.content.Intent.EXTRA_TEXT, body);
        startActivity(Intent.createChooser(sharingIntent, "Share via"));
    }

    //安卓调用Unity
    public void ShowMsg(String msg){
        DisposeUnityFunc("Game","ShowMessage", msg);
    }

    public void DisposeUnityFunc(String obj, String func, String param){
        // 第一个参数是unity中的对象名字，记住是对象名字，不是脚本类名
        // 第二个参数是函数名
        // 第三个参数是传给函数的参数，目前只看到一个参数，并且是string的，自己传进去转吧
        UnityPlayer.UnitySendMessage(obj, func, param);
    }

}
