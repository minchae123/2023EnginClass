using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UILabelWithData : Label
{
    public string KeyData { get; set; }
    public int IndexData { get; set; }
    
    public new class UxmlFactory : UxmlFactory<UILabelWithData, UxmlTraits>
    {

    }

    public new class UxmlTraits : Label.UxmlTraits
    {
        private UxmlStringAttributeDescription keyName
            = new UxmlStringAttributeDescription { name = "key-data", defaultValue = "" };

        private UxmlIntAttributeDescription index
            = new UxmlIntAttributeDescription { name = "index-data", defaultValue = 0 };
        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var label = ve as UILabelWithData;

            label.KeyData = keyName.GetValueFromBag(bag, cc);
            label.IndexData = index.GetValueFromBag(bag, cc);
        }
    }
}