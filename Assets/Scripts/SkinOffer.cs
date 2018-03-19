using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinOffer : MonoBehaviour
{
    public void BuyPack(int packId)
    {
        PurchaseManager.Instance.BuyNonConsumable(packId);
    }
}
