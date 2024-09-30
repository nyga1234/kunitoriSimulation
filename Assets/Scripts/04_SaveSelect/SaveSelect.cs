//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using UniRx;
//using Cysharp.Threading.Tasks;

//public class StageSelect : MonoBehaviour
//{
//    private SaveSelectModel _model;

//    [Header("View")]
//    [SerializeField]
//    private Button _closeButtonView;

//    [Header("Save Select Button")]
//    [SerializeField]
//    private Button _prefab;
//    [SerializeField]
//    private Transform _parent;
//    //[SerializeField]
//    //private UtilityParamObject _param;
//    [SerializeField]
//    private SaveDataObject _saveData;

//    // Start is called before the first frame update
//    void Start()
//    {
//        _model = new StageSelectModel();

//        #region View to Model
//        _closeButtonView.Button.onClick.AsObservable().Subscribe(_ => { UniTask uniTask = _model.OnPressClose(); });
//        #endregion

//        #region Select Buttons
//        for (int i = 1; i <= _param.StageCount; i++)
//        {
//            if (i > _saveData.Progress)
//            {
//                break;
//            }

//            var obj = Instantiate(_prefab, _parent);
//            int index = i;
//            obj.Text.text = $"{i}";
//            obj.Button.onClick.AsObservable().Subscribe(_ =>
//            {
//                _param.SelectStage = index;
//                GameManager.instance.ChangeScene("Title", "GameMain");
//            });

//            if (i == 1)
//            {
//                Navigation nav = _closeButtonView.Button.navigation;
//                nav.selectOnUp = obj.Button;
//                _closeButtonView.Button.navigation = nav;
//                obj.Button.Select();

//            }
//        }
//        #endregion

//        SceneController.instance.Stack.Add(nameof(StageSelect));
//    }

//    private void OnDestroy()
//    {
//        SceneController.instance.Stack.Remove(nameof(StageSelect));
//    }

//    //private void Update()
//    //{
//    //    if (IsPressNavigateAndUnfocus && SceneController.Instance.Active == nameof(StageSelect))
//    //    {
//    //        _closeButtonView.Button.Select();
//    //    }
//    //}
//}