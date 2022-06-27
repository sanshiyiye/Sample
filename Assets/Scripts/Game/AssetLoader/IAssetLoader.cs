 using System.Collections;
 using UnityEngine.Events;

 public interface IAssetLoader
 {
     T loadAsset<T>(string path) where T : class;

     IEnumerable LoadAssetAsync<T>(string path, UnityAction<T> action) where T : class;
 }
