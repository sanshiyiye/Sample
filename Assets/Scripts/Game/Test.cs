
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Collections;
using UnityEditor;
using UnityEngine;


/**
* @classdesc Test
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
using Moonjoy.Core.Utils;
[RequireComponent(typeof(Rigidbody))]
public class Test : MonoBehaviour
{
    //private List<MyStruct> list = new List<MyStruct>(100001);
    public GameObject obj;
    
    private MyStruct a;
    private void Awake()
    {
        a = new MyStruct();
        
        a.x = 1;
        a.y = 2;
        //list.Add(a);
        
    }

    private void Update()
    {
        if (isB)
        {
            for (int i = 0; i < sList.Count; i++)
            {
                sList[i].transform.position = new Vector3(i,i,i);
            
            }

            isB = false;
        }
        
    }

    List<GameObject> sList = new List<GameObject>(10000);
    
    private bool isB = true;
    private void Start()
    {
        // Type type = list[0].GetType();
        // FieldInfo info =  type.GetField("x");
        // Object boxing = list[0];
        // info.SetValue(boxing,(short)2);
        // GCHandle h = GCHandle.Alloc(list[0]);
        // IntPtr p = GCHandle.ToIntPtr(h);
        // //MyStruct  my = (MyStruct) Marshal.PtrToStructure(p, typeof(MyStruct));
        // //my.x = 2;
        // var my = GCHandle.FromIntPtr(p).Target;
        // ((MyStruct)my).x = 2;
        //MoonList<MyStruct> list2 = new MoonList<MyStruct>();
        
        // Profiler.BeginSample("MyStruct");
        Stopwatch watch = new Stopwatch();
        TextMeshProUGUI txt = new TextMeshProUGUI();
        
        // ArrayPool<MyStruct> pp = ArrayPool<MyStruct>.Shared;
        //
        // MyStruct[] str = pp.Rent(100000);
        watch.Start();
        //list.Capacity = 100000;
        // List<Vector3> vList = new List<Vector3>(10000);
        // unsafe
        // {
        //     // /var array = stackalloc MyStruct[100000];
        //     for (int i = 0; i < 10000; i++)
        //     {
        //         vList.Add(new Vector3(i,i,i));
        //     }
        // }
        GameObject defaultObj = new GameObject();
        defaultObj.transform.position = Vector3.zero;
        defaultObj.transform.localPosition = Vector3.zero;
        // defaultObj.transform.rotation = new Quaternion();
        // List<XYZ> sList = new List<XYZ>(10000);
        TMP_TextInfo t = new TMP_TextInfo();
         NativeArray<XYZ> ll = new NativeArray<XYZ>(10000,Allocator.Temp);
        
        for (short i = 0; i < 10000; i++)
        {
            ll[i] = new XYZ(i,i,i);
        }

        ll.Dispose();
#if  UNITY_EDITOR
        var obj = AssetDatabase.LoadAssetAtPath("Assets/AssetsPackage/Test/Panel.prefab",typeof(GameObject));
        TextMeshProUGUI[] arr =  ((GameObject)obj).transform.GetComponentsInChildren<TextMeshProUGUI>();
        for (int i = 0; i < arr.Length; i++)
        {
            arr[i] = txt;
        }
#endif
       
        // GameObject inObj = Instantiate(obj);
        // inObj.transform.SetParent(this.transform.parent);
        // var type2 = Assembly.Load("UnityEngine").GetType("UnityEngine.NoAllocHelpers");
        // var method = type2.GetMethod("ExtractArrayFromListT", BindingFlags.Static | BindingFlags.Public);
        // var generic = method.MakeGenericMethod(typeof(MyStruct));
        // var result = generic.Invoke(null, new[] { list });
        // var array = result as MyStruct[];
        //pp.Return(str,true);
        
        // Array tmp = list2.ToArray();
        watch.Stop();
        // Profiler.EndSample();
       

    }

    struct XYZ
    {
        public XYZ(short mx, short my, short mz)
        {
            this.x = mx;
            this.y = my;
            this.z = mz;
        }
        private short x;
        private short y;
        private short z;
    }
}
