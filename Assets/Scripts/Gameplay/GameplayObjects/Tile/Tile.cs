using System.Collections;
using System.Collections.Generic;
using Unity.Col.Gameplay.Manager;
using UnityEngine;

namespace Unity.Col.Gameplay.GameplayObjects
{
    public class Tile : MonoBehaviour
    {
        public TilePosition tilePosition;

        public bool _isIlluminated;
        public bool isIlluminated
        {
            get
            {
                return _isIlluminated;
            }
            set
            {
                _isIlluminated = value;
                OnChangeIllumination();
            }
        }
        private void OnChangeIllumination()
        {
            if(isIlluminated)
            {
                darkHighlight.SetActive(false);
            }
            else
            {
                darkHighlight.SetActive(true);
            }
        }    
        public GameObject hoverHighlight;
        public GameObject reachableHighlight;
        public GameObject darkHighlight;

        private void Awake()
        {
            hoverHighlight.SetActive(false);
            reachableHighlight.SetActive(false);
            isIlluminated = false;

            // tilePositionをどうやって決めるか。
            //基準となるタイルを決めて、そ子を原点とした相対座標で表すか、world positionが
            //整数値になるようにしてその値をそのまま使うか。
            tilePosition = new TilePosition(transform.position);
            // TODO Instanceとの依存関係の確認
            // Instance側でFindGameObjectsを使ってタイルを登録するか、タイル側から登録するか。パフォーマンスとの相談なのか？
            // TileManager.Instance.AddTile(this);
        }

        public void OnMouseEnter()
        {
            this.hoverHighlight.SetActive(true);
            
        }

        public void OnMouseDown()
        {
            UIManager.Instance.OnTileSelected(this);
        }
        public void OnMouseExit()
        {
            this.hoverHighlight.SetActive(false);
        }
    }
}
