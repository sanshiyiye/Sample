/*
* @classdesc LuaUIController
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.IO;
using Core.Log;
using FrameWork.Runtime;
using FrameWork.UI;
using UnityEngine;
using XLua;
using Logger = Core.Log.Logger;

public  class LuaUIController : UIController
{
        LuaTable _luaTable;
        public Transform transform;
        private Canvas _canvas;
        public object[] LastOnOpenArgs { get; private set; }
        
        public override void OnInit()
        {
            base.OnInit();
            if (!CheckInitScript(true))
                return;
        }

        public override void BeforeOpen(object[] args)
        {
            base.BeforeOpen(args);
            if (_luaTable == null)
            {
                //NOTE 如果需要每次都自动热重载，则每次都调用CheckInitScript，达到修改代码后实时生效
                if (!CheckInitScript())
                    return;
            }
            var onOpenFuncObj = _luaTable.Get<LuaFunction>("BeforeOpen");
            if (onOpenFuncObj != null)
            {
                var newArgs = new object[args.Length + 1];
                newArgs[0] = _luaTable;
                for (var i = 0; i < args.Length; i++)
                {
                    newArgs[i + 1] = args[i];
                }

                (onOpenFuncObj as LuaFunction).Call(newArgs);
            }

            
        }
        /// <summary>
        /// 调用Lua:OnOpen函数
        /// </summary>
        /// <param name="args"></param>
        public override void OnOpen(params object[] args)
        {
            LastOnOpenArgs = args;

            base.OnOpen(args);
            if (_luaTable == null)
            {
                //NOTE 如果需要每次都自动热重载，则每次都调用CheckInitScript，达到修改代码后实时生效
                if (!CheckInitScript())
                    return;
            }

            var onOpenFuncObj = _luaTable.Get<LuaFunction>("OnOpen");
            if (onOpenFuncObj == null)
            {
                Logger.Error(LogLevel.GAME,"Not Exists `OnOpen` in lua: {0}", UITemplateName);
                return;
            }

            var newArgs = new object[args.Length + 1];
            newArgs[0] = _luaTable;
            for (var i = 0; i < args.Length; i++)
            {
                newArgs[i + 1] = args[i];
            }

            (onOpenFuncObj as LuaFunction).Call(newArgs);
        }

        public override void OnClose()
        {
            base.OnClose();
            if (_luaTable == null)
            {
                if (!CheckInitScript())
                    return;
            }
            var closeFunc = _luaTable.Get<LuaFunction>("OnClose");
            if (closeFunc != null)
            {
                (closeFunc as LuaFunction).Call(_luaTable);
            }
        }
		
        public override void OnDestroy()
        {
            base.OnDestroy();
            if (_luaTable != null)
            {
                var destroyFunc = _luaTable.Get<LuaFunction>("OnDestroy");
                if (destroyFunc != null)
                {
                    (destroyFunc as LuaFunction).Call(_luaTable);
                }
            }
            _luaTable = null;
        }
        
        public string UILuaPath
        {
            get
            {
                var relPath = string.Format("UI"+Path.DirectorySeparatorChar +"{0}"+Path.DirectorySeparatorChar+"{0}", UITemplateName);
                return relPath;
            }
        }
        
        bool CheckInitScript(bool showWarn = false)
        {
            if (!GameEnv.CacheMode)
            {
                ClearLuaTableCache();
            }

            var relPath = UILuaPath;
            object scriptResult;
            
            if (!LuaModule.getInstance().TryImport(relPath, out scriptResult))
            {
                if (showWarn)
                    Logger.Info(LogLevel.GAME ,"Import UI Lua Script failed: {0}", relPath);
                return false;
            }
            Debuger.Assert(scriptResult is LuaTable, "{0} Script Must Return Lua Table with functions!", UITemplateName);

            _luaTable = scriptResult as LuaTable;

            var newFuncObj = _luaTable.Get<LuaFunction>("new"); // if a New function exist, new a table!
            if (newFuncObj != null)
            {

                var newTableObj = (newFuncObj as LuaFunction).Call(this);
                _luaTable = newTableObj[0] as LuaTable;
            }

            SetOutlet(_luaTable);
            //TODO 优化:只有代码变化才重新执行OnInit函数
            var luaInitObj = _luaTable.Get<LuaFunction>("OnInit");
            Debuger.Assert(luaInitObj is LuaFunction, "Must have OnInit function - {0}", UIName);

            // set table variable `Controller` to this

            _luaTable.SetInPath("Controller", this);
            (luaInitObj as LuaFunction).Call(_luaTable, this);

            return true;
        }
        public void SetOutlet(LuaTable _luaTable)
        {
            if (_luaTable != null)
            {
                Action<UILuaOutlet> fun = delegate (UILuaOutlet outlet)
                {
                    for (var i = 0; i < outlet.OutletInfos.Count; i++)
                    {
                        var outletInfo = outlet.OutletInfos[i];

                        var gameObj = outletInfo.Object as GameObject;
                        if (gameObj == null || outletInfo.ComponentType == typeof(UnityEngine.GameObject).FullName)
                        {
                            _luaTable.Set<string, UnityEngine.Object>(outletInfo.Name, outletInfo.Object);
                            continue;
                        }

                        if (outletInfo.ComponentType == typeof(UnityEngine.Transform).FullName)
                        {
                            _luaTable.Set<string, Component>(outletInfo.Name, gameObj.transform);
                        }
                        else
                        {
                            var comp = gameObj.GetComponent(outletInfo.ComponentType);
                            //UnityEngine.xxx，非UnityEngine.UI.xxx，只能通过typof获取。
                            if (comp == null && outletInfo.ComponentType.StartsWith("UnityEngine"))
                            {
                                //UnityEngine.xxx下的使用typeof获取
                                var comNames= outletInfo.ComponentType.Split('.');
                                if (!comNames[1].StartsWith("UI"))
                                {
                                    var components = gameObj.GetComponents<Component>();
                                    for (var c = 0; c < components.Length; c++)
                                    {
                                        var typeName = components[c].GetType().FullName;
                                        if (typeName == outletInfo.ComponentType)
                                        {
                                            comp = components[c];
                                            break;
                                        }
                                    }
                                }
                            }
                     
                            if (comp == null)
                            {
                                var fmt = "Missing Component `{0}` at object `{1}` which named `{2}`";
                                Logger.Error(LogLevel.GAME,string.Format(fmt, outletInfo.ComponentType, gameObj, outletInfo.Name));
                            }
                            else
                            {
                                _luaTable.Set<string, Component>(outletInfo.Name, comp);
                            }
                        }
                    }
                };


                UILuaOutletCollection outletCollection = mGameObject.GetComponent<UILuaOutletCollection>();
                if (outletCollection)
                {
                    if (outletCollection.UILuaOutlets != null && outletCollection.UILuaOutlets.Length > 0)
                    {
                        for (int i = 0; i < outletCollection.UILuaOutlets.Length; i++)
                        {
                            UILuaOutlet item = outletCollection.UILuaOutlets[i];
                            if (item != null)
                            {
                                fun(item);
                            }
                        }
                    }
                }
                else
                {
                    var outlet = mGameObject.GetComponent<UILuaOutlet>();
                    if (outlet != null)
                    {
                        fun(outlet);
                    }
                }

            }
        }


        private void ClearLuaTableCache()
        {
            _luaTable = null;
            LuaModule.getInstance().ClearCache(UILuaPath);
            
        }
}
