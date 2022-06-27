using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Packages.FrameWork.Component;
using UnityEngine;
using UnityEngine.UI;

public class MeshCast : MonoBehaviour
{

    public Button button;

    public GameObject plane;

    public GameObject target;

    // public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {


        if (button)
        {
            UIClickLisener.Get(button, OnClick);
        }
        
    }

    void OnClick(MonoBehaviour go)
    {
        var meshFilter = target.GetComponent<MeshFilter>();
        //平面的法向量 即投射方向
        var normal = Vector3.up;
        if (meshFilter)
        {
            Mesh mesh = target.transform.GetMesh();
            Vector3[] vertices = mesh.vertices;
            HashSet<Vector3> colorPoint = new HashSet<Vector3>(new Compare());
            // List<Vector3> colorPoint = new List<Vector3>();
            for (int i = 0; i < vertices.Length; i++)
            {
                
                Vector3 world = this.target.transform.TransformPoint(vertices[i]);
                
                var Localpos = plane.transform.InverseTransformPoint(world);
                // Debug.Log("Target对应目标平面当前的Local位置 " + Localpos);
                var dis = Vector3.Dot(Localpos, normal);       
                var vecN = normal * dis;
                // Debug.Log("Local位置点乘法向量 up  " + dis + "  dos*nomal =" + vecN);
                Localpos = Localpos - vecN;
                colorPoint.Add(Localpos);
                var projectPoint = plane.transform.TransformPoint(Localpos);
                // var p = GameObject.Instantiate(prefab);
                // p.transform.position = projectPoint;
                // p.transform.parent = plane.transform;
                // Debug.Log("平面投影点做标 " + projectPoint);
            }
            List<Vector3> points = new List<Vector3>();
            points.AddRange(colorPoint);
            List<Vector3> newPoint = SortPolyPoints(points);
            Mesh newMesh = CreateMesh(newPoint.ToArray());
            GameObject newMeshObj = new GameObject();
            MeshFilter newMeshFilter =  newMeshObj.AddComponent<MeshFilter>();
            newMeshFilter.mesh = newMesh;
            newMeshObj.transform.parent = plane.transform;
            MeshRenderer mRenderer = newMeshObj.AddComponent<MeshRenderer>();
            
        }
    }

    
    public List<Vector3> SortPolyPoints(List<Vector3> vPoints)
    {
        List<Point> lPoints = Point.FromListVector3(vPoints);
        
        if (lPoints == null || lPoints.Count == 0) return null;
        //计算重心
        Point center = new Point();
        double X = 0, Y = 0;
        for (int i = 0; i < lPoints.Count; i++)
        {
            X += lPoints[i].X;
            Y += lPoints[i].Y;
        }
        center = new Point((int)X / lPoints.Count, (int)Y / lPoints.Count, lPoints[0].Z);
        //冒泡排序
        for (int i = 0; i < lPoints.Count - 1; i++)
        {
            for (int j = 0; j < lPoints.Count - i - 1; j++)
            {
                if (!PointCmp(lPoints[j], lPoints[j + 1], center))
                {
                    Point tmp = lPoints[j];
                    lPoints[j] = lPoints[j + 1];
                    lPoints[j + 1] = tmp;
                }
            }
        }
        
        return Point.FromListPoint(lPoints);
    }
 
    /// <summary>
    /// 若点a大于点b,即点a在点b顺时针方向,返回true,否则返回false
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="center"></param>
    /// <returns></returns>
    private bool PointCmp(Point a, Point b, Point center)
    {
        if (a.X >= 0 && b.Y < 0)
            return true;
        else if (a.X == 0 && b.X == 0)
            return a.Y > b.Y;
        //向量OA和向量OB的叉积
        double det = (a.X - center.X) * (b.Y - center.Y) - (b.X - center.X) * (a.Y - center.Y);
        if (det < 0)
            return true;
        if (det > 0)
            return false;
        //向量OA和向量OB共线，以距离判断大小
        double d1 = (a.X - center.X) * (a.X - center.X) + (a.Y - center.Y) * (a.Y - center.Y);
        double d2 = (b.X - center.X) * (b.X - center.X) + (b.Y - center.Y) * (b.Y - center.Y);
        return d1 > d2;
    }

    // Update is called once per frame
    Mesh  CreateMesh(Vector3 [] inVertexes)
    {
        Mesh newMesh = new Mesh();
        newMesh.vertices = inVertexes;
        int trianglesCount = inVertexes.Length - 2;
        int[] triangles = new int[trianglesCount * 3];
        // for (int i = 0; i < trianglesCount; i++)
        // {
        //     for (int j = 0; j < 3; ++j)
        //     {
        //         triangles[i * 3 + j] = j == 0 ? 0 : i + j;
        //     }
        // }

        int currentCount = 0;
        int startIndex = 0;
        int lastIndex = inVertexes.Length - 1;
        for (int i = 1; i <= trianglesCount; i++)
        {
            // if (currentCount +2 > triangles.Length)
            //     break;
            if (i % 2 == 1)
            {
                triangles[currentCount++] = startIndex++;
                triangles[currentCount++] = startIndex;
                triangles[currentCount++] = lastIndex;
            }
            else
            {
                triangles[currentCount++] = startIndex;
                triangles[currentCount++] = lastIndex - 1;
                triangles[currentCount++] = lastIndex--;
            }
        }
        newMesh.triangles = triangles;
        return newMesh;
    }
    
    
}
public class Compare : IEqualityComparer<Vector3>
{
    public bool Equals(Vector3 v1, Vector3 v2)
    {
        if (v1.x == v2.x && v1.y == v2.y && v1.z == v2.z)
        {
            return true;
        }
        else { return false; }
    }
    public int GetHashCode(Vector3 codeh)
    {
        return 0;
    }
}

public class Point
{
    public float X;
    public float Y;
    public float Z;

    public Point()
    {
        X = 0;
        Y = 0;
        Z = 0;
    }
    public Point(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
    public static List<Point> FromListVector3(List<Vector3> vector3s)
    {
        List<Point> points = new List<Point>();
        foreach (var vector3 in vector3s)
        {
            Point point = new Point(vector3.x,vector3.z,vector3.y);
            points.Add(point);
        }

        return points;
    }

    public static List<Vector3> FromListPoint(List<Point> points)
    {
        List<Vector3> results = new List<Vector3>();
        foreach (var vector3 in points)
        {
            Vector3 point = new Vector3(vector3.X,vector3.Z,vector3.Y);
            results.Add(point);
        }

        return results;
    }

}

public static class TransformEx
{
    public static Mesh GetMesh(this Transform go)
    {
        Mesh curMesh = null;
        if (go)
        {
            MeshFilter curFilter = go.GetComponent<MeshFilter>();
            SkinnedMeshRenderer curSkinn = go.GetComponent<SkinnedMeshRenderer>();
            if (curFilter && !curSkinn)
            {
                curMesh = curFilter.sharedMesh;
            }

            if (!curFilter && curSkinn)
            {
                curMesh = curSkinn.sharedMesh;
            }
        }

        return curMesh;
    }
}