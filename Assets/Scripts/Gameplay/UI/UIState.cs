using System;
using Unity;
using UnityEngine;
using Unity.Col.Gameplay.Manager;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.UI
{
    public abstract class UIState : MonoBehaviour
    {
        public abstract string Name { get; }
        public virtual void Awake()
        {
            //UIManager.Instance.Register(this);
        }
        private void OnDestroy()
        {
            UIManager.Instance.UnRegister(this);
        }
        public virtual void OnShow()
        {
            gameObject.SetActive(true);
        }
        public virtual void OnHide()
        {
            gameObject.SetActive(false);
        }

        public virtual void HandleInput()
        { }
        
        public abstract void OnTileSelected(Tile tile);
        public abstract void OnUnitSelected(Unit unit);
    }

}