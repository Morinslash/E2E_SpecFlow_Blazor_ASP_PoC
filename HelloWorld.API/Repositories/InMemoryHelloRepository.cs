namespace HelloWorld.API.Repositories;

class InMemoryHelloRepository : IDatabaseRepository
{
    public string GetHello()
    {
        return "Hello World";
    }
}