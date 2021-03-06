﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Donner;
using Lnrpc;
using UnityEngine;
using UnityEngine.UI;

public class DonationEventInvoice : EventInvoice
{
    public Text uiText;
    public DonationEventInvoice(Text uiText)
    {
        this.uiText = uiText;
    } 
    public async Task<string> CreateInvoice(LndRpcBridge lnd, string sender, string[] data)
    {
        int amt = int.Parse(data[0]);
        return await lnd.AddInvoice(amt, "donate;" + sender);
    }

    public void OnInvoicePaid(Invoice invoice, string sender, string[] data)
    {
        Debug.Log("DONATION BY: " + sender + " FOR: " + invoice.AmtPaidSat + " MESSAGE: " + dataToMessage(data.Slice(1, data.Length)));
        uiText.text = sender + " for: " + invoice.AmtPaidSat;
    }

    string dataToMessage(string[] data)
    {
        var message = "";
        foreach (var str in data)
        {
            message += str + " ";
        }
        return message;
    }

}
