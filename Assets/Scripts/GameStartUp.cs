using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Core;
using Core.Log;
using FrameWork.Runtime;
using FrameWork.UI;
// using FrameWork.Runtime;
// using FrameWork.UI;
using FrameWork.Utils;
using Sirenix.OdinInspector;
using UnityEngine;
using AppConfig = Core.AppConfig;
using ILogHandler = Core.Log.ILogHandler;
using Logger = Core.Log.Logger;
using LogType = Core.Log.LogType;

// using UnityNativeTool;

public class GameStartUp : BaseGame
{
    public bool isUpdate = false; // Start is called before the first frame update
    [Title("GameStart属性展示",bold: false)]
    [Delayed]
    [LabelText("玩家ID")]
    [PropertySpace]
    [ValidateInput("CheckDelayValue","",InfoMessageType.Error)]
    [DetailedInfoBox("test","Please Touch me",InfoMessageType.Info)]
    public int DelayValue;
    [ShowInInspector]
    [GUIColor(1,0,.5f)]
     public int DelayProperty { get; set; }
    // IEnumerator loadDLL()
    // {
    //     string path = Application.streamingAssetsPath + "Name";
    //     AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
    //     yield return request;
    //     AssetBundle bundle = request.assetBundle;
    //     TextAsset asset = bundle.LoadAsset("MyDLL", typeof(TextAsset)) as TextAsset;
    //     assembly = Assembly.Load(asset.bytes);
    //     Type type = assembly.GetType("DllManager");
    //     var com = this.gameObject.AddComponent(type);
    //     MethodInfo methodInfo = type.GetMethod("InitAssembly");
    //     methodInfo.Invoke(com, new object[] {isUpdate, assembly});
    //     
    // }

    public bool CheckDelayValue(int value,ref string ErroeMessage, ref InfoMessageType? type )
    {
        ErroeMessage = "DelayValue is error";
        return value > 100;
    }
    protected override List<IModule> CreateModules()
    {
        var modules =  base.CreateModules();
        
#if xLua 
        modules.Add(LuaModule.getInstance());
#endif
        /// 添加游戏自己的Modules
        ///
        /// 
         ConfigMgr.getInstance().Init();
        return modules;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnInit()
    {
        // Logger.AddLogHandler(LogType.SYS ,new DefaultLogHandler());
        Logger.init(true,3,new Dictionary<LogType, ILogHandler>()
        {
            {LogType.SYS,new DefaultLogHandler()} 
        });
        Logger.Info(LogLevel.GAME,"=====OnInit=======");
        AppConfig.IsEditorLoadAsset = true;
        //Update check
        //Init Setting 
        //Init Config
        //Preload Reousrce
        //Main Scene Enter

    }

    public override void CheckUpdate()
    {
        
    }
    
    public override void OnStart()
    {
        
        Logger.Info(LogLevel.GAME,"=====OnStart=======");
        Logger.Trace("test trace !");
        UIModule.getInstance().OpenWindow("UILogin",888);
        
    }
    
    
    public IEnumerator Test()
    { 
        yield return  new WaitForSeconds(10);
        
    }
    
    public void OnApplicationQuit()
    {
        
    }
}
