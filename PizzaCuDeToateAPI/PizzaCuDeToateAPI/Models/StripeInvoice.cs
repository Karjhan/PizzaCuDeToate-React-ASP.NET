﻿namespace PizzaCuDeToateAPI.Models;

public class StripeInvoice
{
    public string InvoiceId { get; set; }

    public string CustomerId { get; set; }

    public string HostedStringURL { get; set; }

    public string InvoicePDF { get; set; }
    
    public string CustomerEmail { get; set; }

    public string CustomerName { get; set; }

    public StripeInvoice()
    {
        
    }

    public StripeInvoice(string invoiceId, string customerId, string hostedStringUrl, string invoicePdf, string customerEmail, string customerName)
    {
        InvoiceId = invoiceId;
        CustomerId = customerId;
        HostedStringURL = hostedStringUrl;
        InvoicePDF = invoicePdf;
        CustomerEmail = customerEmail;
        CustomerName = customerName;
    }
}