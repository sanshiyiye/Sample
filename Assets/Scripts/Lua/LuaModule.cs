/*
* @classdesc LuaModule1
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Core;
using FrameWork.Runtime;
using XLua;
using XLua.LuaDLL;
using LuaSvr = XLua.LuaEnv;
using LuaDLL = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;

public class LuaModule : Singleton<LuaModule> ,IModule
{
        private readonly LuaEnv _luaEnv;
        public LuaEnv ENV
        {
            get
            {
                return _luaEnv;
                
            }
        }
        // public static LuaModule Instance = new LuaModule();

        public bool IsInited { get; private set; }

        private double _initProgress = 0;

        public double InitProgress { get { return _initProgress; } }
        
        /// <summary>
        /// 是否开启缓存模式，默认true，首次执行将把执行结果table存起来；在非缓存模式下，可以通过编辑器的Reload来进行强制刷新缓存
        /// 对实时性重载要求高的，可以把开关设置成false，长期都进行Lua脚本重载，理论上会消耗额外的性能用于语法解析
        /// </summary>
        public static bool CacheMode = true;

        /// <summary>
        /// Import result object caching
        /// </summary>
        Dictionary<string, object> _importCache = new Dictionary<string, object>();
        
        public LuaModule()
        {
            
            _luaEnv = new LuaEnv();
        }
        
        public void Init()
        {
            var L = _luaEnv.L;
            //在lua G中增加import函数
            LuaDLL.lua_pushstdcallcfunction(L, LuaImport);
            LuaDLL.xlua_setglobal(L, "import");
            
            //TODO 第三方库加载
            // _luaEnv.AddBuildin("rapidjson", XLua.LuaDLL.Lua.LoadRapidJson);
            // _luaEnv.AddBuildin("lpeg", XLua.LuaDLL.Lua.LoadLpeg);
            // _luaEnv.AddBuildin("pb", XLua.LuaDLL.Lua.LoadLuaProfobuf);
            // _luaEnv.AddBuildin("ffi", XLua.LuaDLL.Lua.LoadFFI);
            CallScript("Init");
            IsInited = true;
            //TODO 启动luaUpdate
            
        }

        public void Destroy()
        {
            
        }


        public void ClearData()
        {
            ClearAllCache();
        }
        
        public void ClearAllCache()
        {
            if (!CacheMode) return;
            _importCache.Clear();
        }
        public bool ClearCache(string uiLuaPath)
        {
            if (!CacheMode) return false;
            return _importCache.Remove(uiLuaPath);
        }
        
        [MonoPInvokeCallback(typeof(lua_CSFunction))]
        internal static int LuaImport(RealStatePtr L)
        {
            LuaModule luaModule = GetInstance();

            string fileName = Lua.lua_tostring(L, 1);
            var obj = luaModule.Import(fileName);

            ObjectTranslator ot = ObjectTranslatorPool.Instance.Find(L);
            ot.PushAny(L, obj);
            ot.PushAny(L, true);

            return 2;

        }
        public object Import(string fileName)
        {
            object obj;

            //NOTE 优先从cache获取
            if (CacheMode && _importCache.TryGetValue(fileName, out obj))
            {
                return obj;
            }

          //TODO 检查脚本是否存在

            return DoImportScript(fileName);
        }
        
        object DoImportScript(string fileName)
        {
            object obj;
            if (CacheMode)
            {
                if (!_importCache.TryGetValue(fileName, out obj))
                {
                    obj = CallScript(fileName);
                    _importCache[fileName] = obj;
                }    
            }
            else
            {
                obj = CallScript(fileName);
            }
            
            return obj;
        }
        
        public object CallScript(string scriptRelativePath)
        {
            if (scriptRelativePath == null || string.IsNullOrEmpty(scriptRelativePath))
            {
                return null;
            }
            var scriptPath = GetScriptPath(scriptRelativePath);
            //TODO  检查Lua文件是否存在
            byte[] script =  ResourceModule.loadFileBytes(scriptPath);
            var ret = ExecuteScript(script, scriptRelativePath);
            return ret;
        }
        
        public object ExecuteScript(byte[] scriptCode, string file = "chunk")
        {
            object ret;
            ExecuteScript(scriptCode, out ret, file);
            return ret;
        }
        public bool ExecuteScript(byte[] scriptCode, out object ret, string file = "chunk")
        {
            var results = _luaEnv.DoString(Encoding.UTF8.GetString(scriptCode), file);

            if (results != null && results.Length == 1)
            {
                ret = results[0];
            }
            else
            {
                ret = results;
            }
            return true;
        }
        
        static string GetScriptPath(string scriptRelativePath)
        {
            return string.Format("{0}/{1}.lua", GameEnv.LuaPath, scriptRelativePath);
        }

        public bool TryImport(string fileName, out object result)
        {
            result = null;

            //TODO 检查脚本是否存在

            result = DoImportScript(fileName);
            return true;
        }
    }