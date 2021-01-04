﻿using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface ITIP
{
    string GetTooltipInfoText();
}

namespace DapperDino.TooltipUI
{
    public class TooltipPopup : MonoBehaviour
    {
        [SerializeField] private GameObject popupCanvasObject;
        [SerializeField] private RectTransform popupObject;
        [SerializeField] private TextMeshProUGUI infoText;
        [SerializeField] private Vector3 offset;
        [SerializeField] private float padding;

        private Canvas popupCanvas;

        private void Start()
        {
            popupCanvas = popupCanvasObject.GetComponent<Canvas>();
            UIManager._instance.OnEscape += HideInfo;
        }

        private void Update()
        {
            FollowCursor();
        }

        private void FollowCursor()
        {
            if (!popupCanvasObject.activeSelf) { return; }

            Vector3 newPos = Input.mousePosition + offset;
            newPos.z = 0f;
            float rightEdgeToScreenEdgeDistance = Screen.width - (newPos.x + popupObject.rect.width * popupCanvas.scaleFactor / 2) - padding;
            if (rightEdgeToScreenEdgeDistance < 0)
            {
                newPos.x += rightEdgeToScreenEdgeDistance;
            }
            float leftEdgeToScreenEdgeDistance = 0 - (newPos.x - popupObject.rect.width * popupCanvas.scaleFactor / 2) + padding;
            if (leftEdgeToScreenEdgeDistance > 0)
            {
                newPos.x += leftEdgeToScreenEdgeDistance;
            }
            float topEdgeToScreenEdgeDistance = Screen.height - (newPos.y + popupObject.rect.height * popupCanvas.scaleFactor) - padding;
            if (topEdgeToScreenEdgeDistance < 0)
            {
                newPos.y += topEdgeToScreenEdgeDistance;
            }
            popupObject.transform.position = newPos;
        }

        public void DisplayInfo(ITIP tipObj)
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(tipObj.GetTooltipInfoText());

            infoText.text = builder.ToString();

            popupCanvasObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate(popupObject);
        }

        public void HideInfo()
        {
            if(popupCanvasObject.activeInHierarchy)
                popupCanvasObject.SetActive(false);
        }

        private void OnDisable()
        {
            UIManager._instance.OnEscape -= HideInfo;
        }
    }
}