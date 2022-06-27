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
    public class FrameWorkRuntimeResourceResourceModuleWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(FrameWork.Runtime.Resource.ResourceModule);
			Utils.BeginObjectRegister(type, L, translator, 0, 4, 1, 1);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "Init", _m_Init);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadAssets", _m_LoadAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadUIAssets", _m_LoadUIAssets);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "LoadSprite", _m_LoadSprite);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "mAssetsLoadController", _g_get_mAssetsLoadController);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "mAssetsLoadController", _s_set_mAssetsLoadController);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 3, 1, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "loadFileBytes", _m_loadFileBytes_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "ReadAllBytes", _m_ReadAllBytes_xlua_st_);
            
			
            
			Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);
            
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					FrameWork.Runtime.Resource.ResourceModule gen_ret = new FrameWork.Runtime.Resource.ResourceModule();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to FrameWork.Runtime.Resource.ResourceModule constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Init(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    
                    gen_to_be_invoked.Init(  );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    FrameWork.UI.UIState _uistate = (FrameWork.UI.UIState)translator.GetObject(L, 2, typeof(FrameWork.UI.UIState));
                    
                    gen_to_be_invoked.LoadAssets( _uistate );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadUIAssets(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FrameWork.Runtime.Resource.ResourceModule.CKPrefabLoaderDelegate>(L, 3)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    FrameWork.Runtime.Resource.ResourceModule.CKPrefabLoaderDelegate _callback = translator.GetDelegate<FrameWork.Runtime.Resource.ResourceModule.CKPrefabLoaderDelegate>(L, 3);
                    
                    gen_to_be_invoked.LoadUIAssets( _uiName, _callback );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)) 
                {
                    string _uiName = LuaAPI.lua_tostring(L, 2);
                    
                    gen_to_be_invoked.LoadUIAssets( _uiName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FrameWork.Runtime.Resource.ResourceModule.LoadUIAssets!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_loadFileBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _path = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = FrameWork.Runtime.Resource.ResourceModule.loadFileBytes( _path );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_LoadSprite(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 4&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)&& translator.Assignable<FrameWork.Runtime.Resource.ResourceModule.CKSpriteAtlasLoaderDelegate>(L, 4)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    string _spriteName = LuaAPI.lua_tostring(L, 3);
                    FrameWork.Runtime.Resource.ResourceModule.CKSpriteAtlasLoaderDelegate _callBack = translator.GetDelegate<FrameWork.Runtime.Resource.ResourceModule.CKSpriteAtlasLoaderDelegate>(L, 4);
                    
                    gen_to_be_invoked.LoadSprite( _path, _spriteName, _callBack );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 3&& (LuaAPI.lua_isnil(L, 2) || LuaAPI.lua_type(L, 2) == LuaTypes.LUA_TSTRING)&& (LuaAPI.lua_isnil(L, 3) || LuaAPI.lua_type(L, 3) == LuaTypes.LUA_TSTRING)) 
                {
                    string _path = LuaAPI.lua_tostring(L, 2);
                    string _spriteName = LuaAPI.lua_tostring(L, 3);
                    
                    gen_to_be_invoked.LoadSprite( _path, _spriteName );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to FrameWork.Runtime.Resource.ResourceModule.LoadSprite!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ReadAllBytes_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _resPath = LuaAPI.lua_tostring(L, 1);
                    
                        byte[] gen_ret = FrameWork.Runtime.Resource.ResourceModule.ReadAllBytes( _resPath );
                        LuaAPI.lua_pushstring(L, gen_ret);
                    
                    
                    
                    return 1;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			    translator.Push(L, FrameWork.Runtime.Resource.ResourceModule.Instance);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_mAssetsLoadController(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.mAssetsLoadController);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_mAssetsLoadController(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                FrameWork.Runtime.Resource.ResourceModule gen_to_be_invoked = (FrameWork.Runtime.Resource.ResourceModule)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.mAssetsLoadController = (FrameWork.Runtime.Resource.AssetsLoadController)translator.GetObject(L, 2, typeof(FrameWork.Runtime.Resource.AssetsLoadController));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
