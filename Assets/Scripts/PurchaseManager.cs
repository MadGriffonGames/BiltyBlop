using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Purchasing;

public class PurchaseManager : MonoBehaviour, IStoreListener
{
    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;
    private int currentProductIndex;

    [Tooltip("Не многоразовые товары. Больше подходит для отключения рекламы и т.п.")]
    public string[] NonConsumableProducts;
    [Tooltip("Многоразовые товары. Больше подходит для покупки игровой валюты и т.п.")]
    public string[] ConsumableProducts;

    /// <summary>
    /// Событие, которое запускается при удачной покупке многоразового товара.
    /// </summary>
    public static event OnSuccessConsumable OnPurchaseConsumable;
    /// <summary>
    /// Событие, которое запускается при удачной покупке не многоразового товара.
    /// </summary>
    public static event OnSuccessNonConsumable OnPurchaseNonConsumable;
    /// <summary>
    /// Событие, которое запускается при неудачной покупке какого-либо товара.
    /// </summary>
    public static event OnFailedPurchase PurchaseFailed;

    private void Awake()
    {
        InitializePurchasing();
    }

    private void Start()
    {
        OnPurchaseNonConsumable += PurchaseManager_OnPurchaseNonConsumable;
        OnPurchaseConsumable += PurchaseManager_OnPurchaseConsumable;
    }

    private void PurchaseManager_OnPurchaseNonConsumable(PurchaseEventArgs args)
    {
        Debug.Log("Purchased: " + args.purchasedProduct.definition.id + " - NonConsumable");
        PlayerPrefs.SetInt("NoAds", 1);
    }

    private void PurchaseManager_OnPurchaseConsumable(PurchaseEventArgs args)
    {
        Debug.Log("Purchased: " + args.purchasedProduct.definition.id + " - Consumable");

        switch (args.purchasedProduct.definition.id)
        {
			case "15_crystals":
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 15);
                break;
			case "kidarian.15_crystals":
				PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 15);
				break;

            case "100__crystals":
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 100);
                break;

            case "200_crystals":
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 200);
                break;

            case "500_crystals":
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 500);
                break;

            case "1500_crystals":
                PlayerPrefs.SetInt("Crystals", PlayerPrefs.GetInt("Crystals") + 1500);
                break;

            default:
                break;
        }
    }

    

    /// <summary>
    /// Проверить, куплен ли товар.
    /// </summary>
    /// <param name="id">Индекс товара в списке.</param>
    /// <returns></returns>
    /// 
    public static bool CheckBuyState(string id)
    {
        Product product = m_StoreController.products.WithID(id);
        if (product.hasReceipt) { return true; }
        else { return false; }
    }

    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
		foreach (string s in ConsumableProducts) 
		{
			builder.AddProduct (s, ProductType.Consumable);
			builder.AddProduct ("kidarian." + s, ProductType.Consumable);
		}
		foreach (string s in NonConsumableProducts) 
		{
			builder.AddProduct (s, ProductType.NonConsumable);
			builder.AddProduct ("kidarian." + s, ProductType.NonConsumable);
		}
        UnityPurchasing.Initialize(this, builder);
    }

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    public void BuyConsumable(int index)
    {
        currentProductIndex = index;
        BuyProductID(ConsumableProducts[index]);
    }

    public void BuyNonConsumable(int index)
    {
        currentProductIndex = index;
        BuyProductID(NonConsumableProducts[index]);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
                OnPurchaseFailed(product, PurchaseFailureReason.ProductUnavailable);
            }
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (ConsumableProducts.Length > 0 && String.Equals(args.purchasedProduct.definition.id, ConsumableProducts[currentProductIndex], StringComparison.Ordinal))
            OnSuccessC(args);
        else if (NonConsumableProducts.Length > 0 && String.Equals(args.purchasedProduct.definition.id, NonConsumableProducts[currentProductIndex], StringComparison.Ordinal))
            OnSuccessNC(args);
        else Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
        return PurchaseProcessingResult.Complete;
    }

    public delegate void OnSuccessConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessC(PurchaseEventArgs args)
    {
        if (OnPurchaseConsumable != null) OnPurchaseConsumable(args);
        Debug.Log(ConsumableProducts[currentProductIndex] + " Buyed!");
    }
    public delegate void OnSuccessNonConsumable(PurchaseEventArgs args);
    protected virtual void OnSuccessNC(PurchaseEventArgs args)
    {
        if (OnPurchaseNonConsumable != null) OnPurchaseNonConsumable(args);
        Debug.Log(NonConsumableProducts[currentProductIndex] + " Buyed!");
    }
    public delegate void OnFailedPurchase(Product product, PurchaseFailureReason failureReason);
    protected virtual void OnFailedP(Product product, PurchaseFailureReason failureReason)
    {
        if (PurchaseFailed != null) PurchaseFailed(product, failureReason);
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        OnFailedP(product, failureReason);
    }
}
