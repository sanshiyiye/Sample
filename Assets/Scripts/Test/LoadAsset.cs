/*
* @classdesc LoadAsset
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Log;
using FrameWork.Runtime;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Profiling;
using YooAsset;
using Debug = UnityEngine.Debug;
using ILogHandler = Core.Log.ILogHandler;
using Logger = Core.Log.Logger;
using LogType = Core.Log.LogType;

namespace Assets.Scripts.Test
{
    public class LoadAsset : MonoBehaviour
    {
        private async  Task Start()
        {
            Core.Log.Logger.init(true,3,new Dictionary<LogType, ILogHandler>()
            {
                {Core.Log.LogType.SYS,new DefaultLogHandler()} 
            });
            
            BetterStreamingAssets.Initialize();

            YooAssets.Initialize();
            
            var resourcePackage = YooAssets.TryGetPackage("MainBuildSetting");
            if (resourcePackage == null)
            {
                
            }
            // var operation = resourcePackage.LoadAssetSync<GameAssemblyManifest>(configuration.assemblyManifestAssetKey);
            // var manifest = operation.GetAssetObject<GameAssemblyManifest>();
        }
        private bool BetterStreamingAssets_CompressedStreamingAssetFound(string arg)
        {
            Debug.Log("====BetterStreamingAssets_CompressedStreamingAssetFound===="+arg);
            return false;
        }

        private void TestSerilize()
        {
           
            RProfiler.BeginWatch("testFloat");
            PlayerInfo p = new PlayerInfo(11,10,true,"w.l.hikaru");
            List<string> d = new List<string>();
            for (int i = 0; i < 20; i++)
            {
                d.Add("0"+i);
            }
            p.SetData(d);
            PlayerData pdata = new PlayerData();
            pdata.dataName = "playerData";
            pdata.dataD = "dataD";
            pdata.dataInt = -111;
            pdata.dataShort = (short) -23;
            pdata.dataf = 1.119f;
            pdata.dataDouble = 123.23d;
            pdata.vect2 = new Vector2(10.0f, 21.3f);
            pdata.dataLong = 1223l;
            p.playerData = pdata;
            // string k = "w.l.hikaru";
            // ByteStream bt = new ByteStream();
            // bt.WriteFloat(1.14f);
            // var b = bt.ReadFloat();
            // var bbf = BitConverter.GetBytes(1.14f);
            // var f = BitConverter.ToSingle(bbf,0);
            // bt.WriteString(k);
            // bt.WriteString(pdata.dataName);
            // bt.WriteString(pdata.dataD);
            // bt.WriteShort(pdata.dataShort);
            // bt.WriteInt(pdata.dataInt);
            // var a = bt.ReadString();
            // var a1 = bt.ReadString();
            // var a2 = bt.ReadString();
            // var a3 = bt.ReadShort();
            PlayerInfo p2 = new PlayerInfo();
            Dictionary<string, PlayerInfo> dic = new Dictionary<string, PlayerInfo>();
            dic.Add("1",p);
            dic.Add("2",p2);
            ByteStream steam = new ByteStream(2,Allocator.Temp);
            // unsafe
            // {
            //     byte[] bytes = null;
            // }
            //
            // steam.WriteObjectDictionary(dic);
            //
            // var pdic = steam.ReadObjectDictionary<string, PlayerInfo>();
            // foreach (var c in pdic)
            // {
            //     Logger.Info(LogLevel.GAME,"====Key===="+c.Key);
            //     PlayerInfo playerInfo = c.Value;
            //     Logger.Info(LogLevel.GAME,"====Value===="+c.Key+ playerInfo.age);
            //     Logger.Info(LogLevel.GAME,"====Value===="+c.Key+ playerInfo.age);
            // }
            // // steam.WriteObject(p);
            // //
            // // var pp = steam.ReadObject<PlayerInfo>();
            // steam.Free();
            // return;
            // int size = p.length;
            // for (int i = 0; i < 1000; i++)
            // {
            Profiler.logFile = "mylog"; //Also supports passing "myLog.raw"
            Profiler.enableBinaryLog = true;
            Profiler.enabled = true;
                Test1(p, steam);
                // steam.Expend(size);
            // }
            Profiler.enabled = false;
            Profiler.logFile = "";
            // Test2();
            
            RProfiler.StopWatch("testFloat");
        }

        public void Test2()
        {
            Texture2D t = new Texture2D(512,512,TextureFormat.RGBA32,false);
            for (int i = 0; i < 100000; i++)
            {
                NativeArray<byte> array = t.GetRawTextureData<byte>();
                TextureFormat f = t.format;
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (j == 200 && k == 200)
                            array[k * 512 + j] = 127;
                    }
                }
                // t.LoadRawTextureData(array);
                NativeArray<byte> array2 = t.GetRawTextureData<byte>();
                for (int j = 0; j < 512; j++)
                {
                    for (int k = 0; k < 512; k++)
                    {
                        if (j == 200 && k == 200)
                        {
                            byte b;
                            b = array2[k * 512 + j];
                            // Debug.Log(b);
                        }
                        else if (j == 200 && k == 201)
                        {
                             var b = array2[k * 512 + j];
                        }
                    }
                }
            }
            
            

        }
        

        public void Test1(PlayerInfo p,ByteStream steam)
        {
            
            // RProfiler
            int size = p.length;
            steam.Allocat(size, Allocator.Temp);
            p.Serialize(steam);
           
            for (int i = 0; i < 10000; i++)
            {
                
                PlayerInfo pp = new PlayerInfo();
                pp.DeSerialize(steam);
                steam.Reset();

            }
            steam.Free();
            
            // ByteStream stream = new ByteStream();
            // stream.WriteInt(p.age);
            // stream.WriteShort(p.atk);
            // stream.WriteBool(p.sex);
            // stream.WriteString(p.name);
            //
            // var in2 = stream.ReadInt();
            // var sh2 = stream.ReadShort();
            // var bl2 = stream.ReadBool();
            // var st2 = stream.ReadString();
            // Stopwatch s = new Stopwatch();
            // s.Start();
            // stream.Free();
            // s.Stop();
            // TimeSpan timeSpan = s.Elapsed;
            // double millseconds = timeSpan.TotalMilliseconds;
            // decimal seconds = (decimal) millseconds / 1000m;
            // Debug.Log("===== "+seconds+"s ======");
            // for (int i = 0; i < 10000; i++)
            // {
            //     GetData(p);
            // }
        }

        
        
        
        private unsafe byte[] GetData(PlayerInfo p)
        {

            // NativeArray<byte> array = new NativeArray<byte>(p.length, Allocator.Temp);
            byte[] tmpBytes = new byte[p.length];
            fixed (byte* a = tmpBytes)
            {
                ByteUtils.WriteInt(a,p.age);
                ByteUtils.WriteShort(a,p.atk);
                ByteUtils.WriteBool(a, p.sex);
                ByteUtils.WriteString(a, p.name);
            }
            List<byte> temp = new List<byte>();
            int index = 0;
            temp.AddRange(BitConverter.GetBytes((int)p.age));
            // index += sizeof(int);
            temp.AddRange(BitConverter.GetBytes((short)p.atk));
            // index += sizeof(short);
            temp.AddRange(BitConverter.GetBytes((bool)p.sex));
            // index += sizeof(bool);
            temp.AddRange(Encoding.UTF8.GetBytes(p.name));
            //
            // array.CopyFrom(BitConverter.GetBytes(p.sex));
            // array.CopyFrom(Encoding.UTF8.GetBytes(p.name));
            // array.CopyTo(tmpBytes);
            return temp.ToArray();
        }
    }
}