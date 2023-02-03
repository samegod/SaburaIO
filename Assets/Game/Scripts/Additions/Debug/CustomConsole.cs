using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shevchenko.Debuging
{
    [StructLayout(LayoutKind.Sequential)]
    [Serializable]
    struct Log
    {
        public string Message;
        public string StackTrace;
        public LogType Type;
    }

    class CustomConsole : MonoBehaviour
    {
#if UNITY_EDITOR || DEBUG
        public bool Show = false;

        public LogType[] Filter =
        {
      //логировать только такого вида:
      LogType.Warning, LogType.Exception, LogType.Log,
    };

        private Text _textUi;
        private StringBuilder _builder = new StringBuilder();
        private List<Log> _logs = new List<Log>();
        private GameObject _canvasContainer;

        private bool Collapse
        {
            get { return _collapse; }
            set
            {
                if (_collapse != value)
                {
                    _collapse = value;
                    HandleCollapseChange();
                }
            }
        }

        private bool _collapse = false;

        void Awake()
        {
            Collapse = true;

            _canvasContainer = CreateChild(gameObject, "Canvas");
            //gameObject.transform.SetParent(GameManager.Instance.gameObject.transform);
            Canvas souceCanvas = _canvasContainer.AddComponent<Canvas>();
            souceCanvas.sortingLayerName = "UI";
            souceCanvas.sortingOrder = 10000;
            souceCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            souceCanvas.worldCamera = Camera.main;
            CanvasScaler scaler = _canvasContainer.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(743, 464);
            scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            scaler.matchWidthOrHeight = 0.5f;
            _canvasContainer.AddComponent<GraphicRaycaster>();

            GameObject panel = CreateChild(_canvasContainer, "Panel");
            RectTransform panelRect = panel.AddComponent<RectTransform>();
            panelRect.pivot = new Vector2(0, 1f);
            panelRect.anchorMin = new Vector2(0, 0);
            panelRect.anchorMax = new Vector2(1, 1);
            panelRect.offsetMin = new Vector2(30, 30);
            panelRect.offsetMax = new Vector2(-30, -30);

        #region ScrollView
        #region CreatingOfHierarchy
            GameObject scrollView = CreateChild(panel, "Scroll View");
            scrollView.AddComponent<RectTransform>();
            scrollView.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            scrollView.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject viewport = CreateChild(scrollView, "Viewport");
            viewport.AddComponent<RectTransform>();
            viewport.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            viewport.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject content = CreateChild(viewport, "Content");
            content.AddComponent<RectTransform>();
            content.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            content.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject textObj = CreateChild(content, "Text");
            textObj.AddComponent<RectTransform>();
            textObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
            textObj.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);

            GameObject scrollbarHorizontalObj = CreateChild(scrollView, "Scrollbar Horizontal");
            scrollbarHorizontalObj.AddComponent<RectTransform>(); ;
            scrollbarHorizontalObj.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            scrollbarHorizontalObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
            GameObject slidingAreaHorizontal = CreateChild(scrollbarHorizontalObj, "Sliding Area Horizontal");
            slidingAreaHorizontal.AddComponent<RectTransform>();
            slidingAreaHorizontal.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            slidingAreaHorizontal.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject handleHorizontal = CreateChild(slidingAreaHorizontal, "Handle Horizontal");
            handleHorizontal.AddComponent<RectTransform>();
            handleHorizontal.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            handleHorizontal.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);

            GameObject scrollbarVerticalObj = CreateChild(scrollView, "Scrollbar Vertical");
            scrollbarVerticalObj.AddComponent<RectTransform>();
            scrollbarVerticalObj.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
            scrollbarVerticalObj.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject slidingAreaVertical = CreateChild(scrollbarVerticalObj, "Scrollbar VertSliding Area Verticalical");
            slidingAreaVertical.AddComponent<RectTransform>();
            slidingAreaVertical.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            slidingAreaVertical.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
            GameObject handleVertical = CreateChild(slidingAreaVertical, "Handle Vertical");
            handleVertical.AddComponent<RectTransform>();
            handleVertical.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            handleVertical.GetComponent<RectTransform>().anchorMin = new Vector2(0, 0);
            handleVertical.GetComponent<RectTransform>().anchorMax = new Vector2(1, 1);
        #endregion

        #region SettingOffsets
            viewport.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            viewport.GetComponent<RectTransform>().offsetMax = new Vector2(-2, -3);
            viewport.AddComponent<Mask>().showMaskGraphic = false;
            viewport.AddComponent<CanvasRenderer>();
            viewport.AddComponent<Image>();

            content.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            content.GetComponent<RectTransform>().offsetMax = new Vector2(0, 0);
            content.GetComponent<RectTransform>().pivot = new Vector2(0, 1);

            scrollView.GetComponent<RectTransform>().offsetMin = new Vector2(30, 30);
            scrollView.GetComponent<RectTransform>().offsetMax = new Vector2(-30, -30);

            scrollbarHorizontalObj.GetComponent<RectTransform>().pivot = new Vector2(0, 0);
            scrollbarHorizontalObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            scrollbarHorizontalObj.GetComponent<RectTransform>().offsetMax = new Vector2(0, 20);

            scrollbarHorizontalObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            slidingAreaHorizontal.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            slidingAreaHorizontal.GetComponent<RectTransform>().offsetMax = new Vector2(20, -17);
            handleHorizontal.GetComponent<RectTransform>().offsetMin = new Vector2(1, 1);
            handleHorizontal.GetComponent<RectTransform>().offsetMax = new Vector2(-21, 16);


            scrollbarVerticalObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            scrollbarVerticalObj.GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
            scrollbarVerticalObj.GetComponent<RectTransform>().offsetMax = new Vector2(20, 0);

            scrollbarVerticalObj.GetComponent<RectTransform>().pivot = new Vector2(1, 1);
            slidingAreaVertical.GetComponent<RectTransform>().offsetMin = new Vector2(10, 10);
            slidingAreaVertical.GetComponent<RectTransform>().offsetMax = new Vector2(10, 10);
            handleVertical.GetComponent<RectTransform>().offsetMin = new Vector2(-9, -9);
            handleVertical.GetComponent<RectTransform>().offsetMax = new Vector2(-11, -11);
        #endregion

        #region handleHorizontal
            handleHorizontal.AddComponent<CanvasRenderer>();
            Image handleHorizontalImage = handleHorizontal.AddComponent<Image>();
            handleHorizontalImage.color = new Color(100f / 255f, 100f / 255f, 100f / 255f, 1);
            handleHorizontalImage.type = Image.Type.Sliced;
            handleHorizontalImage.raycastTarget = true;
            handleHorizontalImage.fillCenter = true;
        #endregion

        #region scrollbarHorizontalObj
            scrollbarHorizontalObj.AddComponent<CanvasRenderer>();
            Image scrollbarHorizontalImage = scrollbarHorizontalObj.AddComponent<Image>();
            scrollbarHorizontalImage.color = new Color(1, 1, 1, 100f / 255f);
            Scrollbar scrollbarHorizontal = scrollbarHorizontalObj.AddComponent<Scrollbar>();
            scrollbarHorizontal.interactable = true;
            scrollbarHorizontal.transition = Selectable.Transition.ColorTint;
            scrollbarHorizontal.targetGraphic = handleHorizontalImage;
            scrollbarHorizontal.navigation = Navigation.defaultNavigation;
            scrollbarHorizontal.handleRect = handleHorizontal.GetComponent<RectTransform>();
            scrollbarHorizontal.direction = Scrollbar.Direction.LeftToRight;
            scrollbarHorizontal.value = 0;
            scrollbarHorizontal.size = 1f;
            scrollbarHorizontal.numberOfSteps = 0;
        #endregion

        #region handleVertical
            handleVertical.AddComponent<CanvasRenderer>();
            Image handleVerticalImage = handleVertical.AddComponent<Image>();
            handleVerticalImage.color = new Color(100f / 255f, 100f / 255f, 100f / 255f, 1);
            handleVerticalImage.type = Image.Type.Sliced;
            handleVerticalImage.raycastTarget = true;
            handleVerticalImage.fillCenter = true;
        #endregion

        #region scrollbarVerticalObj
            scrollbarVerticalObj.AddComponent<CanvasRenderer>();
            Image scrollbarVerticalImage = scrollbarVerticalObj.AddComponent<Image>();
            scrollbarVerticalImage.color = new Color(1, 1, 1, 100f / 255f);
            Scrollbar scrollbarVertical = scrollbarVerticalObj.AddComponent<Scrollbar>();
            scrollbarVertical.interactable = true;
            scrollbarVertical.transition = Selectable.Transition.ColorTint;
            scrollbarVertical.targetGraphic = handleVerticalImage;
            scrollbarVertical.navigation = Navigation.defaultNavigation;
            scrollbarVertical.handleRect = handleVertical.GetComponent<RectTransform>();
            scrollbarVertical.direction = Scrollbar.Direction.BottomToTop;
            scrollbarVertical.value = 0;
            scrollbarVertical.size = 1f;
            scrollbarVertical.numberOfSteps = 0;
        #endregion

        #region scrooRect
            ScrollRect scrooRect = scrollView.AddComponent<ScrollRect>();
            scrooRect.content = content.GetComponent<RectTransform>();
            scrooRect.horizontal = true;
            scrooRect.vertical = true;
            scrooRect.movementType = ScrollRect.MovementType.Elastic;
            scrooRect.elasticity = 0.1f;
            scrooRect.inertia = true;
            scrooRect.scrollSensitivity = 1;
            scrooRect.viewport = viewport.GetComponent<RectTransform>();
            scrooRect.horizontalScrollbar = scrollbarHorizontal;
            scrooRect.horizontalScrollbarSpacing = -3;
            //scrooRect.horizontalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport;

            scrooRect.verticalScrollbar = scrollbarVertical;
            scrooRect.verticalScrollbarSpacing = -3;

            scrollView.AddComponent<CanvasRenderer>();

            Image scrollViewImage = scrollView.AddComponent<Image>();
            scrollViewImage.color = new Color(1, 1, 1, 55f / 255f);
        #endregion

        #region Content
            content.AddComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            content.GetComponent<ContentSizeFitter>().verticalFit = ContentSizeFitter.FitMode.PreferredSize;
            VerticalLayoutGroup contentVerticalLayoutGroup = content.AddComponent<VerticalLayoutGroup>();
            contentVerticalLayoutGroup.padding = new RectOffset(3, 3, 3, 0);
            contentVerticalLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            contentVerticalLayoutGroup.childForceExpandHeight = true;
            contentVerticalLayoutGroup.childForceExpandWidth = true;
        #endregion

        #region Text
            textObj.AddComponent<CanvasRenderer>();
            _textUi = textObj.AddComponent<Text>();
            _textUi.color = new Color(0, 0, 0, 1);
            _textUi.raycastTarget = false;
            _textUi.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            _textUi.fontSize = 14;
            _textUi.verticalOverflow = VerticalWrapMode.Overflow;
        #endregion
        #endregion

        #region ClearButton
            GameObject clearButtonObj = CreateChild(panel, "ClearButton");
            RectTransform rect = clearButtonObj.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.80f, 1);
            rect.anchorMax = new Vector2(0.80f, 1);
            rect.pivot = new Vector2(0, 1);
            rect.offsetMin = new Vector2(-50, -30);
            rect.offsetMax = new Vector2(15, -5);
            clearButtonObj.AddComponent<CanvasRenderer>();
            clearButtonObj.AddComponent<Image>().color = new Color(0, 0, 0, 115f / 255f);
            clearButtonObj.GetComponent<Image>().sprite = Sprite.Create(Texture2D.whiteTexture, new Rect(0, 0, 2, 2), new Vector2(0.5f, 0.5f));
            Button clearButton = clearButtonObj.AddComponent<Button>();
            ColorBlock clearButtonColors = new ColorBlock();
            clearButtonColors.normalColor = new Color(0, 0, 0, 115f / 255f);
            clearButtonColors.highlightedColor = Color.white;
            clearButtonColors.pressedColor = clearButtonColors.normalColor;
            clearButtonColors.colorMultiplier = 1;
            clearButtonColors.disabledColor = Color.white;
            clearButton.colors = clearButtonColors;
            clearButton.onClick.AddListener(ClearLog);
            Text cleatButtonText = CreateChild(clearButtonObj, "Text").AddComponent<Text>();
            cleatButtonText.font = Font.CreateDynamicFontFromOSFont("Arial", 20);
            cleatButtonText.alignment = TextAnchor.MiddleCenter;
            cleatButtonText.fontSize = 14;
            cleatButtonText.text = "ClearLog";

        #endregion

        #region CollapseToggle
            GameObject collapseTogleObj = CreateChild(panel, "CollapseTogle");
            RectTransform rectTogle = collapseTogleObj.AddComponent<RectTransform>();
            rectTogle.anchorMin = new Vector2(0.95f, 1);
            rectTogle.anchorMax = new Vector2(0.95f, 1);
            rectTogle.pivot = new Vector2(0, 1);
            rectTogle.offsetMin = new Vector2(-50, -30);
            rectTogle.offsetMax = new Vector2(105, -6.5f);



            GameObject toggleBackgroundObj = CreateChild(collapseTogleObj, "Background");
            toggleBackgroundObj.AddComponent<CanvasRenderer>();
            RectTransform rectToggleBackground = toggleBackgroundObj.AddComponent<RectTransform>();
            rectToggleBackground.pivot = new Vector2(0, 1);
            rectToggleBackground.anchorMin = new Vector2(0, 1);
            rectToggleBackground.anchorMax = new Vector2(0, 1);
            rectToggleBackground.offsetMin = new Vector2(0, -20);
            rectToggleBackground.offsetMax = new Vector2(20, 0);
            Image toggleBackgroundImage = toggleBackgroundObj.AddComponent<Image>();

            GameObject toggleCheckmarkObj = CreateChild(toggleBackgroundObj, "Checkmark");
            toggleCheckmarkObj.AddComponent<CanvasRenderer>();
            RectTransform rectCheckmark = toggleCheckmarkObj.AddComponent<RectTransform>();
            rectCheckmark.pivot = new Vector2(0.5f, 0.5f);
            rectCheckmark.anchorMin = new Vector2(0.5f, 0.5f);
            rectCheckmark.anchorMax = new Vector2(0.5f, 0.5f);
            rectCheckmark.offsetMin = new Vector2(-5f, -5f);
            rectCheckmark.offsetMax = new Vector2(5, 5);
            Image toggleCheckmarkImage = toggleCheckmarkObj.AddComponent<Image>();
            toggleCheckmarkImage.color = Color.black;

            Toggle collapseTogle = collapseTogleObj.AddComponent<Toggle>();
            collapseTogle.isOn = Collapse;
            collapseTogle.onValueChanged.AddListener(toggleValue => Collapse = toggleValue);
            collapseTogle.targetGraphic = toggleBackgroundImage;
            collapseTogle.graphic = toggleCheckmarkImage;

            Text toggleText = CreateChild(collapseTogleObj, "Lable").AddComponent<Text>();
            RectTransform rectLable = toggleText.gameObject.GetComponent<RectTransform>();
            rectLable.pivot = new Vector2(0.5f, 0.5f);
            rectLable.anchorMin = new Vector2(0, 0);
            rectLable.anchorMax = new Vector2(1, 1);
            rectLable.offsetMin = new Vector2(23, 2);
            rectLable.offsetMax = new Vector2(5, 1);
            toggleText.text = "Collapse";
            toggleText.alignment = TextAnchor.MiddleLeft;
            toggleText.font = Font.CreateDynamicFontFromOSFont("Arial", 14);
            toggleText.fontSize = 14;
        #endregion

            if (FindObjectOfType<EventSystem>() == null)
            {
                GameObject eventSystemObj = new GameObject("EventSystem");
                eventSystemObj.AddComponent<EventSystem>();
                eventSystemObj.AddComponent<StandaloneInputModule>();
                eventSystemObj.transform.SetParent(gameObject.transform);
            }

            _canvasContainer.SetActive(Show);
        }

        void OnEnable()
        {
            Application.logMessageReceived += HandleLog;
        }

        void OnDisable()
        {
            Application.logMessageReceived -= HandleLog;
        }

        private void ClearLog()
        {
            _builder = new StringBuilder();
            _textUi.text = "";
            _logs.Clear();
        }

        private void HandleCollapseChange()
        {
            _builder = new StringBuilder();
            for (int i = 0; i < _logs.Count; i++)
            {
                Log logItem = _logs[i];
                if (!_collapse || ((i <= 0) || (logItem.Message != _logs[i - 1].Message)))
                {
                    AddToBuilder(logItem);
                }
            }
        }

        private void HandleLog(string message, string stackTrace, LogType type)
        {
            if (!Filter.Contains(type))
                return;

            Log logItem = new Log
            {
                Message = message,
                StackTrace = stackTrace,
                Type = type
            };

            if (!Collapse || (_logs.Count == 0 || _logs[_logs.Count - 1].Message != logItem.Message))
                AddToBuilder(logItem);

            if (_builder.Length > 14000)//слишком много символов для UI.Text
            {
                _builder.Remove(0, 5000);//удалить первые 5к
            }

            _logs.Add(logItem);

            _textUi.text = _builder.ToString();
        }

        private void AddToBuilder(Log logItem)
        {
            switch (logItem.Type)
            {
                case LogType.Log:
                    _builder
                      .AppendLine("【LOG】: ")
                      .Append(logItem.Message)
                      .AppendLine()
                      .AppendLine(logItem.StackTrace);
                    break;

                case LogType.Warning:
                    _builder
                      .AppendLine("<color=yellow>【WARNING】: ")
                      .Append(logItem.Message)
                      .Append("</color>")
                      .AppendLine()
                      .AppendLine(logItem.StackTrace);
                    break;

                case LogType.Exception:
                    _builder
                      .AppendLine("<color=red>【EXCEPTION】: ")
                      .Append(logItem.Message)
                      .Append("</color>")
                      .AppendLine()
                      .AppendLine(logItem.StackTrace);
                    break;

                case LogType.Error:
                    _builder
                      .AppendLine("【ERROR】: ")
                      .Append(logItem.Message)
                      .AppendLine()
                      .AppendLine(logItem.StackTrace);
                    break;

                case LogType.Assert:
                    _builder
                      .AppendLine("【ASSERT】: ")
                      .Append(logItem.Message)
                      .AppendLine()
                      .AppendLine(logItem.StackTrace);
                    break;
            }
        }

        void OnGUI()
        {
            GUI.skin.button.fontSize = (int)(23 * Screen.height / 630f);

            if (GUI.Button(
              new Rect(
                Screen.width * 0.4f,
                Screen.height * 0.01f,
                Screen.width * 0.1f,
                Screen.height * 0.06f),
              "Console"))
            {
                Show = !Show;
                _canvasContainer.SetActive(Show);
            }

        }

        private static GameObject CreateChild(GameObject parent, string childName)
        {
            GameObject child = new GameObject(childName);
            child.transform.parent = parent.transform;
            child.transform.localPosition = Vector3.zero;
            return child;
        }

        void OnDestroy()
        {
            Destroy(gameObject);
        }
#endif

    }
}
