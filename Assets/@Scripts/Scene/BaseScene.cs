using UnityEngine;
using UnityEngine.EventSystems;
using Object = UnityEngine.Object;

namespace Scripts.Scene
{
    public abstract class BaseScene : MonoBehaviour
    {
        public abstract ESceneKind SceneKind { get; }
    
        protected virtual void Awake()
        {
            Object obj = GameObject.FindAnyObjectByType(typeof(EventSystem));
            if (obj == null)
                Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }
    
        protected virtual void Start() { }
        protected virtual void Update() { }

        public abstract void Clear();
    }
}
