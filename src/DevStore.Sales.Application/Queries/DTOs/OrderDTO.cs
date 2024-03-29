﻿namespace DevStore.Sales.Application.Queries.DTOs;

public class OrderDTO
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public decimal TotalValue { get; set; }
    public DateTime CreationDate { get; set; }
    public int OrderStatus { get; set; }
}