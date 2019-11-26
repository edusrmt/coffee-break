public class Request {
    public int id;
    public int coffeeAmount = 0;
    public int milkAmount = 0;
    public Client owner;

    public Request (int id, Client owner, int coffee, int milk) {
        this.id = id;
        this.owner = owner;
        this.coffeeAmount = coffee;
        this.milkAmount = milk;
    }
}