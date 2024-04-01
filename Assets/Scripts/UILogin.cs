/*
* @classdesc UILogin
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using FrameWork.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UILogin : UIController
{
    private Button button = null;
    private Button openLogin = null;
    public override void OnInit()
    {
        base.OnInit(); 
        // button = mGameObject.FindChild<Button>("CloseBtn");
        // UIClickLisener.Get(button, OnClick);
        // openLogin = mGameObject.FindChild<Button>("OpenLoginBtn");
        // UIClickLisener.Get(openLogin, OpenLogin);
    }

    public void OnClick(MonoBehaviour go)
    {
        // UIModule.getInstance().CloseWindow("UILogin");
        // UIModule.getInstance().DestroyWindow("UILogin",true);
        UIModule.getInstance().OpenWindow("UIBillboard");
    }

    public void OpenLogin(MonoBehaviour go)
    {
        UIModule.getInstance().CloseWindow("UIBillboard");
        UIModule.getInstance().OpenWindow("UILogin");
    }
}
