/*
* @classdesc UIMain
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using FrameWork.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class UIBillboard : UIController
    {
        private Button button = null;
        public override void OnInit()
        {
            base.OnInit(); 
            button = mGameObject.FindChild<Button>("btn_close");
            UIClickLisener.Get(button, OnClick);
        }
        public void OnClick(MonoBehaviour go)
        {
            UIModule.getInstance().CloseWindow("UIBillboard");
        }
    }
