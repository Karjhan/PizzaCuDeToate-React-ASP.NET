﻿namespace PizzaCuDeToateAPI.Models;

public class StripePayment
{
    public string CustomerId { get; set; }

    public string ReceiptEmail { get; set; }

    public string Description { get; set; }

    public string Currency { get; set; }

    public long Amount { get; set; }

    public string PaymentId { get; set; }

    public StripePayment()
    {
        
    }

    public StripePayment(string customerId, string receiptEmail, string description, string currency, long amount, string paymentId)
    {
        CustomerId = customerId;
        ReceiptEmail = receiptEmail;
        Description = description;
        Currency = currency;
        Amount = amount;
        PaymentId = paymentId;
    }
}