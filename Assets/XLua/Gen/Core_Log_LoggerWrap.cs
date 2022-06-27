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
    public class CoreLogLoggerWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(Core.Log.Logger);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 7, 0, 0);
			Utils.RegisterFunc(L, Utils.CLS_IDX, "AddLogHandler", _m_AddLogHandler_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Info", _m_Info_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Error", _m_Error_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "Trace", _m_Trace_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "init", _m_init_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "open", _m_open_xlua_st_);
            
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					Core.Log.Logger gen_ret = new Core.Log.Logger();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Log.Logger constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddLogHandler_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    Core.Log.LogType _inType;translator.Get(L, 1, out _inType);
                    Core.Log.ILogHandler _inLogHandler = (Core.Log.ILogHandler)translator.GetObject(L, 2, typeof(Core.Log.ILogHandler));
                    
                    Core.Log.Logger.AddLogHandler( _inType, _inLogHandler );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Info_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    byte _lv = (byte)LuaAPI.xlua_tointeger(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    object[] _param = translator.GetParams<object>(L, 3);
                    
                    Core.Log.Logger.Info( _lv, _content, _param );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Error_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
                
                {
                    byte _lv = (byte)LuaAPI.xlua_tointeger(L, 1);
                    string _content = LuaAPI.lua_tostring(L, 2);
                    object[] _param = translator.GetParams<object>(L, 3);
                    
                    Core.Log.Logger.Error( _lv, _content, _param );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_Trace_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    string _content = LuaAPI.lua_tostring(L, 1);
                    
                    Core.Log.Logger.Trace( _content );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_init_xlua_st_(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
            
			    int gen_param_count = LuaAPI.lua_gettop(L);
            
                if(gen_param_count == 3&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)&& translator.Assignable<System.Collections.Generic.Dictionary<Core.Log.LogType, Core.Log.ILogHandler>>(L, 3)) 
                {
                    bool _enable = LuaAPI.lua_toboolean(L, 1);
                    byte _initvalue = (byte)LuaAPI.xlua_tointeger(L, 2);
                    System.Collections.Generic.Dictionary<Core.Log.LogType, Core.Log.ILogHandler> _logHandlers = (System.Collections.Generic.Dictionary<Core.Log.LogType, Core.Log.ILogHandler>)translator.GetObject(L, 3, typeof(System.Collections.Generic.Dictionary<Core.Log.LogType, Core.Log.ILogHandler>));
                    
                    Core.Log.Logger.init( _enable, _initvalue, _logHandlers );
                    
                    
                    
                    return 0;
                }
                if(gen_param_count == 2&& LuaTypes.LUA_TBOOLEAN == LuaAPI.lua_type(L, 1)&& LuaTypes.LUA_TNUMBER == LuaAPI.lua_type(L, 2)) 
                {
                    bool _enable = LuaAPI.lua_toboolean(L, 1);
                    byte _initvalue = (byte)LuaAPI.xlua_tointeger(L, 2);
                    
                    Core.Log.Logger.init( _enable, _initvalue );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
            return LuaAPI.luaL_error(L, "invalid arguments to Core.Log.Logger.init!");
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_open_xlua_st_(RealStatePtr L)
        {
		    try {
            
            
            
                
                {
                    byte _invalue = (byte)LuaAPI.xlua_tointeger(L, 1);
                    
                    Core.Log.Logger.open( _invalue );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        
        
		
		
		
		
    }
}
