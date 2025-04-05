using System;

class Program
{
    static void Main()
    {
        // Create addresses
        var usaAddress = new Address("123 Main St", "New York", "NY", "USA");
        var canadaAddress = new Address("456 Maple Ave", "Toronto", "ON", "Canada");

        // Create customers
        var usaCustomer = new Customer("John Smith", usaAddress);
        var canadaCustomer = new Customer("Marie Dubois", canadaAddress);

        // Create products
        var product1 = new Product("Laptop", "P100", 999.99, 1);
        var product2 = new Product("Mouse", "P101", 19.99, 2);
        var product3 = new Product("Keyboard", "P102", 49.99, 1);
        var product4 = new Product("Monitor", "P103", 199.99, 2);

        // Create first order (USA)
        var order1 = new Order(usaCustomer);
        order1.AddProduct(product1);
        order1.AddProduct(product2);

        // Create second order (Canada)
        var order2 = new Order(canadaCustomer);
        order2.AddProduct(product3);
        order2.AddProduct(product4);
        order2.AddProduct(product2);

        // Display order information
        DisplayOrderDetails(order1);
        DisplayOrderDetails(order2);
    }

    static void DisplayOrderDetails(Order order)
    {
        Console.WriteLine("====================================");
        Console.WriteLine(order.GetPackingLabel());
        Console.WriteLine(order.GetShippingLabel());
        Console.WriteLine($"\nTotal Price: ${order.CalculateTotalCost():0.00}");
        Console.WriteLine("====================================\n");
    }
}