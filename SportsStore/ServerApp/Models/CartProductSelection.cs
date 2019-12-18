namespace ServerApp.Models{
    public class ProductSelection{ //properties with lowercase to make it easier to work with JSON parse, which is how angular app with sent cart data to server for storage
        public long productId{get;set;}
        public string name {get;set;}
        public decimal price {get;set;}
        public int quantity {get;set;}
    }
}