/*
* @classdesc PlayerInfo
* @author Copyright (c) 2017-2020, w.l.hikaru (xiaoguang.wang@rjoy.com)
* @date
* @description
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;

public class PlayerInfo : BaseSerializedObject
{

    public string name;

    public int age;

    public short atk;

    public bool sex;

    public List<string> data;

    public PlayerData playerData;

    
    public PlayerInfo()
    {
        this.data = new List<string>();
        this.playerData = new PlayerData();
    }
    public PlayerInfo(int age, short atk, bool sex,string name)
    {
        this.age = age;
        this.atk = atk;
        this.sex = sex;
        this.name = name;
    }

    public void SetData(List<string> daata)
    {
        this.data = daata;
    }

    public List<string> GetData()
    {
        return this.data;
    }
    public override int length
    {
        get
        {
            // int size = Marshal.SizeOf(this.age);
            // size += Marshal.SizeOf(this.atk);
            //  size   += sizeof(bool) + sizeof(int)+Encoding.UTF8.GetBytes(this.name).Length ;
            //  size += Marshal.SizeOf(this.data.Count);
            //  for (int i = 0; i < this.data.Count; i++)
            //  {
            //      size += sizeof(int)+Encoding.UTF8.GetBytes(this.data[i]).Length;
            //  }
            //  if (this.playerData.GetType().BaseType == typeof(BaseSerializedObject))
            //  {
            //      size +=  playerData.length;
            //  }

             int size2 = 0;
             size2 += SerializeTypeExtends.LengthOfType(this.age);
             size2 += SerializeTypeExtends.LengthOfType(this.atk);
             size2 += SerializeTypeExtends.LengthOfType(this.sex);
             size2 += SerializeTypeExtends.LengthOfType(this.name);
             size2 += SerializeTypeExtends.LengthOfListType(this.data);
             size2 += SerializeTypeExtends.LengthOfType(this.playerData);
             return size2;
        }
    }

    public override void Serialize(ByteStream stream)
    {
        stream.WriteInt(this.age);
        stream.WriteBool(this.sex);
        stream.WriteShort(this.atk);
        stream.WriteString(this.name);
        stream.WriteInt(this.data.Count);
        for (int i = 0; i < this.data.Count; i++)
        {
            stream.WriteString(this.data[i]); 
        }
        playerData.Serialize(stream);
    }

    public override void DeSerialize(ByteStream stream)
    {
        this.age = stream.ReadInt();
        this.sex = stream.ReadBool();
        this.atk = stream.ReadShort();
        this.name = stream.ReadString();
        int count = stream.ReadInt();
        List<string> list = new List<string>(count);
        for (int i = 0; i < count; i++)
        {
            list.Add(stream.ReadString());
        }
        
        this.data = list;
        this.playerData = new PlayerData();
        this.playerData.DeSerialize(stream);
    }
}

public class PlayerData : BaseSerializedObject
{
    public int dataInt;

    public string dataName;

    public string dataD;

    public short dataShort;

    public float dataf;

    public double dataDouble;

    public Vector2 vect2;
    
    public long dataLong;
    
    public override void Serialize(ByteStream stream)
    {
        
        stream.WriteString(dataName);
        stream.WriteString(dataD);
        stream.WriteShort(dataShort);
        stream.WriteInt(dataInt);
        stream.WriteFloat(this.dataf);
        stream.WriteDouble(this.dataDouble);
        stream.WriteVector2(this.vect2);
        stream.WriteLong(this.dataLong);
    }

    public override void DeSerialize(ByteStream stream)
    {
       
        dataName = stream.ReadString();
        dataD = stream.ReadString();
        dataShort = stream.ReadShort();
        dataInt = stream.ReadInt();
        dataf = stream.ReadFloat();
        dataDouble = stream.ReadDouble();
        this.vect2 = stream.ReadVector2();
        dataLong = stream.ReadLong();
    }

    public override int length
    {
        get
        {
             // int size =  Marshal.SizeOf(this.dataInt)
             //             + sizeof(int)  + Encoding.UTF8.GetBytes(this.dataD).Length 
             //             + Marshal.SizeOf(this.dataShort) 
             //             + sizeof(int)  +  Encoding.UTF8.GetBytes(this.dataName).Length
             //             + Marshal.SizeOf(this.dataf)
             //             + Marshal.SizeOf(this.dataDouble)
             //             + Marshal.SizeOf(this.dataLong);
             int size2 = SerializeTypeExtends.LengthOfType<int>(this.dataInt)
                         + SerializeTypeExtends.LengthOfType<string>(this.dataD)
                         + SerializeTypeExtends.LengthOfType<short>(this.dataShort)
                         + SerializeTypeExtends.LengthOfType<string>(this.dataName)
                         + SerializeTypeExtends.LengthOfType(this.dataf)
                         + SerializeTypeExtends.LengthOfType(this.dataDouble)
                         + SerializeTypeExtends.LengthOfType(this.dataLong);
             return size2;
        }
    }
}
