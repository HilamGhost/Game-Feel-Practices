using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T: MonoBehaviour
{
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    
                    _instance = FindObjectOfType<T>();
                    
                    if (_instance == null)
                    {
                        GameObject singletonObject = new GameObject("SingletonExample");
                        _instance = singletonObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    
        
        protected virtual void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
    
            _instance = this as T;
        }
    
        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }
}
