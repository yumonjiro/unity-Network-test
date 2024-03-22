using UnityEngine;
using System.Collections.Generic;
using Unity.Col.Gameplay.UI;
using Unity.Col.Gameplay.GameplayObjects;
using Unity.Col.Gameplay.GameplayObjects.Units;

namespace Unity.Col.Gameplay.Manager
{
	// TODO シングルトン化すべき？
	public class UIManager : MonoBehaviour
	{
		public static UIManager Instance;
		public UIState initialUIState;
		public UIState currentUIState;
		public UIState[] allUIState;
        public Unit activeUnit;

        private void Awake()
        {
            if(Instance != null)
			{
				Destroy(this);
				return;
			}
			Instance = this;
			foreach(var uiState in allUIState)
			{
				this.Register(uiState);
				Hide(uiState.Name);
			}
			Show(initialUIState.Name);
        }
        public void OnTileSelected(Tile tile)
		{
			currentUIState.OnTileSelected(tile);
		}
		public void OnUnitSelected(Unit unit)
		{
			currentUIState.OnUnitSelected(unit);
		}
		private Dictionary<string, UIState> UISet = new();

		public void Register(UIState uiState)
		{
			if(uiState != null && !UISet.ContainsKey(uiState.Name))
			{
				UISet.Add(uiState.Name, uiState);
			}
		}
		public void UnRegister(UIState uiState)
		{
			if(uiState != null)
			{
				UISet.Remove(uiState.Name);
			}
		}

		public void Show(string uiStateName)
		{
			if(UISet.TryGetValue(uiStateName, out var result) && !result.gameObject.activeSelf)
			{
				result.OnShow();
				currentUIState = result;
			}
		}

		public void ShowAndHide(string uiStatename, UIState stateToEnd)
		{
			if(uiStatename ==stateToEnd.Name)
			{
				return;
			}
			if(UISet.TryGetValue(uiStatename, out var result))
			{
				if (!result.gameObject.activeSelf) result.OnShow();
				if (stateToEnd.gameObject.activeSelf) stateToEnd.OnHide();
				currentUIState = result;
			}
		}
		public void Hide(string stateToEnd)
		{
			if(UISet.TryGetValue(stateToEnd, out var result) && result.gameObject.activeSelf)
			{
				result.OnHide();
			}
		}
	}

}