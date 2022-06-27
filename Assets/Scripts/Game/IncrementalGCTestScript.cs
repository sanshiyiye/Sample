using System;
using UnityEngine;
using UnityEngine.Scripting;

/**
* @classdesc IncrementalGCTestScript
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/
public class IncrementalGCTestScript : MonoBehaviour
{
    private int listSize = 1000;
    private int newListSize;
    string[] vsyncLabels = new[]{
        "No VSync",
        "Every VSync",
        "Every other VSync"	
    };	
    const int kNumLinkedLists = 1000;
    float sleepMs=0;
    
    class ReferenceContainer
    {
        public ReferenceContainer rf;
    }
    ReferenceContainer[] refs = new ReferenceContainer[kNumLinkedLists];
    int frame = 0;    
    ReferenceContainer MakeLinkedList()
    {
        ReferenceContainer rf = null;
        for (int i =0; i < listSize; i++)
        {
            ReferenceContainer link = new ReferenceContainer();
            link.rf = rf;
            rf = link;
        }
        return rf;
    }
    
    private void Start()
    {
        QualitySettings.vSyncCount = 1;
        newListSize = listSize;
    }

    private void OnGUI()
    {
        GUIStyle titleStyle2 = new GUIStyle();
        titleStyle2.fontSize = 14;
        titleStyle2.normal.textColor = new Color(46f/256f, 163f/256f, 256f/256f, 256f/256f);

        GUILayout.Label("Vsync Setting");
        QualitySettings.vSyncCount = GUILayout.SelectionGrid(QualitySettings.vSyncCount
            , vsyncLabels, 1);
        if (QualitySettings.vSyncCount == 0)
        {
            GUILayout.Label("Application.targetFrameRate: "+Application.targetFrameRate,titleStyle2);
            Application.targetFrameRate = (int)GUILayout.HorizontalSlider(Application.targetFrameRate, -1 , 100, GUILayout.Width(100));
        }			
        
        GUILayout.Label("Sleep time per frame: "+sleepMs+ "ms",titleStyle2);
        sleepMs = GUILayout.HorizontalSlider(sleepMs, 0.0f , 26.0f, GUILayout.Width(100));
    	
        GUILayout.Label("Size of Linked lists: "+listSize,titleStyle2);
        if (newListSize != listSize && Event.current.type == EventType.MouseUp)
        {
            listSize = newListSize;
            Init();
        }
        newListSize = (int)GUILayout.HorizontalSlider(newListSize, 1 , 5000, GUILayout.Width(100));

		
        GUILayout.Label("GarbageCollector.incrementalTimeSliceNanoseconds: "+((double)GarbageCollector.incrementalTimeSliceNanoseconds/1000.0) + " us",titleStyle2);
        GarbageCollector.incrementalTimeSliceNanoseconds = (ulong)(GUILayout.HorizontalSlider((float)GarbageCollector.incrementalTimeSliceNanoseconds, 1000.0f , 10000000.0f, GUILayout.Width(100)));
    }

    private void Update()
    {
        frame++;
        refs[frame % kNumLinkedLists] = MakeLinkedList();
        System.Threading.Thread.Sleep((int)sleepMs);
    }

    private void Init()
    {
        for (int i =0; i < kNumLinkedLists; i++)
        {
            refs[i] = MakeLinkedList();
        }
    }
}