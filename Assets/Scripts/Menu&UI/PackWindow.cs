using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PackWindow : MonoBehaviour
{
    [SerializeField]
    Button buyButton;
    [SerializeField]
    int productId;

    private void OnEnable()
    {
        buyButton.onClick.AddListener(() => PurchaseManager.Instance.BuyNonConsumable(productId));
    }
}
