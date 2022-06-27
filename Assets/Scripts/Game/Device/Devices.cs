using System.Collections.Generic;

/**
* @classdesc Devices
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

public class Devices
{
    private Dictionary<string, DevicePerformanceLevel> deviceinfos = new Dictionary<string, DevicePerformanceLevel>();

    public Devices()
    {
        
    }

    public Devices(string[][] devicesData)
    {

        for (int i = 0; i < devicesData.Length; i++)
        {
            string [] data = devicesData[i];
            string gpu = data[0];
            string lvStr = data[1];
            deviceinfos[gpu] = (DevicePerformanceLevel) System.Enum.ToObject(typeof(DevicePerformanceLevel), int.Parse(lvStr));
        }
    }

    public DevicePerformanceLevel? DeviceLevelOfGpu(string gpu)
    {
        DevicePerformanceLevel lv ;
        deviceinfos.TryGetValue(gpu,out lv);
        
        return lv;
    }
}