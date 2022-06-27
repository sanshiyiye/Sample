#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class FrameWorkUIUIModuleWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FrameWork.UI.UIModule);
			Utils.BeginObjectRegister(type, L, translator, 0, 8, 16, 10);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "InitProxy", _m_InitProxy);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenWindowAsync", _m_OpenWindowAsync);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "OpenWindow", _m_OpenWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "PreLoadUIWindow", _m_PreLoadUIWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CreateUIController", _m_CreateUIController);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "CloseWindow", _m_CloseWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "DestroyWindow", _m_DestroyWindow);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "TipsUIOrder", _g_get_TipsUIOrder);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NormalUIOrder", _g_get_NormalUIOrder);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MainUIOrder", _g_get_MainUIOrder);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UICamera", _g_get_UICamera);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "UIRoot", _g_get_UIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "MainUIRoot", _g_get_MainUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NormalUIRoot", _g_get_NormalUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "HeadInfoUIRoot", _g_get_HeadInfoUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GameUIRoot", _g_get_GameUIRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "LoadingUICount", _g_get_LoadingUICount);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "IsEditorLoadAsset", _g_get_IsEditorLoadAsset);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "mWindows", _g_get_mWindows);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onOpenHandlers", _g_get_onOpenHandlers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onInitHandlers", _g_get_onInitHandlers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "onCloseHandlers", _g_get_onCloseHandlers);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "CommonAtlases", _g_get_CommonAtlases);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "TipsUIOrder", _s_set_TipsUIOrder);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "NormalUIOrder", _s_set_NormalUIOrder);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "MainUIOrder", _s_set_MainUIOrder);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "UICamera", _s_set_UICamera);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "LoadingUICount", _s_set_LoadingUICount);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "mWindows", _s_set_mWindows);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onOpenHandlers", _s_set_onOpenHandlers);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onInitHandlers", _s_set_onInitHandlers);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "onCloseHandlers", _s_set_onCloseHandlers);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "CommonAtlases", _s_set_CommonAtlases);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FrameWork.UI.UIModule gen_ret = new FrameWork.UI.UIModule();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FrameWork.UI.UIModule constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_InitProxy(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.InitProxy(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenWindowAsync(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _UIName = LuaAPI.lua_tostring(L, 2);
                    object[] _objs = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.OpenWindowAsync( _UIName, _objs );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_OpenWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    object[] _param = translator.GetParams<object>(L, 3);
                    
                    gen_to_be_invoked.OpenWindow( _uiName, _param );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_PreLoadUIWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.PreLoadUIWindow(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CreateUIController(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    UnityEngine.GameObject _uiObj = (UnityEngine.GameObject)translator.GetObject(L, 2, typeof(UnityEngine.GameObject));
                    string _uiTemplateName = LuaAPI.lua_tostring(L, 3);
                    
                        FrameWork.UI.UIController gen_ret = gen_to_be_invoked.CreateUIController( _uiObj, _uiTemplateName );
                        translator.Push(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_CloseWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.CloseWindow( _uiName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_DestroyWindow(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 3)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    bool _destoryImmediate = LuaAPI.lua_toboolean(L, 3);
                    
                    gen_to_be_invoked.DestroyWindow( _uiName, _destoryImmediate );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.DestroyWindow( _uiName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FrameWork.UI.UIModule.DestroyWindow!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_TipsUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.TipsUIOrder);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NormalUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.NormalUIOrder);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MainUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.MainUIOrder);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UICamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UICamera);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_UIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.UIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MainUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MainUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NormalUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NormalUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_HeadInfoUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.HeadInfoUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GameUIRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.GameUIRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_LoadingUICount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                LuaAPI.xlua_pushinteger(L, gen_to_be_invoked.LoadingUICount);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_IsEditorLoadAsset(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                LuaAPI.lua_pushboolean(L, gen_to_be_invoked.IsEditorLoadAsset);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mWindows(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mWindows);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onOpenHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onOpenHandlers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onInitHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onInitHandlers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_onCloseHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.onCloseHandlers);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_CommonAtlases(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.CommonAtlases);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_TipsUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.TipsUIOrder = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_NormalUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.NormalUIOrder = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_MainUIOrder(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.MainUIOrder = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_UICamera(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.UICamera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_LoadingUICount(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.LoadingUICount = LuaAPI.xlua_tointeger(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mWindows(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mWindows = (System.Collections.Generic.Dictionary<string, FrameWork.UI.UIState>)translator.GetObject(L, 2, typeof(System.Collections.Generic.Dictionary<string, FrameWork.UI.UIState>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onOpenHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onOpenHandlers = translator.GetDelegate<System.Action<FrameWork.UI.UIController>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onInitHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onInitHandlers = translator.GetDelegate<System.Action<FrameWork.UI.UIController>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_onCloseHandlers(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.onCloseHandlers = translator.GetDelegate<System.Action<FrameWork.UI.UIController>>(L, 2);
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_CommonAtlases(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.UI.UIModule gen_to_be_invoked = (FrameWork.UI.UIModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.CommonAtlases = (System.Collections.Generic.List<string>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<string>));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
